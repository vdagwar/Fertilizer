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
    [RoutePrefix("api/OrderDispatchedMaster")]
    public class OrderDispatchedMasterController : ApiController
    {
        iAuthContext context = new AuthContext();
        AuthContext db = new AuthContext();

        public static Logger logger = LogManager.GetCurrentClassLogger();
        private static TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);

        [Route("")]
        public List<OrderDispatchedMaster> Get()
        {
            logger.Info("start get all OrderDispatchedMaster: ");
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                int compid = 0, userid = 0;
                List<OrderDispatchedMaster> displist = new List<OrderDispatchedMaster>();
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
                AuthContext db = new AuthContext();
                displist = db.OrderDispatchedMasters.Where(x => x.Status == "Ready to Dispatch").ToList();
                logger.Info("End  UnitMaster: ");
                return displist;
            }
            catch (Exception ex)
            {
                logger.Error("Error in getall OrderDispatchedMaster " + ex.Message);
                logger.Info("End getall OrderDispatchedMaster: ");
                return null;
            }
        }

        [Route("")]
        public OrderDispatchedMaster Get(string id)
        {
            logger.Info("start PurchaseOrderMaster: ");
            OrderDispatchedMaster wh = new OrderDispatchedMaster();

            if (id != null)
            {
                int Id = Convert.ToInt32(id);
                AuthContext db = new AuthContext();
                wh = db.OrderDispatchedMasters.Where(x => x.OrderId == Id).SingleOrDefault();
                return wh;
            }
            return null;
        }

        [Route("")]
        public PaggingData Get(int list, int page, string DBoyNo, DateTime? datefrom, DateTime? dateto, int? OrderId)
        {
            List<OrderDispatchedMaster> displist = new List<OrderDispatchedMaster>();
            logger.Info("start OrderSettle: ");
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
                AuthContext context = new AuthContext();
                logger.Info("User ID : {0} , Company Id : {1}", compid, userid);
                var lst = context.AllDispatchedOrderMasterPaging(list, page, DBoyNo, datefrom, dateto, OrderId);
                logger.Info("End OrderSettle: ");
                return lst;
            }
            catch (Exception ex)
            {
                logger.Error("Error in OrderSettle " + ex.Message);
                logger.Info("End  OrderSettle: ");
                return null;
            }
        }

        [Route("")]
        public List<OrderDispatchedMaster> Get(DateTime datefrom, DateTime dateto, int id)
        {
            List<OrderDispatchedMaster> displist = new List<OrderDispatchedMaster>();
            logger.Info("start OrderSettle: ");
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
                var lst = context.AllFOrderDispatchedDeliveryDetails(datefrom, dateto);
                logger.Info("End OrderSettle: ");
                return lst;
            }
            catch (Exception ex)
            {
                logger.Error("Error in OrderSettle " + ex.Message);
                logger.Info("End  OrderSettle: ");
                return null;
            }
        }

        [Route("")]
        public List<OrderDispatchedMaster> Get(DateTime datefrom, DateTime dateto, string DboyName, int id)
        {
            List<OrderDispatchedMaster> displist = new List<OrderDispatchedMaster>();
            logger.Info("start OrderSettle: ");
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
                var lst = context.AllFOrderDispatchedDeliveryBoyDetails(datefrom, dateto, DboyName);
                logger.Info("End OrderSettle: ");
                return lst;
            }
            catch (Exception ex)
            {
                logger.Error("Error in OrderSettle " + ex.Message);
                logger.Info("End  OrderSettle: ");
                return null;
            }
        }
        [ResponseType(typeof(OrderDispatchedMaster))]
        [Route("")]
        [AcceptVerbs("POST")]
        public OrderDispatchedMaster add(OrderDispatchedMaster item)
        {
            logger.Info("start OrderDispatchedMaster: ");
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
                if (item == null)
                {
                    throw new ArgumentNullException("item");
                }
                logger.Info("User ID : {0} , Company Id : {1}", compid, userid);
                context.AddOrderDispatchedMaster(item);
                logger.Info("End  UnitMaster: ");
                return item;
            }
            catch (Exception ex)
            {
                logger.Error("Error in OrderDispatchedMaster " + ex.Message);
                logger.Info("End  OrderDispatchedMaster: ");
                return null;
            }
        }
        ///////////////////////Taslim code start////////////////////////
       
        //[Route("searchsettle")]
        //[HttpPost]
        //public dynamic Post(DBOYinfo DBI)
        //{

        //    try
        //    {
        //        List<OrderDispatchedMaster> returnlist = new List<OrderDispatchedMaster>();
        //        AuthContext context = new AuthContext();
        //        foreach (var i in DBI.ids)
        //        {
        //            var lst = context.AllDispatchedOrderMasterPaging(100, 1, i.mob, DBI.datefrom, DBI.dateto,OrderId);
        //            if (lst != null)
        //            {
        //                List<OrderDispatchedMaster> os = lst.ordermaster;
        //                returnlist.AddRange(os);
        //            }
        //        }
        //        return returnlist;
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.Error("Error in OrderMaster " + ex.Message);
        //        logger.Info("End  OrderMaster: ");
        //        return null;
        //    }
        //}

        [ResponseType(typeof(OrderDispatchedMaster))]
        [Route("")]
        [AcceptVerbs("PUT")]
        public OrderDispatchedMaster put(int id, string DboyNo)
        {
            var db = new AuthContext();
            logger.Info("start OrderDispatchedMaster: ");
            OrderDispatchedMaster obj = db.OrderDispatchedMasters.Where(x => x.OrderDispatchedMasterId == id).FirstOrDefault();
            People DeliveryOBJ = db.Peoples.Where(x => x.Mobile == DboyNo).FirstOrDefault();

            obj.DboyName = DeliveryOBJ.DisplayName;
            obj.DboyMobileNo = DeliveryOBJ.Mobile;


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
                // item.CompanyId = compid;
                if (obj == null)
                {
                    throw new ArgumentNullException("item");
                }
                logger.Info("User ID : {0} , Company Id : {1}", compid, userid);
                context.UpdateOrderDispatchedMaster(obj);
                logger.Info("End  UnitMaster: ");
                return obj;
            }
            catch (Exception ex)
            {
                logger.Error("Error in OrderDispatchedMaster " + ex.Message);
                logger.Info("End  OrderDispatchedMaster: ");
                return null;
            }
        }

    }
}