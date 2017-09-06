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
    [RoutePrefix("api/damageorder")]
    public class CreateDamageOrderController : ApiController
    {

        AuthContext context = new AuthContext();
        public static Logger logger = LogManager.GetCurrentClassLogger();
        private static TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);

       

        [Route("")]
        [HttpPost]
        public HttpResponseMessage post(DamageOrder Do) //Order
        {
            try
            {
                var data = context.AddDamageOrder(Do);
                if (data == null)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Error Occured");
                }
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }


        [Route("damagecrtOrder")]
        [AcceptVerbs("GET")]
        [HttpGet]
        public HttpResponseMessage get(int id)
        {
            try
            { 
                //var co = context.CreatDamageOrders.Where(x => x.Deleted == false && x.DamageCreatOrderId == id).ToList();
               //CreatDamageOrder obj = new CreatDamageOrder();
              // obj.DamageCreatOrderId = context.CreatDamageOrders.Count();
               // obj.ordermaster = sc;
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}
