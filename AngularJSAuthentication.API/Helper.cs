using AngularJSAuthentication.API.Controllers;
using AngularJSAuthentication.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Security.Cryptography;
using System.Web;

namespace AngularJSAuthentication.API
{
    public class Helper
    {
        protected static TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
        public static string GetHash(string input)
        {
            HashAlgorithm hashAlgorithm = new SHA256CryptoServiceProvider();
       
            byte[] byteValue = System.Text.Encoding.UTF8.GetBytes(input);

            byte[] byteHash = hashAlgorithm.ComputeHash(byteValue);

            return Convert.ToBase64String(byteHash);
        }
        public static void refreshItemMaster(int warid, int catid)
        {
            try
            {
                Helper h = new Helper();
                AuthContext context = new AuthContext();
                var cachePolicty = new CacheItemPolicy();
                cachePolicty.AbsoluteExpiration = h.indianTime.AddMonths(25);
                ControllerV1.SubCatItemController.SSITEM item = new ControllerV1.SubCatItemController.SSITEM();
                var cache = MemoryCache.Default;
                var subsubcategory = context.SubsubCategorys.Where(x => x.IsActive == true && x.Categoryid == catid).Select(x => new ControllerV1.SubCatItemController.factorySubSubCategory()
                {
                    Categoryid = x.Categoryid,
                    SubCategoryId = x.SubCategoryId,
                    SubsubCategoryid = x.SubsubCategoryid,
                    SubsubcategoryName = x.SubsubcategoryName
                }).ToList();
                var ItemMasters = context.itemMasters.Where(x => x.active == true && x.Categoryid == catid && x.warehouse_id == warid).Select(x => new ControllerV1.SubCatItemController.factoryItemdata()
                {
                    Categoryid = x.Categoryid,
                    Discount = x.Discount,
                    ItemId = x.ItemId,
                    itemname = x.SellingUnitName,
                    LogoUrl = x.SellingSku,
                    MinOrderQty = x.MinOrderQty,
                    price = x.price,
                    SubCategoryId = x.SubCategoryId,
                    SubsubCategoryid = x.SubsubCategoryid,
                    TotalTaxPercentage = x.TotalTaxPercentage,
                    UnitPrice = x.UnitPrice,
                    VATTax = x.VATTax,
                    SellingUnitName = x.SellingUnitName,
                    SellingSku = x.SellingSku,
                    HindiName = x.HindiName,
                    marginPoint = x.marginPoint,
                    promoPerItems = x.promoPerItems
                }).ToList();

                cache.Remove("CAT" + warid.ToString() + catid.ToString());
                item.SubsubCategories = subsubcategory;
                item.ItemMasters = ItemMasters;
                cache.Add("CAT" + warid.ToString() + catid.ToString(), item, cachePolicty);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static void refreshItemMaster(int warehouseid)
        {
            Helper h = new Helper();
            AuthContext context = new AuthContext();
            customeritems ibjtosend = new customeritems();

            var cachePolicty = new CacheItemPolicy();
            cachePolicty.AbsoluteExpiration = h.indianTime.AddMonths(25);

            var cache = MemoryCache.Default;

            var dbware = context.DbWarehouseCategory.Where(x => x.Warehouseid == warehouseid).ToList();
            List<Categories> categories = new List<Categories>();
            foreach (var d in dbware)
            {
                var cat = context.Categorys.Where(x => x.IsActive == true && x.Deleted == false && x.Categoryid == d.Categoryid).FirstOrDefault();
                if (cat != null)
                {
                    categories.Add(new Categories()
                    {
                        Categoryid = cat.Categoryid,
                        Warehouseid = d.Warehouseid,
                        CategoryName = cat.CategoryName,
                        BaseCategoryId = cat.BaseCategoryId,
                        LogoUrl = cat.LogoUrl
                    });
                }
            }
            cache.Remove(warehouseid.ToString());

            ibjtosend.Basecats = context.BaseCategoryDb.Where(x => x.IsActive == true && x.Deleted == false).Select(s => new Basecats() { BaseCategoryId = s.BaseCategoryId, BaseCategoryName = s.BaseCategoryName, Warehouseid = s.Warehouseid, LogoUrl = s.LogoUrl });
            ibjtosend.Categories = categories;
            ibjtosend.SubCategories = context.SubCategorys.Where(x => x.IsActive == true && x.Deleted == false).Select(s => new SubCategories() { SubCategoryId = s.SubCategoryId, SubcategoryName = s.SubcategoryName, Categoryid = s.Categoryid });

            cache.Add(warehouseid.ToString(), ibjtosend, cachePolicty);
        }
        public static customeritems getItemMaster(int warehouseid)
        {
            Helper h = new Helper();
            AuthContext context = new AuthContext();
            customeritems ibjtosend = new customeritems();

            var cachePolicty = new CacheItemPolicy();
            cachePolicty.AbsoluteExpiration = h.indianTime.AddMonths(25);

            var cache = MemoryCache.Default;
            if (cache.Get(warehouseid.ToString()) == null)
            {
                var dbware = context.DbWarehouseCategory.Where(x => x.Warehouseid == warehouseid && x.IsVisible==true).ToList();
                List<Categories> categories = new List<Categories>();
                foreach (var d in dbware)
                {
                    var cat = context.Categorys.Where(x => x.IsActive == true && x.Categoryid == d.Categoryid).FirstOrDefault();
                    if (cat != null)
                    {
                        categories.Add(new Categories()
                        {
                            Categoryid = cat.Categoryid,
                            Warehouseid = d.Warehouseid,
                            CategoryName = cat.CategoryName,
                            BaseCategoryId = cat.BaseCategoryId,
                            LogoUrl = cat.LogoUrl
                        });
                    }
                }
                cache.Remove(warehouseid.ToString());

                ibjtosend.Basecats = context.BaseCategoryDb.Where(x => x.IsActive == true).Select(s => new Basecats() { BaseCategoryId = s.BaseCategoryId, BaseCategoryName = s.BaseCategoryName, Warehouseid = s.Warehouseid, LogoUrl = s.LogoUrl });
                ibjtosend.Categories = categories;
                ibjtosend.SubCategories = context.SubCategorys.Where(x => x.IsActive == true).Select(s =>  new SubCategories() { SubCategoryId = s.SubCategoryId, SubcategoryName = s.SubcategoryName, Categoryid = s.Categoryid });

                cache.Add(warehouseid.ToString(), ibjtosend, cachePolicty);
            }
            else
            {
                ibjtosend = (customeritems)cache.Get(warehouseid.ToString());
            }

            return ibjtosend;
        }


        public static void refreshCategory()
        {
            AuthContext context = new AuthContext(); 
            var dbware = context.Warehouses.Where(x => x.Deleted == false).ToList();
            foreach (var d in dbware)
            {
                refreshItemMaster(d.Warehouseid);
            }
        }



        public static void refreshsubsubCategory(int id)
        {
            AuthContext context = new AuthContext();
            List<Warehouse> warehouse = context.Warehouses.Where(x => x.Deleted == false).ToList();
            foreach (var w in warehouse)
            {
                refreshItemMaster(w.Warehouseid, id);
            }
        }
    }
}