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
    [RoutePrefix("api/WarehouseCategory")]
    public class WarehoseCategoryController : ApiController
    {
        iAuthContext context = new AuthContext();
        private static Logger logger = LogManager.GetCurrentClassLogger();

        [Authorize]
        [Route("")]
        public IEnumerable<WarehouseCategory> Get()
        {
            logger.Info("start Category: ");
            List<WarehouseCategory> ass = new List<WarehouseCategory>();
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                int compid = 0, userid=0;
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
                ass = context.AllWarehouseCategory(compid).ToList() ;
                logger.Info("End  WarehouseCategory: ");
                return ass;
            }
            catch (Exception ex)
            {
                logger.Error("Error in WarehouseCategory " + ex.Message);
                logger.Info("End  WarehouseCategory: ");
                return null;
            }
        }

        [ResponseType(typeof(WarehouseCategory))]
        [Route("")]
        public IEnumerable<WarehouseCategory> GetAllCategory(string i)
        {
            logger.Info("start Category: ");
            List<WarehouseCategory> warehouseCategory = new List<WarehouseCategory>();
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
                warehouseCategory = context.AllWhCategory().ToList();
                logger.Info("End  WarehouseCategory: ");
                return warehouseCategory;
            }
            catch (Exception ex)
            {
                logger.Error("Error in WarehouseCategory " + ex.Message);
                logger.Info("End  WarehouseCategory: ");
                return null;
            }
        }

        [ResponseType(typeof(WarehouseCategory))]
        [Route("")]
        [AcceptVerbs("POST")]
        public List<WarehouseCategory> add(List<WarehouseCategory> item)
        {
            logger.Info("start addWarehouseCategory: ");
            try
            {
                string des = item[0].Discription;
                item[0].Discription = null;
                var identity = User.Identity as ClaimsIdentity;
                int compid = 0, userid=0;
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
               
                if (item == null)
                {
                    throw new ArgumentNullException("item");
                }
                logger.Info("User ID : {0} , Company Id : {1}", compid, userid);

                //item = compid;
                context.AddWarehouseCategory(item,des);

                logger.Info("End  addWarehouseCategory: ");
                return item;
            }
            catch (Exception ex)
            {
                logger.Error("Error in addWarehouseCategory " + ex.Message);
                logger.Info("End  addWarehouseCategory: ");
                return null;
            }
        }

        [ResponseType(typeof(WarehouseCategory))]
        [Route("")]
        [AcceptVerbs("PUT")]
        public List<WarehouseCategory> Put(List<WarehouseCategory> item)
        {

            try
            {
                string des = item[0].Discription;
                item[0].Discription = null;

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
                return context.PutWarehouseCategory(item,des);
            }
            catch
            {
                return null;
            }
        }


        [ResponseType(typeof(WarehouseCategory))]
        [Route("")]
        [AcceptVerbs("Delete")]
        public void Remove(int id)
        {
            logger.Info("start del WarehouseCategory: ");
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
                context.DeleteWarehouseCategory(id);
                logger.Info("End  delete WarehouseCategory: ");
            }
            catch (Exception ex)
            {

                logger.Error("Error in del WarehouseCategory " + ex.Message);
                
             
            }
        }    
    }
}



