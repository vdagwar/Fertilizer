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
    [RoutePrefix("api/SubsubCategory")]
    public class SubsubCategoryController : ApiController
    {
        iAuthContext context = new AuthContext();
        private static Logger logger = LogManager.GetCurrentClassLogger();

        //[Authorize]
       [Route("")]
        public IEnumerable<SubsubCategory> Get()
        {
            logger.Info("start Subsubategory: ");
            List<SubsubCategory> ass = new List<SubsubCategory>();
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
                ass = context.AllSubsubCat(compid).ToList();
                logger.Info("End  Subsubategory: ");
                return ass;
           }
            catch (Exception ex)
           {
                logger.Error("Error in Subsubategory " + ex.Message);
                logger.Info("End  Subsubategory: ");
                return null;
            }
        }


        [ResponseType(typeof(SubsubCategory))]
        [Route("")]
        [AcceptVerbs("POST")]
        public SubsubCategory add(SubsubCategory item)
       {
           logger.Info("start Subsubategory: ");
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
                item.CompanyId = compid;
                if (item == null)
                {
                    throw new ArgumentNullException("item");
                }
               logger.Info("User ID : {0} , Company Id : {1}", compid, userid);
               context.AddSubsubCat(item);
                logger.Info("End  Subsubategory: ");
                return item;
           }
            catch (Exception ex)
          {
               logger.Error("Error in Subsubategory " + ex.Message);
               logger.Info("End  Subsubategory: ");
               return null;
            }
       }
    
        [ResponseType(typeof(SubsubCategory))]
        [Route("")]
        [AcceptVerbs("PUT")]
        public SubsubCategory Put(SubsubCategory item)
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
                return context.PutSubsubCat(item);
            }
            catch
            {
                return null;
            }
        }


        [ResponseType(typeof(SubsubCategory))]
        [Route("")]
        [AcceptVerbs("Delete")]
        public void Remove(int id)
        {
            logger.Info("start Subsubategory: ");
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
                context.DeleteSubsubCat(id);
                logger.Info("End  delete Subsubategory: ");
            }
            catch (Exception ex)
            {

                logger.Error("Error in Subsubategory " + ex.Message);


            }
        }
    }
}



