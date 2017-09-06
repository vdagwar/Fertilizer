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


namespace AngularJSAuthentication.API.Controllers
{
    [RoutePrefix("api/CurrentStock")]
    public class CurrentStockController : ApiController
    {
        iAuthContext context = new AuthContext();
        private static Logger logger = LogManager.GetCurrentClassLogger();

    // [Authorize]
        [Route("")]
        public IEnumerable<CurrentStock> Get()
        {
            logger.Info("start current stock: ");
            List<CurrentStock> ass = new List<CurrentStock>();
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                int compid = 0, userid = 0;
                //Access claims
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
                ass = context.GetAllCurrentStock().ToList();
                logger.Info("End  current stock: ");
                return ass;
            }
            catch (Exception ex)
            {
                logger.Error("Error in current stock " + ex.Message);
                logger.Info("End  current stock: ");
                return null;
            }
        }

        //[Authorize]
        [Route("")]
        public CurrentStock Get(int id)
        {
            logger.Info("start current stock: ");
            CurrentStock ass = new CurrentStock();
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                int compid = 0, userid = 0;
                // Access claims
                //foreach (Claim claim in identity.Claims)
                //{
                //    if (claim.Type == "compid")
                //    {
                //        compid = int.Parse(claim.Value);
                //    }
                //    if (claim.Type == "userid")
                //    {
                //        userid = int.Parse(claim.Value);
                //    }
                //}

                logger.Info("User ID : {0} , Company Id : {1}", compid, userid);
              
                ass = context.GetCurrentStock(id);
                logger.Info("End  current stock: ");
                return ass;
            }
            catch (Exception ex)
            {
                logger.Error("Error in current stock" + ex.Message);
                logger.Info("End  current stock: ");
                return null;
            }
        }

        //[ResponseType(typeof(CurrentStock))]
        //[Route("")]
        //[AcceptVerbs("POST")]
        //public CurrentStock add(CurrentStock item)
        //{
        //    logger.Info("start add City: ");
        //    try
        //    {
        //        var identity = User.Identity as ClaimsIdentity;
        //        int compid = 0, userid = 0;
        //        // Access claims
        //        foreach (Claim claim in identity.Claims)
        //        {
        //            if (claim.Type == "compid")
        //            {
        //                compid = int.Parse(claim.Value);
        //            }
        //            if (claim.Type == "userid")
        //            {
        //                userid = int.Parse(claim.Value);
        //            }
        //        }
        //        item.CompanyId = compid;
        //        if (item == null)
        //        {
        //            throw new ArgumentNullException("item");
        //        }
        //        logger.Info("User ID : {0} , Company Id : {1}", compid, userid);
        //        context.AddCity(item);
        //        logger.Info("End add City: ");
        //        return item;
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.Error("Error in add City " + ex.Message);
        //        logger.Info("End  addCity: ");
        //        return null;
        //    }
        //}

        [ResponseType(typeof(CurrentStockHistory))]
        [Route("")]
        [AcceptVerbs("PUT")]
        public CurrentStockHistory Put(CurrentStockHistory item)
        {
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
                return context.PutCurrentStock(item);
            }
            catch
            {
                return null;
            }
        }


        [ResponseType(typeof(City))]
        [Route("")]
        [AcceptVerbs("Delete")]
        public void Remove(int id)
        {
            logger.Info("start deleteCityy: ");
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
                context.DeleteCity(id);
                logger.Info("End  delete City: ");
            }
            catch (Exception ex)
            {

                logger.Error("Error in deleteCity" + ex.Message);


            }
        }



        [Route("")]
        //[HttpGet]
        public PaggingData_st GetD(int list, int page, int StockId)
        {

            logger.Info("start OrderMaster: ");
            //  List<OrderMaster> ass = new List<OrderMaster>();
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
                var ass = context.AllItemHistory(list, page, StockId);
                logger.Info("End OrderMaster: ");
                return ass;
            }
            catch (Exception ex)
            {
                logger.Error("Error in OrderMaster " + ex.Message);
                logger.Info("End  OrderMaster: ");
                return null;
            }
        }
    }

    public class dataSelects
    {
        public int totalOrder { get; set; }
        public double totalSale { get; set; }
        public int pendingOrder { get; set; }
        public double PendingSale { get; set; }
      
    }


}



