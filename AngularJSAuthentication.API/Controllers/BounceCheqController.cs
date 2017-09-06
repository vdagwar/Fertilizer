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
    [RoutePrefix("api/bouncecheq")]
    public class BounceCheqController : ApiController
    {
        AuthContext db = new AuthContext();
      
        [Route("saless")]
        [HttpGet]
        public PaggingData salessettlement(int list, int page)
        {
            
            try
            {
                PaggingData data = new PaggingData();
                var total_count = db.OrderDispatchedMasters.Where(x => x.Deleted == false && x.Status == "Partial receiving -Bounce").Count();
                var ordermaster = db.OrderDispatchedMasters.Where(x => x.Deleted == false && x.Status == "Partial receiving -Bounce").OrderByDescending(x => x.OrderId).Skip((page - 1) * list).Take(list).ToList();
                data.ordermaster = ordermaster;
                data.total_count = total_count;
                return data;
            }
            catch (Exception ex)
            {               
                return null;
            }
        }


        [Route("search")]
        [HttpGet]
        public dynamic search(DateTime? start, DateTime? end, int? OrderId)
        {

            try
            {
                if (OrderId != 0 && OrderId > 0)
                {
                    var data = db.OrderDispatchedMasters.Where(x => x.Deleted == false &&  x.Status == "Partial receiving -Bounce").ToList();

                    return data;
                }
               else if ((OrderId > 0) && start != null)
                {
                    var data = db.OrderDispatchedMasters.Where(x => x.Deleted == false && (x.OrderId == OrderId) && (x.Status == "Partial receiving -Bounce") && (x.CreatedDate > start && x.CreatedDate <= end)).ToList();

                    return data;
                }
                else {
                    var data = db.OrderDispatchedMasters.Where(x => x.Deleted == false && (x.Status == "Partial receiving -Bounce") && (x.CreatedDate > start && x.CreatedDate < end)).ToList();

                    return data;
                }
                    
            }
            catch (Exception ex)
            {

                return false;
            }
        }

        [Route("Bounce")]
        [HttpGet, HttpPut]
        public dynamic Bounce(OrderDispatchedMaster data)
        {
            var comp = db.OrderDispatchedMasters.Where(x => x.OrderId == data.OrderId).FirstOrDefault();
            try
            {
                comp.Status = "Account settled";
                //comp.UpdatedDate = indianTime;
                db.OrderDispatchedMasters.Attach(comp);
                db.Entry(comp).State = EntityState.Modified;
                db.SaveChanges();

                if (data.OrderId != 0) {
                    var obj = db.DbOrderMaster.Where(x => x.OrderId == data.OrderId).FirstOrDefault();

                    obj.Status = "Account settled";
                    obj.UpdatedDate = DateTime.Now;
                    db.DbOrderMaster.Attach(obj);
                    db.Entry(obj).State = EntityState.Modified;
                    db.SaveChanges();

                }
                return comp;
            }
            catch (Exception ex)
            {
            }
            return null;
        }


    } 
}
