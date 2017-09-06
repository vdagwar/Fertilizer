using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using NLog;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AngularJSAuthentication.API.Controllers
{
    [RoutePrefix("api/target")]
    public class tagetController : ApiController
    {
        private static TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
        AuthContext db = new AuthContext();
        private static Logger logger = LogManager.GetCurrentClassLogger();

        [Authorize]
        [Route("")]
        [HttpGet]
        [AcceptVerbs("GET")]
        public HttpResponseMessage Get()
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                int compid = 0, userid = 0;     // Access claims
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
                var item = db.TargetDb.AsEnumerable();
                return Request.CreateResponse(HttpStatusCode.OK, item);
            }
            catch (Exception ex)
            {
                logger.Error("Error in add feedBack " + ex.Message);
                logger.Info("End  addCity: ");
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [Route("")]
        [HttpPost]
        [AcceptVerbs("POST")]
        public HttpResponseMessage add(Target item)
        {
            logger.Info("start add RequestItem: ");
            try
            {
                Target fdback = db.TargetDb.Where(f => f.name.Trim() == item.name.Trim()).SingleOrDefault();
                if (fdback == null)
                {
                    item.createdDate = indianTime;
                    db.TargetDb.Add(item);
                    db.SaveChanges();
                }
                else
                {
                    fdback.name = item.name;
                    fdback.value = item.value;
                    fdback.monthValue = item.monthValue;
                    db.TargetDb.Attach(fdback);
                    db.Entry(fdback).State = EntityState.Modified;
                    db.SaveChanges();
                }
                logger.Info("End add RequestItem: ");
                return Request.CreateResponse(HttpStatusCode.OK, item);
            }
            catch (Exception ex)
            {
                logger.Error("Error in add RequestItem " + ex.Message);
                logger.Info("End  RequestItem: ");
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
        [Route("Report")]
        [HttpGet]
        [AcceptVerbs("GET")]
        public dynamic Getdata(string day,int id)
        {
            try
            {
                List<Target> item = new List<Target>();
                item = db.TargetDb.ToList();
                var date = indianTime;
                var sDate = indianTime.Date;
                if (day == "Month") {
                    sDate = new DateTime(date.Year, date.Month, 1);
                }
                var list = (from i in db.DbOrderDetails
                            where i.CreatedDate > sDate && i.CreatedDate <= date 
                            join j in db.DbOrderMaster on i.OrderId equals j.OrderId where j.SalesPersonId ==id 
                            join k in db.itemMasters on i.ItemId equals k.ItemId
                            join l in db.SubsubCategorys on k.SubsubCategoryid equals l.SubsubCategoryid                           
                            select new ReportData
                            {
                                Type = l.Type,                                
                                id =j.SalesPersonId,
                                TotalAmount = i.TotalAmt,
                            }).OrderBy(x => x.Type).ToList();

                if (item.Count != 0)
                {                    
                    foreach (var a in list)
                    {
                        if (a.Type == null)
                            continue;
                        Target l = item.Where(x => x.name.Trim() == a.Type.Trim()).SingleOrDefault();
                        if (l != null)
                        {
                            l.TotalAmount = l.TotalAmount + a.TotalAmount;
                        }                        
                    }
                }
                return item;
            }
            catch (Exception ex)
            {
                logger.Error("Error in add feedBack " + ex.Message);
                logger.Info("End  addCity: ");
                return null;
            }
        }
        [Route("Report")]
        [HttpGet]
        [AcceptVerbs("GET")]
        public dynamic Get(string day, string skcode)
        {
            try
            {
                var sdate = indianTime.AddDays(-30).Date;
                var today = indianTime;
                if (day == "3 month")
                {
                    sdate = indianTime.AddMonths(-3).Date;
                }
                var list = db.DbOrderMaster.Where(i => i.CreatedDate >= sdate && i.CreatedDate <= today && i.Skcode.Trim() == skcode.Trim()).ToList();
                List<Target> uniqe = new List<Target>();
                if (list.Count != 0)
                {                    
                    foreach (var a in list)
                    {
                        Target l = uniqe.Where(x => x.createdDate.Date == a.CreatedDate.Date).SingleOrDefault();
                        if (l != null)
                        {
                            l.TotalAmount = l.TotalAmount + a.TotalAmount;
                        }
                        else
                        {
                            Target b = new Target();
                            b.name = a.ShopName;
                            b.createdDate = a.CreatedDate.Date;
                            b.day = a.CreatedDate.Day;
                            b.month = a.CreatedDate.Month;
                            b.year = a.CreatedDate.Year;
                            b.TotalAmount = a.GrossAmount;
                            uniqe.Add(b);
                        }
                    }
                }
                return uniqe;
            }
            catch (Exception ex)
            {
                logger.Error("Error in add feedBack " + ex.Message);
                logger.Info("End  addCity: ");
                return null;
            }
        }
    }
public class Target
    {
        [Key]
        public int Id { get; set; }
        public string name { get; set; }
        public double value { get; set; }
        public double monthValue { get; set; }
        public DateTime createdDate { get; set; }
        [NotMapped]
        public double TotalAmount { get; set; }
        [NotMapped]
        public int day { get; set; }
        [NotMapped]
        public int month { get; set; }
        [NotMapped]
        public int year { get; set; }
    }
    public class ReportData
    {
        public int? id { get; set; }
        public string Type { get; set; }
        public double TotalAmount { get; set; }
    }   
}