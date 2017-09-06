using AngularJSAuthentication.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Caching;
using System.Security.Claims;
using System.Web.Http;

namespace AngularJSAuthentication.API.ControllerV1
{
    [RoutePrefix("api/ExcelOrder")]
    public class ExcelOrderController : ApiController
    {
        iAuthContext context = new AuthContext();

        [Route("")]
        [HttpGet]
        public HttpResponseMessage getAppOrders(DateTime? start, DateTime? end, int OrderId, string Skcode, string ShopName, string Mobile, string status) //get search orders for delivery
        {
            try
            {
                var DBoyorders = context.searchorderbycustomer(start, end, OrderId, Skcode, ShopName, Mobile, status);
                return Request.CreateResponse(HttpStatusCode.OK, DBoyorders);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [Route("")]
        [HttpGet]
        public HttpResponseMessage getExports(string type, DateTime? start, DateTime? end, int Warehouseid) //get search orders for delivery
        {
            try
            {
                AuthContext db = new AuthContext();
                List<OrderDetailsExport> newdata = new List<OrderDetailsExport>();

                if (Warehouseid != 0)
                {
                    newdata = (from od in db.DbOrderDetails
                               where od.Deleted == false && od.OrderDate >= start && od.OrderDate <= end && od.Warehouseid == Warehouseid
                            join ii in db.Customers on od.CustomerId equals ii.CustomerId
                              // join item in db.itemMasters on od.ItemId equals item.ItemId
                              // join cat in db.Categorys on item.Categoryid equals cat.Categoryid
                              // join sbcat in db.SubsubCategorys on item.SubsubCategoryid equals sbcat.SubsubCategoryid
                               select new OrderDetailsExport
                               {
                                   ItemId = od.ItemId,
                                   itemname = od.itemname,
                                   itemNumber = od.itemNumber,
                                   CustomerName = od.CustomerName,
                                   ShopName = ii.ShopName,
                                   Skcode = ii.Skcode,
                                   OrderId = od.OrderId,
                                   UnitPrice = od.UnitPrice,
                                   Mobile = ii.Mobile,
                                   qty = od.qty,
                                   WarehouseName = od.WarehouseName,
                                   Date = od.OrderDate,
                                   Status = od.Status,
                                   TotalAmt = od.TotalAmt,
                                   //CategoryName = cat.CategoryName,
                                   // BrandName = sbcat.SubsubcategoryName,
                                   orderDispatchedDetailsExport = (from d in db.OrderDispatchedDetailss
                                                                   where d.Deleted == false && d.OrderDetailsId == od.OrderDetailsId && d.Warehouseid == Warehouseid
                                                                    join iii in db.Customers on od.CustomerId equals iii.CustomerId
                                                                   //join items in db.itemMasters on d.ItemId equals items.ItemId
                                                                   //join catt in db.Categorys on item.Categoryid equals catt.Categoryid
                                                                   //join sbcatt in db.SubsubCategorys on item.SubsubCategoryid equals sbcatt.SubsubCategoryid
                                                                   select new OrderDispatchedDetailsExport
                                                                   {
                                                                       ItemId = d.ItemId,
                                                                       itemname = d.itemname,
                                                                       itemNumber = d.itemNumber,
                                                                       CustomerName = d.CustomerName,
                                                                       ShopName = iii.ShopName,
                                                                       Skcode = iii.Skcode,
                                                                       Mobile = iii.Mobile,
                                                                     QtyChangeReason = d.QtyChangeReason,
                                                                       OrderId = d.OrderId,
                                                                       dUnitPrice = d.UnitPrice,
                                                                       TaxPercentage = d.TaxPercentage,
                                                                       dqty = d.qty,
                                                                       WarehouseName = d.WarehouseName,
                                                                       Date = d.OrderDate,
                                                                       Status = d.Status,
                                                                       dTotalAmt = d.TotalAmt,
                                                                       //CategoryName = catt.CategoryName,
                                                                       //BrandName = sbcatt.SubsubcategoryName
                                                                   }).ToList()    
                               }).OrderByDescending(x => x.OrderId).ToList();
                }
                else
                {
                    newdata = (from od in db.DbOrderDetails
                               where od.Warehouseid.Equals(Warehouseid)
                               join item in db.itemMasters on od.ItemId equals item.ItemId
                               join cat in db.Categorys on item.Categoryid equals cat.Categoryid
                               join sbcat in db.SubsubCategorys on item.SubsubCategoryid equals sbcat.SubsubCategoryid
                               select new OrderDetailsExport
                               {
                                   ItemId = od.ItemId,
                                   itemname = item.itemname,
                                   itemNumber = item.Number,
                                   sellingSKU = item.SellingSku,
                                   price = od.price,
                                   UnitPrice = od.UnitPrice,
                                   MinOrderQtyPrice = od.UnitPrice * od.MinOrderQty,
                                   qty = od.qty,
                                   DiscountPercentage = od.DiscountPercentage,
                                   DiscountAmmount = od.DiscountAmmount,
                                   TaxPercentage = od.TaxPercentage,
                                   TaxAmmount = od.TaxAmmount,
                                   TotalAmt = od.TotalAmt,
                                   CategoryName = cat.CategoryName,
                                   BrandName = sbcat.SubsubcategoryName,
                                   orderDispatchedDetailsExport = (from d in db.OrderDispatchedDetailss
                                                                   join items in db.itemMasters on od.ItemId equals item.ItemId
                                                                   //join cat in db.Categorys on item.Categoryid equals cat.Categoryid
                                                                   //join sbcat in db.SubsubCategorys on item.SubsubCategoryid equals sbcat.SubsubCategoryid
                                                                   select new OrderDispatchedDetailsExport
                                                                   {
                                                                       ItemId = d.ItemId,
                                                                       itemname = items.itemname,
                                                                       itemNumber = items.Number,
                                                                       sellingSKU = items.SellingSku,
                                                                       price = d.price,
                                                                       dUnitPrice = d.UnitPrice,
                                                                       MinOrderQtyPrice = d.UnitPrice * d.MinOrderQty,
                                                                       dqty = d.qty,
                                                                       DiscountPercentage = d.DiscountPercentage,
                                                                       DiscountAmmount = d.DiscountAmmount,
                                                                       TaxPercentage = d.TaxPercentage,
                                                                       TaxAmmount = d.TaxAmmount,
                                                                       dTotalAmt = d.TotalAmt,
                                                                       CategoryName = cat.CategoryName,
                                                                       BrandName = sbcat.SubsubcategoryName
                                                                   }).ToList()     /*a.orderDetails,*/
                               }).OrderByDescending(x => x.OrderId).ToList();
                }
                if (newdata.Count == 0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "");
                }
                return Request.CreateResponse(HttpStatusCode.OK, newdata);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}
