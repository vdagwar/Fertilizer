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
using AngularJSAuthentication.Model;

namespace AngularJSAuthentication.API.Controllers
{
    [RoutePrefix("api/wallet")]
    public class WalletController : ApiController
    {
        AuthContext context = new AuthContext();
        private static Logger logger = LogManager.GetCurrentClassLogger();

        //[Authorize]
        [Route("")]
        public HttpResponseMessage Get()
        {
            logger.Info("start WalletList: ");
            try
            {
                var pointList = (from i in context.WalletDb
                                 where i.Deleted == false
                                 join j in context.Customers on i.CustomerId equals j.CustomerId into ts
                                 from j in ts.DefaultIfEmpty()
                                 select new
                                 {
                                     Id = i.Id,
                                     CustomerId = i.CustomerId,
                                     TotalAmount = i.TotalAmount,
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
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message); ;
            }
        }
        [Route("")]
        [AcceptVerbs("Get")]
        public HttpResponseMessage Get(int CustomerId)
        {
            logger.Info("start single  GetcusomerWallets: ");
            WalletReward Item = new WalletReward();
            try
            {
                logger.Info("in Wallets");

                Item.wallet = context.GetWalletbyCustomerid(CustomerId);
                Item.reward = context.GetRewardbyCustomerid(CustomerId);
                Item.conversion = context.CashConversionDb.FirstOrDefault();
                //Item.rewardConversion = context.RPConversionDb.FirstOrDefault();
                return Request.CreateResponse(HttpStatusCode.OK, Item);
            }
            catch (Exception ex)
            {
                logger.Error("Error in Get single GetcusomerWallets " + ex.Message);
                logger.Info("End  single GetcusomerWallets: ");
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Got Error"); ;
            }
        }
        [Route("cash")]
        [AcceptVerbs("Get")]
        public HttpResponseMessage GetCashConversion()
        {
            CashConversion pointList = new CashConversion();
            try
            {
                pointList = context.CashConversionDb.FirstOrDefault();
                return Request.CreateResponse(HttpStatusCode.OK, pointList);
            }
            catch (Exception ex)
            {
                logger.Error("Error in conversion " + ex.Message);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Got Error");
            }
        }
        [Route("customer")]
        public HttpResponseMessage GetCustomer(string skcode)
        {
            logger.Info("start custmer wallet: ");
            Customer cust = new Customer();
            try
            {
                cust = context.Customers.Where(x => x.Skcode.Contains(skcode) && x.Deleted == false).FirstOrDefault();
                logger.Info("End  custmer: ");
                return Request.CreateResponse(HttpStatusCode.OK, cust);
            }
            catch (Exception ex)
            {
                logger.Error("Error in cusomer " + ex.Message);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Got Error");
            }
        }
        [Route("")]
        [AcceptVerbs("Post")]
        public HttpResponseMessage post(Wallet wallet)
        {
            logger.Info("start single  GetcusomerWallets: ");
            Wallet Item = new Wallet();
            try
            {
                logger.Info("in Wallets");
                if (wallet.CustomerId > 0)
                    Item = context.postWalletbyCustomerid(wallet);
                else
                    Item = null;
                return Request.CreateResponse(HttpStatusCode.OK, Item);
            }
            catch (Exception ex)
            {
                logger.Error("Error in Get single GetcusomerWallets " + ex.Message);
                logger.Info("End  single GetcusomerWallets: ");
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Got Error"); ;
            }
        }
        [Route("cash")]
        [AcceptVerbs("Post")]
        public HttpResponseMessage postCashConversion(CashConversion point)
        {
            try
            {
                if (point.Id > 0) { }
                else
                    point.Id = 0;
                var rpoint = context.CashConversionDb.Where(c => c.Id == point.Id).SingleOrDefault();
                if (rpoint != null)
                {
                    rpoint.point = point.point;
                    rpoint.rupee = point.rupee;

                    context.CashConversionDb.Attach(rpoint);
                    context.Entry(rpoint).State = EntityState.Modified;
                    context.SaveChanges();
                }
                else
                {
                    context.CashConversionDb.Add(point);
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
    }
    public class WalletReward
    {
        public Wallet wallet { get; set; }
        public RewardPoint reward { get; set; }
        public CashConversion conversion { get; set; }
        //public RPConversion rewardConversion { get; set; }
    }
}
