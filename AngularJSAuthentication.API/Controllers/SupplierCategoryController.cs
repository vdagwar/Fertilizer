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
     [RoutePrefix("api/SupplierCategory")]
    public class SupplierCategoryController : ApiController
    {
        
         iAuthContext context = new AuthContext();
        public static Logger logger = LogManager.GetCurrentClassLogger();
        [Authorize]
        [Route("")]
        public IEnumerable<SupplierCategory> Get()
        {
            logger.Info("start Get Supplier Category: ");
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
                logger.Info("End  Customer: ");
                return context.AllSupplierCategory();
            }
            catch (Exception ex)
            {
                logger.Error("Error in Supplier Category " + ex.Message);
                logger.Info("End  Supplier Category: ");
                return null;
            }
        }

        [Authorize]
        [ResponseType(typeof(SupplierCategory))]
        [Route("")]
        [AcceptVerbs("POST")]
        public SupplierCategory add(SupplierCategory suppcategory)
        {
            logger.Info("start add supplier caegory: ");
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
                suppcategory.CompanyId = compid;
                if (suppcategory == null)
                {
                    throw new ArgumentNullException("suppcategory");
                }

                context.AddSupplierCategory(suppcategory);
                logger.Info("User ID : {0} , Company Id : {1}", compid, userid);
                logger.Info("End  add supplier category: ");
                return suppcategory;

            }
            catch (Exception ex)
            {
                logger.Error("Error in add supplier category " + ex.Message);
                
                return null;
            }
        }


        [Authorize]
        [ResponseType(typeof(SupplierCategory))]
        [Route("")]
        [AcceptVerbs("PUT")]
        public SupplierCategory Put(SupplierCategory item)
        {
            logger.Info("start putCustomer: ");
            try
            {
                return context.PutSupplierCategory(item);
            }
            catch (Exception ex)
            {
                logger.Error("Error in put supplier category " + ex.Message);
                return null;
            }
        }

        [ResponseType(typeof(SupplierCategory))]
        [Route("")]
        [AcceptVerbs("Delete")]
        public void Remove(int id)
        {
            logger.Info("start delete supplier caegory: ");
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
                context.DeleteSupplierCategory(id);
                logger.Info("End  delete supplier caegory: ");
            }
            catch (Exception ex)
            {
                logger.Error("Error in delete supplier category " + ex.Message);
               
            }
            }
    }



}
