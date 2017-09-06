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
    [RoutePrefix("api/OrderDispatchedMasterFinal")]
    public class OrderDispatchedMasterFinalController : ApiController
    {
        iAuthContext context = new AuthContext();
        AuthContext db = new AuthContext();
        public static Logger logger = LogManager.GetCurrentClassLogger();



        [Route("")]
        public FinalOrderDispatchedMaster Get(string id)
        {
            logger.Info("start PurchaseOrderMaster: ");
            FinalOrderDispatchedMaster wh = new FinalOrderDispatchedMaster();

            if (id != null)
            {
                int Id = Convert.ToInt32(id);
                AuthContext db = new AuthContext();
                wh = db.FinalOrderDispatchedMasterDb.Where(x => x.OrderId == Id).SingleOrDefault();

                return wh;

            }
            return null;

        }



        [ResponseType(typeof(FinalOrderDispatchedMaster))]
        [Route("")]
        [AcceptVerbs("POST")]
        public FinalOrderDispatchedMaster add(FinalOrderDispatchedMaster item)
        {
            logger.Info("start FinalOrderDispatchedMaster: ");
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
                if (item == null)
                {
                    throw new ArgumentNullException("item");
                }
                logger.Info("User ID : {0} , Company Id : {1}", compid, userid);
                context.AddFinalOrderDispatchedMaster(item);
                logger.Info("End  UnitMaster: ");
                return item;
            }
            catch (Exception ex)
            {
                logger.Error("Error in Final OrderDispatchedMaster " + ex.Message);
                logger.Info("End  Final OrderDispatchedMaster: ");
                return null;
            }
        }


        //[ResponseType(typeof(OrderDispatchedMaster))]
        //[Route("")]
        //[AcceptVerbs("PUT")]
        //public OrderDispatchedMaster put(int id,string DboyNo)
        //{
        //    var db = new AuthContext();
        //    logger.Info("start OrderDispatchedMaster: ");
        //    OrderDispatchedMaster obj = db.OrderDispatchedMasters.Where(x => x.OrderDispatchedMasterId == id).FirstOrDefault();
        //    People DeliveryOBJ = db.Peoples.Where(x => x.Mobile == DboyNo).FirstOrDefault();

        //    obj.DboyName = DeliveryOBJ.DisplayName;
        //    obj.DboyMobileNo = DeliveryOBJ.Mobile;


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
        //       // item.CompanyId = compid;
        //        if (obj == null)
        //        {
        //            throw new ArgumentNullException("item");
        //        }
        //        logger.Info("User ID : {0} , Company Id : {1}", compid, userid);
        //        context.UpdateOrderDispatchedMaster(obj);
        //        logger.Info("End  UnitMaster: ");
        //        return obj;
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.Error("Error in OrderDispatchedMaster " + ex.Message);
        //        logger.Info("End  OrderDispatchedMaster: ");
        //        return null;
        //    }
        //}

        // For Multi Select 
        [Route("Multisettle")]
        [HttpPost, HttpPut]
        public HttpResponseMessage PostMultiSettle(FinalOrderDispatchedMaster obj)//add issuance
        {
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
                obj.CompanyId = compid;
                if (obj != null)
                {
                    db.DBIsettle(obj.AssignedOrders);

                }



                return Request.CreateResponse(HttpStatusCode.OK, true);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }





    }
}