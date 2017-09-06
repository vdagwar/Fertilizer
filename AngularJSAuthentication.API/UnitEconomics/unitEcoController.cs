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
    [RoutePrefix("api/uniteconomic")]
    public class unitEcoController : ApiController
    {
        AuthContext context = new AuthContext();
        public static Logger logger = LogManager.GetCurrentClassLogger();
        private static TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);

        [Authorize]
        [Route("")]
        public HttpResponseMessage Get()
        {
            logger.Info("start Vehicles: ");
            List<UnitEconomic> ass = new List<UnitEconomic>();
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
                ass = context.UnitEconomicDb.ToList();
                logger.Info("End  Vehicle: ");
                return Request.CreateResponse(HttpStatusCode.OK,ass);
            }
            catch (Exception ex)
            {
                logger.Error("Error in Vehicle " + ex.Message);
                logger.Info("End  Vehicle: ");
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }        
        
        [Route("")]
        [AcceptVerbs("POST")]
        public HttpResponseMessage add(UnitEconomic uE)
        {
            logger.Info("start add Vehicle: ");
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
                if (uE == null)
                {
                    throw new ArgumentNullException("item");
                }
                logger.Info("User ID : {0} , Company Id : {1}", compid, userid);
                var unEco = context.UnitEconomicDb.Where(x=>x.unitId == uE.unitId).SingleOrDefault();
                if (unEco != null)
                {
                    if (uE.Label1 != null)
                        unEco.Label1 = uE.Label1;
                    if (uE.Label2 != null)
                        unEco.Label2 = uE.Label2;
                    if (uE.Label3 != null)
                        unEco.Label3 = uE.Label3;
                    unEco.Amount = uE.Amount;
                    unEco.Discription = uE.Discription;
                    context.UnitEconomicDb.Attach(unEco);
                    context.Entry(unEco).State = System.Data.Entity.EntityState.Modified;
                }
                else
                {
                    uE.CreatedDate = indianTime;
                    uE.IsActive = true;
                    context.UnitEconomicDb.Add(uE);
                }
                context.SaveChanges();
                logger.Info("End add Vehicle: ");
                return Request.CreateResponse(HttpStatusCode.OK, uE);
            }
            catch (Exception ex)
            {
                logger.Error("Error in add Vehicle " + ex.Message);
                logger.Info("End  addVehicle: ");
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message); ;
            }
        }  
    }
}



