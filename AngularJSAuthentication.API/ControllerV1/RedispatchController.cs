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
    [RoutePrefix("api/Redispatch")]
    public class RedispatchController : ApiController
    {
        AuthContext context = new AuthContext();

        [Route("")]
        [HttpGet]
        public HttpResponseMessage get() //get orders for Redispatch 
        {
            try
            {
                var RDorders = context.OrderDispatchedMasters.Where(x=>x.Status == "Delivery Redispatch").ToList();
                return Request.CreateResponse(HttpStatusCode.OK, RDorders);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
        [Route("")]
        [HttpGet]
        public HttpResponseMessage getbyDboy(string mob) //get orders Redispatch by DBoy mob
        {
            try
            {
               // var RDorders = context.OrderDispatchedMasters.Where(x =>x.Status == "Delivery Redispatch" && x.DboyMobileNo == mob).ToList();
                var RDorders = context.getRedispatchordersbyboy(mob);

                return Request.CreateResponse(HttpStatusCode.OK, RDorders);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }



    }
}
