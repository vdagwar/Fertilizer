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
    [RoutePrefix("api/Vehicles")]
    public class VehicleController : ApiController
    {
        iAuthContext context = new AuthContext();
        private static Logger logger = LogManager.GetCurrentClassLogger();

        [Authorize]
        [Route("")]
        public IEnumerable<Vehicle> Get()
        {
            logger.Info("start Vehicles: ");
            List<Vehicle> ass = new List<Vehicle>();
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
                ass = context.AllVehicles().ToList();
                logger.Info("End  Vehicle: ");
                return ass;
            }
            catch (Exception ex)
            {
                logger.Error("Error in Vehicle " + ex.Message);
                logger.Info("End  Vehicle: ");
                return null;
            }
        }

        //[Authorize]
        //[Route("")]
        //public IEnumerable<Vehicle> Get(string id)
        //{
        //    logger.Info("start Vehicle: ");
        //    List<Vehicle> ass = new List<Vehicle>();
        //    try
        //    {
        //        var identity = User.Identity as ClaimsIdentity;
        //        int compid = 0, userid = 0;
        //        // Access claims
        //        foreach (Claim claim in identity.Claims)
        //        {
        //            if (claim.Type == "compid")
        //            {
        //                compid = int.Parse(claim.Value);
        //            }
        //            if (claim.Type == "userid")
        //            {
        //                userid = int.Parse(claim.Value);
        //            }
        //        }

        //        logger.Info("User ID : {0} , Company Id : {1}", compid, userid);
        //        int idd = Int32.Parse(id);
        //        ass = context.AllVehicle(idd).ToList();
        //        logger.Info("End  Vehicle: ");
        //        return ass;
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.Error("Error in Vehicle " + ex.Message);
        //        logger.Info("End  Vehicle: ");
        //        return null;
        //    }
        //}

        [ResponseType(typeof(Vehicle))]
        [Route("")]
        [AcceptVerbs("POST")]
        public Vehicle add(Vehicle item)
        {
            logger.Info("start add Vehicle: ");
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
                item.CompanyId = compid;
                if (item == null)
                {
                    throw new ArgumentNullException("item");
                }
                logger.Info("User ID : {0} , Company Id : {1}", compid, userid);
                context.AddVehicle(item);
                logger.Info("End add Vehicle: ");
                return item;
            }
            catch (Exception ex)
            {
                logger.Error("Error in add Vehicle " + ex.Message);
                logger.Info("End  addVehicle: ");
                return null;
            }
        }

        [ResponseType(typeof(Vehicle))]
        [Route("")]
        [AcceptVerbs("PUT")]
        public Vehicle Put(Vehicle item)
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
                return context.PutVehicle(item);
            }
            catch
            {
                return null;
            }
        }


        [ResponseType(typeof(Vehicle))]
        [Route("")]
        [AcceptVerbs("Delete")]
        public void Remove(int id)
        {
            logger.Info("start deleteVehicley: ");
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
                context.DeleteVehicle(id);
                logger.Info("End  delete Vehicle: ");
            }
            catch (Exception ex)
            {

                logger.Error("Error in deleteVehicle" + ex.Message);


            }
        }
    }
}



