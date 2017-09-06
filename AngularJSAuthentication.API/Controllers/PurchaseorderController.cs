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
    [RoutePrefix("api/PurchaseOrder")]
    public class PurchaseorderController : ApiController
    {
        iAuthContext context = new AuthContext();
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private AuthContext db = new AuthContext();
        //[Authorize]
        [Route("")]
        public IEnumerable<PurchaseOrder> Get()
        {
            logger.Info("start PurchaseOrder: ");
            List<PurchaseOrder> ass = new List<PurchaseOrder>();
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                int compid = 1, userid=0;
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
                ass = context.AllPurchaseOrder(compid).ToList() ;
                logger.Info("End  PurchaseOrder: ");
                return ass;
            }
            catch (Exception ex)
            {
                logger.Error("Error in PurchaseOrder " + ex.Message);
                logger.Info("End  PurchaseOrder: ");
                return null;
            }
        }
       

        [ResponseType(typeof(PurchaseOrder))]
        [Route("")]
        [AcceptVerbs("POST")]
        public List<PurchaseOrder> add(List<PurchaseOrderList> po)
        {
            logger.Info("start add PurchaseOrder: ");
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
                //po.CompanyId = compid;
                //if (item == null)
                //{
                //    throw new ArgumentNullException("item");
                //}
                logger.Info("User ID : {0} , Company Id : {1}", compid, userid);
                context.AddPurchaseOrder(po);
                logger.Info("End  Warehouse: ");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Error in addQuesAns " + ex.Message);
                logger.Info("End  addWarehouse: ");
                return null;
            }
        }

        [ResponseType(typeof(HttpResponseMessage))]
        [Route("")]
        [AcceptVerbs("POST")]
        public HttpResponseMessage add(PurchaseOrder po ,string a)
        {
            logger.Info("start add PurchaseOrder: ");
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
                context.AddPurchaseItem(po);
                logger.Info("End  Warehouse: ");
                return Request.CreateResponse(HttpStatusCode.OK, po);
            }
            catch (Exception ex)
            {
                logger.Error("Error in addQuesAns " + ex.Message);
                logger.Info("End  addWarehouse: ");
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex); ;
            }
        }
    }
}



