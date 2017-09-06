

using AngularJSAuthentication.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using System.Web.Http.Description;
using NLog;
using System.Data.Entity;

namespace AngularJSAuthentication.API.Controllers
{
    [RoutePrefix("api/PurchaseOrderDetailRecived")]
    public class PurchaseOrderDetailRecivedController : ApiController
    {
        iAuthContext context = new AuthContext();
        public static Logger logger = LogManager.GetCurrentClassLogger();
        private static TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
        private AuthContext db = new AuthContext();
        
        [Route("")]
        public IEnumerable<PurchaseOrderDetail> Get(string recordtype)
        {
            if (recordtype == "details")
            {
                logger.Info("start PurchaseOrderDetail: ");
                List<PurchaseOrderDetail> ass = new List<PurchaseOrderDetail>();
                try
                {
                    var identity = User.Identity as ClaimsIdentity;
                    int compid = 1, userid = 0;
                    // Access claims
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

                    logger.Info("User ID : {0} , Company Id : {1}", compid, userid);
                    ass = context.AllPOdetails(compid).ToList();
                    logger.Info("End  order: ");
                    return ass;
                }
                catch (Exception ex)
                {
                    logger.Error("Error in PurchaseOrderDetail " + ex.Message);
                    logger.Info("End  PurchaseOrderDetail: ");
                    return null;
                }
            }
            return null;
        }
        
