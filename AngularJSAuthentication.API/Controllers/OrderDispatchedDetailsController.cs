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
    [RoutePrefix("api/OrderDispatchedDetails")]
    public class OrderDispatchedDetailsController : ApiController
    {
        iAuthContext context = new AuthContext();
        public static Logger logger = LogManager.GetCurrentClassLogger();
        
        private AuthContext db = new AuthContext();

        [ResponseType(typeof(OrderDispatchedDetails))]
        [Route("")]
        [AcceptVerbs("POST")]
        public List<OrderDispatchedDetails> add(List<OrderDispatchedDetails> po)
        {
            bool isDiscount = false;
            //bool isBaseWithoutChild = false;
            double finaltotal = 0;
            double finalTaxAmount = 0;
            double finalSGSTTaxAmount = 0;
            double finalCGSTTaxAmount = 0;
            double finalGrossAmount = 0;
            double finalTotalTaxAmount = 0;          

            foreach (OrderDispatchedDetails pc in po)
            {                
                ItemMaster items = db.itemMasters.Where(x => x.ItemId == pc.ItemId).Select(x => x).FirstOrDefault();
                OrderMaster om = db.DbOrderMaster.Where(x => x.OrderId == pc.OrderId && x.Deleted == false).FirstOrDefault();
                om.Status = "Ready to Dispatch";
                db.DbOrderMaster.Attach(om);
                db.Entry(om).State = EntityState.Modified;
                db.SaveChanges();


                if (pc != null && pc.isDeleted != true)
                {
                    ItemMaster master = db.itemMasters.Where(c => c.ItemId == pc.ItemId).SingleOrDefault();
                    CurrentStock item = db.DbCurrentStock.Where(x => x.ItemNumber == master.Number && x.Warehouseid == pc.Warehouseid).SingleOrDefault();                    
                    if (item != null)
                    {


                        CurrentStockHistory Oss = new CurrentStockHistory();
                        if (item != null)
                        {
                            Oss.StockId = item.StockId;
                            Oss.ItemNumber = item.ItemNumber;
                            Oss.ItemName = item.ItemName;
                            Oss.OdOrPoId = pc.OrderId;
                            Oss.CurrentInventory = item.CurrentInventory;
                            Oss.InventoryOut = Convert.ToInt32(pc.qty);
                            Oss.TotalInventory = Convert.ToInt32(item.CurrentInventory - pc.qty);
                            Oss.WarehouseName = item.WarehouseName;
                            Oss.CreationDate = DateTime.Now;
                            db.CurrentStockHistoryDb.Add(Oss);
                            int id = db.SaveChanges();
                        }


                        item.CurrentInventory = Convert.ToInt32(item.CurrentInventory - pc.qty);
                        context.UpdateCurrentStock(item);
                    }

                    var ord = db.DbOrderDetails.Where(r => r.OrderDetailsId == pc.OrderDetailsId).SingleOrDefault();
                    ord.Status = "Ready to Dispatch";
                    ord.DiscountPercentage = pc.DiscountPercentage;
                    db.DbOrderDetails.Attach(ord);
                    db.Entry(ord).State = EntityState.Modified;
                    db.SaveChanges();
                   
                    // calculation
                    var od = new OrderDispatchedDetails();
                    od.OrderDispatchedDetailsId = pc.OrderDispatchedDetailsId;
                    if (pc.DiscountPercentage == 0)
                    {
                        isDiscount = false;
                    }
                    else
                    {
                        isDiscount = true;
                    }
                    od.DiscountPercentage = pc.DiscountPercentage;
                    od.OrderDetailsId = pc.OrderDetailsId;
                    od.OrderId = pc.OrderId;
                    od.OrderDispatchedMasterId = pc.OrderDispatchedMasterId;
                    od.CustomerId = pc.CustomerId;
                    od.CustomerName = pc.CustomerName;
                    od.City = pc.City;
                    od.Mobile = pc.Mobile;
                    od.OrderDate = pc.OrderDate;
                    od.CompanyId = pc.CompanyId;
                    od.CityId = pc.CityId;
                    od.Warehouseid = pc.Warehouseid;
                    od.WarehouseName = pc.WarehouseName;
                    od.CategoryName = pc.CategoryName;
                    od.ItemId = pc.ItemId;
                    od.Itempic = pc.Itempic;
                    od.itemname = pc.itemname;
                    od.SubcategoryName = pc.SubcategoryName;
                    od.SubsubcategoryName = pc.SubsubcategoryName;
                    od.itemcode = pc.itemcode;
                    od.Barcode = pc.Barcode;
                    od.UnitPrice = pc.UnitPrice;
                    od.Purchaseprice = pc.Purchaseprice;
                    od.MinOrderQty = pc.MinOrderQty;
                    od.MinOrderQtyPrice = pc.MinOrderQtyPrice;
                    od.qty = pc.qty;
                    od.price = pc.price;
                    od.MinOrderQty = pc.MinOrderQty;
                    int MOQ = items.MinOrderQty;
                    od.MinOrderQtyPrice = MOQ * items.UnitPrice;
                    od.price = items.price;
                    od.MinOrderQty = items.MinOrderQty;
                    od.MinOrderQtyPrice = MOQ * items.UnitPrice;
                    od.qty = Convert.ToInt32(pc.qty);
                    od.SizePerUnit = pc.SizePerUnit;



                    int qty = 0;
                    if (master != null)
                    {
                        qty = Convert.ToInt32(pc.qty);
                    }
                    else
                    {
                        qty = Convert.ToInt32(od.qty);

                    }

                    od.TaxPercentage = items.TotalTaxPercentage;
                    if (od.TaxPercentage >= 0)
                    {
                        od.SGSTTaxPercentage = od.TaxPercentage / 2;
                        od.CGSTTaxPercentage = od.TaxPercentage / 2;
                    }


                    od.HSNCode = items.HSNCode;
                    //........CALCULATION FOR NEW SHOPKIRANA.............................
                    od.Noqty = qty; // for total qty (no of items)

                    // STEP 1  (UNIT PRICE * QTY)     - SHOW PROPERTY                                   
                    od.TotalAmt = System.Math.Round(pc.UnitPrice * qty, 2);

                    // STEP 2 (AMOUT WITHOU TEX AND WITHOUT DISCOUNT ) - SHOW PROPERTY
                    od.AmtWithoutTaxDisc = ((100 * od.UnitPrice * qty) / (1 + od.TaxPercentage / 100)) / 100;

                    // STEP 3 (AMOUNT WITHOUT TAX AFTER DISCOUNT) - UNSHOW PROPERTY
                    od.AmtWithoutAfterTaxDisc = (100 * od.AmtWithoutTaxDisc) / (100 + od.DiscountPercentage);

                    //STEP 4 (TAX AMOUNT) - UNSHOW PROPERTY
                    od.TaxAmmount = (od.AmtWithoutAfterTaxDisc * od.TaxPercentage) / 100;

                    if (od.TaxAmmount >= 0)
                    {
                        od.SGSTTaxAmmount = od.TaxAmmount / 2;
                        od.CGSTTaxAmmount = od.TaxAmmount / 2;
                    }



                    //STEP 5(TOTAL TAX AMOUNT) - UNSHOW PROPERTY
                    od.TotalAmountAfterTaxDisc = od.AmtWithoutAfterTaxDisc + od.TaxAmmount;


                    //...............Calculate Discount.............................
                    //od.DiscountPercentage = items.PramotionalDiscount;
                    od.DiscountAmmount = 0;
                    // double DiscountAmmount = od.DiscountAmmount;
                    // double NetAmtAfterDis = (od.NetAmmount - DiscountAmmount);
                    od.NetAmtAfterDis = 0;
                    //double TaxAmmount = od.TaxAmmount;
                    //...................................................................
                    //od.UnitId = items.UnitId;
                    //od.Unitname = items.UnitName;
                    od.Purchaseprice = items.price;
                    //od.VATTax = items.VATTax;
                    od.CreatedDate = Convert.ToDateTime(pc.CreatedDate);
                    od.UpdatedDate = Convert.ToDateTime(pc.CreatedDate);
                    od.Deleted = false;
                    od.Status = "Ready to Dispatch";
                    od.QtyChangeReason = pc.QtyChangeReason;
                    od.itemNumber = pc.itemNumber;
                    context.AddOrderDispatchedDetails(od);
                    finaltotal = finaltotal + od.TotalAmt;
                    finalTaxAmount = finalTaxAmount + od.TaxAmmount;
                    finalSGSTTaxAmount = finalSGSTTaxAmount + od.SGSTTaxAmmount;
                    finalCGSTTaxAmount = finalCGSTTaxAmount + od.CGSTTaxAmmount;
                


                    finalGrossAmount = finalGrossAmount + od.TotalAmountAfterTaxDisc;
                    finalTotalTaxAmount = finalTotalTaxAmount + od.TotalAmountAfterTaxDisc;
                }
                else
                {

                }
            }
            int masterid = Convert.ToInt32(po[0].OrderDispatchedMasterId);
            OrderDispatchedMaster ODM = db.OrderDispatchedMasters.Where(x => x.OrderDispatchedMasterId == masterid).FirstOrDefault();
            finaltotal = finaltotal + ODM.deliveryCharge;
            finalGrossAmount = finalGrossAmount + ODM.deliveryCharge;
            if (ODM.WalletAmount > 0) { 
                ODM.TotalAmount = System.Math.Round(finaltotal, 2) - ODM.WalletAmount.GetValueOrDefault();
                ODM.TaxAmount = System.Math.Round(finalTaxAmount, 2);
                ODM.SGSTTaxAmmount = System.Math.Round(finalSGSTTaxAmount, 2);
                ODM.CGSTTaxAmmount = System.Math.Round(finalCGSTTaxAmount, 2);

                ODM.GrossAmount = System.Math.Round((Convert.ToInt32(finalGrossAmount) - ODM.WalletAmount.GetValueOrDefault()), 0);
            }
            else
            {
                ODM.TotalAmount = System.Math.Round(finaltotal, 2);
                ODM.TaxAmount = System.Math.Round(finalTaxAmount, 2);
                ODM.SGSTTaxAmmount = System.Math.Round(finalSGSTTaxAmount, 2);
                ODM.CGSTTaxAmmount = System.Math.Round(finalCGSTTaxAmount, 2);
                ODM.GrossAmount = System.Math.Round((finalGrossAmount), 0);
            }
            if (isDiscount == true)
            {
                ODM.DiscountAmount = finalTotalTaxAmount - finaltotal;
            }
            else
            {
                ODM.DiscountAmount = 0;
            }
            db.OrderDispatchedMasters.Attach(ODM);
            db.Entry(ODM).State = EntityState.Modified;
            db.SaveChanges();

            try
            {
                var FreeItemList = db.SKFreeItemDb.Where(f => f.OrderId == po[0].OrderId).ToList();
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

        // For cancle dispatch order
        [ResponseType(typeof(OrderDispatchedDetails))]
        [Route("")]
        [AcceptVerbs("PUT")]
        public List<OrderDispatchedDetails> add(List<OrderDispatchedDetails> ODD, string cancle)
        {    
            foreach (OrderDispatchedDetails pc in ODD)
            {
                // update Status Of Dispatch Master
                db = new AuthContext();
                OrderMaster om = db.DbOrderMaster.Where(x => x.OrderId == pc.OrderId && x.Deleted == false).FirstOrDefault();
                om.Status = "Cancel";
                db.DbOrderMaster.Attach(om);
                db.Entry(om).State = EntityState.Modified;
                db.SaveChanges();

                //  update Status Of Order Master Detail
                var ord = db.DbOrderDetails.Where(r => r.OrderDetailsId == pc.OrderDetailsId).FirstOrDefault();
                ord.Status = "Cancel";
                db.DbOrderDetails.Attach(ord);
                db.Entry(ord).State = EntityState.Modified;
                db.SaveChanges();

                // update status of DispatchDetailMaster
                var ODM = db.OrderDispatchedMasters.Where(p => p.OrderDispatchedMasterId == pc.OrderDispatchedMasterId).FirstOrDefault();
                ODM.Status = "Cancel";
                db.OrderDispatchedMasters.Attach(ODM);
                db.Entry(ODM).State = EntityState.Modified;
                db.SaveChanges();

                //update status of dispatchDetail
                var dispDetailobj = db.OrderDispatchedDetailss.Where(y => y.OrderDispatchedMasterId == pc.OrderDispatchedMasterId).FirstOrDefault();
                dispDetailobj.Status = "Cancel";
                db.OrderDispatchedDetailss.Attach(dispDetailobj);
                db.Entry(dispDetailobj).State = EntityState.Modified;
                db.SaveChanges();

                if (pc != null && pc.isDeleted != true)
                {                                      
                    ItemMaster master = db.itemMasters.Where(c => c.ItemId ==pc.ItemId  && c.active == true).SingleOrDefault();
                    var item = db.DbCurrentStock.Where(x => x.ItemNumber == master.Number && x.Warehouseid == pc.Warehouseid).SingleOrDefault();
                    if (item != null)
                    {
                        item.CurrentInventory = item.CurrentInventory + pc.qty;
                        context.UpdateCurrentStock(item);
                    }
                    else { }
                }
                else { }
            }
            return ODD;
        }
        // end 
        
        [Authorize]
        [Route("")]
        public IEnumerable<OrderDispatchedDetails> GetalldispatchDetailbyId(string id)
        {
            logger.Info("start : ");
            List<OrderDispatchedDetails> ass = new List<OrderDispatchedDetails>();
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
                 ass = context.AllPOrderDispatchedDetails(idd).ToList();
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