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
    [RoutePrefix("api/DeliveryTask")]
    public class DeliveryTaskController : ApiController
    {
        iAuthContext context = new AuthContext();

        [Route("")]
        [HttpGet]
        public HttpResponseMessage getAppOrders(string mob) //get orders for delivery
        {
            try
            {
                var DBoyorders = context.getAcceptedOrders(mob);
                return Request.CreateResponse(HttpStatusCode.OK, DBoyorders);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [Route("")]
        [HttpGet]
        public HttpResponseMessage getOrdersHistory(string mob,DateTime start,DateTime end,int dboyId) //get orders History by Date time
        {
            try
            {
                if (start != null && end != null)
                {
                   var k= end.AddDays(1);
                   var DBoyorders = context.getDBoyOrdersHistory(mob, start, k, dboyId);
                    return Request.CreateResponse(HttpStatusCode.OK, DBoyorders);
                }
                else {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Select Date");
                }
               
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [Route("")]
        [HttpPost]
        public HttpResponseMessage post(OrderDispatchedMaster obj) //Order delivered or canceled
        {
            try
            {
                var DBoyorders = context.orderdeliveredreturn(obj);
                if (DBoyorders == null) {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Not Delivered");
                }
                return Request.CreateResponse(HttpStatusCode.OK, DBoyorders);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }


    }
}
