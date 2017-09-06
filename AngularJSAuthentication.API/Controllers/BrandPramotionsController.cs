using AngularJSAuthentication.Model;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace AngularJSAuthentication.API.Controllers
{
    [RoutePrefix("api/BrandPramotions")]
    public class BrandPramotionsController : ApiController
    {
        iAuthContext context = new AuthContext();
        private static Logger logger = LogManager.GetCurrentClassLogger();


        [Route("")]
        public List<SubCategory> Getbywarehouse(int warehouseid)
        {
            //if (recordtype == "city")
            // {
            logger.Info("start Category: ");
            List<SubCategory> ass = new List<SubCategory>();
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
                ass = context.PramotionalBrand(warehouseid).ToList();
                logger.Info("End  WarehouseCategory: ");
                return ass;
            }
            catch (Exception ex)
            {
                logger.Error("Error in WarehouseCategory " + ex.Message);
                logger.Info("End  WarehouseCategory: ");
                return null;
            }
            // }
        }
    }
}
