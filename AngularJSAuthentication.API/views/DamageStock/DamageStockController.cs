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
    [RoutePrefix("api/damagestock")]
    public class DamageStockController : ApiController
    {
        AuthContext db = new AuthContext();
        [Route("get")]
        [HttpGet]
        public PaggingDatastock get(int list, int page)
        {

            try
            {
                PaggingDatastock data = new PaggingDatastock();
                var total_count = db.DamageStockDB.Where(x => x.Deleted == false).Count();
                var damagest = db.DamageStockDB.Where(x => x.Deleted == false ).OrderByDescending(x => x.DamageStockId).Skip((page - 1) * list).Take(list).ToList();
                data.damagest = damagest;
                data.total_count = total_count;
                return data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        [Route("getall")]
        [HttpGet]
        public HttpResponseMessage Get()//get all Issuances which are active for the delivery boy
        {

            try
            {
                var DamageitemData = db.DamageStockDB.Where(x => x.Deleted == false && x.DamageInventory >0).ToList();
                return Request.CreateResponse(HttpStatusCode.OK, DamageitemData);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [Route("search")]
        [HttpGet, HttpPost]
        public dynamic search(int ItemId)
        {

            try
            {
                if (ItemId != 0 && ItemId > 0)
                {
                    var data = db.itemMasters.Where(x => x.Deleted == false && x.active==true && x.ItemId==ItemId).SingleOrDefault();

                    return data;
                }
                else {
                   
                    return null;
                }
                    
            }
            catch (Exception ex)
            {

                return false;
            }
        }


        [Route("damage")]
        [HttpPost]
        [AcceptVerbs("POST")]
        public DamageStock Post(DamageStock DamageStock)
        {
            try
            {
                return db.Adddemand(DamageStock);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        ///////////////////new code/////////////


        [Route("filtre")]
        [HttpGet, HttpPost]
        public dynamic get(DBOYinfo1 DBI)
        {

            try
            {
                List<ItemMaster> returnlist = new List<ItemMaster>();
                AuthContext context = new AuthContext();
                foreach (var i in DBI.ids)
                {
                    var lst = context.AddDamageStock(i.id, DBI.Warehouseid);
                    if (lst != null)
                    {
                        List<ItemMaster> os = lst.otmaster;
                        returnlist.AddRange(os);
                    }
                }
                return returnlist;
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public class DBOYinfo1
        {
            public List<dbinf> ids { get; set; }
            public int Warehouseid { get; set; }
        }
        public class dbinf
        {
            public int id { get; set; }
          

        }

    }
}
