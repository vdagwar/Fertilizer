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
    [RoutePrefix("api/DamageOrderDetails")]
    public class DamageOrderDetailsController : ApiController
    {
        iAuthContext context = new AuthContext();
        public static Logger logger = LogManager.GetCurrentClassLogger();
        
        private AuthContext db = new AuthContext();

        // [Authorize]
        [Route("")]
        public IEnumerable<DamageOrderDetails> Get(string recordtype)
        {
            if (recordtype == "details")
            {
                logger.Info("start City: ");
                List<DamageOrderDetails> ass = new List<DamageOrderDetails>();
                try
                {
                    var identity = User.Identity as ClaimsIdentity;
                    int compid = 1, userid = 0;
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
                    ass = context.AllDorddetails(compid).ToList();
                    logger.Info("End  order: ");
                    return ass;
                }
                catch (Exception ex)
                {
                    logger.Error("Error in orderdetails " + ex.Message);
                    logger.Info("End  orderdetails: ");
                    return null;
                }
            }
            return null;
        }
       


        [Authorize]
        [Route("")]
        public IEnumerable<DamageOrderDetails> GetallDorderdetails(string id)
        {
            logger.Info("start : ");
            List<DamageOrderDetails> ass = new List<DamageOrderDetails>();
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
                int idd = Int32.Parse(id);
                ass = context.AllDOrderDetails(idd).ToList();
                logger.Info("End  : ");
                return ass;
            }
            catch (Exception ex)
            {
                logger.Error("Error in OrderDetails " + ex.Message);
                logger.Info("End  OrderDetails: ");
                return null;
            }
        }
        
  

    }
}