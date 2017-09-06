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
    [RoutePrefix("api/area")]
    public class AreaController : ApiController
    {
        AuthContext context = new AuthContext();
        private static Logger logger = LogManager.GetCurrentClassLogger();

        [Authorize]
        [Route("all")]
        public IEnumerable<Area> Get()
        {
            logger.Info("start Warehouse: ");
            List<Area> ass = new List<Area>();
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

                ass = context.AreaDb.Where(x => x.Deleted == false).ToList();
                logger.Info("End  Cluster: ");
                return ass;
            }
            catch (Exception ex)
            {
                logger.Error("Error in Warehouse " + ex.Message);
                logger.Info("End  Warehouse: ");
                return null;
            }
        }

        [Route("add")]
        [AcceptVerbs("POST")]
        public Area add(Area area)
        {
            logger.Info("start cluster: ");
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

                if (area == null)
                {
                    throw new ArgumentNullException("area");
                }
                logger.Info("User ID : {0} , Company Id : {1}", compid, userid);
                context.AddArea(area);
                logger.Info("End  Cluster: ");
                return area;
            }
            catch (Exception ex)
            {
                logger.Error("Error in addQuesAns " + ex.Message);
                logger.Info("End  AddCluster: ");
                return null;
            }
        }

        [Route("put")]
        [AcceptVerbs("PUT")]
        public Area Put(Area item)
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
                return context.Putarea(item);
            }
            catch
            {
                return null;
            }
        }

        [Route("delete")]
        public bool delete(int id)
        {
            logger.Info("start cluster: ");
            try
            {
                context.DeleteArea(id);
                logger.Info("End  Cluster: ");
                return true;
            }
            catch (Exception ex)
            {
                logger.Error("Error in addQuesAns " + ex.Message);
                logger.Info("End  AddCluster: ");
                return false;
            }
        }
    }
}



