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
    public class PurchaseList
    {
        public PurchaseList()
        {            
        }
        public string name { get; set; }
        public double qty { get; set; }
        public string Supplier { get; set; }
        public int? SupplierId { get; set; }
        public int OrderDetailsId { get; set; }
        public int? WareHouseId { get; set; }
        public string WareHouseName { get; set; }
        public double? Price { get; set; }
        public double conversionfactor { get; set; }
        public double PurchaseMinOrderQty { get; set; }
        public int currentinventory { get; set; }
        public int? ItemId { get; set; }
        public string itemNumber { get; set; }
        public string PurchaseSku { get; set; }
        public string ItemName { get; set; }
        public string orderMasterIDs { get; set; }
        public string orderIDs { get; set; }
        public int PurchaseOrderId { get; set; }
        public double finalqty
        {
            get
            {
                return Math.Ceiling(qty / conversionfactor);
            }
        }
    }
    public class TempPO
    {
        List<PurchaseList> _PurchaseList;
        public TempPO()
        {
            _PurchaseList = new List<PurchaseList>();
        }
        public string SupplierName { get; set; }
        public int? SupplierId { get; set; }
        public List<PurchaseList> Purchases
        {
            get
            {
                return _PurchaseList;
            }
            set { }
        }
    }

    [RoutePrefix("api/PurchaseOrderList")]
    public class PurchaseOrderListController : ApiController
    {
        iAuthContext context = new AuthContext();
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private static TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
        private AuthContext db = new AuthContext();

        [Route("")]
        public IList<PurchaseList> Get(int wid)
        {
            try//|| a.Status == "Process"
            {
                var poList = (from a in db.DbOrderDetails
                              where (a.Status == "Pending" || a.Status == "Process") && a.Deleted == false
                              join i in db.itemMasters on a.ItemId equals i.ItemId
                              select new PurchaseOrderList
                              {
                                  OrderDetailsId = a.OrderDetailsId,
                                  Warehouseid = a.Warehouseid,
                                  WarehouseName = a.WarehouseName,
                                  OrderDate = a.OrderDate,
                                  SupplierId = i.SupplierId,
                                  PurchaseSku = i.PurchaseSku,
                                  SupplierName = i.SupplierName,
                                  OrderId = a.OrderId,
                                  ItemId = a.ItemId,
                                  SKUCode = i.Number,
                                  ItemName = i.itemname,
                                  PurchaseUnitName = i.PurchaseUnitName,
                                  Unit = i.SellingUnitName, 
                                  Conversionfactor = i.PurchaseMinOrderQty,
                                  Discription = "",
                                  qty = a.qty,
                                  //CurrentInventory = c == null ? 0 : c.CurrentInventory,
                                  StoringItemName = i.StoringItemName,
                                  Price = i.PurchasePrice,
                                  NetAmmount = a.NetAmmount,
                                  TaxPercentage = a.TaxPercentage,
                                  TaxAmount = a.TaxAmmount,
                                  TotalAmountIncTax = a.TotalAmt,
                                  PurchaseMinOrderQty = i.PurchaseMinOrderQty,
                                  Status = a.Status,
                                  CreationDate = a.CreatedDate,
                                  Deleted = a.Deleted
                              }).ToList();
                
                List<PurchaseList> uniquelist = new List<PurchaseList>();

                foreach (PurchaseOrderList item in poList)
                {
                    int count = 0; //01AE101110
                    PurchaseList l = uniquelist.Where(x => x.PurchaseSku == item.PurchaseSku).SingleOrDefault();
                    if (l == null)
                    {
                        count += 1;
                        l = new PurchaseList();
                        l.name = item.PurchaseUnitName;
                        l.conversionfactor = item.Conversionfactor;
                        l.Supplier = item.SupplierName;
                        l.SupplierId = item.SupplierId;
                        l.WareHouseId = item.Warehouseid;
                        l.WareHouseName = item.WarehouseName;
                        l.OrderDetailsId = item.OrderDetailsId;
                        l.itemNumber = item.SKUCode;
                        l.PurchaseSku = item.PurchaseSku;
                        l.orderIDs = item.OrderDetailsId + "," + l.orderIDs;
                        l.ItemId = item.ItemId;
                        l.ItemName = item.ItemName;
                        l.qty = l.qty + item.qty;
                        l.currentinventory = item.CurrentInventory;
                        l.Price = item.Price;
                        uniquelist.Add(l);
                    }
                    else {
                        l.orderIDs = item.OrderDetailsId + "," + l.orderIDs;
                        l.qty = l.qty + item.qty;
                        uniquelist.First(d => d.PurchaseSku == item.PurchaseSku).qty = l.qty;
                        uniquelist.First(d => d.PurchaseSku == item.PurchaseSku).orderIDs = l.orderIDs;
                    }
                    
                }
                List<PurchaseList> cc = new List<PurchaseList>();
                foreach (PurchaseList l in uniquelist)
                {
                    CurrentStock cs = db.DbCurrentStock.Where(k => k.ItemNumber == l.itemNumber && k.Deleted == false && k.Warehouseid == l.WareHouseId).SingleOrDefault();
                    if (cs != null)
                    {
                        l.currentinventory = cs.CurrentInventory;
                        if (l.qty > cs.CurrentInventory)
                        {
                            l.qty = l.qty - cs.CurrentInventory;
                            List<PurchaseOrderDetailRecived> po = db.PurchaseOrderRecivedDetails.Where(x => x.ItemId == l.ItemId && x.Status != "Received").ToList();
                            List<PurchaseOrderDetail> po1 = db.DPurchaseOrderDeatil.Where(x => x.ItemId == l.ItemId && x.Status == "ordered").ToList();
                            if (po.Count != 0 && po1.Count != 0)
                            {
                                foreach (var p in po)
                                {
                                    l.qty = l.qty - Convert.ToInt32((p.PurchaseQty - p.QtyRecived) * p.MOQ);
                                }
                                foreach (var p1 in po1)
                                {
                                    l.qty = l.qty - Convert.ToInt32(p1.PurchaseQty * p1.MOQ);
                                }
                                if (l.qty > 0)
                                {
                                    cc.Add(l);
                                }
                            }
                            else if (po.Count != 0 && po1.Count == 0)
                            {
                                foreach (var p in po)
                                {
                                    l.qty = l.qty - Convert.ToInt32((p.PurchaseQty - p.QtyRecived) * p.MOQ);
                                }
                                if (l.qty > 0)
                                {
                                    cc.Add(l);
                                }
                            }
                            else if (po.Count == 0 && po1.Count != 0)
                            {
                                foreach (var p in po1)
                                {
                                    l.qty = l.qty - Convert.ToInt32(p.PurchaseQty * p.MOQ);
                                }
                                if (l.qty > 0)
                                {
                                    cc.Add(l);
                                }
                            }
                            else
                            {
                                cc.Add(l);
                            }
                        }
                    }
                }
                return cc;
            }
            catch (Exception ex)
            {
                logger.Error("Error in getting Company " + ex.Message);
                return null;
            }
        }

        [Authorize]
        [Route("")]
        public IEnumerable<PurchaseOrderList> Getallorderdetails(string Cityid, string Warehouseid, DateTime? datefrom, DateTime? dateto)
        {
            logger.Info("start : ");
            IList<PurchaseOrderList> list = null;
            List<PurchaseOrderList> ass = new List<PurchaseOrderList>();
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
                //int idd = Int32.Parse(id);
                list = context.AllfilteredOrderDetails2(Cityid, Warehouseid, datefrom, dateto).ToList();
                logger.Info("End  : ");
                return list;
            }
            catch (Exception ex)
            {
                logger.Error("Error in OrderDetails " + ex.Message);
                logger.Info("End  OrderDetails: ");
                return null;
            }
        }
        
        [ResponseType(typeof(PurchaseOrderList))]
        [Route("")]
        //[ActionName("addAll")]
        [AcceptVerbs("POST")]
        public List<PurchaseOrderList> addAll(string returntype, data d)
        {
            logger.Info("start add PurchaseOrderList: ");
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
                int whid = Convert.ToInt32(d.warehouseid);
                int SID = Convert.ToInt32(d.Supplierid);
                List<PurchaseOrderList> po = new List<PurchaseOrderList>();
                int cityid = Convert.ToInt32(d.Cityid);

                po = d.datalist;
                if (d.Supplierid != null && d.Supplierid != "0" && d.warehouseid != null)
                {

                    int supplierid = Convert.ToInt32(d.Supplierid);
                    List<PurchaseOrderList> filterpo = po.Where(x => x.SupplierId == supplierid).Where(x => x.Warehouseid == whid).ToList();
                    context.AddPurchaseOrder(filterpo);
                }
                else if (d.warehouseid != null)
                {
                    context.AddPurchaseOrder(po.Where(x => x.Warehouseid == whid).ToList());
                }
                else if (d.Cityid != null && d.Supplierid != null)
                {
                    context.AddPurchaseOrder(po.Where(x => x.Cityid == cityid).Where(x => x.SupplierId == SID).ToList());
                }

                else if (d.Supplierid == "0")
                {
                    context.AddPurchaseOrder(po.ToList());
                }
                else if (d.Supplierid != null)
                {
                    context.AddPurchaseOrder(po.Where(x => x.SupplierId == SID).ToList());
                }

                //logger.Info("User ID : {0} , Company Id : {1}", compid, userid);

                //logger.Info("End  Warehouse: ");
                return po;
            }
            catch (Exception ex)
            {
                logger.Error("Error in addQuesAns " + ex.Message);
                logger.Info("End  addWarehouse: ");
                return null;
            }
        }
        
        [ResponseType(typeof(PurchaseList))]
        [Route("")]
        //[ActionName("addAll")]
        [AcceptVerbs("POST")]
        public List<PurchaseList> Add(List<PurchaseList> PurchaseOrderListdat)
        {
            logger.Info("start add PurchaseOrderList: ");
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
                //context.AddPurchaseOrder(PurchaseOrderListdat);
                return PurchaseOrderListdat;
            }
            catch (Exception ex)
            {
                logger.Error("Error in addQuesAns " + ex.Message);
                logger.Info("End  addWarehouse: ");
                return null;
            }
        }
        [ResponseType(typeof(TempPO))]
        [Route("")]
        //[ActionName("addAll")]
        [AcceptVerbs("POST")]
        public List<TempPO> Add(List<PurchaseList> temppo, string a)
        {
            List<TempPO> listtemp = new List<TempPO>();
            TempPO tempobj = new TempPO();
            List<PurchaseList> purcheslistobj = new List<PurchaseList>();                     
           
            foreach (var x in temppo)
            {
                string s = x.orderIDs;
                string[] values = s.Split(',');
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = values[i].Trim();
                }
                for (int i = 0; i < values.Length; i++)
                {
                    if (values[i] != "")
                    {
                        int ids = int.Parse(values[i]);
                        var ord = db.DbOrderDetails.Where(r => r.OrderDetailsId == ids).SingleOrDefault();
                        ord.Status = "Process";
                        db.DbOrderDetails.Attach(ord);
                        db.Entry(ord).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
                TempPO po = listtemp.Where(y => y.SupplierId == x.SupplierId).SingleOrDefault();
                if (po == null)
                {
                    po = new TempPO();
                    po.SupplierId = x.SupplierId;
                    po.SupplierName = x.Supplier;
                    po.Purchases.Add(x);
                    listtemp.Add(po);
                }
                else
                {
                    po.Purchases.Add(x);
                }
            }
            context.addPurchaseOrderMaster(listtemp);
            return listtemp;
        }
        
        [Route("")]
        [AcceptVerbs("POST")]
        public PurchaseList AddItem(PurchaseList temppo, string a, int  b)
        {
            AuthContext db = new AuthContext();
            List<TempPO> listtemp = new List<TempPO>();
            if (temppo != null)
            {
                var purId = 0;
                PurchaseOrderMaster pom = db.DPurchaseOrderMaster.Where(c => c.PurchaseOrderId == temppo.PurchaseOrderId).SingleOrDefault();
                if (pom == null)
                {
                    PurchaseOrderMaster pm = new PurchaseOrderMaster();
                    pm.SupplierId = temppo.SupplierId.GetValueOrDefault();
                    pm.SupplierName = temppo.Supplier;
                    pm.Warehouseid = temppo.WareHouseId;
                    pm.WarehouseName = temppo.WareHouseName;
                    pm.Status = "pending";
                    pm.Acitve = true;
                    pm.CreationDate = indianTime;
                    db.DPurchaseOrderMaster.Add(pm);
                    int id = db.SaveChanges();

                    purId = pm.PurchaseOrderId;
                }
                else
                {   
                    purId = pom.PurchaseOrderId;
                }
                var item = db.itemMasters.Where(z => z.PurchaseSku == temppo.PurchaseSku).FirstOrDefault();
                PurchaseOrderDetail pd = new PurchaseOrderDetail();
                pd.PurchaseOrderId = purId;
                pd.ItemId = item.ItemId;
                pd.ItemName = item.itemname;
                pd.TotalQuantity = int.Parse(temppo.qty.ToString());
                pd.CreationDate = indianTime;
                pd.Status = "ordered";
                pd.MOQ = item.PurchaseMinOrderQty;
                pd.Price = Convert.ToDouble(item.PurchasePrice);
                pd.Warehouseid = temppo.WareHouseId;
                pd.WarehouseName = temppo.WareHouseName;
                pd.SupplierId = temppo.SupplierId.GetValueOrDefault();
                pd.SupplierName = temppo.Supplier;
                //pd.TotalQuantity = Convert.ToInt16(temppo.qty);
                pd.PurchaseName = temppo.name;
                pd.PurchaseSku = temppo.PurchaseSku;
                pd.ConversionFactor = Convert.ToInt16(temppo.conversionfactor);
                pd.PurchaseQty = temppo.finalqty;


                db.DPurchaseOrderDeatil.Add(pd);
                int idd = db.SaveChanges();
            }
            else
            {
                return null;
            }
            return temppo;
            
        }

        [Route("addPo")]
        [AcceptVerbs("POST")]
        public HttpResponseMessage Add(int ItemId, int qty, int SupplierId)
        {
            AuthContext db = new AuthContext();
            try
            {
                PurchaseOrderMaster pm = new PurchaseOrderMaster();
                var item = db.itemMasters.Where(z => z.ItemId == ItemId).FirstOrDefault();
                var supplier = db.Suppliers.Where(s => s.SupplierId == SupplierId).SingleOrDefault();

                pm.SupplierId = supplier.SupplierId;
                pm.SupplierName = supplier.Name;
                pm.CreationDate = indianTime;
                pm.Warehouseid = item.warehouse_id;
                pm.WarehouseName = item.WarehouseName;
                pm.Status = "pending";
                pm.Acitve = true;
                db.DPurchaseOrderMaster.Add(pm);
                int id = db.SaveChanges();

                PurchaseOrderDetail pd = new PurchaseOrderDetail();
                pd.PurchaseOrderId = pm.PurchaseOrderId;
                pd.ItemId = item.ItemId;
                pd.ItemName = item.itemname;
                pd.PurchaseQty = qty;
             
                pd.CreationDate = indianTime;
                pd.Status = "ordered";
                pd.MOQ = item.PurchaseMinOrderQty;
                pd.Price = Convert.ToDouble(item.PurchasePrice);
                pd.Warehouseid = item.warehouse_id;
                pd.WarehouseName = item.WarehouseName;
                pd.SupplierId = supplier.SupplierId;
                pd.SupplierName = supplier.Name;
                //  pd.TotalQuantity = Convert.ToInt32(qty * pd.PurchaseQty);
                pd.TotalQuantity = Convert.ToInt32(pd.PurchaseQty);
                pd.PurchaseName = item.PurchaseUnitName;
                pd.PurchaseSku = item.PurchaseSku;
                pd.ConversionFactor = Convert.ToInt16(item.PurchaseMinOrderQty);

                db.DPurchaseOrderDeatil.Add(pd);
                int idd = db.SaveChanges();

                return Request.CreateResponse(HttpStatusCode.OK, "Add Successfuly");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}