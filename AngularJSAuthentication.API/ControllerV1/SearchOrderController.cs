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
    [RoutePrefix("api/SearchOrder")]
    public class SearchOrderController : ApiController
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
        public HttpResponseMessage getExports(string type, DateTime? start, DateTime? end, int OrderId, string Skcode, string ShopName, string Mobile, string status) //get search orders for delivery
        {
            try
            {
                AuthContext db = new AuthContext();
                List<OrderMasterDTO> newdata = new List<OrderMasterDTO>();
                if ((Mobile != null) && start != null)
                {
                    newdata = (from a in db.DbOrderMaster
                               where (a.Deleted == false && a.CreatedDate >= start && a.CreatedDate <= end
                               && (a.Customerphonenum.Contains(Mobile)))
                               join i in db.Customers on a.CustomerId equals i.CustomerId 
                               join clstr in db.Clusters on i.ClusterId equals clstr.ClusterId 
                               select new OrderMasterDTO
                               {
                                   Skcode = i.Skcode,
                                   ShopName = i.ShopName,
                                   CustomerName = a.CustomerName,
                                   CustomerId = i.CustomerId,
                                   OrderBy = a.OrderTakenSalesPerson,
                                   Customerphonenum = a.Customerphonenum,
                                   Warehouseid = a.Warehouseid,
                                   WarehouseName = a.WarehouseName,
                                   ClusterName = clstr.ClusterName,
                                   ClusterId = clstr.ClusterId,
                                   OrderId = a.OrderId,
                                   CreatedDate = a.CreatedDate,
                                   BillingAddress = a.BillingAddress,
                                   CityId = a.CityId,
                                   CompanyId = a.CompanyId,
                                   Deliverydate = a.Deliverydate,
                                   DiscountAmount = a.DiscountAmount,
                                   DivisionId = a.DivisionId,
                                   GrossAmount = a.GrossAmount,
                                   invoice_no = a.invoice_no,
                                   ReDispatchCount = a.ReDispatchCount,
                                   SalesPerson = a.SalesPerson,
                                   SalesPersonId = a.SalesPersonId,
                                   ShippingAddress = a.ShippingAddress,
                                   deliveryCharge = a.deliveryCharge,
                                   Status = a.Status,
                                   comments = a.comments,
                                   ReasonCancle = a.ReasonCancle,
                                   orderDetailsExport = (from od in a.orderDetails
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
                                                             SGSTTaxPercentage = od.SGSTTaxPercentage,
                                                             CGSTTaxPercentage = od.CGSTTaxPercentage,
                                                             TaxAmmount = od.TaxAmmount,
                                                             SGSTTaxAmmount = od.SGSTTaxAmmount,
                                                             CGSTTaxAmmount = od.CGSTTaxAmmount,
                                                             TotalAmt = od.TotalAmt,
                                                             CategoryName = cat.CategoryName,
                                                             BrandName = sbcat.SubsubcategoryName
                                                         }).ToList()     /*a.orderDetails,*/
                               }).OrderByDescending(x => x.OrderId).ToList();
                }
                else if ((Skcode != null) && start != null)
                {
                    newdata = (from a in db.DbOrderMaster
                               where (a.Deleted == false && a.CreatedDate >= start && a.CreatedDate <= end
                               && a.Skcode.Contains(Skcode))
                               join i in db.Customers on a.CustomerId equals i.CustomerId
                               join clstr in db.Clusters on i.ClusterId equals clstr.ClusterId
                               select new OrderMasterDTO
                               {
                                   Skcode = i.Skcode,
                                   ShopName = i.ShopName,
                                   CustomerName = a.CustomerName,
                                   OrderBy = a.OrderTakenSalesPerson,
                                   CustomerId = i.CustomerId,
                                   Customerphonenum = a.Customerphonenum,
                                   Warehouseid = a.Warehouseid,
                                   WarehouseName = a.WarehouseName,
                                   ClusterName = clstr.ClusterName,
                                   ClusterId = clstr.ClusterId,
                                   OrderId = a.OrderId,
                                   CreatedDate = a.CreatedDate,
                                   BillingAddress = a.BillingAddress,
                                   CityId = a.CityId,
                                   CompanyId = a.CompanyId,
                                   Deliverydate = a.Deliverydate,
                                   DiscountAmount = a.DiscountAmount,
                                   DivisionId = a.DivisionId,
                                   GrossAmount = a.GrossAmount,
                                   invoice_no = a.invoice_no,
                                   ReDispatchCount = a.ReDispatchCount,
                                   SalesPerson = a.SalesPerson,
                                   SalesPersonId = a.SalesPersonId,
                                   ShippingAddress = a.ShippingAddress,
                                   deliveryCharge = a.deliveryCharge,
                                   Status = a.Status,
                                   comments = a.comments,
                                   ReasonCancle = a.ReasonCancle,
                                   orderDetailsExport = (from od in a.orderDetails
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
                                                             SGSTTaxPercentage = od.SGSTTaxPercentage,
                                                             CGSTTaxPercentage = od.CGSTTaxPercentage,
                                                             TaxAmmount = od.TaxAmmount,
                                                             SGSTTaxAmmount = od.SGSTTaxAmmount,
                                                             CGSTTaxAmmount = od.CGSTTaxAmmount,
                                                             TotalAmt = od.TotalAmt,
                                                             CategoryName = cat.CategoryName,
                                                             BrandName = sbcat.SubsubcategoryName
                                                         }).ToList()     /*a.orderDetails,*/
                               }).OrderByDescending(x => x.OrderId).ToList();
                }
                else if ((ShopName != null) && start != null)
                {
                    newdata = (from a in db.DbOrderMaster
                               where (a.Deleted == false && a.CreatedDate >= start && a.CreatedDate <= end
                               && a.ShopName.Contains(ShopName))
                               join i in db.Customers on a.CustomerId equals i.CustomerId
                               join clstr in db.Clusters on i.ClusterId equals clstr.ClusterId
                               select new OrderMasterDTO
                               {
                                   Skcode = i.Skcode,
                                   ShopName = i.ShopName,
                                   CustomerName = a.CustomerName,
                                   OrderBy = a.OrderTakenSalesPerson,
                                   CustomerId = i.CustomerId,
                                   Customerphonenum = a.Customerphonenum,
                                   Warehouseid = a.Warehouseid,
                                   WarehouseName = a.WarehouseName,
                                   ClusterName = clstr.ClusterName,
                                   ClusterId = clstr.ClusterId,
                                   OrderId = a.OrderId,
                                   CreatedDate = a.CreatedDate,
                                   BillingAddress = a.BillingAddress,
                                   CityId = a.CityId,
                                   CompanyId = a.CompanyId,
                                   Deliverydate = a.Deliverydate,
                                   DiscountAmount = a.DiscountAmount,
                                   DivisionId = a.DivisionId,
                                   GrossAmount = a.GrossAmount,
                                   invoice_no = a.invoice_no,
                                   ReDispatchCount = a.ReDispatchCount,
                                   SalesPerson = a.SalesPerson,
                                   SalesPersonId = a.SalesPersonId,
                                   ShippingAddress = a.ShippingAddress,
                                   deliveryCharge = a.deliveryCharge,
                                   Status = a.Status,
                                   comments = a.comments,
                                   ReasonCancle = a.ReasonCancle,
                                   orderDetailsExport = (from od in a.orderDetails
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
                                                             SGSTTaxPercentage = od.SGSTTaxPercentage,
                                                             CGSTTaxPercentage = od.CGSTTaxPercentage,
                                                             TaxAmmount = od.TaxAmmount,
                                                             SGSTTaxAmmount = od.SGSTTaxAmmount,
                                                             CGSTTaxAmmount = od.CGSTTaxAmmount,
                                                             TotalAmt = od.TotalAmt,
                                                             CategoryName = cat.CategoryName,
                                                             BrandName = sbcat.SubsubcategoryName
                                                         }).ToList()     /*a.orderDetails,*/
                               }).OrderByDescending(x => x.OrderId).ToList();
                }
                else if ((OrderId != 0) && start != null)
                {
                    newdata = (from a in db.DbOrderMaster
                               where (a.Deleted == false && a.CreatedDate >= start && a.CreatedDate <= end
                               && a.OrderId.Equals(OrderId))
                               join i in db.Customers on a.CustomerId equals i.CustomerId
                               join clstr in db.Clusters on i.ClusterId equals clstr.ClusterId
                               select new OrderMasterDTO
                               {
                                   Skcode = i.Skcode,
                                   ShopName = i.ShopName,
                                   CustomerName = a.CustomerName,
                                   OrderBy = a.OrderTakenSalesPerson,
                                   CustomerId = i.CustomerId,
                                   Customerphonenum = a.Customerphonenum,
                                   Warehouseid = a.Warehouseid,
                                   WarehouseName = a.WarehouseName,
                                   ClusterName = clstr.ClusterName,
                                   ClusterId = clstr.ClusterId,
                                   OrderId = a.OrderId,
                                   CreatedDate = a.CreatedDate,
                                   BillingAddress = a.BillingAddress,
                                   CityId = a.CityId,
                                   CompanyId = a.CompanyId,
                                   Deliverydate = a.Deliverydate,
                                   DiscountAmount = a.DiscountAmount,
                                   DivisionId = a.DivisionId,
                                   GrossAmount = a.GrossAmount,
                                   invoice_no = a.invoice_no,
                                   ReDispatchCount = a.ReDispatchCount,
                                   SalesPerson = a.SalesPerson,
                                   SalesPersonId = a.SalesPersonId,
                                   ShippingAddress = a.ShippingAddress,
                                   deliveryCharge = a.deliveryCharge,
                                   Status = a.Status,
                                   comments = a.comments,
                                   ReasonCancle = a.ReasonCancle,
                                   orderDetailsExport = (from od in a.orderDetails
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
                                                             SGSTTaxPercentage = od.SGSTTaxPercentage,
                                                             CGSTTaxPercentage = od.CGSTTaxPercentage,
                                                             TaxAmmount = od.TaxAmmount,
                                                             SGSTTaxAmmount = od.SGSTTaxAmmount,
                                                             CGSTTaxAmmount = od.CGSTTaxAmmount,
                                                             TotalAmt = od.TotalAmt,
                                                             CategoryName = cat.CategoryName,
                                                             BrandName = sbcat.SubsubcategoryName
                                                         }).ToList()     /*a.orderDetails,*/
                               }).OrderByDescending(x => x.OrderId).ToList();
                }
                else if ((status != null) && start != null)
                {
                    newdata = (from a in db.DbOrderMaster
                               where (a.Deleted == false && a.CreatedDate >= start && a.CreatedDate <= end
                               && a.Status.Equals(status))
                               join i in db.Customers on a.CustomerId equals i.CustomerId
                               join clstr in db.Clusters on i.ClusterId equals clstr.ClusterId
                               select new OrderMasterDTO
                               {
                                   Skcode = i.Skcode,
                                   ShopName = i.ShopName,
                                   CustomerName = a.CustomerName,
                                   OrderBy = a.OrderTakenSalesPerson,
                                   CustomerId = i.CustomerId,
                                   Customerphonenum = a.Customerphonenum,
                                   Warehouseid = a.Warehouseid,
                                   WarehouseName = a.WarehouseName,
                                   ClusterName = clstr.ClusterName,
                                   ClusterId = clstr.ClusterId,
                                   OrderId = a.OrderId,
                                   CreatedDate = a.CreatedDate,
                                   BillingAddress = a.BillingAddress,
                                   CityId = a.CityId,
                                   CompanyId = a.CompanyId,
                                   Deliverydate = a.Deliverydate,
                                   DiscountAmount = a.DiscountAmount,
                                   DivisionId = a.DivisionId,
                                   GrossAmount = a.GrossAmount,
                                   invoice_no = a.invoice_no,
                                   ReDispatchCount = a.ReDispatchCount,
                                   SalesPerson = a.SalesPerson,
                                   SalesPersonId = a.SalesPersonId,
                                   ShippingAddress = a.ShippingAddress,
                                   deliveryCharge = a.deliveryCharge,
                                   Status = a.Status,
                                   comments = a.comments,
                                   ReasonCancle = a.ReasonCancle,
                                   orderDetailsExport = (from od in a.orderDetails
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
                                                             SGSTTaxPercentage = od.SGSTTaxPercentage,
                                                             CGSTTaxPercentage = od.CGSTTaxPercentage,
                                                             TaxAmmount = od.TaxAmmount,
                                                             SGSTTaxAmmount = od.SGSTTaxAmmount,
                                                             CGSTTaxAmmount = od.CGSTTaxAmmount,
                                                             TotalAmt = od.TotalAmt,
                                                             CategoryName = cat.CategoryName,
                                                             BrandName = sbcat.SubsubcategoryName
                                                         }).ToList()     /*a.orderDetails,*/
                               }).OrderByDescending(x => x.OrderId).ToList();
                }
                else if (Mobile != null)
                {
                    newdata = (from a in db.DbOrderMaster
                               where a.Customerphonenum.Contains(Mobile)
                               join i in db.Customers on a.CustomerId equals i.CustomerId
                               join clstr in db.Clusters on i.ClusterId equals clstr.ClusterId
                               select new OrderMasterDTO
                               {
                                   Skcode = i.Skcode,
                                   ShopName = i.ShopName,
                                   CustomerName = a.CustomerName,
                                   OrderBy = a.OrderTakenSalesPerson,
                                   CustomerId = i.CustomerId,
                                   Customerphonenum = a.Customerphonenum,
                                   Warehouseid = a.Warehouseid,
                                   WarehouseName = a.WarehouseName,
                                   ClusterName = clstr.ClusterName,
                                   ClusterId = clstr.ClusterId,
                                   OrderId = a.OrderId,
                                   CreatedDate = a.CreatedDate,
                                   BillingAddress = a.BillingAddress,
                                   CityId = a.CityId,
                                   CompanyId = a.CompanyId,
                                   Deliverydate = a.Deliverydate,
                                   DiscountAmount = a.DiscountAmount,
                                   DivisionId = a.DivisionId,
                                   GrossAmount = a.GrossAmount,
                                   invoice_no = a.invoice_no,
                                   ReDispatchCount = a.ReDispatchCount,
                                   SalesPerson = a.SalesPerson,
                                   SalesPersonId = a.SalesPersonId,
                                   ShippingAddress = a.ShippingAddress,
                                   deliveryCharge = a.deliveryCharge,
                                   Status = a.Status,
                                   comments = a.comments,
                                   ReasonCancle = a.ReasonCancle,
                                   orderDetailsExport = (from od in a.orderDetails
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
                                                             SGSTTaxPercentage = od.SGSTTaxPercentage,
                                                             CGSTTaxPercentage = od.CGSTTaxPercentage,
                                                             TaxAmmount = od.TaxAmmount,
                                                             SGSTTaxAmmount = od.SGSTTaxAmmount,
                                                             CGSTTaxAmmount = od.CGSTTaxAmmount,
                                                             TotalAmt = od.TotalAmt,
                                                             CategoryName = cat.CategoryName,
                                                             BrandName = sbcat.SubsubcategoryName
                                                         }).ToList()     /*a.orderDetails,*/
                               }).OrderByDescending(x => x.OrderId).ToList();
                }
                else if (Skcode != null)
                {
                    newdata = (from a in db.DbOrderMaster
                               where a.Skcode.Contains(Skcode)
                               join i in db.Customers on a.CustomerId equals i.CustomerId
                               join clstr in db.Clusters on i.ClusterId equals clstr.ClusterId
                               select new OrderMasterDTO
                               {
                                   Skcode = i.Skcode,
                                   ShopName = i.ShopName,
                                   CustomerName = a.CustomerName,
                                   OrderBy = a.OrderTakenSalesPerson,
                                   CustomerId = i.CustomerId,
                                   Customerphonenum = a.Customerphonenum,
                                   Warehouseid = a.Warehouseid,
                                   WarehouseName = a.WarehouseName,
                                   ClusterName = clstr.ClusterName,
                                   ClusterId = clstr.ClusterId,
                                   OrderId = a.OrderId,
                                   CreatedDate = a.CreatedDate,
                                   BillingAddress = a.BillingAddress,
                                   CityId = a.CityId,
                                   CompanyId = a.CompanyId,
                                   Deliverydate = a.Deliverydate,
                                   DiscountAmount = a.DiscountAmount,
                                   DivisionId = a.DivisionId,
                                   GrossAmount = a.GrossAmount,
                                   invoice_no = a.invoice_no,
                                   ReDispatchCount = a.ReDispatchCount,
                                   SalesPerson = a.SalesPerson,
                                   SalesPersonId = a.SalesPersonId,
                                   ShippingAddress = a.ShippingAddress,
                                   deliveryCharge = a.deliveryCharge,
                                   Status = a.Status,
                                   comments = a.comments,
                                   ReasonCancle = a.ReasonCancle,
                                   orderDetailsExport = (from od in a.orderDetails
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
                                                             SGSTTaxPercentage = od.SGSTTaxPercentage,
                                                             CGSTTaxPercentage = od.CGSTTaxPercentage,
                                                             TaxAmmount = od.TaxAmmount,
                                                             SGSTTaxAmmount = od.SGSTTaxAmmount,
                                                             CGSTTaxAmmount = od.CGSTTaxAmmount,
                                                             TotalAmt = od.TotalAmt,
                                                             CategoryName = cat.CategoryName,
                                                             BrandName = sbcat.SubsubcategoryName
                                                         }).ToList()     /*a.orderDetails,*/
                               }).OrderByDescending(x => x.OrderId).ToList();
                }
                else if (ShopName != null)
                {
                    newdata = (from a in db.DbOrderMaster
                               where a.ShopName.Contains(ShopName)
                               join i in db.Customers on a.CustomerId equals i.CustomerId
                               join clstr in db.Clusters on i.ClusterId equals clstr.ClusterId
                               select new OrderMasterDTO
                               {
                                   Skcode = i.Skcode,
                                   ShopName = i.ShopName,
                                   CustomerName = a.CustomerName,
                                   OrderBy = a.OrderTakenSalesPerson,
                                   CustomerId = i.CustomerId,
                                   Customerphonenum = a.Customerphonenum,
                                   Warehouseid = a.Warehouseid,
                                   WarehouseName = a.WarehouseName,
                                   ClusterName = clstr.ClusterName,
                                   ClusterId = clstr.ClusterId,
                                   OrderId = a.OrderId,
                                   CreatedDate = a.CreatedDate,
                                   BillingAddress = a.BillingAddress,
                                   CityId = a.CityId,
                                   CompanyId = a.CompanyId,
                                   Deliverydate = a.Deliverydate,
                                   DiscountAmount = a.DiscountAmount,
                                   DivisionId = a.DivisionId,
                                   GrossAmount = a.GrossAmount,
                                   invoice_no = a.invoice_no,
                                   ReDispatchCount = a.ReDispatchCount,
                                   SalesPerson = a.SalesPerson,
                                   SalesPersonId = a.SalesPersonId,
                                   ShippingAddress = a.ShippingAddress,
                                   deliveryCharge = a.deliveryCharge,
                                   Status = a.Status,
                                   comments = a.comments,
                                   ReasonCancle = a.ReasonCancle,
                                   orderDetailsExport = (from od in a.orderDetails
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
                                                             SGSTTaxPercentage = od.SGSTTaxPercentage,
                                                             CGSTTaxPercentage = od.CGSTTaxPercentage,
                                                             TaxAmmount = od.TaxAmmount,
                                                             SGSTTaxAmmount = od.SGSTTaxAmmount,
                                                             CGSTTaxAmmount = od.CGSTTaxAmmount,
                                                             TotalAmt = od.TotalAmt,
                                                             CategoryName = cat.CategoryName,
                                                             BrandName = sbcat.SubsubcategoryName
                                                         }).ToList()     /*a.orderDetails,*/
                               }).OrderByDescending(x => x.OrderId).ToList();
                }
                else if (OrderId != 0)
                {
                    newdata = (from a in db.DbOrderMaster
                               where a.OrderId.Equals(OrderId)
                               join i in db.Customers on a.CustomerId equals i.CustomerId
                               join clstr in db.Clusters on i.ClusterId equals clstr.ClusterId
                               select new OrderMasterDTO
                               {
                                   Skcode = i.Skcode,
                                   ShopName = i.ShopName,
                                   CustomerName = a.CustomerName,
                                   OrderBy = a.OrderTakenSalesPerson,
                                   CustomerId = i.CustomerId,
                                   Customerphonenum = a.Customerphonenum,
                                   Warehouseid = a.Warehouseid,
                                   WarehouseName = a.WarehouseName,
                                   ClusterName = clstr.ClusterName,
                                   ClusterId = clstr.ClusterId,
                                   OrderId = a.OrderId,
                                   CreatedDate = a.CreatedDate,
                                   BillingAddress = a.BillingAddress,
                                   CityId = a.CityId,
                                   CompanyId = a.CompanyId,
                                   Deliverydate = a.Deliverydate,
                                   DiscountAmount = a.DiscountAmount,
                                   DivisionId = a.DivisionId,
                                   GrossAmount = a.GrossAmount,
                                   invoice_no = a.invoice_no,
                                   ReDispatchCount = a.ReDispatchCount,
                                   SalesPerson = a.SalesPerson,
                                   SalesPersonId = a.SalesPersonId,
                                   ShippingAddress = a.ShippingAddress,
                                   deliveryCharge = a.deliveryCharge,
                                   Status = a.Status,
                                   comments = a.comments,
                                   ReasonCancle = a.ReasonCancle,
                                   orderDetailsExport = (from od in a.orderDetails
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
                                                             SGSTTaxPercentage = od.SGSTTaxPercentage,
                                                             CGSTTaxPercentage = od.CGSTTaxPercentage,
                                                             TaxAmmount = od.TaxAmmount,
                                                             SGSTTaxAmmount = od.SGSTTaxAmmount,
                                                             CGSTTaxAmmount = od.CGSTTaxAmmount,
                                                             TotalAmt = od.TotalAmt,
                                                             CategoryName = cat.CategoryName,
                                                             BrandName = sbcat.SubsubcategoryName
                                                         }).ToList()     /*a.orderDetails,*/
                               }).OrderByDescending(x => x.OrderId).ToList();
                }
                else if (status != null)
                {
                    newdata = (from a in db.DbOrderMaster
                               where a.Status.Equals(status)
                               join i in db.Customers on a.CustomerId equals i.CustomerId
                               join clstr in db.Clusters on i.ClusterId equals clstr.ClusterId
                               select new OrderMasterDTO
                               {
                                   Skcode = i.Skcode,
                                   ShopName = i.ShopName,
                                   CustomerName = a.CustomerName,
                                   OrderBy = a.OrderTakenSalesPerson,
                                   CustomerId = i.CustomerId,
                                   Customerphonenum = a.Customerphonenum,
                                   Warehouseid = a.Warehouseid,
                                   WarehouseName = a.WarehouseName,
                                   ClusterName = clstr.ClusterName,
                                   ClusterId = clstr.ClusterId,
                                   OrderId = a.OrderId,
                                   CreatedDate = a.CreatedDate,
                                   BillingAddress = a.BillingAddress,
                                   CityId = a.CityId,
                                   CompanyId = a.CompanyId,
                                   Deliverydate = a.Deliverydate,
                                   DiscountAmount = a.DiscountAmount,
                                   DivisionId = a.DivisionId,
                                   GrossAmount = a.GrossAmount,
                                   invoice_no = a.invoice_no,
                                   ReDispatchCount = a.ReDispatchCount,
                                   SalesPerson = a.SalesPerson,
                                   SalesPersonId = a.SalesPersonId,
                                   ShippingAddress = a.ShippingAddress,
                                   deliveryCharge = a.deliveryCharge,
                                   Status = a.Status,
                                   comments = a.comments,
                                   ReasonCancle = a.ReasonCancle,
                                   orderDetailsExport = (from od in a.orderDetails
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
                                                             SGSTTaxPercentage = od.SGSTTaxPercentage,
                                                             CGSTTaxPercentage = od.CGSTTaxPercentage,
                                                             TaxAmmount = od.TaxAmmount,
                                                             SGSTTaxAmmount = od.SGSTTaxAmmount,
                                                             CGSTTaxAmmount = od.CGSTTaxAmmount,
                                                             TotalAmt = od.TotalAmt,
                                                             CategoryName = cat.CategoryName,
                                                             BrandName = sbcat.SubsubcategoryName
                                                         }).ToList()     /*a.orderDetails,*/
                               }).OrderByDescending(x => x.OrderId).ToList();
                }
                else
                {
                    newdata = (from a in db.DbOrderMaster
                               where (a.Deleted == false && a.CreatedDate >= start && a.CreatedDate <= end)
                               join i in db.Customers on a.CustomerId equals i.CustomerId 
                               join clstr in db.Clusters on i.ClusterId equals clstr.ClusterId 
                               select new OrderMasterDTO
                               {
                                   Skcode = i.Skcode,
                                   ShopName = i.ShopName,
                                   CustomerName = a.CustomerName,
                                   OrderBy = a.OrderTakenSalesPerson,
                                   CustomerId = i.CustomerId,
                                   Customerphonenum = a.Customerphonenum,
                                   Warehouseid = a.Warehouseid,
                                   WarehouseName = a.WarehouseName,
                                   ClusterName = clstr.ClusterName,
                                   ClusterId = clstr.ClusterId,
                                   OrderId = a.OrderId,
                                   CreatedDate = a.CreatedDate,
                                   BillingAddress = a.BillingAddress,
                                   CityId = a.CityId,
                                   CompanyId = a.CompanyId,
                                   Deliverydate = a.Deliverydate,
                                   DiscountAmount = a.DiscountAmount,
                                   DivisionId = a.DivisionId,
                                   GrossAmount = a.GrossAmount,
                                   invoice_no = a.invoice_no,
                                   ReDispatchCount = a.ReDispatchCount,
                                   SalesPerson = a.SalesPerson,
                                   SalesPersonId = a.SalesPersonId,
                                   ShippingAddress = a.ShippingAddress,
                                   deliveryCharge = a.deliveryCharge,
                                   Status = a.Status,
                                   comments = a.comments,
                                   ReasonCancle = a.ReasonCancle,
                                   orderDetailsExport = (from od in a.orderDetails
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
                                                             SGSTTaxPercentage = od.SGSTTaxPercentage,
                                                             CGSTTaxPercentage = od.CGSTTaxPercentage,
                                                             TaxAmmount = od.TaxAmmount,
                                                             SGSTTaxAmmount = od.SGSTTaxAmmount,
                                                             CGSTTaxAmmount = od.CGSTTaxAmmount,
                                                             TotalAmt = od.TotalAmt,
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

        // from ordispatchmaster
        [Route("all")]
        [HttpGet]
        public HttpResponseMessage getExportsAll(string type, DateTime? start, DateTime? end, int OrderId, string Skcode, string ShopName, string Mobile, string status) //get search orders for delivery
        {
            try
            {
                AuthContext db = new AuthContext();
                List<OrderMasterDTO> newdata = new List<OrderMasterDTO>();
                if ((Mobile != null) && start != null)
                {
                    newdata = (from a in db.OrderDispatchedMasters
                               where (a.Deleted == false && a.OrderDate >= start && a.OrderDate <= end
                               && (a.Customerphonenum.Contains(Mobile)))
                               join i in db.Customers on a.CustomerId equals i.CustomerId
                               join j in db.DbOrderMaster on a.OrderId equals j.OrderId
                               join clstr in db.Clusters on i.ClusterId equals clstr.ClusterId
                               select new OrderMasterDTO
                               {
                                   Skcode = i.Skcode,
                                   ShopName = i.ShopName,
                                   CustomerName = a.CustomerName,
                                   CustomerId = i.CustomerId,
                                   OrderBy = a.OrderTakenSalesPerson,
                                   Customerphonenum = a.Customerphonenum,
                                   Warehouseid = a.Warehouseid,
                                   WarehouseName = a.WarehouseName,
                                   ClusterName = clstr.ClusterName,
                                   ClusterId = clstr.ClusterId,
                                   OrderId = a.OrderId,
                                   CreatedDate = a.OrderedDate,
                                   BillingAddress = a.BillingAddress,
                                   CityId = a.CityId,
                                   CompanyId = a.CompanyId,
                                   Deliverydate = a.Deliverydate,
                                   DiscountAmount = a.DiscountAmount,
                                   DivisionId = a.DivisionId,
                                   GrossAmount = a.GrossAmount,
                                   invoice_no = a.invoice_no,
                                   ReDispatchCount = a.ReDispatchCount,
                                   SalesPerson = a.SalesPerson,
                                   SalesPersonId = a.SalesPersonId,
                                   ShippingAddress = a.ShippingAddress,
                                   deliveryCharge = a.deliveryCharge,
                                   Status = a.Status,
                                   comments = a.comments,
                                   ReasonCancle = j.ReasonCancle,
                                   orderDetailsExport = (from od in a.orderDetails
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
                                                             BrandName = sbcat.SubsubcategoryName
                                                         }).ToList()     /*a.orderDetails,*/
                               }).OrderByDescending(x => x.OrderId).ToList();
                }
                else if ((Skcode != null) && start != null)
                {
                    newdata = (from a in db.OrderDispatchedMasters
                               where (a.Deleted == false && a.OrderDate >= start && a.OrderDate <= end
                               && a.Skcode.Contains(Skcode))
                               join i in db.Customers on a.CustomerId equals i.CustomerId
                               join j in db.DbOrderMaster on a.OrderId equals j.OrderId
                               join clstr in db.Clusters on i.ClusterId equals clstr.ClusterId
                               select new OrderMasterDTO
                               {
                                   Skcode = i.Skcode,
                                   ShopName = i.ShopName,
                                   CustomerName = a.CustomerName,
                                   OrderBy = a.OrderTakenSalesPerson,
                                   CustomerId = i.CustomerId,
                                   Customerphonenum = a.Customerphonenum,
                                   Warehouseid = a.Warehouseid,
                                   WarehouseName = a.WarehouseName,
                                   ClusterName = clstr.ClusterName,
                                   ClusterId = clstr.ClusterId,
                                   OrderId = a.OrderId,
                                   CreatedDate = a.OrderedDate,
                                   BillingAddress = a.BillingAddress,
                                   CityId = a.CityId,
                                   CompanyId = a.CompanyId,
                                   Deliverydate = a.Deliverydate,
                                   DiscountAmount = a.DiscountAmount,
                                   DivisionId = a.DivisionId,
                                   GrossAmount = a.GrossAmount,
                                   invoice_no = a.invoice_no,
                                   ReDispatchCount = a.ReDispatchCount,
                                   SalesPerson = a.SalesPerson,
                                   SalesPersonId = a.SalesPersonId,
                                   ShippingAddress = a.ShippingAddress,
                                   deliveryCharge = a.deliveryCharge,
                                   Status = a.Status,
                                   comments = a.comments,
                                   ReasonCancle = j.ReasonCancle,
                                   orderDetailsExport = (from od in a.orderDetails
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
                                                             BrandName = sbcat.SubsubcategoryName
                                                         }).ToList()     /*a.orderDetails,*/
                               }).OrderByDescending(x => x.OrderId).ToList();
                }
                else if ((ShopName != null) && start != null)
                {
                    newdata = (from a in db.OrderDispatchedMasters
                               where (a.Deleted == false && a.OrderDate >= start && a.OrderDate <= end
                               && a.ShopName.Contains(ShopName))
                               join i in db.Customers on a.CustomerId equals i.CustomerId
                               join j in db.DbOrderMaster on a.OrderId equals j.OrderId
                               join clstr in db.Clusters on i.ClusterId equals clstr.ClusterId
                               select new OrderMasterDTO
                               {
                                   Skcode = i.Skcode,
                                   ShopName = i.ShopName,
                                   CustomerName = a.CustomerName,
                                   OrderBy = a.OrderTakenSalesPerson,
                                   CustomerId = i.CustomerId,
                                   Customerphonenum = a.Customerphonenum,
                                   Warehouseid = a.Warehouseid,
                                   WarehouseName = a.WarehouseName,
                                   ClusterName = clstr.ClusterName,
                                   ClusterId = clstr.ClusterId,
                                   OrderId = a.OrderId,
                                   CreatedDate = a.OrderedDate,
                                   BillingAddress = a.BillingAddress,
                                   CityId = a.CityId,
                                   CompanyId = a.CompanyId,
                                   Deliverydate = a.Deliverydate,
                                   DiscountAmount = a.DiscountAmount,
                                   DivisionId = a.DivisionId,
                                   GrossAmount = a.GrossAmount,
                                   invoice_no = a.invoice_no,
                                   ReDispatchCount = a.ReDispatchCount,
                                   SalesPerson = a.SalesPerson,
                                   SalesPersonId = a.SalesPersonId,
                                   ShippingAddress = a.ShippingAddress,
                                   deliveryCharge = a.deliveryCharge,
                                   Status = a.Status,
                                   comments = a.comments,
                                   ReasonCancle = j.ReasonCancle,
                                   orderDetailsExport = (from od in a.orderDetails
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
                                                             BrandName = sbcat.SubsubcategoryName
                                                         }).ToList()     /*a.orderDetails,*/
                               }).OrderByDescending(x => x.OrderId).ToList();
                }
                else if ((OrderId != 0) && start != null)
                {
                    newdata = (from a in db.OrderDispatchedMasters
                               where (a.Deleted == false && a.OrderDate >= start && a.OrderDate <= end
                               && a.OrderId.Equals(OrderId))
                               join i in db.Customers on a.CustomerId equals i.CustomerId
                               join j in db.DbOrderMaster on a.OrderId equals j.OrderId
                               join clstr in db.Clusters on i.ClusterId equals clstr.ClusterId
                               select new OrderMasterDTO
                               {
                                   Skcode = i.Skcode,
                                   ShopName = i.ShopName,
                                   CustomerName = a.CustomerName,
                                   OrderBy = a.OrderTakenSalesPerson,
                                   CustomerId = i.CustomerId,
                                   Customerphonenum = a.Customerphonenum,
                                   Warehouseid = a.Warehouseid,
                                   WarehouseName = a.WarehouseName,
                                   ClusterName = clstr.ClusterName,
                                   ClusterId = clstr.ClusterId,
                                   OrderId = a.OrderId,
                                   CreatedDate = a.OrderedDate,
                                   BillingAddress = a.BillingAddress,
                                   CityId = a.CityId,
                                   CompanyId = a.CompanyId,
                                   Deliverydate = a.Deliverydate,
                                   DiscountAmount = a.DiscountAmount,
                                   DivisionId = a.DivisionId,
                                   GrossAmount = a.GrossAmount,
                                   invoice_no = a.invoice_no,
                                   ReDispatchCount = a.ReDispatchCount,
                                   SalesPerson = a.SalesPerson,
                                   SalesPersonId = a.SalesPersonId,
                                   ShippingAddress = a.ShippingAddress,
                                   deliveryCharge = a.deliveryCharge,
                                   Status = a.Status,
                                   comments = a.comments,
                                   ReasonCancle = j.ReasonCancle,
                                   orderDetailsExport = (from od in a.orderDetails
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
                                                             BrandName = sbcat.SubsubcategoryName
                                                         }).ToList()     /*a.orderDetails,*/
                               }).OrderByDescending(x => x.OrderId).ToList();
                }
                else if ((status != null) && start != null)
                {
                    newdata = (from a in db.OrderDispatchedMasters
                               where (a.Deleted == false && a.OrderDate >= start && a.OrderDate <= end
                               && a.Status.Equals(status))
                               join i in db.Customers on a.CustomerId equals i.CustomerId
                               join j in db.DbOrderMaster on a.OrderId equals j.OrderId
                               join clstr in db.Clusters on i.ClusterId equals clstr.ClusterId
                               select new OrderMasterDTO
                               {
                                   Skcode = i.Skcode,
                                   ShopName = i.ShopName,
                                   CustomerName = a.CustomerName,
                                   OrderBy = a.OrderTakenSalesPerson,
                                   CustomerId = i.CustomerId,
                                   Customerphonenum = a.Customerphonenum,
                                   Warehouseid = a.Warehouseid,
                                   WarehouseName = a.WarehouseName,
                                   ClusterName = clstr.ClusterName,
                                   ClusterId = clstr.ClusterId,
                                   OrderId = a.OrderId,
                                   CreatedDate = a.OrderedDate,
                                   BillingAddress = a.BillingAddress,
                                   CityId = a.CityId,
                                   CompanyId = a.CompanyId,
                                   Deliverydate = a.Deliverydate,
                                   DiscountAmount = a.DiscountAmount,
                                   DivisionId = a.DivisionId,
                                   GrossAmount = a.GrossAmount,
                                   invoice_no = a.invoice_no,
                                   ReDispatchCount = a.ReDispatchCount,
                                   SalesPerson = a.SalesPerson,
                                   SalesPersonId = a.SalesPersonId,
                                   ShippingAddress = a.ShippingAddress,
                                   deliveryCharge = a.deliveryCharge,
                                   Status = a.Status,
                                   comments = a.comments,
                                   ReasonCancle = j.ReasonCancle,
                                   orderDetailsExport = (from od in a.orderDetails
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
                                                             BrandName = sbcat.SubsubcategoryName
                                                         }).ToList()     /*a.orderDetails,*/
                               }).OrderByDescending(x => x.OrderId).ToList();
                }
                else if (Mobile != null)
                {
                    newdata = (from a in db.OrderDispatchedMasters
                               where a.Customerphonenum.Contains(Mobile)
                               join i in db.Customers on a.CustomerId equals i.CustomerId
                               join j in db.DbOrderMaster on a.OrderId equals j.OrderId
                               join clstr in db.Clusters on i.ClusterId equals clstr.ClusterId
                               select new OrderMasterDTO
                               {
                                   Skcode = i.Skcode,
                                   ShopName = i.ShopName,
                                   CustomerName = a.CustomerName,
                                   OrderBy = a.OrderTakenSalesPerson,
                                   CustomerId = i.CustomerId,
                                   Customerphonenum = a.Customerphonenum,
                                   Warehouseid = a.Warehouseid,
                                   WarehouseName = a.WarehouseName,
                                   ClusterName = clstr.ClusterName,
                                   ClusterId = clstr.ClusterId,
                                   OrderId = a.OrderId,
                                   CreatedDate = a.OrderedDate,
                                   BillingAddress = a.BillingAddress,
                                   CityId = a.CityId,
                                   CompanyId = a.CompanyId,
                                   Deliverydate = a.Deliverydate,
                                   DiscountAmount = a.DiscountAmount,
                                   DivisionId = a.DivisionId,
                                   GrossAmount = a.GrossAmount,
                                   invoice_no = a.invoice_no,
                                   ReDispatchCount = a.ReDispatchCount,
                                   SalesPerson = a.SalesPerson,
                                   SalesPersonId = a.SalesPersonId,
                                   ShippingAddress = a.ShippingAddress,
                                   deliveryCharge = a.deliveryCharge,
                                   Status = a.Status,
                                   comments = a.comments,
                                   ReasonCancle = j.ReasonCancle,
                                   orderDetailsExport = (from od in a.orderDetails
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
                                                             BrandName = sbcat.SubsubcategoryName
                                                         }).ToList()     /*a.orderDetails,*/
                               }).OrderByDescending(x => x.OrderId).ToList();
                }
                else if (Skcode != null)
                {
                    newdata = (from a in db.OrderDispatchedMasters
                               where a.Skcode.Contains(Skcode)
                               join i in db.Customers on a.CustomerId equals i.CustomerId
                               join j in db.DbOrderMaster on a.OrderId equals j.OrderId
                               join clstr in db.Clusters on i.ClusterId equals clstr.ClusterId
                               select new OrderMasterDTO
                               {
                                   Skcode = i.Skcode,
                                   ShopName = i.ShopName,
                                   CustomerName = a.CustomerName,
                                   OrderBy = a.OrderTakenSalesPerson,
                                   CustomerId = i.CustomerId,
                                   Customerphonenum = a.Customerphonenum,
                                   Warehouseid = a.Warehouseid,
                                   WarehouseName = a.WarehouseName,
                                   ClusterName = clstr.ClusterName,
                                   ClusterId = clstr.ClusterId,
                                   OrderId = a.OrderId,
                                   CreatedDate = a.OrderedDate,
                                   BillingAddress = a.BillingAddress,
                                   CityId = a.CityId,
                                   CompanyId = a.CompanyId,
                                   Deliverydate = a.Deliverydate,
                                   DiscountAmount = a.DiscountAmount,
                                   DivisionId = a.DivisionId,
                                   GrossAmount = a.GrossAmount,
                                   invoice_no = a.invoice_no,
                                   ReDispatchCount = a.ReDispatchCount,
                                   SalesPerson = a.SalesPerson,
                                   SalesPersonId = a.SalesPersonId,
                                   ShippingAddress = a.ShippingAddress,
                                   deliveryCharge = a.deliveryCharge,
                                   Status = a.Status,
                                   comments = a.comments,
                                   ReasonCancle = j.ReasonCancle,
                                   orderDetailsExport = (from od in a.orderDetails
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
                                                             BrandName = sbcat.SubsubcategoryName
                                                         }).ToList()     /*a.orderDetails,*/
                               }).OrderByDescending(x => x.OrderId).ToList();
                }
                else if (ShopName != null)
                {
                    newdata = (from a in db.OrderDispatchedMasters
                               where a.ShopName.Contains(ShopName)
                               join i in db.Customers on a.CustomerId equals i.CustomerId
                               join j in db.DbOrderMaster on a.OrderId equals j.OrderId
                               join clstr in db.Clusters on i.ClusterId equals clstr.ClusterId
                               select new OrderMasterDTO
                               {
                                   Skcode = i.Skcode,
                                   ShopName = i.ShopName,
                                   CustomerName = a.CustomerName,
                                   OrderBy = a.OrderTakenSalesPerson,
                                   CustomerId = i.CustomerId,
                                   Customerphonenum = a.Customerphonenum,
                                   Warehouseid = a.Warehouseid,
                                   WarehouseName = a.WarehouseName,
                                   ClusterName = clstr.ClusterName,
                                   ClusterId = clstr.ClusterId,
                                   OrderId = a.OrderId,
                                   CreatedDate = a.OrderedDate,
                                   BillingAddress = a.BillingAddress,
                                   CityId = a.CityId,
                                   CompanyId = a.CompanyId,
                                   Deliverydate = a.Deliverydate,
                                   DiscountAmount = a.DiscountAmount,
                                   DivisionId = a.DivisionId,
                                   GrossAmount = a.GrossAmount,
                                   invoice_no = a.invoice_no,
                                   ReDispatchCount = a.ReDispatchCount,
                                   SalesPerson = a.SalesPerson,
                                   SalesPersonId = a.SalesPersonId,
                                   ShippingAddress = a.ShippingAddress,
                                   deliveryCharge = a.deliveryCharge,
                                   Status = a.Status,
                                   comments = a.comments,
                                   ReasonCancle = j.ReasonCancle,
                                   orderDetailsExport = (from od in a.orderDetails
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
                                                             BrandName = sbcat.SubsubcategoryName
                                                         }).ToList()     /*a.orderDetails,*/
                               }).OrderByDescending(x => x.OrderId).ToList();
                }
                else if (OrderId != 0)
                {
                    newdata = (from a in db.OrderDispatchedMasters
                               where a.OrderId.Equals(OrderId)
                               join i in db.Customers on a.CustomerId equals i.CustomerId
                               join j in db.DbOrderMaster on a.OrderId equals j.OrderId
                               join clstr in db.Clusters on i.ClusterId equals clstr.ClusterId
                               select new OrderMasterDTO
                               {
                                   Skcode = i.Skcode,
                                   ShopName = i.ShopName,
                                   CustomerName = a.CustomerName,
                                   OrderBy = a.OrderTakenSalesPerson,
                                   CustomerId = i.CustomerId,
                                   Customerphonenum = a.Customerphonenum,
                                   Warehouseid = a.Warehouseid,
                                   WarehouseName = a.WarehouseName,
                                   ClusterName = clstr.ClusterName,
                                   ClusterId = clstr.ClusterId,
                                   OrderId = a.OrderId,
                                   CreatedDate = a.OrderedDate,
                                   BillingAddress = a.BillingAddress,
                                   CityId = a.CityId,
                                   CompanyId = a.CompanyId,
                                   Deliverydate = a.Deliverydate,
                                   DiscountAmount = a.DiscountAmount,
                                   DivisionId = a.DivisionId,
                                   GrossAmount = a.GrossAmount,
                                   invoice_no = a.invoice_no,
                                   ReDispatchCount = a.ReDispatchCount,
                                   SalesPerson = a.SalesPerson,
                                   SalesPersonId = a.SalesPersonId,
                                   ShippingAddress = a.ShippingAddress,
                                   deliveryCharge = a.deliveryCharge,
                                   Status = a.Status,
                                   comments = a.comments,
                                   ReasonCancle = j.ReasonCancle,
                                   orderDetailsExport = (from od in a.orderDetails
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
                                                             BrandName = sbcat.SubsubcategoryName
                                                         }).ToList()     /*a.orderDetails,*/
                               }).OrderByDescending(x => x.OrderId).ToList();
                }
                else if (status != null)
                {
                    newdata = (from a in db.OrderDispatchedMasters
                               where a.Status.Equals(status)
                               join i in db.Customers on a.CustomerId equals i.CustomerId
                               join j in db.DbOrderMaster on a.OrderId equals j.OrderId
                               join clstr in db.Clusters on i.ClusterId equals clstr.ClusterId
                               select new OrderMasterDTO
                               {
                                   Skcode = i.Skcode,
                                   ShopName = i.ShopName,
                                   CustomerName = a.CustomerName,
                                   OrderBy = a.OrderTakenSalesPerson,
                                   CustomerId = i.CustomerId,
                                   Customerphonenum = a.Customerphonenum,
                                   Warehouseid = a.Warehouseid,
                                   WarehouseName = a.WarehouseName,
                                   ClusterName = clstr.ClusterName,
                                   ClusterId = clstr.ClusterId,
                                   OrderId = a.OrderId,
                                   CreatedDate = a.OrderedDate,
                                   BillingAddress = a.BillingAddress,
                                   CityId = a.CityId,
                                   CompanyId = a.CompanyId,
                                   Deliverydate = a.Deliverydate,
                                   DiscountAmount = a.DiscountAmount,
                                   DivisionId = a.DivisionId,
                                   GrossAmount = a.GrossAmount,
                                   invoice_no = a.invoice_no,
                                   ReDispatchCount = a.ReDispatchCount,
                                   SalesPerson = a.SalesPerson,
                                   SalesPersonId = a.SalesPersonId,
                                   ShippingAddress = a.ShippingAddress,
                                   deliveryCharge = a.deliveryCharge,
                                   Status = a.Status,
                                   comments = a.comments,
                                   ReasonCancle = j.ReasonCancle,
                                   orderDetailsExport = (from od in a.orderDetails
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
                                                             BrandName = sbcat.SubsubcategoryName
                                                         }).ToList()     /*a.orderDetails,*/
                               }).OrderByDescending(x => x.OrderId).ToList();
                }
                else
                {
                    newdata = (from a in db.OrderDispatchedMasters
                               where (a.Deleted == false && a.OrderedDate >= start && a.OrderedDate <= end)
                               join i in db.Customers on a.CustomerId equals i.CustomerId
                               join j in db.DbOrderMaster on a.OrderId equals j.OrderId
                               join clstr in db.Clusters on i.ClusterId equals clstr.ClusterId
                               select new OrderMasterDTO
                               {
                                   Skcode = i.Skcode,
                                   ShopName = i.ShopName,
                                   CustomerName = a.CustomerName,
                                   OrderBy = a.OrderTakenSalesPerson,
                                   CustomerId = i.CustomerId,
                                   Customerphonenum = a.Customerphonenum,
                                   Warehouseid = a.Warehouseid,
                                   WarehouseName = a.WarehouseName,
                                   ClusterName = clstr.ClusterName,
                                   ClusterId = clstr.ClusterId,
                                   OrderId = a.OrderId,
                                   CreatedDate = a.OrderedDate,
                                   BillingAddress = a.BillingAddress,
                                   CityId = a.CityId,
                                   CompanyId = a.CompanyId,
                                   Deliverydate = a.Deliverydate,
                                   DiscountAmount = a.DiscountAmount,
                                   DivisionId = a.DivisionId,
                                   GrossAmount = a.GrossAmount,
                                   invoice_no = a.invoice_no,
                                   ReDispatchCount = a.ReDispatchCount,
                                   SalesPerson = a.SalesPerson,
                                   SalesPersonId = a.SalesPersonId,
                                   ShippingAddress = a.ShippingAddress,
                                   deliveryCharge = a.deliveryCharge,
                                   Status = a.Status,
                                   comments = a.comments,
                                   ReasonCancle = j.ReasonCancle,
                                   orderDetailsExport = (from od in a.orderDetails
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
