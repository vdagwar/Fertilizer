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
    [RoutePrefix("api/OrderDispatchedDetailsReturn")]
    public class OrderDispatchedDetailsReturnController : ApiController
    {
        iAuthContext context = new AuthContext();
        public static Logger logger = LogManager.GetCurrentClassLogger();
        private static TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
        private AuthContext db = new AuthContext();

        //// [Authorize]
        //[Route("")]
        //public IEnumerable<PurchaseOrderDetail> Get(string recordtype)
        //{
        //    if (recordtype == "details")
        //    {
        //        logger.Info("start PurchaseOrderDetail: ");
        //        List<PurchaseOrderDetail> ass = new List<PurchaseOrderDetail>();
        //        try
        //        {
        //            var identity = User.Identity as ClaimsIdentity;
        //            int compid = 1, userid = 0;
        //            // Access claims
        //            foreach (Claim claim in identity.Claims)
        //            {
        //                if (claim.Type == "compid")
        //                {
        //                    compid = int.Parse(claim.Value);
        //                }
        //                if (claim.Type == "userid")
        //                {
        //                    userid = int.Parse(claim.Value);
        //                }
        //            }

        //            logger.Info("User ID : {0} , Company Id : {1}", compid, userid);
        //            ass = context.AllPOdetails(compid).ToList();
        //            logger.Info("End  order: ");
        //            return ass;
        //        }
        //        catch (Exception ex)
        //        {
        //            logger.Error("Error in PurchaseOrderDetail " + ex.Message);
        //            logger.Info("End  PurchaseOrderDetail: ");
        //            return null;
        //        }
        //    }
        //    return null;
        //}


        [ResponseType(typeof(ReturnOrderDispatchedDetails))]
        [Route("")]
        [AcceptVerbs("POST")]
        public List<ReturnOrderDispatchedDetails> add(List<ReturnOrderDispatchedDetails> po)
        {
            int Oid = po[0].OrderId;
            List<OrderDispatchedDetails> dispatchedobj = new List<OrderDispatchedDetails>();
            dispatchedobj = db.OrderDispatchedDetailss.Where(x => x.OrderId == Oid).ToList();
            List<FinalOrderDispatchedDetails> Finalobj = new List<FinalOrderDispatchedDetails>();
            try
            {
                for (var i = 0; i < dispatchedobj.Count; i++)
                {
                    for (var k = 0; k < po.Count; k++)
                    {
                        if (po[k].isDeleted != true && dispatchedobj[i].ItemId == po[k].ItemId)
                        {
                            dispatchedobj[i].qty = dispatchedobj[i].qty - po[k].qty;
                            var itemIDmaster = dispatchedobj[i].ItemId;
                            ItemMaster items = db.itemMasters.Where(x => x.ItemId == itemIDmaster).Select(x => x).FirstOrDefault();
                            FinalOrderDispatchedDetails newfinal = new FinalOrderDispatchedDetails();
                            newfinal.OrderDispatchedDetailsId = dispatchedobj[i].OrderDispatchedDetailsId;
                            newfinal.OrderDetailsId = dispatchedobj[i].OrderDetailsId;
                            newfinal.OrderId = dispatchedobj[i].OrderId;
                            newfinal.OrderDispatchedMasterId = dispatchedobj[i].OrderDispatchedMasterId;
                            newfinal.CustomerId = dispatchedobj[i].CustomerId;
                            newfinal.CustomerName = dispatchedobj[i].CustomerName;
                            newfinal.City = dispatchedobj[i].City;
                            newfinal.Mobile = dispatchedobj[i].Mobile;
                            newfinal.OrderDate = dispatchedobj[i].OrderDate;
                            newfinal.CompanyId = dispatchedobj[i].CompanyId;
                            newfinal.CityId = dispatchedobj[i].CityId;
                            newfinal.Warehouseid = dispatchedobj[i].Warehouseid;
                            newfinal.WarehouseName = dispatchedobj[i].WarehouseName;
                            newfinal.CategoryName = dispatchedobj[i].CategoryName;

                            newfinal.ItemId = dispatchedobj[i].ItemId;
                            newfinal.Itempic = dispatchedobj[i].Itempic;
                            newfinal.itemname = dispatchedobj[i].itemname;
                            newfinal.itemcode = dispatchedobj[i].itemcode;
                            newfinal.Barcode = dispatchedobj[i].Barcode;
                            newfinal.UnitPrice = dispatchedobj[i].UnitPrice;
                            newfinal.Purchaseprice = dispatchedobj[i].Purchaseprice;
                            newfinal.MinOrderQty = dispatchedobj[i].MinOrderQty;
                            newfinal.MinOrderQtyPrice = dispatchedobj[i].MinOrderQtyPrice;
                            newfinal.qty = dispatchedobj[i].qty;
                            newfinal.price = dispatchedobj[i].price;
                            newfinal.MinOrderQty = dispatchedobj[i].MinOrderQty;
                            int MOQ = dispatchedobj[i].MinOrderQty;
                            newfinal.MinOrderQtyPrice = MOQ * dispatchedobj[i].UnitPrice;
                            newfinal.qty = Convert.ToInt32(dispatchedobj[i].qty);

                            int qty = 0;
                            qty = Convert.ToInt32(newfinal.qty);

                            newfinal.TaxPercentage = items.TotalTaxPercentage;
                            //........CALCULATION FOR NEW SHOPKIRANA.............................
                            newfinal.Noqty = qty; // for total qty (no of items)

                            // STEP 1  (UNIT PRICE * QTY)     - SHOW PROPERTY                  
                            newfinal.TotalAmt = System.Math.Round(newfinal.UnitPrice * qty, 2);

                            // STEP 2 (AMOUT WITHOU TEX AND WITHOUT DISCOUNT ) - SHOW PROPERTY
                            newfinal.AmtWithoutTaxDisc = ((100 * newfinal.UnitPrice * qty) / (1 + newfinal.TaxPercentage / 100)) / 100;

                            // STEP 3 (AMOUNT WITHOUT TAX AFTER DISCOUNT) - UNSHOW PROPERTY
                            newfinal.AmtWithoutAfterTaxDisc = (100 * newfinal.AmtWithoutTaxDisc) / (100 + items.PramotionalDiscount);

                            //STEP 4 (TAX AMOUNT) - UNSHOW PROPERTY
                            newfinal.TaxAmmount = (newfinal.AmtWithoutAfterTaxDisc * newfinal.TaxPercentage) / 100;

                            //STEP 5(TOTAL TAX AMOUNT) - UNSHOW PROPERTY
                            newfinal.TotalAmountAfterTaxDisc = newfinal.AmtWithoutAfterTaxDisc + newfinal.TaxAmmount;

                            //...............Calculate Discount.............................
                            newfinal.DiscountPercentage = items.PramotionalDiscount;
                            newfinal.DiscountAmmount = 0;
                            newfinal.NetAmtAfterDis = 0;
                            //...................................................................
                            newfinal.Purchaseprice = items.price;
                            //newfinal.VATTax = items.VATTax;
                            newfinal.CreatedDate = Convert.ToDateTime(dispatchedobj[i].CreatedDate);
                            newfinal.UpdatedDate = Convert.ToDateTime(dispatchedobj[i].CreatedDate);
                            newfinal.Deleted = false;

                            Finalobj.Add(newfinal);
                        }
                    }
                }
                foreach (FinalOrderDispatchedDetails x1 in Finalobj)
                {
                    db = new AuthContext();
                    db.FinalOrderDispatchedDetailsDb.Add(x1);
                    int id = db.SaveChanges();
                }
                List<int> ItemIds = new List<int>();
                foreach (ReturnOrderDispatchedDetails pc in po)
                {
                    db = new AuthContext();
                    OrderMaster om = db.DbOrderMaster.Where(x => x.OrderId == pc.OrderId && x.Deleted == false).FirstOrDefault();
                    om.Status = "Order Canceled";
                    db.DbOrderMaster.Attach(om);
                    db.Entry(om).State = EntityState.Modified;
                    db.SaveChanges();

                    OrderDispatchedMaster odm = db.OrderDispatchedMasters.Where(x => x.OrderId == pc.OrderId && x.Deleted == false).FirstOrDefault();
                    odm.Status = "Order Canceled";
                    db.OrderDispatchedMasters.Attach(odm);
                    db.Entry(odm).State = EntityState.Modified;
                    db.SaveChanges();

                    //update stock
                    ItemMaster master = db.itemMasters.Where(c => c.ItemId == pc.ItemId).SingleOrDefault();
                    CurrentStock itemm = db.DbCurrentStock.Where(x => x.ItemNumber == master.Number && x.Warehouseid == pc.Warehouseid).SingleOrDefault();
                    
                    if (itemm != null && !ItemIds.Any(x=> x==master.ItemId))
                    {

                        CurrentStockHistory Oss = new CurrentStockHistory();
                        if (itemm != null)
                        {
                            Oss.StockId = itemm.StockId;
                            Oss.ItemNumber = itemm.ItemNumber;
                            Oss.ItemName = itemm.ItemName;
                            Oss.CurrentInventory = itemm.CurrentInventory;
                            Oss.OdOrPoId = pc.OrderId;
                            Oss.OrderCancelInventoryIn = Convert.ToInt32(pc.qty);
                            Oss.TotalInventory = Convert.ToInt32(itemm.CurrentInventory + pc.qty);
                            Oss.WarehouseName = itemm.WarehouseName;
                            Oss.CreationDate = DateTime.Now;
                            db.CurrentStockHistoryDb.Add(Oss);
                            int id = db.SaveChanges();
                        }

                        itemm.CurrentInventory = Convert.ToInt32(itemm.CurrentInventory + (pc.qty));
                        context.UpdateCurrentStock(itemm);
                        ItemIds.Add(master.ItemId);
                    }
                    var ord = db.DbOrderDetails.Where(r => r.OrderDetailsId == pc.OrderDetailsId).SingleOrDefault();
                    ord.Status = "Order Canceled";
                    db.DbOrderDetails.Attach(ord);
                    db.Entry(ord).State = EntityState.Modified;
                    db.SaveChanges();

                    db.AddReturnOrderDispatchedDetails(pc);                    
                }
                try
                {
                    OrderMaster om = db.DbOrderMaster.Where(x => x.OrderId == Oid && x.Deleted == false).FirstOrDefault();
                    var rpoint = db.RewardPointDb.Where(c => c.CustomerId == om.CustomerId).FirstOrDefault();
                    if (rpoint != null)
                    {
                        if (om.RewardPoint > 0)
                        {
                            rpoint.EarningPoint -= om.RewardPoint;
                            if (rpoint.EarningPoint < 0)
                                rpoint.EarningPoint = 0;
                            rpoint.UpdatedDate = indianTime;
                            rpoint.TransactionDate = indianTime;
                            db.RewardPointDb.Attach(rpoint);
                            db.Entry(rpoint).State = EntityState.Modified;
                            db.SaveChanges();
                        }
                    }
                }
                catch (Exception ex) { }

                try
                {
                    var FreeItemList = db.SKFreeItemDb.Where(f => f.OrderId == Oid).ToList();
                    foreach (var freeitem in FreeItemList)
                    {
                        CurrentStock itemms = db.DbCurrentStock.Where(x => x.ItemNumber == freeitem.itemNumber).SingleOrDefault();

                        if (itemms != null)
                        {
                            itemms.CurrentInventory = Convert.ToInt32(itemms.CurrentInventory + (freeitem.TotalQuantity));
                            context.UpdateCurrentStock(itemms);
                        }
                    }
                }
                catch (Exception ex) { }
                return po;
            }
            catch (Exception exe)
            {
                return null;
            }
        }


        [Authorize]
        [Route("")]
        public IEnumerable<ReturnOrderDispatchedDetails> GetallReturndispatchDetailbyId(string id)
        {
            logger.Info("start : ");
            List<ReturnOrderDispatchedDetails> ass = new List<ReturnOrderDispatchedDetails>();
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
                ass = context.AllReturnOrderDispatchedDetails(idd).ToList();
                logger.Info("End  : ");
                return ass;
            }
            catch (Exception ex)
            {
                logger.Error("Error in returnorderby id " + ex.Message);
                logger.Info("End  returnorderby id: ");
                return null;
            }
        }

    }
}