        [ResponseType(typeof(PurchaseOrderMaster))]
        [Route("")]
        [AcceptVerbs("POST")]
        public HttpResponseMessage add(PurchaseOrderMaster pom)
        {
            var po = pom.purDetails;
            var count = 0;
            foreach (var a in po)
            {
                if (count == 0){
                    if (a.QtyRecived5 != 0) 
                        count = 5;        
                    else {
                        if (a.QtyRecived4 != 0) 
                            count = 4;                        
                        else {
                            if (a.QtyRecived3 != 0) 
                                count = 3;
                            else {
                                if (a.QtyRecived2 != 0) 
                                    count = 2;  
                                else {
                                    if (a.QtyRecived1 != 0) 
                                        count = 1;
                                }
                            }
                        }
                    }
                }
                else if (count == 1) {
                    if (a.QtyRecived5 != 0) 
                        count = 5;
                    else {
                        if (a.QtyRecived4 != 0) 
                            count = 4;
                        else {
                            if (a.QtyRecived3 != 0) 
                                count = 3;
                            else {
                                if (a.QtyRecived2 != 0) 
                                    count = 2;
                            }
                        }
                    }
                }
                else if (count == 2) {
                    if (a.QtyRecived5 != 0) 
                        count = 5;
                    else {
                        if (a.QtyRecived4 != 0) 
                            count = 4;
                        else {
                            if (a.QtyRecived3 != 0) 
                                count = 3;
                        }
                    }
                }
                else if (count == 3) {
                    if (a.QtyRecived5 != 0) 
                        count = 5;
                    else {
                        if (a.QtyRecived4 != 0) 
                            count = 4;
                    }
                }
                else if (count == 4) {
                    if (a.QtyRecived5 != 0)
                        count = 5;
                }
            }
            int pomID;
            PurchaseOrderMasterRecived m = new PurchaseOrderMasterRecived();
            try
            {
                var flag = 0;
                foreach (PurchaseOrderDetailRecived pc in po)
                {
                    try {
                        if (pc.isDeleted != true) {
                            var st = context.AddPurchaseOrderDetailsRecived(pc,count);                                      
                        }
                    }
                    catch (Exception ee) {
                        logger.Error(ee.Message);
                        return null;
                    }
                    if ((pc.QtyRecived1 + pc.QtyRecived2+ pc.QtyRecived3+ pc.QtyRecived4+ pc.QtyRecived5) < pc.TotalQuantity) {
                        flag = 1;
                    }                 
                }
                //for status on Purchase ordermaster...Received
                pomID = po[0].PurchaseOrderId;
                PurchaseOrderMaster pm = db.DPurchaseOrderMaster.Where(x => x.PurchaseOrderId == pomID).FirstOrDefault();
                List<PurchaseOrderDetailRecived> podrList = db.PurchaseOrderRecivedDetails.Where(x => x.PurchaseOrderId == pm.PurchaseOrderId).ToList();
                var amount = 0.00;
                foreach (var pord in podrList)
                {
                    if (count == 1) {
                        if(pord.dis1 != 0 && pord.dis1 != null)
                            amount += ((pord.QtyRecived1 * pord.Price1 * 100) / (100 + pord.dis1)).GetValueOrDefault();
                        else
                            amount += (pord.QtyRecived1 * pord.Price1).GetValueOrDefault();
                    }
                    else if (count == 2) {
                        if (pord.dis2 != 0 && pord.dis2 != null)
                            amount += ((pord.QtyRecived2 * pord.Price2 * 100) / (100 + pord.dis2)).GetValueOrDefault();
                        else
                            amount += (pord.QtyRecived2 * pord.Price2).GetValueOrDefault();
                    }
                    else if (count == 3) {
                        if (pord.dis3 != 0 && pord.dis3 != null)
                            amount += ((pord.QtyRecived3 * pord.Price3 * 100) / (100 + pord.dis3)).GetValueOrDefault();
                        else
                            amount += (pord.QtyRecived3 * pord.Price3).GetValueOrDefault();
                    }
                    else if (count == 4) {
                        if (pord.dis4 != 0 && pord.dis4 != null)
                            amount += ((pord.QtyRecived4 * pord.Price4 * 100) / (100 + pord.dis4)).GetValueOrDefault();
                        else
                            amount += (pord.QtyRecived4 * pord.Price4).GetValueOrDefault();
                    }
                    else if (count == 5) {
                        if (pord.dis5 != 0 && pord.dis5 != null)
                            amount += ((pord.QtyRecived5 * pord.Price5 * 100) / (100 + pord.dis5)).GetValueOrDefault();
                        else
                            amount += (pord.QtyRecived5 * pord.Price5).GetValueOrDefault();
                    }
                }
                if (count == 1){
                    if (pom.discount1 != null){
                        pm.discount1 = pom.discount1;                        
                        pm.Gr1_Amount = ((amount * 100) / (100 + pom.discount1)).GetValueOrDefault();
                        pm.TotalAmount = ((amount * 100) / (100 + pom.discount1)).GetValueOrDefault();
                    }
                    else{
                        pm.TotalAmount += amount;
                        pm.Gr1_Amount = amount;
                    }
                    pm.Gr1_Date = indianTime;
                }
                else if (count == 2){
                    if (pom.discount2 != null) {
                        pm.discount2 = pom.discount2;
                        pm.Gr2_Amount = ((amount * 100) / (100 + pom.discount2)).GetValueOrDefault();
                        pm.TotalAmount += ((amount * 100) / (100 + pom.discount2)).GetValueOrDefault();
                    }
                    else{
                        pm.TotalAmount += amount;
                        pm.Gr2_Amount = amount;
                    }
                    pm.Gr2_Date = indianTime;
                }
                else if (count == 3){
                    if (pom.discount3 != null){
                        pm.discount3 = pom.discount3;
                        pm.Gr3_Amount = ((amount * 100) / (100 + pom.discount3)).GetValueOrDefault();
                        pm.TotalAmount += ((amount * 100) / (100 + pom.discount3)).GetValueOrDefault();
                    }
                    else{
                        pm.TotalAmount += amount;
                        pm.Gr3_Amount = amount;
                    }
                    pm.Gr3_Date = indianTime;
                }
                else if (count == 4){
                    if (pom.discount4 != null){
                        pm.discount4 = pom.discount4;
                        pm.Gr4_Amount = ((amount * 100) / (100 + pom.discount4)).GetValueOrDefault();
                        pm.TotalAmount += ((amount * 100) / (100 + pom.discount4)).GetValueOrDefault();
                    }
                    else{
                        pm.TotalAmount += amount;
                        pm.Gr4_Amount = amount;
                    }
                    pm.Gr4_Date = indianTime;
                }
                else if (count == 5){
                    if (pom.discount5 != null){
                        pm.discount5 = pom.discount5;
                        pm.Gr5_Amount = ((amount * 100) / (100 + pom.discount5)).GetValueOrDefault();
                        pm.TotalAmount += ((amount * 100) / (100 + pom.discount5)).GetValueOrDefault();
                    }
                    else{
                        pm.TotalAmount += amount;
                        pm.Gr5_Amount = amount;
                    }
                    pm.Gr5_Date = indianTime;
                }
                if (flag == 1 && count != 5)
                {                      
                    pm.Status = "Partial Received";
                    db.DPurchaseOrderMaster.Attach(pm);
                    db.Entry(pm).State = EntityState.Modified;
                    db.SaveChanges();

                    List<PurchaseOrderDetail> pod = db.DPurchaseOrderDeatil.Where(q => q.PurchaseOrderId == pm.PurchaseOrderId).ToList();
                    if(pod.Count != 0)
                    {
                        foreach(var a in pod)
                        {
                            a.Status = "open";
                            db.DPurchaseOrderDeatil.Attach(a);
                            db.Entry(a).State = EntityState.Modified;
                            db.SaveChanges();
                        }
                    }
                }
                else
                {                 
                    pm.Status = "Received";
                    db.DPurchaseOrderMaster.Attach(pm);
                    db.Entry(pm).State = EntityState.Modified;
                    db.SaveChanges();

                    List<PurchaseOrderDetail> pod = db.DPurchaseOrderDeatil.Where(q => q.PurchaseOrderId == pm.PurchaseOrderId).ToList();
                    if (pod.Count != 0)
                    {
                        foreach (var a in pod)
                        {
                            a.Status = "Close";
                            db.DPurchaseOrderDeatil.Attach(a);
                            db.Entry(a).State = EntityState.Modified;
                            db.SaveChanges();
                        }
                    }
                }

                return Request.CreateResponse(HttpStatusCode.OK,pom);
            }
            catch (Exception exe)
            {
                Console.Write(exe.Message);
                return Request.CreateResponse(HttpStatusCode.BadRequest, "got Excepion");
            }
        }

