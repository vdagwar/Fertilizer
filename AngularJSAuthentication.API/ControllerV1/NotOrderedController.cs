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
    [RoutePrefix("api/NotOrdered")]
    public class NotOrderedController : ApiController
    {
        AuthContext db = new AuthContext();
        public static Logger logger = LogManager.GetCurrentClassLogger();
        [Route("")]
        public List<SalesPersonBeat> Get()
        {
            logger.Info("start get all Sales Executive: ");
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                int compid = 0, userid = 0;
                List<SalesPersonBeat> displist = new List<SalesPersonBeat>();
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
                displist = db.SalesPersonBeatDb.Where(x => x.Skcode != null).ToList();
                logger.Info("End  Sales Executive: ");
                return displist;
            }
            catch (Exception ex)
            {
                logger.Error("Error in getall Sales Executive " + ex.Message);
                logger.Info("End getall Sales Executive: ");
                return null;
            }
        }


        [Route("search")]
        [HttpGet]
        public dynamic search(DateTime? start, DateTime? end, string skcode)
        {

            try
            {
                  if (skcode != null && start == null)
                {
                    var data = db.SalesPersonBeatDb.Where(x => x.Skcode == skcode).ToList();

                    return data;
                }

                else if (start != null && skcode != null)
                {
                    var data = db.SalesPersonBeatDb.Where(x => x.Skcode == skcode && (x.CreatedDate > start && x.CreatedDate <= end)).ToList();

                    return data;
                }
               

                else if (start != null && skcode==null)
                {
                    var data = db.SalesPersonBeatDb.Where(x => x.CreatedDate > start && x.CreatedDate <= end).ToList();

                    return data;
                }

               

                else {
                      return null;
                }

            }
            catch (Exception ex)
            {

                return false;
            }
        }
    }   
}
