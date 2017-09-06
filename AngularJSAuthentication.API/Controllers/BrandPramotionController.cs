using AngularJSAuthentication.Model;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using System.Web.Http.Description;

namespace AngularJSAuthentication.API.Controllers
{
    [RoutePrefix("api/BrandPramotion")]
    public class BrandPramotionController : ApiController
    {
        iAuthContext context = new AuthContext();
        private static Logger logger = LogManager.GetCurrentClassLogger();        
        [Route("")]
        public List<SubCategory> Get(int warehouse, int id)
        {
            logger.Info("start subcategorybyWarehouse: ");
            List<SubCategory> ass = new List<SubCategory>();
            try
            {
                ass = context.subcategorybyPramotion(warehouse).ToList();
                logger.Info("End  subcategorybyWarehouse: ");
                return ass;
            }
            catch (Exception ex)
            {
                logger.Error("Error in subcategorybyWarehouse " + ex.Message);
                logger.Info("End  subcategorybyWarehouse: ");
                return null;
            }
        }
        
        [Authorize]
        [Route("")]
        public List<SubCategory> Get(string recordtype,int warehouse)
        {
            if (recordtype == "warehouse")
            {
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
                    ass = context.subcategorybyWarehouse(warehouse).ToList();
                    logger.Info("End  WarehouseCategory: ");
                    return ass;
                }
                catch (Exception ex)
                {
                    logger.Error("Error in WarehouseCategory " + ex.Message);
                    logger.Info("End  WarehouseCategory: ");
               
            }
            }
            return null;
        }
        
        [Authorize]
        [Route("")]
        public List<SubCategory> Getbycity(int city)
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
                    ass = context.subcategorybycity(city).ToList();
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
        [ResponseType(typeof(SubCategory))]
        [Route("")]
        [AcceptVerbs("PUT")]
        public List<SubCategory> Put(List<SubCategory> item)
        {
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
                logger.Info("User ID : {0} , Company Id : {1}", compid, userid);
                return context.Updatebrands(item);
            }
            catch
            {
                return null;
            }
        }

    }
}
