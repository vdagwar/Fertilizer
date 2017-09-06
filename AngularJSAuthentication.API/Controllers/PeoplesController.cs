using AngularJSAuthentication.Model;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Caching;
using System.Security.Claims;
using System.Web.Http;
using System.Web.Http.Description;

namespace AngularJSAuthentication.API.Controllers
{
    [RoutePrefix("api/Peoples")]
    public class PeoplesController : ApiController
    {
        iAuthContext context = new AuthContext();
        private static Logger logger = LogManager.GetCurrentClassLogger();

        //[Authorize(Roles="Admin")]
        [Route("")]
        public IEnumerable<People> Get()
        {
            logger.Info("Get Peoples: ");
            int compid = 0, userid = 0;
            try
            {
                var identity = User.Identity as ClaimsIdentity;

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
            }
            catch (Exception ex)
            {
                logger.Error("Error in getting Peoples " + ex.Message);
            }

            //return context.AllPeoples(compid);
            logger.Info("End Get Company: ");
            //return context.GetPeoplebyCompanyId(compid);
            List<People> person = context.AllPeoples(compid).ToList();
            return person;
        }
        [Route("user")]
        public People Get(int PeopleId)
        {
            AuthContext db = new AuthContext();
            int compid = 0, userid = 0;
            try
            {
                var identity = User.Identity as ClaimsIdentity;

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
            }
            catch (Exception ex)
            {
                logger.Error("Error in getting Peoples " + ex.Message);
            }
            People person = db.Peoples.Where(u=>u.PeopleID == PeopleId).SingleOrDefault();
            return person;
        }
        [Route("")]
        public IEnumerable<People> Get(string department)
        {
            logger.Info("Get Peoples: ");
            int compid = 0, userid = 0;
            try
            {
                var identity = User.Identity as ClaimsIdentity;

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
            }
            catch (Exception ex)
            {
                logger.Error("Error in getting Peoples " + ex.Message);
            }
            logger.Info("End Get Company: ");
            List<People> person = context.AllPeoplesDep(department).ToList();
            return person;
        }
        [Route("")]
        public People Get(string mob,string password)
        {
            AuthContext context = new AuthContext();
            People check = context.CheckPeople(mob,password);           
            return check;   
        }

        [ResponseType(typeof(People))]
        [Route("")]
        [AcceptVerbs("POST")]
        public People add(People item)
        {
            logger.Info("Add Peoples: ");
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

                item.CompanyID = compid;
                if (item == null)
                {
                    throw new ArgumentNullException("item");
                }
                logger.Info("User ID : {0} , Company Id : {1}", compid, userid);
                var pp = context.AddPeoplebyAdmin(item);
                if (pp == null) {
                    return null;
                }
                logger.Info("End  Add Peoples: ");
                return item;
            }
            catch (Exception ex)
            {
                logger.Error("Error in Add Peoples " + ex.Message);
              
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
                return context.PutPeoplebyAdmin(item);
            }
            catch
            {
                return null;
            }
        }
        
        [ResponseType(typeof(People))]
        [Route("")]
        [AcceptVerbs("Delete")]
        public void Remove(int id)
        {
            logger.Info("DELETE Peoples: ");
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
                context.DeletePeople(id);
            }
            catch (Exception ex)
            {
                logger.Error("Error in Add Peoples " + ex.Message);

            }
        }

        [ResponseType(typeof(People))]
        [Route("retailer")]
        [AcceptVerbs("POST")]
        public People postfromaapp(string mob, string password)
        {
            AuthContext context = new AuthContext();
            People check = context.CheckPeople(mob, password);

            return check;

        }
    }
}



