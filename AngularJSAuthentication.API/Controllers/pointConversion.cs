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
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace AngularJSAuthentication.API.Controllers
{
    [RoutePrefix("api/pointConversion")]
    public class pointConversionController : ApiController
    {
        private static TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
        AuthContext context = new AuthContext();
        private static Logger logger = LogManager.GetCurrentClassLogger();

        //[Authorize]

        [Route("magin")]
        [AcceptVerbs("Get")]
        public HttpResponseMessage GetRP()
        {
            RPConversion pointList = new RPConversion();
            try
            {
                pointList = context.RPConversionDb.FirstOrDefault();
                return Request.CreateResponse(HttpStatusCode.OK, pointList);
            }
            catch (Exception ex)
            {
                logger.Error("Error in conversion " + ex.Message);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Got Error");
            }
        }
        [Route("promo")]
        [AcceptVerbs("Get")]
        public HttpResponseMessage GetPromo()
        {
            promoPurConv pointList = new promoPurConv();
            try
            {
                pointList = context.promoPurConvDb.FirstOrDefault();
                return Request.CreateResponse(HttpStatusCode.OK, pointList);
            }
            catch (Exception ex)
            {
                logger.Error("Error in conversion " + ex.Message);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Got Error");
            }
        }
        [Route("milestone")]
        [AcceptVerbs("Get")]
        public HttpResponseMessage GetMP()
        {
            List<MilestonePoint> pointList = new List<MilestonePoint>();
            try
            {
                pointList = context.MilestonePointDb.Where(m => m.active == true).ToList();
                return Request.CreateResponse(HttpStatusCode.OK, pointList);
            }
            catch (Exception ex)
            {
                logger.Error("Error in conversion " + ex.Message);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Got Error");
            }
        }
        [Route("milestonbackend")]
        [AcceptVerbs("Get")]
        public HttpResponseMessage GetMilestone()
        {
            List<MilestonePoint> pointList = new List<MilestonePoint>();
            try
            {
                pointList = context.MilestonePointDb.ToList();
                return Request.CreateResponse(HttpStatusCode.OK, pointList);
            }
            catch (Exception ex)
            {
                logger.Error("Error in conversion " + ex.Message);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Got Error");
            }
        }
        [Route("share")]
        [AcceptVerbs("Get")]
        public HttpResponseMessage Getshare(int id)
        {
            RetailerShare pointList = new RetailerShare();
            try
            {
                pointList = context.RetailerShareDb.Where(c => c.cityid == id).FirstOrDefault();
                return Request.CreateResponse(HttpStatusCode.OK, pointList);
            }
            catch (Exception ex)
            {
                logger.Error("Error in conversion " + ex.Message);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Got Error");
            }
        }
        [Route("shareAll")]
        [AcceptVerbs("Get")]
        public HttpResponseMessage GetAllshare()
        {
            List<RetailerShare> pointList = new List<RetailerShare>();
            try
            {
                pointList = context.RetailerShareDb.ToList();
                return Request.CreateResponse(HttpStatusCode.OK, pointList);
            }
            catch (Exception ex)
            {
                logger.Error("Error in conversion " + ex.Message);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Got Error");
            }
        }
        [Route("magin")]
        [AcceptVerbs("Post")]
        public HttpResponseMessage postRP(RPConversion point)
        {
            try
            {
                if (point.Id > 0) { }
                else
                    point.Id = 0;
                var rpoint = context.RPConversionDb.Where(c => c.Id == point.Id).SingleOrDefault();
                if (rpoint != null)
                {
                    rpoint.point = point.point;
                    rpoint.rupee = point.rupee;

                    context.RPConversionDb.Attach(rpoint);
                    context.Entry(rpoint).State = EntityState.Modified;
                    context.SaveChanges();
                }
                else
                {
                    context.RPConversionDb.Add(point);
                    context.SaveChanges();
                    rpoint = point;
                }
                return Request.CreateResponse(HttpStatusCode.OK, rpoint);
            }
            catch (Exception ex)
            {
                logger.Error("Error" + ex.Message);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Got Error"); ;
            }
        }
        [Route("promo")]
        [AcceptVerbs("Post")]
        public HttpResponseMessage postPromo(promoPurConv point)
        {
            try
            {
                if (point.Id > 0) { }
                else
                    point.Id = 0;
                var rpoint = context.promoPurConvDb.Where(c => c.Id == point.Id).SingleOrDefault();
                if (rpoint != null)
                {
                    rpoint.point = point.point;
                    rpoint.rupee = point.rupee;

                    context.promoPurConvDb.Attach(rpoint);
                    context.Entry(rpoint).State = EntityState.Modified;
                    context.SaveChanges();
                }
                else
                {
                    context.promoPurConvDb.Add(point);
                    context.SaveChanges();
                    rpoint = point;
                }
                return Request.CreateResponse(HttpStatusCode.OK, rpoint);
            }
            catch (Exception ex)
            {
                logger.Error("Error" + ex.Message);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Got Error"); ;
            }
        }
        [Route("milestone")]
        [AcceptVerbs("Post")]
        public HttpResponseMessage postMP(MilestonePoint point)
        {
            try
            {
                if (point.M_Id > 0) { }
                else
                    point.M_Id = 0;
                var rpoint = context.MilestonePointDb.Where(c => c.M_Id == point.M_Id).SingleOrDefault();
                if (rpoint != null)
                {
                    rpoint.rPoint = point.rPoint;
                    rpoint.mPoint = point.mPoint;
                    rpoint.active = point.active;
                    context.MilestonePointDb.Attach(rpoint);
                    context.Entry(rpoint).State = EntityState.Modified;
                    context.SaveChanges();
                }
                else
                {
                    context.MilestonePointDb.Add(point);
                    context.SaveChanges();
                    rpoint = point;
                }
                return Request.CreateResponse(HttpStatusCode.OK, rpoint);
            }
            catch (Exception ex)
            {
                logger.Error("Error" + ex.Message);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Got Error"); ;
            }
        }
        [Route("share")]
        [AcceptVerbs("Post")]
        public HttpResponseMessage postShare(RetailerShare point)
        {
            try
            {
                var city = context.Cities.Where(c => c.Cityid == point.cityid).FirstOrDefault();
                var rpoint = context.RetailerShareDb.Where(c => c.cityid == point.cityid).SingleOrDefault();
                if (rpoint != null)
                {
                    rpoint.cityid = city.Cityid;
                    rpoint.cityName = city.CityName;
                    rpoint.share = point.share;

                    context.RetailerShareDb.Attach(rpoint);
                    context.Entry(rpoint).State = EntityState.Modified;
                    context.SaveChanges();
                }
                else
                {
                    point.cityName = city.CityName;
                    context.RetailerShareDb.Add(point);
                    context.SaveChanges();
                    rpoint = point;
                }
                return Request.CreateResponse(HttpStatusCode.OK, rpoint);
            }
            catch (Exception ex)
            {
                logger.Error("Error" + ex.Message);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Got Error"); ;
            }
        }

        [Route("promopurchase")]
        [AcceptVerbs("Get")]
        public HttpResponseMessage getPromoPurchase(string SupplierCode)
        {
            try
            {
                supplierPoint pointList = new supplierPoint();
                try
                {
                    pointList = context.supplierPointDb.Where(c => c.SupplierCode == SupplierCode).SingleOrDefault();
                    return Request.CreateResponse(HttpStatusCode.OK, pointList);
                }
                catch (Exception ex)
                {
                    logger.Error("Error in conversion " + ex.Message);
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Got Error");
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error" + ex.Message);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Got Error"); ;
            }
        }

        [Route("promopur")]
        [AcceptVerbs("Get")]
        public HttpResponseMessage getPromo(string SupplierCode)
        {
            try
            {
                List<supplierPoint> pointList = new List<supplierPoint>();
                try
                {
                    if (SupplierCode != "" && SupplierCode != null && SupplierCode != "null")
                        pointList = context.supplierPointDb.Where(x => x.SupplierCode == SupplierCode).ToList();
                    else
                        pointList = context.supplierPointDb.ToList();
                    return Request.CreateResponse(HttpStatusCode.OK, pointList);
                }
                catch (Exception ex)
                {
                    logger.Error("Error in conversion " + ex.Message);
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Got Error");
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error" + ex.Message);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Got Error"); ;
            }
        }

        [Route("promopurchase")]
        [AcceptVerbs("Post")]
        public HttpResponseMessage postPromoPurchase(supplierPoint point)
        {
            try
            {
                var supp = context.Suppliers.Where(c => c.SUPPLIERCODES == point.SupplierCode).FirstOrDefault();
                var rpoint = context.supplierPointDb.Where(c => c.SupplierCode == point.SupplierCode).SingleOrDefault();
                if (rpoint != null)
                {
                    rpoint.SupplierName += supp.Name;
                    rpoint.Amount += point.Amount;
                    rpoint.Point += point.Point;
                    point.confirm = false;

                    context.supplierPointDb.Attach(rpoint);
                    context.Entry(rpoint).State = EntityState.Modified;
                    context.SaveChanges();
                }
                else
                {
                    point.SupplierName += supp.Name;
                    point.confirm = false;
                    context.supplierPointDb.Add(point);
                    context.SaveChanges();
                    rpoint = point;
                }
                return Request.CreateResponse(HttpStatusCode.OK, rpoint);
            }
            catch (Exception ex)
            {
                logger.Error("Error" + ex.Message);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Got Error"); ;
            }
        }
        [Route("comfirm")]
        [AcceptVerbs("Post")]
        public HttpResponseMessage postConfirm(supplierPoint point)
        {
            try
            {
                var rpoint = context.supplierPointDb.Where(c => c.SupplierCode == point.SupplierCode).SingleOrDefault();
                if (rpoint != null)
                {
                    rpoint.Amount = 0;
                    rpoint.PromoPoint += point.Point;
                    rpoint.Point = 0;
                    point.confirm = true;

                    context.supplierPointDb.Attach(rpoint);
                    context.Entry(rpoint).State = EntityState.Modified;
                    context.SaveChanges();
                }
                return Request.CreateResponse(HttpStatusCode.OK, rpoint);
            }
            catch (Exception ex)
            {
                logger.Error("Error" + ex.Message);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Got Error"); ;
            }
        }
        public class supplierPoint
        {
            [Key]
            public int id { get; set; }
            public string SupplierCode { get; set; }
            public string SupplierName { get; set; }
            public int PromoPoint { get; set; }
            public int Point { get; set; }
            public int UsedPoint { get; set; }
            public double Amount { get; set; }
            public bool confirm { get; set; }
        }
        public class promoPurConv
        {
            [Key]
            public int Id { get; set; }
            public double point { get; set; }
            public double rupee { get; set; }
        }
    }
}
