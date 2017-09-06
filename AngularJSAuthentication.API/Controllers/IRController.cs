using AngularJSAuthentication.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using NLog;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AngularJSAuthentication.API.Controllers
{
    [RoutePrefix("api/IR")]
    public class IRController : ApiController
    {
        AuthContext context = new AuthContext();
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private static TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);

        [Authorize]
        [Route("")]
        public HttpResponseMessage Get(int PurchaseOrderId)
        {
            List<InvoiceImage> ass = new List<InvoiceImage>();
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                int compid = 0, userid = 0;

                foreach (Claim claim in identity.Claims)
                {
                    if (claim.Type == "compid")
                    {
                        compid = int.Parse(claim.Value);
                    }
                    if (claim.Type == "userid")
                    {
                        userid = int.Parse(claim.Value);
                    }
                }
                ass = context.InvoiceImageDb.Where(c=>c.PurchaseOrderId == PurchaseOrderId).ToList();
                logger.Info("End  Return: ");
                return Request.CreateResponse(HttpStatusCode.OK,ass);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest,ex.Message);
            }
        }

        [Authorize]
        [Route("getIR")]
        public HttpResponseMessage getIR(int PurchaseOrderId)
        {
            InvoiceReceive ass = new InvoiceReceive();
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                int compid = 0, userid = 0;

                foreach (Claim claim in identity.Claims)
                {
                    if (claim.Type == "compid")
                    {
                        compid = int.Parse(claim.Value);
                    }
                    if (claim.Type == "userid")
                    {
                        userid = int.Parse(claim.Value);
                    }
                }
                ass = context.InvoiceReceiveDb.Where(c => c.PurchaseOrderId == PurchaseOrderId).SingleOrDefault();
                if (ass != null)
                {
                    ass.purDetails = context.IR_ConfirmDb.Where(c => c.PurchaseOrderDetailId == ass.Id).ToList();
                    if (ass.purDetails != null ) {
                        foreach (var a in ass.purDetails)
                        {
                            var item = context.itemMasters.Where(c => c.ItemId == a.ItemId).SingleOrDefault();
                            if (item != null)
                            {
                                a.TotalTaxPercentage = item.TotalTaxPercentage;
                            }
                        }
                    }
                }                
                logger.Info("End  Return: ");
                return Request.CreateResponse(HttpStatusCode.OK, ass);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [Authorize]
        [Route("getIRSupplier")]
        public HttpResponseMessage getIRSupplier(int id)
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                int compid = 0, userid = 0;

                foreach (Claim claim in identity.Claims)
                {
                    if (claim.Type == "compid")
                    {
                        compid = int.Parse(claim.Value);
                    }
                    if (claim.Type == "userid")
                    {
                        userid = int.Parse(claim.Value);
                    }
                }
                var ir = context.InvoiceReceiveDb.Where(c => c.supplierId == id).ToList();
                List<IRGR> supplierIr = new List<IRGR>();
                foreach (var spIr in ir) {
                    try {
                        var gr = context.DPurchaseOrderMaster.Where(x => x.PurchaseOrderId == spIr.PurchaseOrderId && x.SupplierId == spIr.supplierId).SingleOrDefault();
                        IRGR ass = new IRGR();
                        if (gr != null) {
                            ass.PurchaseOrderId = gr.PurchaseOrderId;
                            ass.GRAmount1 = gr.Gr1_Amount;
                            ass.GRAmount2 = gr.Gr2_Amount;
                            ass.GRAmount3 = gr.Gr3_Amount;
                            ass.GRAmount4 = gr.Gr4_Amount;
                            ass.GRAmount5 = gr.Gr5_Amount;
                            ass.GRTotal = gr.TotalAmount;
                            ass.IRAmount1 = spIr.IRAmount1;
                            ass.IRAmount2 = spIr.IRAmount2;
                            ass.IRAmount3 = spIr.IRAmount3;
                            ass.IRTotal = spIr.TotalAmount;

                            supplierIr.Add(ass);
                        }
                    }
                    catch (Exception ex) {
                        logger.Error(ex.Message);
                    }
                }               
                return Request.CreateResponse(HttpStatusCode.OK, supplierIr);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [Route("add")]
        [AcceptVerbs("POST")]
        public HttpResponseMessage add(InvoiceImage IR)
        {
            logger.Info("start Return: ");
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                int compid = 0, userid = 0;

                foreach (Claim claim in identity.Claims)
                {
                    if (claim.Type == "compid")
                    {
                        compid = int.Parse(claim.Value);
                    }
                    if (claim.Type == "userid")
                    {
                        userid = int.Parse(claim.Value);
                    }
                }
                if (IR != null)
                {
                    if (IR.Id != 0)
                    {
                        InvoiceImage invoice = context.InvoiceImageDb.Where(x => x.Id == IR.Id).SingleOrDefault();
                        if (invoice != null)
                        {
                            invoice.IRAmount = IR.IRAmount;
                            invoice.IRLogoURL = IR.IRLogoURL;
                            context.InvoiceImageDb.Attach(invoice);
                            context.Entry(invoice).State = EntityState.Modified;
                            context.SaveChanges();
                        }
                        else
                        {
                            IR.CreationDate = indianTime;
                            context.InvoiceImageDb.Add(IR);
                            context.SaveChanges();
                        }
                    }
                    else
                    {
                        IR.CreationDate = indianTime;
                        context.InvoiceImageDb.Add(IR);
                        context.SaveChanges();
                    }

                }
                return Request.CreateResponse(HttpStatusCode.OK, IR);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [Route("")]
        [AcceptVerbs("POST")]
        public HttpResponseMessage confiirmIR(InvoiceReceive pom)
        {
            try
            {
                var count = 0;
                List<IR_Confirm> p = pom.purDetails;
                var ir = context.InvoiceReceiveDb.Where(x => x.PurchaseOrderId == pom.PurchaseOrderId).SingleOrDefault();
                if (ir != null){
                    count = ir.irCount.GetValueOrDefault();
                }
                else
                {
                    pom.irCount = 0;
                    context.InvoiceReceiveDb.Add(pom);
                    context.SaveChanges();
                    ir = pom;
                }
                if (pom.discount > 0) {
                }
                else {
                    pom.discount = 0;
                }
                //double dics = 0;
                foreach (IR_Confirm pc in p)
                {
                    try
                    {                               
                        var irconfirm = context.IR_ConfirmDb.Where(x=>x.PurchaseOrderId == pc.PurchaseOrderId && x.ItemId == pc.ItemId).SingleOrDefault();
                        if (irconfirm != null) {
                            if(count == 1) {                                
                                if (pc.Qty > 0)
                                {
                                    irconfirm.IRQuantity += pc.Qty;
                                    irconfirm.Status = pc.Status;
                                    irconfirm.QtyRecived2 = pc.Qty;
                                    irconfirm.PriceRecived += pc.PriceRecived;
                                    irconfirm.Price2 = pc.Price;
                                    irconfirm.dis2 = pc.discount;
                                }
                                else
                                {
                                    irconfirm.Price2 = 0;
                                    irconfirm.dis2 = 0 ;
                                }
                                //dics += irconfirm.dis2.GetValueOrDefault();
                                ir.discount2 = pom.discount; //+ dics;
                                ir.irCount = 2;
                                ir.IRAmount2 = pom.TotalAmount;
                            }
                            else if (count == 2) {
                                if (pc.Qty > 0)
                                {
                                    irconfirm.PriceRecived += pc.PriceRecived;
                                    irconfirm.IRQuantity += pc.Qty;
                                    irconfirm.Status = pc.Status;
                                    irconfirm.QtyRecived3 = pc.Qty;
                                    irconfirm.Price3 = pc.Price;
                                    irconfirm.dis3 = pc.discount;
                                }
                                else
                                {
                                    irconfirm.Price3 =0;
                                    irconfirm.dis3 = 0;
                                }
                                //dics += irconfirm.dis3.GetValueOrDefault();
                                ir.discount3 = pom.discount; //+ dics;
                                ir.irCount = 3;
                                ir.IRAmount3 = pom.TotalAmount;
                            }                                              

                            context.IR_ConfirmDb.Attach(irconfirm);
                            context.Entry(irconfirm).State = EntityState.Modified;
                            context.SaveChanges();                            
                        }
                        else
                        {
                            pc.PurchaseOrderDetailId = ir.Id;
                            if (pc.Qty > 0)
                                pc.IRQuantity = pc.Qty;
                            else
                                pc.IRQuantity = 0;
                            pc.QtyRecived1 = pc.IRQuantity;
                            if (pc.QtyRecived1 > 0)
                            {
                                pc.Price1 = pc.Price;
                                if (pc.discount > 0)
                                    pc.dis1 = pc.discount;
                                else
                                    pc.dis1 = 0;
                            }
                            else
                            {
                                pc.QtyRecived1 = 0;
                                pc.Price1 = 0;
                                pc.dis1 = 0;
                            }
                            pc.QtyRecived2 = 0;
                            pc.Price2 = 0;
                            pc.dis2 = 0;
                            pc.QtyRecived3 = 0;
                            pc.Price3 = 0;
                            pc.dis3 = 0;

                            pc.CreationDate = indianTime;
                            context.IR_ConfirmDb.Add(pc);
                            context.SaveChanges();

                            pom.irCount = 1;
                            ir.IRAmount1 = pom.TotalAmount;
                            ir.IRAmount2 = 0;
                            ir.IRAmount3 = 0;
                            //dics += pc.dis1.GetValueOrDefault();
                            ir.discount1 = pom.discount; //+ dics;                           
                        }                                               
                    }
                    catch (Exception ee)
                    {
                        logger.Error(ee.Message);
                        return Request.CreateResponse(HttpStatusCode.BadRequest, "got Excepion"); ;
                    }
                }
                if(ir.irCount != 1 && ir.irCount != 0)
                    ir.TotalAmount += pom.TotalAmount;

                context.InvoiceReceiveDb.Attach(ir);
                context.Entry(ir).State = EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception exe)
            {
                Console.Write(exe.Message);
                return Request.CreateResponse(HttpStatusCode.BadRequest, "got Excepion");
            }
            return Request.CreateResponse(HttpStatusCode.OK, pom);
        }
    }
    public class InvoiceImage
    {
        [Key]
        public int Id { get; set; }
        public string InvoiceNumber { get; set; }
        public int PurchaseOrderId { get; set; }
        public int WarehouseId { get; set; }
        public double IRAmount { get; set; }
        public string IRLogoURL { get; set; }
        public DateTime CreationDate { get; set; }
    }
    public class InvoiceReceive
    {
        [Key]
        public int Id { get; set; }
        public int PurchaseOrderId { get; set; }
        public int supplierId { get; set; }
        public string SupplierName { get; set; }
        public int WarehouseId { get; set; }
        public double TotalAmount { get; set; }
        public string Status { get; set; }
        public DateTime CreationDate { get; set; }
        public bool Deleted { get; set; }
        public double? discount1 { get; set; }
        public double? discount2 { get; set; }
        public double? discount3 { get; set; }
        public int? irCount { get; set; }
        public double? IRAmount1 { get; set; }
        public double? IRAmount2 { get; set; }
        public double? IRAmount3 { get; set; }

        [NotMapped]
        public double? discount { get; set; }
        [NotMapped]
        public List<IR_Confirm> purDetails { get; set; }
    }
    public class IR_Confirm
    {
        [Key]
        public int IRreceiveid { get; set; }
        public int PurchaseOrderDetailId { get; set; }
        public int PurchaseOrderId { get; set; }
        public int SupplierId { get; set; }
        public int? Warehouseid { get; set; }
        public string WarehouseName { get; set; }
        public string SupplierName { get; set; }
        public int ItemId { get; set; }
        public string PurchaseSku { get; set; }
        public string ItemName { get; set; }
        public double PriceRecived { get; set; }
        public int? TotalQuantity { get; set; }
        public int? IRQuantity { get; set; }
        public string Status { get; set; }
        public DateTime CreationDate { get; set; }
        public double? QtyRecived1 { get; set; }
        public double? QtyRecived2 { get; set; }
        public double? QtyRecived3 { get; set; }
        public double? Price1 { get; set; }
        public double? Price2 { get; set; }
        public double? Price3 { get; set; }
        public double? dis1 { get; set; }
        public double? dis2 { get; set; }
        public double? dis3 { get; set; }
        public int? QtyRecived { get; set; }
        public double TotalTaxPercentage { get; set; }

        [NotMapped]
        public int? Qty { get; set; }
        [NotMapped]
        public double? Price { get; set; }
        [NotMapped]
        public double? discount { get; set; }
    }
    public class IRGR
    {
        public int PurchaseOrderId { get; set; }
        public double? IRAmount1 { get; set; }
        public double? IRAmount2 { get; set; }
        public double? IRAmount3 { get; set; }
        public double? GRAmount1 { get; set; }
        public double? GRAmount2 { get; set; }
        public double? GRAmount3 { get; set; }
        public double? GRAmount4 { get; set; }
        public double? GRAmount5 { get; set; }
        public double? IRTotal { get; set; }
        public double? GRTotal { get; set; }
    }
}



