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
    [RoutePrefix("api/SalePersonRetailer")]
    public class SalePersonRetailerController : ApiController
    {
        iAuthContext context = new AuthContext();
        private static Logger logger = LogManager.GetCurrentClassLogger();
        

        //[Authorize]
        [Route("")]
        [HttpGet]
        public Customer Get(int ExecutiveId, string srch)
        {
            logger.Info("start Customer: ");
            Customer ass = new Customer();
            try
            {
                ass= context.AllSalePersonRetailer(srch,ExecutiveId);
                return ass;
            }
            catch (Exception ex)
            {
                logger.Error("Error in Customer " + ex.Message);
                logger.Info("End  Customer: ");
                return null;
            }
        }
        
    }
}



