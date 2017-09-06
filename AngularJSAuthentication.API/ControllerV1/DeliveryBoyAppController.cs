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
using System.Runtime.Caching;

namespace AngularJSAuthentication.API.Controllers
{
    [RoutePrefix("api/DBSignup")]
    public class DeliveryBoyAppController : ApiController
    {
        public static Logger logger = LogManager.GetCurrentClassLogger();

        AuthContext context = new AuthContext();
        
       // [ResponseType(typeof(CustomerRegistration))]
        [Route("")]
        [HttpPost]
        public HttpResponseMessage Post(People customer)
        {

            People newcustomer = new People();
            People cust = context.Peoples.Where(x => x.Deleted == false).Where(x => x.Mobile == customer.Mobile).SingleOrDefault();
            if (cust == null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, "Not a Registered Delivery Boy");
            }
            else if (cust.Password != customer.Password)
            {
                return Request.CreateResponse(HttpStatusCode.OK, "Wrong Password");
            }
            else if (cust.Password == customer.Password && cust.Mobile == customer.Mobile)
            {
                return Request.CreateResponse(HttpStatusCode.OK, cust);
            }
            else {
                return Request.CreateResponse(HttpStatusCode.OK, "");
           }

        }

        
    }


}