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
using System.Data.Entity;

namespace AngularJSAuthentication.API.Controllers
{
    [RoutePrefix("api/ShortageSettle")]
    public class ShortageSettleController : ApiController
    {
        AuthContext db = new AuthContext();
        public static Logger logger = LogManager.GetCurrentClassLogger();
  
        [Route("")]
        public PaggingData Get(int list, int page, string DBoyNo, DateTime? datefrom, DateTime? dateto)
        {
            List<ShortSetttle> displist = new List<ShortSetttle>();
            logger.Info("start OrderSettle: ");
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
                AuthContext context = new AuthContext();
                logger.Info("User ID : {0} , Company Id : {1}", compid, userid);
                var lst = db.AllShortagePaging(list, page, DBoyNo, datefrom, dateto);
                logger.Info("End OrderSettle: ");
                return lst;
            }
            catch (Exception ex)
            {
                logger.Error("Error in OrderSettle " + ex.Message);
                logger.Info("End  OrderSettle: ");
                return null;
            }
        }


    }   
}
