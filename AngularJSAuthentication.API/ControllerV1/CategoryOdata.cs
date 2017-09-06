using AngularJSAuthentication.API;
using AngularJSAuthentication.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.OData;

namespace AngularJSAuthentication.API.ControllerV1
{
    [RoutePrefix("api/Orderfix")]
    public class CategoryOdataController : ODataController
    {
        AuthContext authContext = new AuthContext();

        [Route("")]
        public string Get()
        {
            //var i = 3460;
            //for  (var j=i;j<= 4265;j++)
            //{
            //   var cstk = authContext.DbCurrentStock.Where(x => x.StockId == j).FirstOrDefault();
            //    if (cstk != null)
            //    {
            //        authContext.DbCurrentStock.Remove(cstk);
            //        authContext.SaveChanges();
            //    }
            //}
            //var Olist = authContext.DbOrderDetails.Where(x => x.Status == "Pending" || x.Status == "Process").ToList();
            //foreach (var o in Olist)
            //{
            //    if (o.OrderId == 455) { }
            //    else
            //    {
            //        OrderDispatchedMaster om = new OrderDispatchedMaster();
            //        om.active = o.active;
            //        om.BillingAddress = o.BillingAddress;
            //        om.CashAmount = 0;
            //        om.CheckAmount = 0;
            //        om.CheckNo = "";
            //        om.CityId = o.CityId;
            //        om.ClusterId = o.ClusterId;
            //        om.ClusterName = o.ClusterName;
            //        om.comments = "";
            //        om.CompanyId = o.CompanyId;
            //        om.CreatedDate = indianTime;
            //        om.CustomerId = o.CustomerId;
            //        om.CustomerName = o.CustomerName;
            //        om.Customerphonenum = o.Customerphonenum;
            //        om.DboyMobileNo = "9893042879"; ////////////////////////
            //        om.DboyName = "Rajesh Rai";
            //        om.deliveryCharge = 0;
            //        om.Deleted = false;
            //        om.Deliverydate = o.CreatedDate.AddDays(2);
            //        om.DiscountAmount = o.DiscountAmount;
            //        om.DivisionId = o.DivisionId;
            //        om.ElectronicAmount = 0;
            //        om.ElectronicPaymentNo = "";
            //        om.GrossAmount = o.GrossAmount;
            //        om.invoice_no = o.invoice_no;
            //        om.OrderId = o.OrderId;
            //        om.PaymentAmount = 0;
            //        om.RecivedAmount = 0;
            //        om.ReDispatchCount = 0;
            //        om.SalesPerson = o.SalesPerson;
            //        om.SalesPersonId = o.SalesPersonId;
            //        om.ShippingAddress = o.ShippingAddress;
            //        om.ShopName = o.ShopName;
            //        om.Skcode = o.Skcode;
            //        om.Status = o.Status;
            //        om.TaxAmount = o.TaxAmount;
            //        om.TotalAmount = o.TotalAmount;
            //        om.UpdatedDate = indianTime;
            //        om.Warehouseid = o.Warehouseid;
            //        om.WarehouseName = o.WarehouseName;
            //        List<OrderDispatchedDetails> dets = new List<OrderDispatchedDetails>();
            //        foreach (var d in o.orderDetails)
            //        {
            //            OrderDispatchedDetails dd = new OrderDispatchedDetails();
            //            dd.AmtWithoutAfterTaxDisc = d.AmtWithoutAfterTaxDisc;
            //            dd.AmtWithoutTaxDisc = d.AmtWithoutTaxDisc;
            //            dd.Barcode = d.Barcode;
            //            dd.CategoryName = d.CategoryName;
            //            dd.City = d.City;
            //            dd.CityId = d.CityId;
            //            dd.CompanyId = d.CompanyId;
            //            dd.CreatedDate = indianTime;
            //            dd.CustomerId = d.CustomerId;
            //            dd.CustomerName = d.CustomerName;
            //            dd.Deleted = d.Deleted;
            //            dd.DiscountAmmount = d.DiscountAmmount;
            //            dd.DiscountPercentage = d.DiscountPercentage;
            //            dd.isDeleted = false;
            //            dd.itemcode = d.itemcode;
            //            dd.ItemId = d.ItemId;
            //            dd.itemname = d.itemname;
            //            dd.itemNumber = d.itemNumber;
            //            dd.Itempic = d.Itempic;
            //            dd.MinOrderQty = d.MinOrderQty;
            //            dd.MinOrderQtyPrice = d.MinOrderQtyPrice;
            //            dd.Mobile = d.Mobile;
            //            dd.NetAmmount = d.NetAmmount;
            //            dd.NetAmtAfterDis = d.NetAmtAfterDis;
            //            dd.Noqty = d.Noqty;
            //            dd.OrderDate = d.OrderDate;
            //            dd.OrderDetailsId = d.OrderDetailsId;
            //            dd.OrderId = d.OrderId;
            //            dd.price = d.price;
            //            dd.Purchaseprice = d.Purchaseprice;
            //            dd.qty = d.qty;
            //            dd.SizePerUnit = d.SizePerUnit;
            //            dd.Status = d.Status;
            //            dd.TaxAmmount = d.TaxAmmount;
            //            dd.TaxPercentage = d.TaxPercentage;
            //            dd.TotalAmountAfterTaxDisc = d.TotalAmountAfterTaxDisc;
            //            dd.TotalAmt = d.TotalAmt;
            //            dd.UnitId = 0;
            //            dd.Unitname = "";
            //            dd.UnitPrice = d.UnitPrice;
            //            dd.UpdatedDate = indianTime;
            //            dd.Warehouseid = d.Warehouseid;
            //            dd.WarehouseName = d.WarehouseName;
            //            dets.Add(dd);
            //        }
            //        om.orderDetails = dets;
            //        authContext.AddOrderDispatchedMaster(om);
            //    }
            //    try
            //    {
            //        foreach (var od in o.orderDetails)
            //        {
            //            var Odet = authContext.DbOrderDetails.Where(x => x.OrderDetailsId == od.OrderDetailsId && x.Status != "Ready to Dispatch").SingleOrDefault();
            //            if (Odet != null)
            //            {
            //                Odet.Status = o.Status;
            //                Odet.UpdatedDate = indianTime;
            //                authContext.DbOrderDetails.Attach(Odet);
            //                authContext.Entry(Odet).State = EntityState.Modified;
            //                authContext.SaveChanges();
            //            }
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        Console.Write(ex.Message);
            //    }
            //    try
            //    {
            //        var Odet = authContext.DbOrderMaster.Where(x => x.OrderId == o.OrderId && x.Status != "Pending").SingleOrDefault();
            //        if (Odet != null)
            //        {
            //            o.Status = Odet.Status;
            //            o.UpdatedDate = indianTime;
            //            authContext.DbOrderDetails.Attach(o);
            //            authContext.Entry(o).State = EntityState.Modified;
            //            authContext.SaveChanges();
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        Console.Write(ex.Message);
            //    }
            //}
            //var item = authContext.itemMasters.ToList();
            //foreach (var itemmaster in item)
            //{
            //    if (itemmaster.Margin > 0)
            //    {
            //        var rs = authContext.RetailerShareDb.Where(r => r.cityid == itemmaster.Cityid).FirstOrDefault();
            //        if (rs != null)
            //        {
            //            var cf = authContext.RPConversionDb.FirstOrDefault();
            //            try
            //            {
            //                double mv = (itemmaster.PurchasePrice * (itemmaster.Margin / 100) * (rs.share / 100) * cf.point);
            //                var value = Math.Round(mv, MidpointRounding.AwayFromZero);
            //                itemmaster.marginPoint = Convert.ToInt32(value);
            //                authContext.itemMasters.Attach(itemmaster);
            //                authContext.Entry(itemmaster).State = EntityState.Modified;
            //                authContext.SaveChanges();
            //            }
            //            catch (Exception ex)
            //            {
            //            }
            //        }
            //    }
            //}
            return "done";
        }
    }
}
