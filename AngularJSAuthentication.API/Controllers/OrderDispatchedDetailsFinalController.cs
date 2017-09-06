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
    [RoutePrefix("api/OrderDispatchedDetailsFinal")]
    public class OrderDispatchedDetailsFinalController : ApiController
    {
        iAuthContext context = new AuthContext();
        public static Logger logger = LogManager.GetCurrentClassLogger();
        private static TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
        private AuthContext db= new AuthContext();


        [Authorize]
        [Route("")]
        public IEnumerable<FinalOrderDispatchedDetails> GetallFinaldispatchDetailbyId(string id)
        {
            logger.Info("start : ");
            List<FinalOrderDispatchedDetails> ass = new List<FinalOrderDispatchedDetails>();
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
                int idd = Int32.Parse(id);
                ass = context.AllFOrderDispatchedDetails(idd).ToList();
                logger.Info("End  : ");
                return ass;
            }
            catch (Exception ex)
            {
                logger.Error("Error in returnorderby id " + ex.Message);
                logger.Info("End  returnorderby id: ");
                return null;
            }
        }


        public List<filtered> arrange(List<FinalOrderDispatchedDetails> lst)
        {
            List<filtered> fl = new List<filtered>();
            foreach (FinalOrderDispatchedDetails o in lst)
            {
                bool found = false;
                foreach (filtered f in fl)
                {
                    if (f.OrderDate == o.OrderDate)
                    {
                        found = true;
                        f.lst.Add(o);
                    }
                }
                if (found == false)
                {
                    filtered obj = new filtered();
                    obj.OrderDate = o.OrderDate;
                    obj.lst.Add(o);
                    fl.Add(obj);

                }
            }

            return fl;
        }




        [Route("GetReport")]
      
        public IEnumerable<filtered> Get(DateTime datefrom, DateTime dateto)
        {
            logger.Info("start : ");
            List<filtered> ass = new List<filtered>();
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
               ass = context.AllFOrderDispatchedReportDetails(datefrom, dateto).ToList();
                logger.Info("End  : ");
                return ass;
            }
            catch (Exception ex)
            {
                logger.Error("Error in returnorderby id " + ex.Message);
                logger.Info("End  returnorderby id: ");
                return null;
            }
        }

        //for report class

        public class filtered
        {
            public DateTime OrderDate { get; set; }
           

            public List<FinalOrderDispatchedDetails> lst = new List<FinalOrderDispatchedDetails>();
            public double pricetotal;
            public double Taxtotal;
            public double priceTotaltotal;
            public double TaxAftertotal;

        }
        //

        [ResponseType(typeof(FinalOrderDispatchedDetails))]
        [Route("")]
        [AcceptVerbs("POST")]
        public List<FinalOrderDispatchedDetails> add(List<FinalOrderDispatchedDetails> po)
        {
            
            foreach (FinalOrderDispatchedDetails x1 in po)
            {
                db = new AuthContext();
                x1.Status = "sattled";
                db.FinalOrderDispatchedDetailsDb.Add(x1);
                int id = db.SaveChanges();

            }

     
            return po;
        }

        [ResponseType(typeof(FinalOrderDispatchedDetails))]
        [Route("")]
        [AcceptVerbs("PUT")]
        public List<FinalOrderDispatchedDetails> add(Int32 oID,Int32 fID)
        {
            db = new AuthContext();
            var listDetail = db.FinalOrderDispatchedDetailsDb.Where(x => x.OrderId == oID).ToList();





            foreach (FinalOrderDispatchedDetails x1 in listDetail)
            {
                x1.FinalOrderDispatchedMasterId = fID;
                x1.UpdatedDate = indianTime;
                db.FinalOrderDispatchedDetailsDb.Attach(x1);
                db.Entry(x1).State = EntityState.Modified;
                db.SaveChanges();
                
                

            }


            return listDetail;
        }

    }
}