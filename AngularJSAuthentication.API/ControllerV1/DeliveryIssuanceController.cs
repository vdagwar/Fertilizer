using AngularJSAuthentication.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Caching;
using System.Security.Claims;
using System.Web.Http;


namespace AngularJSAuthentication.API.Controllers
{
    [RoutePrefix("api/DeliveryIssuance")]
    public class DeliveryIssuanceController : ApiController
    {
        AuthContext context = new AuthContext();
        private static TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
        [Route("")]
        [HttpGet]
        public HttpResponseMessage Get(int id)//get all Issuances which are active for the delivery boy
        {

            try
            {
                var DBoyorders = context.DeliveryIssuanceDb.Where(x => x.IsActive == true && x.PeopleID == id).ToList();
                return Request.CreateResponse(HttpStatusCode.OK, DBoyorders);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }


        [Route("")]
        [HttpGet]
        public HttpResponseMessage Get(int id , DateTime? start, DateTime end)//get all Issuances which b/w dates the delivery boy
        { 
            
            try
            {

                List<DeliveryIssuance> list = new List<DeliveryIssuance>();
                if (start != null && end != null)
                {
                    var k = end.AddDays(1);
                      list = context.DeliveryIssuanceDb.Where(x => x.CreatedDate > start && x.CreatedDate <= k && x.PeopleID == id).ToList();
                }
                else {
                    list = context.DeliveryIssuanceDb.Where(x => x.PeopleID == id).Take(25).ToList();
                }
               // var DBoyorders = context.DeliveryIssuanceDb.Where(x => x.IsActive == true && x.PeopleID == id).ToList();
                return Request.CreateResponse(HttpStatusCode.OK, list);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }



        [Route("")]
        [HttpGet]
        public HttpResponseMessage Get(string all ,int id)//get all issuabces within one month
        {
            try
            {
                var dt = indianTime.Date;
                var st = dt.AddDays(-30);
                var DBoyorders = context.DeliveryIssuanceDb.Where(x => x.CreatedDate>st&& x.CreatedDate<=dt && x.PeopleID == id).ToList();
                return Request.CreateResponse(HttpStatusCode.OK, DBoyorders);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [Route("")]
        [HttpPut]
        public HttpResponseMessage PutAccetance(DeliveryIssuance obj)//Accept or reject issuance
        {
            try
            {

                var db = context.deliveryIssuance(obj);
                return Request.CreateResponse(HttpStatusCode.OK, db);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [Route("")]
        [HttpPost]
        public HttpResponseMessage PostIssuance(DeliveryIssuance obj)//add issuance
        {
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
                obj.CompanyId = compid;
                obj.CreatedDate = indianTime;
                obj.UpdatedDate = indianTime;
                obj.OrderdispatchIds = "";
                foreach (var o in obj.AssignedOrders) {
                    if (obj.OrderdispatchIds == "")
                    {
                        obj.OrderdispatchIds = Convert.ToString(o.OrderDispatchedMasterId);
                    }
                    else {
                        obj.OrderdispatchIds = obj.OrderdispatchIds +","+ Convert.ToString(o.OrderDispatchedMasterId); 
                    }
                }
                obj.OrderIds = "";
                foreach (var o in obj.AssignedOrders)
                {
                    if (obj.OrderIds == "")
                    {
                        obj.OrderIds = Convert.ToString(o.OrderId);
                    }
                    else {
                        obj.OrderIds = obj.OrderIds + "," + Convert.ToString(o.OrderId);
                    }
                }
                obj.Status = "Assigned";
                obj.IsActive = true;
                var DBoyorders = context.DeliveryIssuanceDb.Add(obj);
                int id = context.SaveChanges();
                if (id > 0) {
                    context.DBIssueWailt(obj.AssignedOrders);
                }
                return Request.CreateResponse(HttpStatusCode.OK, DBoyorders);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

    }
}