        [Route("closePO")]
        [AcceptVerbs("POST")]
        public bool post(int id, List<PurchaseOrderDetailRecived> po)
        {
            try
            {
                PurchaseOrderMaster pm = db.DPurchaseOrderMaster.Where(x => x.PurchaseOrderId == id).FirstOrDefault();
                pm.Status = "Received";
                db.DPurchaseOrderMaster.Attach(pm);
                db.Entry(pm).State = EntityState.Modified;
                db.SaveChanges();
                List<PurchaseOrderDetailRecived> podrList = db.PurchaseOrderRecivedDetails.Where(x => x.PurchaseOrderId == pm.PurchaseOrderId).ToList();
                if (podrList.Count != 0)
                {
                    foreach (var a in podrList)
                    {
                        a.CreationDate = indianTime;
                        a.Status = "Received";
                        db.PurchaseOrderRecivedDetails.Attach(a);
                        db.Entry(a).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
                List<PurchaseOrderDetail> pod = db.DPurchaseOrderDeatil.Where(q => q.PurchaseOrderId == pm.PurchaseOrderId).ToList();
                if (pod.Count != 0)
                {
                    foreach (var a in pod)
                    {
                        a.Status = "Close";
                        db.DPurchaseOrderDeatil.Attach(a);
                        db.Entry(a).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
                return true;
            }
            catch (Exception exe)
            {
                Console.Write(exe.Message);
                return false;
            }
        }

        [Authorize]
        [Route("")]
        public HttpResponseMessage GetallorderdetailRecived(string id,string a)
        {
            logger.Info("start : ");
            PurchaseOrderMaster ass = new PurchaseOrderMaster();
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                int compid = 0, userid = 0;
                // Access claims
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

                logger.Info("User ID : {0} , Company Id : {1}", compid, userid);
                int idd = Int32.Parse(id);
                ass = context.AllPOrderDetails1(idd);
                logger.Info("End  : ");
                return Request.CreateResponse(HttpStatusCode.OK, ass); ;
            }
            catch (Exception ex)
            {
                logger.Error("Error in PurchaseOrderDetail " + ex.Message);
                logger.Info("End  PurchaseOrderDetail: ");
                return Request.CreateResponse(HttpStatusCode.BadRequest, "");
            }
        }

        [Authorize]
        [Route("")]
        public IEnumerable<PurchaseOrderDetail> Getallorderdetails(string id)
        {
            logger.Info("start : ");
            List<PurchaseOrderDetail> ass = new List<PurchaseOrderDetail>();
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                int compid = 0, userid = 0;
                // Access claims
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

                logger.Info("User ID : {0} , Company Id : {1}", compid, userid);
                int idd = Int32.Parse(id);
                ass = context.AllPOrderDetails(idd).ToList();
                logger.Info("End  : ");
                return ass;
            }
            catch (Exception ex)
            {
                logger.Error("Error in PurchaseOrderDetail " + ex.Message);
                logger.Info("End  PurchaseOrderDetail: ");
                return null;
            }
        }
    }
}