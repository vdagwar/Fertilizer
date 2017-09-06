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
    [RoutePrefix("api/PurchaseOrderMasterRecived")]
    public class PurchaseOrderMasterRecivedController : ApiController
    {
        iAuthContext context = new AuthContext();
        public static Logger logger = LogManager.GetCurrentClassLogger();
       
        [Route("")]
        public IEnumerable<PurchaseOrderMasterRecived> Get()
        {
            logger.Info("start PurchaseOrderMaster: ");
            List<PurchaseOrderMasterRecived> ass = new List<PurchaseOrderMasterRecived>();
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
              //  ass = context.AllPOMaster().ToList();
                logger.Info("End PurchaseOrderMaster: ");
                return ass;
            }
            catch (Exception ex)
            {
                logger.Error("Error in PurchaseOrderMaster " + ex.Message);
                logger.Info("End  PurchaseOrderMaster: ");
                return null;
            }
        }

        [Route("")]
        public Warehouse Get(string id)
        {
            logger.Info("start PurchaseOrderMaster: ");
            Warehouse wh = new Warehouse();
           
            if(id!=null)
            {
                int Id = Convert.ToInt32(id);
                AuthContext db = new AuthContext();
                wh = db.Warehouses.Where(x => x.Warehouseid == Id).SingleOrDefault();

                return wh;

            }
            return null;

        }

        [ResponseType(typeof(PurchaseOrderMasterRecived))]
        [Route("")]
        [AcceptVerbs("POST")]
        public PurchaseOrderMasterRecived add(PurchaseOrderMasterRecived item)
        {
           // PurchaseOrderMasterRecived m = new PurchaseOrderMasterRecived();

            

             logger.Info("start PurchaseOrderMasterRecived: ");
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
               // item.CompanyId = compid;
                if (item == null)
                {
                    throw new ArgumentNullException("item");
                }
                logger.Info("User ID : {0} , Company Id : {1}", compid, userid);
                context.AddPurchaseOrderMasterRecived(item);
                logger.Info("End  UnitMaster: ");
                return item;
            }
            catch (Exception ex)
            {
                logger.Error("Error in PurchaseOrderMasterRecived " + ex.Message);
                logger.Info("End  PurchaseOrderMasterRecived: ");
                return null;
            }
        }
        }
}