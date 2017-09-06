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

namespace AngularJSAuthentication.API.Controllers
{
    [RoutePrefix("api/reward")]
    public class RewardController : ApiController
    {
        private static TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
        AuthContext context = new AuthContext();
        private static Logger logger = LogManager.GetCurrentClassLogger();

        //[Authorize]
        [Route("")]
        [AcceptVerbs("Get")]
        public HttpResponseMessage Get()
        {
            logger.Info("start WalletList: ");         
            try
            {
                var pointList = (from i in context.RewardPointDb where i.Deleted == false
                         join j in context.Customers on i.CustomerId equals j.CustomerId into ts
                         from j in ts.DefaultIfEmpty()
                         select new 
                         {
                             Id = i.Id,
                             CustomerId = i.CustomerId,
                             TotalPoint = i.TotalPoint,
                             EarningPoint = i.EarningPoint,
                             UsedPoint = i.UsedPoint,
                             MilestonePoint = i.MilestonePoint,
                             CreatedDate = i.CreatedDate,
                             TransactionDate = i.TransactionDate,
                             UpdatedDate = i.UpdatedDate,
                             Skcode = j.Skcode,
                             ShopName = j.ShopName
                         }).ToList();
                logger.Info("End  wallet: ");
                return Request.CreateResponse(HttpStatusCode.OK, pointList);
            }
            catch (Exception ex)
            {
                logger.Error("Error in WalletList " + ex.Message);
                logger.Info("End  WalletList: ");
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [Route("")]
        [AcceptVerbs("Get")]
        public HttpResponseMessage Get(int CustomerId)
        {
            logger.Info("start single  GetcusomerWallets: ");
            RewardPoint Item = new RewardPoint();
            try
            {
                var point =  context.GetRewardbyCustomerid(CustomerId);
                return Request.CreateResponse(HttpStatusCode.OK, point);
            }
            catch (Exception ex)
            {
                logger.Error("Error in Get single GetcusomerWallets " + ex.Message);
                logger.Info("End  single GetcusomerWallets: ");
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Got Error");
            }
        }
        [Route("")]
        [AcceptVerbs("Post")]
        public HttpResponseMessage post(RewardPoint point)
        {
            try
            {
                if (point.CustomerId > 0)
                {
                    var rpoint = context.RewardPointDb.Where(c => c.CustomerId == point.CustomerId).SingleOrDefault();
                    if (rpoint != null)
                    {
                        rpoint.CustomerId = rpoint.CustomerId;
                        if (rpoint.EarningPoint > 0)
                        {
                            rpoint.EarningPoint += rpoint.EarningPoint;
                            rpoint.UpdatedDate = indianTime;
                        }
                        if (rpoint.UsedPoint > 0)
                        {
                            rpoint.TotalPoint -= point.UsedPoint;
                            rpoint.UsedPoint += point.UsedPoint;
                            rpoint.TransactionDate = indianTime;
                        }
                        context.RewardPointDb.Attach(rpoint);
                        context.Entry(rpoint).State = EntityState.Modified;
                        context.SaveChanges();
                    }
                    else
                    {
                        if (point.EarningPoint > 0) { }
                        else
                            point.EarningPoint = 0;
                        point.TotalPoint = 0;
                        point.UsedPoint = 0;
                        point.MilestonePoint = 0;
                        point.CreatedDate = indianTime;
                        point.UpdatedDate = indianTime;
                        point.Deleted = false;
                        context.RewardPointDb.Add(point);
                        context.SaveChanges();
                        rpoint = point;
                    }
                    return Request.CreateResponse(HttpStatusCode.OK, rpoint);
                }
                else
                    return Request.CreateResponse(HttpStatusCode.OK, "CustomerID null");
            }
            catch (Exception ex)
            {
                logger.Error("Error in Get single GetcusomerWallets " + ex.Message);
                logger.Info("End  single GetcusomerWallets: ");
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Got Error"); 
            }
        }
    }
}
