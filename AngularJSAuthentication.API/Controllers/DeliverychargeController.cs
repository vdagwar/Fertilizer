using AngularJSAuthentication.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web.Http;
using System.Web.Http.Description;
using NLog;
using GenricEcommers.Models;

namespace AngularJSAuthentication.API.Controllers
{
    [RoutePrefix("api/deliverycharge")]
    public class DeliverychargeController : ApiController
    {
        AuthContext context = new AuthContext();
        private static Logger logger = LogManager.GetCurrentClassLogger();
        [AcceptVerbs("GET")]
        //[Authorize]
        [Route("")]
        public IEnumerable<DeliveryCharge> Get()
        {
            logger.Info("start Category: ");
            List<DeliveryCharge> ass = new List<DeliveryCharge>();
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
                ass = context.DeliveryChargeDb.ToList();
                logger.Info("End  Category: ");
                return ass;
            }
            catch (Exception ex)
            {
                logger.Error("Error in Category " + ex.Message);
                logger.Info("End  Category: ");
                return null;
            }
        }
        [Route("")]
        public DeliveryCharge Get(int id)
        {
            logger.Info("start Category: ");
            DeliveryCharge ass = new DeliveryCharge();
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
                ass = context.DeliveryChargeDb.Where(x=>x.warhouse_Id == id).FirstOrDefault();
                logger.Info("End  Category: ");
                return ass;
            }
            catch (Exception ex)
            {
                logger.Error("Error in Category " + ex.Message);
                logger.Info("End  Category: ");
                return null;
            }
        }

        [AcceptVerbs("POST")]
        [Authorize]
        [Route("")]
        public DeliveryCharge add(DeliveryCharge delivery)
        {
            logger.Info("start deliverycharge");
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
                var del = context.AddUpdateDeliveryCharge(delivery);
                return del;
            }
            catch (Exception ex)
            {
                logger.Error("Error in deliverycharge" + ex.Message);
                logger.Info("End  deliverycharge: ");
                return null;
            }
        }
    }
}



