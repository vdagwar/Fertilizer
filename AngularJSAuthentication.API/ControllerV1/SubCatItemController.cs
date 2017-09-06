using AngularJSAuthentication.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Caching;
using System.Web.Http;

namespace AngularJSAuthentication.API.ControllerV1
{
    [RoutePrefix("api/ssitem")]
    public class SubCatItemController : ApiController
    {
        AuthContext context = new AuthContext();
        private static TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
        [Route("")]
        [HttpGet]
        public HttpResponseMessage getbyid(int warid, int catid)
        {
            try
            {

                var cachePolicty = new CacheItemPolicy();
                cachePolicty.AbsoluteExpiration = indianTime.AddMonths(25);
                SSITEM item = new SSITEM();
                var cache = MemoryCache.Default;

                if (cache.Get("CAT" + warid.ToString() + catid.ToString()) == null)
                {
                    var subsubcategory = context.SubsubCategorys.Where(x => x.IsActive==true && x.Categoryid == catid).Select(x => new factorySubSubCategory()
                    {
                        Categoryid = x.Categoryid,
                        SubCategoryId = x.SubCategoryId,
                        SubsubCategoryid = x.SubsubCategoryid,
                        SubsubcategoryName = x.SubsubcategoryName
                    }).ToList();
                    var ItemMasters = context.itemMasters.Where(x => x.active==true && x.Categoryid == catid && x.warehouse_id == warid).Select(x => new factoryItemdata()
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
                        SellingUnitName = x.SellingUnitName,
                        SellingSku = x.SellingSku,
                        UnitPrice = x.UnitPrice,
                        HindiName = x.HindiName,
                        VATTax = x.VATTax,
                        marginPoint = x.marginPoint,
                        promoPerItems = x.promoPerItems
                    }).ToList();
                
                    item.SubsubCategories = subsubcategory;
                    item.ItemMasters = ItemMasters;

                    cache.Add("CAT" + warid.ToString() + catid.ToString(), item, cachePolicty); 
                }
                else
                {                  
                    item = (SSITEM)cache.Get("CAT" + warid.ToString() + catid.ToString());                   
                }
                return Request.CreateResponse(HttpStatusCode.OK, item);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [Route("")]
        [HttpGet]
        public HttpResponseMessage getdailyessential(int warid)
        {
            try
            {
                var ItemMasters = context.itemMasters.Where(x => x.warehouse_id == warid && x.IsDailyEssential==true).Select(x => new factoryItemdata()
                {
                    Categoryid = x.Categoryid,
                    Discount = x.Discount,
                    ItemId = x.ItemId,
                    itemname = x.SellingUnitName,
                    LogoUrl = x.LogoUrl,
                    MinOrderQty = x.MinOrderQty,
                    price = x.price,
                    SubCategoryId = x.SubCategoryId,
                    SubsubCategoryid = x.SubsubCategoryid,
                    TotalTaxPercentage = x.TotalTaxPercentage,
                    SellingUnitName = x.SellingUnitName,
                    SellingSku = x.SellingSku,
                    UnitPrice = x.UnitPrice,
                    HindiName = x.HindiName,
                    VATTax = x.VATTax,
                    marginPoint = x.marginPoint,
                    promoPerItems = x.promoPerItems
                }).ToList();
                return Request.CreateResponse(HttpStatusCode.OK, new SSITEM()
                {
                    ItemMasters = ItemMasters
                });
            }
            catch (Exception)
            {

                throw;
            }
        }
        public class SSITEM
        {
            public List<factoryItemdata> ItemMasters { get; set; }
            public List<factorySubSubCategory> SubsubCategories { get; set; }
        }

        public class factoryItemdata
        {
            public int ItemId { get; set; }
            public int Categoryid { get; set; }
            public int SubCategoryId { get; set; }
            public int SubsubCategoryid { get; set; }
            public string itemname { get; set; }
            public string HindiName { get; set; }
            public double price { get; set; }
            public string SellingUnitName { get; set; }
            public string SellingSku { get; set; }
            public double UnitPrice { get; set; }
            public double VATTax { get; set; }
            public string LogoUrl { get; set; }
            public int MinOrderQty { get; set; }
            public double Discount { get; set; }
            public double TotalTaxPercentage { get; set; }
            public int? marginPoint { get; internal set; }
            public int? promoPerItems { get; internal set; }
        }
        public class factorySubSubCategory
        {
            public int SubsubCategoryid { get; set; }
            public string SubsubcategoryName { get; set; }
            public int Categoryid { get; set; }
            public int SubCategoryId { get; set; }
        }
    }
}
