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
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.Net.Http.Headers;

namespace AngularJSAuthentication.API.Controllers
{
    [RoutePrefix("api/OrderPending")]
    public class OrderPendingController : ApiController
    {
        AuthContext context = new AuthContext();
        public static Logger logger = LogManager.GetCurrentClassLogger();     
        
        [Route("")]
        [HttpGet]
        public HttpResponseMessage Get(int list, int page)
        {
            logger.Info("start OrderMaster: ");
            PaggingData obj = new PaggingData();
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
                var newdata = context.DbOrderMaster.Where(x => x.Deleted == false && x.Status=="Pending").OrderByDescending(x => x.OrderId).Skip((page - 1) * list).Take(list).ToList();
                obj.total_count = context.DbOrderMaster.Where(x => x.Deleted == false && x.Status == "Pending").Count();
                obj.ordermaster = newdata;
                logger.Info("End OrderMaster: ");
                return Request.CreateResponse(HttpStatusCode.OK, obj);
            }
            catch (Exception ex)
            {
                logger.Error("Error in OrderMaster " + ex.Message);
                logger.Info("End  OrderMaster: ");
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest,ex.Message);
            }
        }
    }
}