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
using GenricEcommers.Models;

namespace AngularJSAuthentication.API.Controllers
{
    [RoutePrefix("api/CurrencyStock")]
    public class CurrencyStockController : ApiController
    {
        iAuthContext context = new AuthContext();
        AuthContext db = new AuthContext();
        private static Logger logger = LogManager.GetCurrentClassLogger();

        [Authorize]
        [Route("")]
        [HttpGet]
        public IEnumerable<CurrencyHistory> Get(int id)
        {
            //return null;
            logger.Info("start StockCurrency: ");
            List<CurrencyHistory> ass = new List<CurrencyHistory>();
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
                ass = context.TotalStockCurrencys(id).ToList();
                logger.Info("End  StockCurrency: ");
                return ass;
            }
            catch (Exception ex)
            {
                logger.Error("Error in StockCurrency " + ex.Message);
                logger.Info("End  StockCurrency: ");
                return null;
            }
        }

        //[Authorize]
        [Route("")]
        [HttpGet]
        public IEnumerable<CurrencyHistory> Get11(string Stock_status)
        {
            logger.Info("start StockCurrency: ");
            List<CurrencyHistory> ass = new List<CurrencyHistory>();
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
                ass = context.AllStockCurrencys(Stock_status).ToList();
                logger.Info("End  StockCurrency: ");
                return ass;
            }
            catch (Exception ex)
            {
                logger.Error("Error in StockCurrency " + ex.Message);
                logger.Info("End  StockCurrency: ");
                return null;
            }
        }
        [Route("historyget")]
        [HttpGet]
        public IEnumerable<CurrencyHistory> GetHistory(string Stock_status)
        {
            logger.Info("start StockCurrency: ");
            List<CurrencyHistory> ass = new List<CurrencyHistory>();
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
                ass = context.AllStockCurrencysHistory(Stock_status).ToList();
                logger.Info("End  StockCurrency: ");
                return ass;
            }
            catch (Exception ex)
            {
                logger.Error("Error in StockCurrency " + ex.Message);
                logger.Info("End  StockCurrency: ");
                return null;
            }
        }

        [Route("BanksettleCurrency")]
        [HttpPost]
        public HttpResponseMessage BankCurrencyStock(CurrencyBankSettle obj, int id)
        {
         try
            {
                var DBoyorders = db.BankStock(obj, id);
                if (DBoyorders == null)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Error Occured");
                }
                return Request.CreateResponse(HttpStatusCode.OK, DBoyorders);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }


        [Route("BankSettleAmount")]
        [HttpGet]
        public IEnumerable<CurrencyBankSettle> Get1()
        {
            logger.Info("start StockCurrency: ");
            List<CurrencyBankSettle> ass = new List<CurrencyBankSettle>();
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
                ass = context.AllBankStockCurrencys().ToList();
                logger.Info("End  StockCurrency: ");
                return ass;
            }
            catch (Exception ex)
            {
                logger.Error("Error in StockCurrency " + ex.Message);
                logger.Info("End  StockCurrency: ");
                return null;
            }
        }

        [Route("BankSettleAmountPut")]
        [HttpPut]
        public CurrencyBankSettle Put(CurrencyBankSettle obj)
        {
            //return null;
            try
            {
                return db.BankCurrencyPut(obj);
            }
            catch (Exception ex)
            {
                logger.Error("Error in Put News " + ex.Message);
                return null;
            }
        }
        [Route("BankSettleAmountGet")]
        [HttpGet]
        public IEnumerable<CurrencyBankSettle> Getbank(int id)
        {
            //return null;
            logger.Info("start StockCurrency: ");
            List<CurrencyBankSettle> ass = new List<CurrencyBankSettle>();
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
                ass = context.Imagegetview(id).ToList();
                logger.Info("End  StockCurrency: ");
                return ass;
            }
            catch (Exception ex)
            {
                logger.Error("Error in StockCurrency " + ex.Message);
                logger.Info("End  StockCurrency: ");
                return null;
            }
        }

        [Route("Checkget")]
        [HttpGet]
        public IEnumerable<CheckCurrency> Getcheckdata(string status)
        {
            logger.Info("start StockCurrency: ");
            List<CheckCurrency> ass = new List<CheckCurrency>();
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
                ass = context.AllStockCurrencyscheck(status).ToList();
                logger.Info("End  StockCurrency: ");
                return ass;
            }
            catch (Exception ex)
            {
                logger.Error("Error in StockCurrency " + ex.Message);
                logger.Info("End  StockCurrency: ");
                return null;
            }
        }
    }
}




