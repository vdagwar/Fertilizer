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
    [RoutePrefix("api/NppHistoryPrice")]
    public class NppHistoryPriceController : ApiController
    {
        iAuthContext context = new AuthContext();
        public static Logger logger = LogManager.GetCurrentClassLogger();
        //[Authorize]
        //[Route("")]
        //public IEnumerable<ItemMaster> Get()
        //{
        //    logger.Info("start Item Master: ");
        //    List<ItemMaster> ass = new List<ItemMaster>();
        //    try
        //    {
        //        var identity = User.Identity as ClaimsIdentity;
        //        int compid = 0, userid = 0;
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
        //        ass = context.AllItemMaster().ToList();
        //        logger.Info("End  Item Master: ");
        //        return ass;
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.Error("Error in Item Master " + ex.Message);
        //        logger.Info("End  Item Master: ");
        //        return null;
        //    }
        //}


        [Authorize]
        [ResponseType(typeof(EditPriceHistory))]
        [Route("")]
        public IEnumerable<EditPriceHistory> Get(DateTime start, DateTime end, string cityid, string categoryid, string subcategoryid, string subsubcategoryid)
        {
            logger.Info("start ItemMaster: ");
            List<EditPriceHistory> ass = new List<EditPriceHistory>();
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
                ass = context.filteredEditPriceHistory(start,end,cityid, categoryid, subcategoryid, subsubcategoryid).ToList();
                logger.Info("End ItemMaster: ");
                return ass;
            }
            catch (Exception ex)
            {
                logger.Error("Error in ItemMaster " + ex.Message);
                logger.Info("End  ItemMaster: ");
                return null;
            }
        }

    }
}