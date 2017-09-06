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
    [RoutePrefix("api/AsignDay")]
    public class AsignDayController : ApiController
    {
        AuthContext db = new AuthContext();
        public static Logger logger = LogManager.GetCurrentClassLogger();
        [Route("")]
        public List<People> Get()
        {
            logger.Info("start get all Sales Executive: ");
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                int compid = 0, userid = 0;
                List<People> displist = new List<People>();
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
                displist = db.Peoples.Where(x => x.Department == "Sales Executive").ToList();
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
        public List<Customer> Get(string key)
       {
            List<Customer> ass = db.Customers.Where(t => (t.ShopName.Contains(key)|| t.Skcode.Contains(key)) && t.Active == true).ToList();
            return ass;
        }
        [Route("customer")]
        public List<Customer> Get(int id,string day)
        {
            logger.Info("start get all Sales Executive: ");
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                int compid = 0, userid = 0;
                List<Customer> displist = new List<Customer>();
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
                if (string.IsNullOrEmpty(day))
                {
                    displist = db.Customers.Where(x => x.ExecutiveId == id && x.Day != null && (x.Day != "0")).ToList();
                }
                else if (day.Equals("undefined"))
                {
                    displist = db.Customers.Where(x => x.ExecutiveId == id).ToList();
                }
                else
                {
                    displist = db.Customers.Where(x => x.ExecutiveId == id && x.Day.Equals(day)).ToList();
                }
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
        [Route("")]
        [AcceptVerbs("PUT")]
        public HttpResponseMessage PUT(setday itemlist)  // Asign orders
        {
            try
            {
                foreach (var item in itemlist.clist)
                {
                    Customer asss = db.Customers.Where(b => b.CustomerId == item.CustomerId).FirstOrDefault();
                    try
                    {
                        asss.Day = item.Day;
                        asss.BeatNumber = item.BeatNumber;
                        db.Entry(asss).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        return null;
                    }
                }
                return Request.CreateResponse(HttpStatusCode.OK, itemlist);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
        public class setday
        {
            public List<Customer> clist { get; set; }
        }
        [Route("addBeat")]
        [AcceptVerbs("POST")]
        public SalesPersonBeat POST(SalesPersonBeat obj)
        {
            try
            {
                return db.Addsalesbeat(obj);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }   
}
