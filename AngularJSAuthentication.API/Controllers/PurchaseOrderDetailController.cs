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
    [RoutePrefix("api/PurchaseOrderDetail")]
    public class PurchaseOrderDetailController : ApiController
    {
        iAuthContext context = new AuthContext();
        public static Logger logger = LogManager.GetCurrentClassLogger();        
        private AuthContext db = new AuthContext();
        // [Authorize]
        [Route("")]
        public IEnumerable<PurchaseOrderDetail> Get(string recordtype)
        {
            if (recordtype == "details")
            {
                logger.Info("start PurchaseOrderDetail: ");
                List<PurchaseOrderDetail> ass = new List<PurchaseOrderDetail>();
                try
                {
                    var identity = User.Identity as ClaimsIdentity;
                    int compid = 1, userid = 0;
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
                    ass = context.AllPOdetails(compid).ToList();
                    logger.Info("End  order: ");
                    return ass;
                }
                catch (Exception ex)
                {
                    logger.Error("Error in PurchaseOrderDetail " + ex.Message);
                    logger.Info("End  PurchaseOrderDetail: ");
                    return null;
                }
            }
            return null;
        }

        [Authorize]
        [Route("")]
        public IEnumerable<PurchaseOrderDetail> Getallorderdetails(string id)
        {
            logger.Info("start : ");
            List<PurchaseOrderDetail> ass = new List<PurchaseOrderDetail>();
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
                ass = context.AllPOrderDetails(idd).ToList();
                logger.Info("End  : ");
                return ass;
            }
            catch (Exception ex)
            {
                logger.Error("Error in PurchaseOrderDetail " + ex.Message);
                logger.Info("End  PurchaseOrderDetail: ");
                return null;
            }
        }

        ////Cityid=' + data.Cityid + "&&" + "Warehouseid="+ data.Warehouseid + "&&" + "datefrom="+ data.datefrom + "&&" + "dateto="+ data.dateto
        //[Authorize]
        //[Route("")]
        //public IEnumerable<DemandDetailsNew> Getallorderdetails(string Cityid, string Warehouseid,DateTime datefrom, DateTime dateto)
        //{
        //    logger.Info("start : ");
        //    IList<DemandDetailsNew> list = null;
        //    List<OrderDetails> ass = new List<OrderDetails>();
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

        //        logger.Info("User ID : {0} , Company Id : {1}", compid, userid);
        //        //int idd = Int32.Parse(id);
        //        list = context.AllfilteredOrderDetails(Cityid, Warehouseid, datefrom, dateto).ToList();
        //        logger.Info("End  : ");
        //        return list;
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.Error("Error in OrderDetails " + ex.Message);
        //        logger.Info("End  OrderDetails: ");
        //        return null;
        //    }
        //}

    }
}