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
    [RoutePrefix("api/DeliveryBoy")]
    public class DeliveryBoyController : ApiController
    {
        iAuthContext context = new AuthContext();
        private static Logger logger = LogManager.GetCurrentClassLogger();

        [Authorize]
        [Route("")]
        public IEnumerable<People> Get()
        {
            logger.Info("start Peoples: ");
            List<People> ass = new List<People>();
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
                ass = context.AllDBoy().ToList();
                logger.Info("End  People: ");
                return ass;
            }
            catch (Exception ex)
            {
                logger.Error("Error in People " + ex.Message);
                logger.Info("End  People: ");
                return null;
            }
        }
        
        [ResponseType(typeof(People))]
        [Route("")]
        [AcceptVerbs("POST")]
        public People add(People item)
        {
            logger.Info("start add People: ");
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
                
                if (item == null)
                {
                    throw new ArgumentNullException("item");
                }
                item.CompanyID = compid;
                logger.Info("User ID : {0} , Company Id : {1}", compid, userid);
                var dboy = context.AddDboys(item);
                if (dboy != null)
                {
                    return dboy;
                }
                else { return null; }
                logger.Info("End add People: ");
                return item;
            }
            catch (Exception ex)
            {
                logger.Error("Error in add People " + ex.Message);
                logger.Info("End  addPeople: ");
                return null;
            }
        }

        [ResponseType(typeof(People))]
        [Route("")]
        [AcceptVerbs("PUT")]
        public People Put(People item)
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
                return context.PutDboys(item);
            }
            catch(Exception ex)
            {
                return null;
            }
        }


        [ResponseType(typeof(People))]
        [Route("")]
        [AcceptVerbs("Delete")]
        public void Remove(int id)
        {
            logger.Info("start deletePeopley: ");
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
                context.DeleteDboys(id);
                logger.Info("End  delete People: ");
            }
            catch (Exception ex)
            {

                logger.Error("Error in deletePeople" + ex.Message);


            }
        }
    }
}



