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
    [RoutePrefix("api/editPrice")]
    public class EditPriceController : ApiController
    {
        iAuthContext context = new AuthContext();
        public static Logger logger = LogManager.GetCurrentClassLogger();
        [Authorize]
        [Route("")]
        public IEnumerable<ItemMaster> Get()
        {
            logger.Info("start Item Master: ");
            List<ItemMaster> ass = new List<ItemMaster>();
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

                logger.Info("User ID : {0} , Company Id : {1}", compid, userid);
                ass = context.AllItemMaster().ToList();
                logger.Info("End  Item Master: ");
                return ass;
            }
            catch (Exception ex)
            {
                logger.Error("Error in Item Master " + ex.Message);
                logger.Info("End  Item Master: ");
                return null;
            }
        }


        [Authorize]
        [ResponseType(typeof(ItemMaster))]
        [Route("")]
        public IEnumerable<ItemMaster> Get(string cityid, string categoryid, string subcategoryid, string subsubcategoryid)
        {
            logger.Info("start ItemMaster: ");
            List<ItemMaster> ass = new List<ItemMaster>();
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

                logger.Info("User ID : {0} , Company Id : {1}", compid, userid);
                ass = context.filteredItemMaster(cityid, categoryid, subcategoryid, subsubcategoryid).ToList();
                logger.Info("End ItemMaster: ");
                return ass;
            }
            catch (Exception ex)
            {
                logger.Error("Error in ItemMaster " + ex.Message);
                logger.Info("End  ItemMaster: ");
                return null;
            }
        }

        [Authorize]
        [ResponseType(typeof(ItemMaster))]
        [Route("")]
        [AcceptVerbs("PUT")]
        public ItemMaster Put(List<ItemMaster> item)
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
                logger.Info("User ID : {0} , Company Id : {1}", compid, userid);
                return context.Saveediteditem(item);
                //context.Saveediteditem(item);
            }
            catch
            {
                return null;
            }
        }
        [Route("byid")]
        [AcceptVerbs("PUT")]
        public dynamic marginupdate(ItemMaster data)
        {
            AuthContext db = new AuthContext();

            var im = db.itemMasters.Where(i => i.ItemId == data.ItemId).SingleOrDefault();
            if (im != null)
            {
                try
                {

                    EditPriceHistory Os = new EditPriceHistory();
                    if (im.PurchasePrice != data.PurchasePrice || im.Margin != data.Margin || im.price != data.price || im.NetPurchasePrice != data.NetPurchasePrice || im.Discount != data.Discount)
                    {
                        Os.ItemId = im.ItemId;
                        Os.Cityid = im.Cityid;
                        Os.CityName = im.CityName;
                        Os.Categoryid = im.Categoryid;
                        Os.SubCategoryId = im.SubCategoryId;
                        Os.SubsubCategoryid = im.SubsubCategoryid;
                        Os.warehouse_id = im.warehouse_id;
                        Os.SupplierId = im.SupplierId;
                        Os.SUPPLIERCODES = im.SUPPLIERCODES;
                        Os.CompanyId = im.CompanyId;
                        Os.CategoryName = im.CategoryName;
                        Os.BaseCategoryid = im.BaseCategoryid;
                        Os.BaseCategoryName = im.BaseCategoryName;
                        Os.SubcategoryName = im.SubcategoryName;
                        Os.SubsubcategoryName = im.SubsubcategoryName;
                        Os.SupplierName = im.SupplierName;
                        Os.itemname = im.itemname;
                        Os.itemcode = im.itemcode;
                        Os.SellingUnitName = im.SellingUnitName;
                        Os.PurchaseUnitName = im.PurchaseUnitName;
                        Os.price = im.price;
                        Os.VATTax = im.VATTax;
                        Os.active = im.active;
                        Os.LogoUrl = im.LogoUrl;
                        Os.CatLogoUrl = im.CatLogoUrl;
                        Os.MinOrderQty = im.MinOrderQty;
                        Os.PurchaseMinOrderQty = im.PurchaseMinOrderQty;
                        Os.GruopID = im.GruopID;
                        Os.TGrpName = im.TGrpName;
                        Os.Discount = im.Discount;
                        Os.UnitPrice = im.UnitPrice;
                        Os.Number = im.Number;
                        Os.PurchaseSku = im.PurchaseSku;
                        Os.SellingSku = im.SellingSku;
                        Os.PurchasePrice = im.PurchasePrice;
                        Os.GeneralPrice = im.GeneralPrice;
                        Os.title = im.title;
                        Os.Description = im.Description;
                        Os.StartDate = im.StartDate;
                        Os.EndDate = im.EndDate;
                        Os.PramotionalDiscount = im.PramotionalDiscount;
                        Os.TotalTaxPercentage = im.TotalTaxPercentage;
                        Os.WarehouseName = im.WarehouseName;
                        Os.CreatedDate = DateTime.Now;
                        Os.UpdatedDate = im.UpdatedDate;
                        Os.Deleted = im.Deleted;
                        Os.IsDailyEssential = im.IsDailyEssential;
                        Os.DisplaySellingPrice = im.DisplaySellingPrice;
                        Os.StoringItemName = im.StoringItemName;
                        Os.SizePerUnit = im.SizePerUnit;
                        Os.HindiName = im.HindiName;
                        Os.Barcode = im.Barcode;
                        Os.HindiName = im.HindiName;
                        Os.Barcode = im.Barcode;
                        Os.Margin = im.Margin;
                        Os.PurchasePrice = im.PurchasePrice;
                        Os.NetPurchasePrice = im.NetPurchasePrice;
                        db.EditPriceHistoryDb.Add(Os);
                        int id = db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    logger.Error("Error loading  item:- " + im.itemname + "\n\n" + ex.Message + "\n\n" + ex.InnerException + "\n\n" + ex.StackTrace);
                }

                double withouttaxvalue = (data.PurchasePrice / ((100 + data.TotalTaxPercentage) / 100));

                double withouttax = Math.Round(withouttaxvalue, 3);

                double netDiscountAmountvalue = (withouttax * (data.Discount / 100));

                double netDiscountAmount = Math.Round(netDiscountAmountvalue, 3);

                im.NetPurchasePrice = Math.Round((withouttax - netDiscountAmount), 3);

                im.Discount = data.Discount;
                im.Margin = data.Margin;
                im.price = data.price;
                im.PurchasePrice = data.PurchasePrice;
                var value = data.PurchasePrice + (data.PurchasePrice * data.Margin / 100);
                im.UnitPrice = Math.Round(value, 3);
                im.UpdatedDate = DateTime.Now;
                if (im.Margin > 0)
                {
                    var rs = db.RetailerShareDb.Where(r => r.cityid == im.Cityid).FirstOrDefault();
                    if (rs != null)
                    {
                        var cf = db.RPConversionDb.FirstOrDefault();
                        try
                        {
                            double mv = (im.PurchasePrice * (im.Margin / 100) * (rs.share / 100) * cf.point);
                            var value1 = Math.Round(mv, MidpointRounding.AwayFromZero);
                            im.marginPoint = Convert.ToInt32(value1);
                        }
                        catch (Exception ex)
                        {
                            logger.Error(ex.Message);
                        }
                    }
                }

                db.itemMasters.Attach(im);
                db.Entry(im).State = EntityState.Modified;
                db.SaveChanges();
                AngularJSAuthentication.API.Helper.refreshItemMaster(im.warehouse_id, im.Categoryid);
            }
            return im;
        }
    }
}