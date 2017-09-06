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
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.Net.Http.Headers;

namespace AngularJSAuthentication.API.Controllers
{
    [RoutePrefix("api/OrderMaster")]
    public class OrderMasterrController : ApiController
    {
        iAuthContext context = new AuthContext();
        public static Logger logger = LogManager.GetCurrentClassLogger();
        [Route("priority")]
        public HttpResponseMessage Getp(int time)
        {
            logger.Info("Order Automation");
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
                bool status = context.AllOrderMasterspriority(time);
                logger.Info("End OrderMaster: ");
                if (status == true) {
                return Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest);
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error in OrderMaster " + ex.Message);
                logger.Info("End  OrderMaster: ");
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest,ex.Message); ;
            }
        }

        [Route("")]
        public IEnumerable<OrderMaster> Get()
        {
            logger.Info("start OrderMaster: ");
            List<OrderMaster> ass = new List<OrderMaster>();
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
                ass = context.AllOrderMasters.ToList();
                logger.Info("End OrderMaster: ");
                return ass;
            }
            catch (Exception ex)
            {
                logger.Error("Error in OrderMaster " + ex.Message);
                logger.Info("End  OrderMaster: ");
                return null;
            }
        }

        [Route("")]
        public OrderMaster Get(int id)
        {
            logger.Info("start OrderMaster: ");
           
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
                var ass = context.GetOrderMaster(id);
                logger.Info("End OrderMaster: ");
                return ass;
            }
            catch (Exception ex)
            {
                logger.Error("Error in OrderMaster " + ex.Message);
                logger.Info("End  OrderMaster: ");
                return null;
            }
        }
        
        [Route("")]
        public PaggingData Get(int list, int page)
        {
            logger.Info("start OrderMaster: ");
            //  List<OrderMaster> ass = new List<OrderMaster>();
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
                var ass = context.AllOrderMaster(list, page);
                logger.Info("End OrderMaster: ");
                return ass;
            }
            catch (Exception ex)
            {
                logger.Error("Error in OrderMaster " + ex.Message);
                logger.Info("End  OrderMaster: ");
                return null;
            }
        }

        [Route("")]
        public IEnumerable<OrderDispatchedMaster> Get(string OrderStatus, string t)
        {
            logger.Info("start OrderMaster: ");
            List<OrderDispatchedMaster> ass = new List<OrderDispatchedMaster>();
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
                ass = context.AllDispatchedOrderMaster().ToList();
                logger.Info("End OrderMaster: ");
                return ass;
            }
            catch (Exception ex)
            {
                logger.Error("Error in OrderMaster " + ex.Message);
                logger.Info("End  OrderMaster: ");
                return null;
            }
        }
        [ResponseType(typeof(OrderMaster))]
        [Route("")]
        [AcceptVerbs("PUT")]
        public OrderMaster Put(OrderMaster item)
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
                return context.PutOrderMaster(item);
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        [ResponseType(typeof(OrderMaster))]
        [Route("")]
        public IEnumerable<OrderMaster> Get(string Cityid, string Warehouseid, DateTime datefrom, DateTime dateto ,string search,string status,string deliveryboy)
        {
            logger.Info("start OrderMaster: ");
                List<OrderMaster> ass = new List<OrderMaster>();
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
                    ass = context.filteredOrderMaster(Cityid, Warehouseid, datefrom, dateto ,search,status,deliveryboy).ToList();
                    logger.Info("End OrderMaster: ");
                    return ass;
                }
                catch (Exception ex)
                {
                    logger.Error("Error in OrderMaster " + ex.Message);
                    logger.Info("End  OrderMaster: ");
                    return null;
                }
            }

            [ResponseType(typeof(OrderMaster))]
        [Route("")]
        public IEnumerable<OrderMaster> Get(string mobile)
        {
            logger.Info("start OrderMaster: ");
            List<OrderMaster> ass = new List<OrderMaster>();
            try
            {

                ass = context.OrderMasterbymobile(mobile).ToList();
                logger.Info("End OrderMaster: ");
                return ass;
            }
            catch (Exception ex)
            {
                logger.Error("Error in OrderMaster " + ex.Message);
                logger.Info("End  OrderMaster: ");
                return null;
            }
        }
        
        [Authorize]
        [ResponseType(typeof(OrderMaster))]
        [Route("")]
        [AcceptVerbs("POST")]
        //[Route("api/ItemMaster")]
        public OrderMaster add(OrderMaster item)
        {
            logger.Info("start add OrderMaster: ");
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

                item.CompanyId = compid;
                if (item == null)
                {
                    throw new ArgumentNullException("item");
                }

                context.AddOrderMaster(item);
                logger.Info("User ID : {0} , Company Id : {1}", compid, userid);
                logger.Info("End  add OrderMaster: ");
                return item;

            }
            catch (Exception ex)
            {
                logger.Error("Error in AddOrderMaster " + ex.Message);

                return null;
            }
        }
        //[Authorize]
        //[ResponseType(typeof(OrderMaster))]
        //[Route("")]
        //[AcceptVerbs("PUT")]
        //public OrderMaster Put(OrderMaster item)
        //{
        //    try
        //    {
        //        var identity = User.Identity as ClaimsIdentity;
        //        int compid = 0, userid = 0;
        //        foreach (Claim claim in identity.Claims)
        //        {
        //            if (claim.Type == "compid")
        //            {
        //                compid = int.Parse(claim.Value);
        //            }
        //            if (claim.Type == "userid")
        //            {
        //                userid = int.Parse(claim.Value);
        //            }
        //        }
        //        logger.Info("User ID : {0} , Company Id : {1}", compid, userid);
        //        return context.PutOrderMaster(item);
        //    }
        //    catch
        //    {
        //        return null;
        //    }
        //}

        [ResponseType(typeof(OrderMaster))]
        [Route("")]
        [AcceptVerbs("Delete")]
        public void Remove(int id)
        {
            logger.Info("start delete OrderMaster: ");
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
                context.DeleteOrderMaster(id);
                logger.Info("End  delete OrderMaster: ");
            }
            catch (Exception ex)
            {
                logger.Error("Error in delete OrderMaster " + ex.Message);

            }
        }

        [Route("")]
        public IEnumerable<OrderMaster> Get(string Warehouseid, DateTime datefrom, DateTime dateto)
        {
            logger.Info("start OrderMaster: ");
            List<OrderMaster> ass = new List<OrderMaster>();
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
                ass = context.filteredOrderMasters1(Warehouseid,datefrom, dateto).ToList();
                logger.Info("End OrderMaster: ");
                return ass;
            }
            catch (Exception ex)
            {
                logger.Error("Error in OrderMaster " + ex.Message);
                logger.Info("End  OrderMaster: ");
                return null;
            }
        }
    }
}