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
    [RoutePrefix("api/DeliveryOrder")]
    public class DeliveryOrderController : ApiController
    {
        AuthContext context = new AuthContext();

        [Route("")]
        [HttpGet]//get Delivery Boys list
        public HttpResponseMessage get()
        {
            try
            {
                var dboys = context.Peoples.Where(x=>x.Department == "Delivery Boy" && x.Deleted == false).ToList();
                
                return Request.CreateResponse(HttpStatusCode.OK, dboys);
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Error in getting Data");
            }
        }
        [Route("")]
        [HttpGet]
        public HttpResponseMessage getbyId(string mob)
        {
            try
            {

                var DBoyorders = context.getdboysOrder(mob);

                

                return Request.CreateResponse(HttpStatusCode.OK, DBoyorders);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [Route("")]
        [HttpGet]
        public HttpResponseMessage getAppOrders(string M, string mob) //get orders for delivery
        {
            try
            {
                if (M == "all")
                {
                    var DBoyorders = context.getallOrderofboy(mob);
                    
                    return Request.CreateResponse(HttpStatusCode.OK, DBoyorders);
                }
                else {
                    var DBoyorders = context.getAcceptedOrders(mob);
                    return Request.CreateResponse(HttpStatusCode.OK, DBoyorders);
                }
                
                
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [Route("")]
        [HttpPost]
        public HttpResponseMessage post(List<OrderDispatchedMaster> obj,string mob) //Order change delivery boy
        {
            try
            {
                var DBoyorders = context.changeDBoy(obj ,mob);
                if (DBoyorders == null) {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Error Occured");
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
