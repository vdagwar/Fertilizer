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
    [RoutePrefix("api/freeitem")]
    public class FreeItemController : ApiController
    {
        AuthContext context = new AuthContext();
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private static TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
        [Authorize]
        [Route("")]
        public HttpResponseMessage Get(int PurchaseOrderId)
        {
            List<FreeItem> ass = new List<FreeItem>();
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
                ass = context.FreeItemDb.Where(c=>c.Deleted==false && c.PurchaseOrderId == PurchaseOrderId).ToList();
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
        public HttpResponseMessage add(FreeItem item)
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
                        stok.CurrentInventory = stok.CurrentInventory + item.TotalQuantity;
                        if (stok.CurrentInventory < 0)
                        {
                            stok.CurrentInventory = 0;
                        }
                        context.DbCurrentStock.Attach(stok);
                        context.Entry(stok).State = EntityState.Modified;
                        context.SaveChanges();

                        context.FreeItemDb.Add(item);
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

        [Authorize]
        [Route("SkFree")]
        public HttpResponseMessage Getitem(int oderid)
        {
            List<SKFreeItem> ass = new List<SKFreeItem>();
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
                ass = context.SKFreeItemDb.Where(c => c.Deleted == false && c.OrderId == oderid).ToList();
                logger.Info("End  Return: ");
                return Request.CreateResponse(HttpStatusCode.OK, ass);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [Route("addSkFree")]
        [AcceptVerbs("POST")]
        public HttpResponseMessage addSkFree(SKFreeItem item)
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

                ItemMaster it = context.itemMasters.Where(t => t.ItemId == item.ItemId).SingleOrDefault();
                if (it != null)
                {
                    item.CreationDate = indianTime;
                    item.itemname = it.SellingUnitName;
                    item.itemNumber = it.Number;
                    item.SellingSku = it.SellingSku;
                    item.Deleted = false;
                    CurrentStock stok = context.DbCurrentStock.Where(x => x.ItemNumber == item.itemNumber && x.Warehouseid == item.WarehouseId).SingleOrDefault();
                    if (stok != null)
                    {
                        stok.CurrentInventory = stok.CurrentInventory - item.TotalQuantity;
                        if (stok.CurrentInventory < 0)
                        {
                            stok.CurrentInventory = 0;
                        }
                        context.DbCurrentStock.Attach(stok);
                        context.Entry(stok).State = EntityState.Modified;
                        context.SaveChanges();

                        context.SKFreeItemDb.Add(item);
                        context.SaveChanges();
                    }
                    else
                        return Request.CreateResponse(HttpStatusCode.OK, "item Addition Failed");
                }
                return Request.CreateResponse(HttpStatusCode.OK, item);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}



