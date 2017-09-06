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

namespace AngularJSAuthentication.API.Controllers
{
    [RoutePrefix("api/OrderDetails")]
    public class OrderDetailsController : ApiController
    {
        iAuthContext context = new AuthContext();
        public static Logger logger = LogManager.GetCurrentClassLogger();
        
        private AuthContext db = new AuthContext();

        // [Authorize]
        [Route("")]
        public IEnumerable<OrderDetails> Get(string recordtype)
        {
            if (recordtype == "details")
            {
                logger.Info("start City: ");
                List<OrderDetails> ass = new List<OrderDetails>();
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
                    ass = context.Allorddetails(compid).ToList();
                    logger.Info("End  order: ");
                    return ass;
                }
                catch (Exception ex)
                {
                    logger.Error("Error in orderdetails " + ex.Message);
                    logger.Info("End  orderdetails: ");
                    return null;
                }
            }
            return null;
        }
        [Authorize]
        [Route("")]
        public IList<DemandDetailsNew> Getallorderdetails(int wid)
        {
            var currentstock = db.DbCurrentStock.Where(x => x.Deleted == false).ToList();

            List<DemandDetailsNew> DemandDetails = (from a in db.DbOrderDetails
                                 where (a.Status == "Pending" || a.Status == "Process") && a.Deleted == false
                                 join i in db.itemMasters on a.ItemId equals i.ItemId
                                 select new DemandDetailsNew
                                 {
                                     ItemId = a.ItemId,
                                     itemname = a.itemname,
                                     ItemCode = i.itemcode,
                                     MinOrderQty = i.PurchaseMinOrderQty,
                                     qty = a.qty,
                                     City = a.City,
                                     CityId=a.CityId,
                                     Warehouseid = a.Warehouseid,
                                     WarehouseName = a.WarehouseName,
                                     CreatedDate = a.CreatedDate,
                                     status =a.Status,
                                     itemNumber = i.Number,                                     
                                     PurchaseSku = i.PurchaseSku,
                                     PurchaseUnitName = i.PurchaseUnitName

                                 }).ToList();
            List<DemandDetailsNew> demdlist = new List<DemandDetailsNew>();
            List<DemandDetailsNew> uniquelist = new List<DemandDetailsNew>();
            foreach (var selectunique in DemandDetails)
            {
                int count = 0; //01AE101110
                var check = uniquelist.Where(x => x.PurchaseSku == selectunique.PurchaseSku).SingleOrDefault();
                if (check == null)
                {
                    count += 1;
                    uniquelist.Add(selectunique);
                }
                else
                {
                    check.qty = check.qty + selectunique.qty;
                    uniquelist.First(d => d.PurchaseSku == selectunique.PurchaseSku).qty = check.qty;
                }
            }
            foreach ( var a in uniquelist)
            {
                CurrentStock itm = currentstock.Where(x => x.ItemNumber.Trim() == a.itemNumber.Trim()).SingleOrDefault();
                if (itm != null) {
                    if (itm.CurrentInventory < a.qty)
                    {
                        a.qty = a.qty - itm.CurrentInventory;
                        List<PurchaseOrderDetailRecived> poList = db.PurchaseOrderRecivedDetails.Where(x => x.ItemId == a.ItemId && x.Status != "Received").ToList();
                        List<PurchaseOrderDetail> po1 = db.DPurchaseOrderDeatil.Where(x => x.ItemId == a.ItemId && x.Status == "ordered").ToList();
                        if (poList.Count != 0 && po1.Count != 0)
                        {
                            foreach (var p in poList)
                            {
                                a.qty = a.qty - Convert.ToInt32((p.PurchaseQty - p.QtyRecived) * p.MOQ);
                            }
                            foreach (var p1 in po1)
                            {
                                a.qty = a.qty - Convert.ToInt32(p1.TotalQuantity);
                            }
                            if (a.qty > 0)
                            {
                                demdlist.Add(a);
                            }
                        }
                        else if (poList.Count != 0 && po1.Count == 0)
                        {
                            foreach (var p in poList)
                            {
                                a.qty = a.qty - Convert.ToInt32((p.PurchaseQty - p.QtyRecived) * p.MOQ);
                            }
                            if (a.qty > 0)
                            {
                                demdlist.Add(a);
                            }
                        }
                        else if (poList.Count == 0 && po1.Count != 0)
                        {
                            foreach (var p in po1)
                            {
                                a.qty = a.qty - Convert.ToInt32(p.TotalQuantity);
                            }
                            if (a.qty > 0)
                            {
                                demdlist.Add(a);
                            }
                        }
                        else
                        {
                            demdlist.Add(a);
                        }
                    }                      
                }
            }         
            return demdlist;
        }


        [Authorize]
        [Route("")]
        public IEnumerable<OrderDetails> Getallorderdetails(string id)
        {
            logger.Info("start : ");
            List<OrderDetails> ass = new List<OrderDetails>();
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
                ass = context.AllOrderDetails(idd).ToList();
                logger.Info("End  : ");
                return ass;
            }
            catch (Exception ex)
            {
                logger.Error("Error in OrderDetails " + ex.Message);
                logger.Info("End  OrderDetails: ");
                return null;
            }
        }
        
        [Authorize]
        [Route("")]
        public IEnumerable<DemandDetailsNew> Getallorderdetails(string Cityid, string Warehouseid,DateTime datefrom, DateTime dateto)
        {
            logger.Info("start : ");
            IList<DemandDetailsNew> list = null;
            List<OrderDetails> ass = new List<OrderDetails>();
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
                list = context.AllfilteredOrderDetails(Cityid, Warehouseid, datefrom, dateto).ToList();
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

    }
}