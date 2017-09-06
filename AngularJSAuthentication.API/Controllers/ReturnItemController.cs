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
    [RoutePrefix("api/ReturnPurchaseItem")]
    public class ReturnItemController : ApiController
    {
        AuthContext context = new AuthContext();
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private static TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
        [Authorize]
        [Route("")]
        public HttpResponseMessage Get()
        {
            List<PurchaseReturn> ass = new List<PurchaseReturn>();
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
                ass = context.PurchaseReturnDb.Where(c=>c.Deleted==false).ToList();
                logger.Info("End  Return: ");
                return Request.CreateResponse(HttpStatusCode.OK,ass);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest,ex.Message);
            }
        }

        [Route("add")]
        [AcceptVerbs("POST")]
        public HttpResponseMessage add(PurchaseReturn item)
        {
            logger.Info("start Return: ");
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
                if (item != null)
                {
                    item.CreationDate = indianTime;
                    item.Deleted = false;
                    CurrentStock stok = context.DbCurrentStock.Where(x => x.ItemNumber == item.itemNumber && x.Warehouseid == item.WarehouseId).SingleOrDefault();
                    if (stok != null)
                    {


                        CurrentStockHistory Oss = new CurrentStockHistory();
                        if (stok != null)
                        {
                            Oss.StockId = stok.StockId;
                            Oss.ItemNumber = stok.ItemNumber;
                            Oss.ItemName = stok.ItemName;
                            Oss.CurrentInventory = stok.CurrentInventory;
                            Oss.OdOrPoId = item.PurchaseReturnId;
                            Oss.PurchaseInventoryOut = Convert.ToInt32(item.TotalQuantity);
                            Oss.TotalInventory = Convert.ToInt32(stok.CurrentInventory - item.TotalQuantity);
                            Oss.WarehouseName = stok.WarehouseName;
                            Oss.CreationDate = indianTime;
                            context.CurrentStockHistoryDb.Add(Oss);
                            int idd = context.SaveChanges();
                        }
                        stok.CurrentInventory = stok.CurrentInventory - item.TotalQuantity;
                        if (stok.CurrentInventory < 0)
                        {
                            stok.CurrentInventory = 0;
                        }
                        context.DbCurrentStock.Attach(stok);
                        context.Entry(stok).State = EntityState.Modified;
                        context.SaveChanges();

                        context.PurchaseReturnDb.Add(item);
                        context.SaveChanges();
                    }
                    else
                        return Request.CreateResponse(HttpStatusCode.OK, "item Updation Failed");
                }
                return Request.CreateResponse(HttpStatusCode.OK, item);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }


        //[ResponseType(typeof(Cluster))]
        //[Route("update")]
        //[AcceptVerbs("PUT")]
        //public Cluster put(Cluster item)
        //{
        //    logger.Info("start cluster: ");
        //    try
        //    {
        //        context.UpdateCluster(item);
        //        logger.Info("End  Cluster: ");
        //        return item;
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.Error("Error in addQuesAns " + ex.Message);
        //        logger.Info("End  AddCluster: ");
        //        return null;
        //    }
        //}
        //[ResponseType(typeof(Cluster))]
        //[Route("delete")]
        //[AcceptVerbs("PUT")]
        //public bool delete(int id)
        //{
        //    logger.Info("start cluster: ");
        //    try
        //    {
        //        context.DeleteCluster(id);
        //        logger.Info("End  Cluster: ");
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.Error("Error in addQuesAns " + ex.Message);
        //        logger.Info("End  AddCluster: ");
        //        return false;
        //    }
        //}
    }
}



