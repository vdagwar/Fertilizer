using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using AngularJSAuthentication.API;
using AngularJSAuthentication.Model;
using NLog;
using GenricEcommers.Models;

namespace AngularJSAuthentication.API.Controllers
{
    [RoutePrefix("api/OrderMastersAPI")]
    public class OrderMastersAPIController : ApiController
    {        
        AuthContext context = new AuthContext();
        public static Logger logger = LogManager.GetCurrentClassLogger();
        private static TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);

        [ResponseType(typeof(OrderMaster))]
        [Route("")]
        [AcceptVerbs("POST")]
        public dynamic Post(ShoppingCart sc)
        {
            OrderMaster order1 = new OrderMaster();
            try
            {
                //order.CustomerId = 1;
                var ord = context.AddOrderMaster(sc);
                if(ord != null) {
                    sc.DreamPoint = Convert.ToInt32(ord.RewardPoint);
                    return sc;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [ResponseType(typeof(CustomerRegistration))]
        [Route("")]
        [AcceptVerbs("PUT")]
        public CustomerRegistration Put(CustomerRegistration cust)
        {
            try
            {
                //var context = new AuthContext(new AuthContext());
                return context.PutCustomerRegistration(cust);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [ResponseType(typeof(DreamOrder))]
        [Route("dreamitem")]
        [AcceptVerbs("POST")]
        public HttpResponseMessage Post(DreamOrder sc)
        {
            try
            {
                Customer cust = context.Customers.Where(x => x.Skcode == sc.Skcode && x.CustomerId == sc.CustomerId).SingleOrDefault();
                if (cust != null)
                {
                    Warehouse w = context.Warehouses.Where(wr => wr.Warehouseid == cust.Warehouseid).FirstOrDefault();
                    if (w != null)
                    {
                        sc.Warehouseid = w.Warehouseid;
                        sc.WarehouseName = w.WarehouseName;
                        sc.CityId = w.Cityid;
                    }
                    sc.ShopName = cust.ShopName;
                    sc.Status = "Pending";
                    sc.CustomerMobile = cust.Mobile;
                    sc.ShippingAddress = cust.ShippingAddress;
                    sc.SalesPersonId = cust.ExecutiveId;
                    sc.CompanyId = cust.CompanyId;                    
                }
                sc.CreatedDate = indianTime;
                sc.UpdatedDate = indianTime;
                sc.Deliverydate = indianTime.AddDays(2);
                sc.Deleted = false;
                sc.ReDispatchCount = 0;

                foreach (var a in sc.DreamItemDetails)
                {
                    if(a.ItemId > 0)
                    {
                        RewardItems it = context.RewardItemsDb.Where(x => x.rItemId == a.ItemId).SingleOrDefault();
                        if (cust != null)
                        {
                            a.ShopName = cust.ShopName;
                            a.Skcode = cust.Skcode;
                            a.ItemName = it.rItem;
                            a.Discription = it.Description;
                            a.Status = "Pending";
                            a.CreatedDate = indianTime;
                            a.UpdatedDate = indianTime;
                            a.Deleted = false;
                        }                        
                    }
                }
                context.DreamOrderDb.Add(sc);
                int id = context.SaveChanges();

                Wallet wlt = context.WalletDb.Where(c => c.CustomerId == cust.CustomerId).SingleOrDefault();
                if (wlt != null)
                {
                    if (sc.WalletPoint > 0)
                    {
                        wlt.TotalAmount -= sc.WalletPoint;
                        wlt.TransactionDate = indianTime;

                        context.WalletDb.Attach(wlt);
                        context.Entry(wlt).State = EntityState.Modified;
                        context.SaveChanges();
                        var rpoint = context.RewardPointDb.Where(c => c.CustomerId == cust.CustomerId).SingleOrDefault();
                        if (rpoint != null)
                        {                            
                            rpoint.UsedPoint += sc.WalletPoint;
                            rpoint.UpdatedDate = indianTime;
                            context.RewardPointDb.Attach(rpoint);
                            context.Entry(rpoint).State = EntityState.Modified;
                            context.SaveChanges();
                        }
                    }
                }
                return Request.CreateResponse(HttpStatusCode.OK,sc);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest,ex.Message);
            }
        }

        [Route("dreamitem")]
        [AcceptVerbs("PUT")]
        public HttpResponseMessage Put(DreamOrder sc)
        {
            try
            {
                var od = context.DreamOrderDb.Where(x => x.Order_Id == sc.Order_Id).SingleOrDefault();
                if (od != null) {
                    od.Status = sc.Status;
                    if (sc.Status == "Dispatched")
                    {
                        od.DboyName = sc.DboyName;
                        od.DboyMobileNo = sc.DboyMobileNo;
                    }
                    od.UpdatedDate = indianTime;

                    context.DreamOrderDb.Attach(od);
                    context.Entry(od).State = EntityState.Modified;
                    context.SaveChanges();
                }               
                
                return Request.CreateResponse(HttpStatusCode.OK, od);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
        [Route("cancel")]
        [AcceptVerbs("PUT")]
        public HttpResponseMessage Put(string cancel, int id)
        {
            try
            {
                var od = context.DreamOrderDb.Where(x => x.Order_Id == id).SingleOrDefault();
                if (od != null)
                {
                    od.Status = "Canceled";
                    od.UpdatedDate = indianTime;

                    context.DreamOrderDb.Attach(od);
                    context.Entry(od).State = EntityState.Modified;
                    context.SaveChanges();
                    Wallet wlt = context.WalletDb.Where(c => c.CustomerId == od.CustomerId).SingleOrDefault();
                    if (wlt != null)
                    {
                        if (od.WalletPoint > 0)
                        {
                            wlt.TotalAmount += od.WalletPoint;
                            wlt.TransactionDate = indianTime;

                            context.WalletDb.Attach(wlt);
                            context.Entry(wlt).State = EntityState.Modified;
                            context.SaveChanges();
                            var rpoint = context.RewardPointDb.Where(c => c.CustomerId == od.CustomerId).SingleOrDefault();
                            if (rpoint != null)
                            {
                                rpoint.UsedPoint -= od.WalletPoint;
                                rpoint.UpdatedDate = indianTime;
                                context.RewardPointDb.Attach(rpoint);
                                context.Entry(rpoint).State = EntityState.Modified;
                                context.SaveChanges();
                            }
                        }
                    }
                }

                return Request.CreateResponse(HttpStatusCode.OK, od);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
        [Route("dreamitem")]
        [AcceptVerbs("GET")]
        public HttpResponseMessage get(int list, int page)
        {
            try
            {                
                var sc = context.DreamOrderDb.Where(x => x.Deleted == false).OrderByDescending(x => x.Order_Id).Skip((page - 1) * list).Take(list).ToList();
                PaggingData obj = new PaggingData();
                obj.total_count = context.DreamOrderDb.Count();
                obj.ordermaster = sc;
                return Request.CreateResponse(HttpStatusCode.OK, obj);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}