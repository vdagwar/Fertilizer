using GenricEcommers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using System.Web.Http.Description;
using NLog;
using GenricEcommers;
using AngularJSAuthentication.Model;

namespace AngularJSAuthentication.API.Controllers
{
    [RoutePrefix("api/ReqService")]
    public class ReqServiceController : ApiController
    {
        AuthContext context = new AuthContext();
        private static Logger logger = LogManager.GetCurrentClassLogger();

        [Route("")]
        [HttpPost]
        public HttpResponseMessage postservicer(ReqServiceit FIt)
        {
            var item = FIt.items;
            try
            {
                List<ReqService> itemlist = new List<ReqService>();
                foreach (var a in item)
                {
                    try
                    {
                        AuthContext db = new AuthContext();
                        ReqService itm = new ReqService();
                        itm.Skcode = FIt.Skcode;
                        itm.PeopleID = FIt.PeopleID;
                        itm.ItemName = a.Item;
                        itm.PeopleName = FIt.PeopleName;
                        itm.WarehouseId = FIt.WarehouseId;
                        itm.CreatedDate = DateTime.Now;
                        db.ReqServiceDB.Add(itm);
                        int id = db.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex.Message);
                    }
                }
                return Request.CreateResponse(HttpStatusCode.OK, true);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

       // [Authorize]
        [Route("get")]
        public IEnumerable<ReqService> Get()
        {
            logger.Info("start Get Customer: ");
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
                logger.Info("End  Customer: ");
                var Reqdata = from c in context.ReqServiceDB.OrderByDescending(c => c.CreatedDate) select c;
               return Reqdata;
            }
            catch (Exception ex)
            {
                logger.Error("Error in Customer " + ex.Message);
                logger.Info("End  Customer: ");
                return null;
            }
        }
    }

    public class ReqServiceit
    {
        public List<favtItem> items { get; set; }
        public string Skcode { get; set; }
        public int WarehouseId { get; set; }
        public int PeopleID { get; set; }
        public string PeopleName { get; set; }
    }
    public class favtItem
    {
        public string Item { get; set; }
    }

}
