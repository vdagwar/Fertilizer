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
    [RoutePrefix("api/cluster")]
    public class clusterController : ApiController
    {
        iAuthContext context = new AuthContext();
        private static Logger logger = LogManager.GetCurrentClassLogger();

        [Authorize]
        [Route("all")]
        public IEnumerable<Cluster> Get()
        {
            logger.Info("start Warehouse: ");
            List<Cluster> ass = new List<Cluster>();
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
                ass = context.AllCluster(compid).ToList();
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
        [ResponseType(typeof(Cluster))]
        [Route("add")]
        [AcceptVerbs("POST")]
        public Cluster add(Cluster item)
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
                item.CompanyId = compid;
                if (item == null)
                {
                    throw new ArgumentNullException("item");
                }
                logger.Info("User ID : {0} , Company Id : {1}", compid, userid);
                context.Addcluster(item);
                logger.Info("End  Cluster: ");
                return item;
            }
            catch (Exception ex)
            {
                logger.Error("Error in addQuesAns " + ex.Message);
                logger.Info("End  AddCluster: ");
                return null;
            }
        }
        [ResponseType(typeof(Cluster))]
        [Route("update")]
        [AcceptVerbs("PUT")]
        public Cluster put(Cluster item)
        {
            logger.Info("start cluster: ");
            try
            {
                context.UpdateCluster(item);
                logger.Info("End  Cluster: ");
                return item;
            }
            catch (Exception ex)
            {
                logger.Error("Error in addQuesAns " + ex.Message);
                logger.Info("End  AddCluster: ");
                return null;
            }
        }
        [ResponseType(typeof(Cluster))]
        [Route("delete")]
        [AcceptVerbs("PUT")]
        public bool delete(int id)
        {
            logger.Info("start cluster: ");
            try
            {
                context.DeleteCluster(id);
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



