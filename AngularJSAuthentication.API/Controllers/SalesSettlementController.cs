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
    [RoutePrefix("api/SalesSettlement")]
    public class SalesSettlementController : ApiController
    {
        AuthContext db = new AuthContext();
        private static Logger logger = LogManager.GetCurrentClassLogger();

        [Route("saless")]
        [HttpGet]
        public PaggingData salessettlement(int list, int page)
        {

            try
            {
                PaggingData data = new PaggingData();
                var total_count = db.OrderDispatchedMasters.Where(x => x.Deleted == false && (x.Status == "sattled" || x.Status == "Partial settled" || x.Status == "Partial receiving -Bounce")).Count();
                var ordermaster = db.OrderDispatchedMasters.Where(x => x.Deleted == false && (x.Status == "sattled" || x.Status == "Partial settled" || x.Status == "Partial receiving -Bounce")).OrderByDescending(x => x.OrderId).Skip((page - 1) * list).Take(list).ToList();
                data.ordermaster = ordermaster;
                data.total_count = total_count;
                return data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        
        [Route("exportall")]
        [HttpGet]
        public HttpResponseMessage get() //get all export sales settlemt 
        {
            try
            {
                var saleSorder = db.OrderDispatchedMasters.Where(x => x.Status == "sattled" || x.Status == "Partial settled" || x.Status == "Partial receiving -Bounce").ToList();
                return Request.CreateResponse(HttpStatusCode.OK, saleSorder);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
        
        [Route("search")]
        [HttpGet]
        public dynamic search(DateTime? start, DateTime? end, int? OrderId, double? totalAmount)
        {

            try
            {
                if (OrderId != 0 && OrderId > 0)
                {
                    var data = db.OrderDispatchedMasters.Where(x => x.Deleted == false && (x.OrderId == OrderId) && (x.Status == "sattled" || x.Status == "Partial settled" || x.Status == "Partial receiving -Bounce")).ToList();

                    return data;
                }
                else if ((OrderId > 0) && start != null)
                {
                    var data = db.OrderDispatchedMasters.Where(x => x.Deleted == false && (x.OrderId == OrderId) && (x.Status == "sattled" || x.Status == "Partial settled" || x.Status == "Partial receiving -Bounce") && (x.CreatedDate > start && x.CreatedDate <= end)).ToList();

                    return data;
                }
                else if (totalAmount != 0 && totalAmount > 0)
                {
                    var data = db.OrderDispatchedMasters.Where(x => x.Deleted == false && (x.GrossAmount == totalAmount) && (x.Status == "sattled" || x.Status == "Partial settled" || x.Status == "Partial receiving -Bounce")).ToList();

                    return data;
                }



                else {
                    var data = db.OrderDispatchedMasters.Where(x => x.Deleted == false && (x.Status == "sattled" || x.Status == "Partial settled" || x.Status == "Partial receiving -Bounce") && (x.CreatedDate > start && x.CreatedDate < end)).ToList();

                    return data;
                }

            }
            catch (Exception ex)
            {

                return false;
            }
        }
        
        [Route("cashstatus")]
        [HttpGet, HttpPut]
        public dynamic cashstatus(OrderDispatchedMaster data)
        {
            var comp = db.OrderDispatchedMasters.Where(x => x.OrderId == data.OrderId).FirstOrDefault();
            try
            {

                if (data.CheckAmount == 0 && data.ElectronicAmount == 0)
                {
                    comp.cash = true;
                    comp.Status = "Account settled";
                    comp.UpdatedDate = DateTime.Now;
                    db.OrderDispatchedMasters.Attach(comp);
                    db.Entry(comp).State = EntityState.Modified;
                    db.SaveChanges();
                    try
                    {
                        var obj = db.DbOrderMaster.Where(x => x.OrderId == data.OrderId).FirstOrDefault();

                        obj.Status = "Account settled";
                        obj.UpdatedDate = DateTime.Now;
                        db.DbOrderMaster.Attach(obj);
                        db.Entry(obj).State = EntityState.Modified;
                        db.SaveChanges();
                    }

                    catch
                    {

                    }
                }
                else if (data.CheckAmount == 0 && data.ElectronicAmount != 0 && data.electronic == true)
                {
                    comp.cash = true;
                    comp.Status = "Account settled";
                    comp.UpdatedDate = DateTime.Now;
                    db.OrderDispatchedMasters.Attach(comp);
                    db.Entry(comp).State = EntityState.Modified;
                    db.SaveChanges();


                    try
                    {
                        var obj = db.DbOrderMaster.Where(x => x.OrderId == data.OrderId).FirstOrDefault();

                        obj.Status = "Account settled";
                        obj.UpdatedDate = DateTime.Now;
                        db.DbOrderMaster.Attach(obj);
                        db.Entry(obj).State = EntityState.Modified;
                        db.SaveChanges();
                    }

                    catch
                    {

                    }

                }
                else if (data.CheckAmount != 0 && data.ElectronicAmount == 0 && data.cheq == true)
                {
                    comp.cash = true;
                    comp.Status = "Account settled";
                    comp.UpdatedDate = DateTime.Now;
                    db.OrderDispatchedMasters.Attach(comp);
                    db.Entry(comp).State = EntityState.Modified;
                    db.SaveChanges();
                    try
                    {
                        var obj = db.DbOrderMaster.Where(x => x.OrderId == data.OrderId).FirstOrDefault();

                        obj.Status = "Account settled";
                        obj.UpdatedDate = DateTime.Now;
                        db.DbOrderMaster.Attach(obj);
                        db.Entry(obj).State = EntityState.Modified;
                        db.SaveChanges();
                    }

                    catch
                    {

                    }
                }
                else if (data.CheckAmount != 0 && data.ElectronicAmount != 0 && data.cheq == true && data.electronic == true)
                {
                    comp.cash = true;
                    comp.Status = "Account settled";
                    comp.UpdatedDate = DateTime.Now;
                    db.OrderDispatchedMasters.Attach(comp);
                    db.Entry(comp).State = EntityState.Modified;
                    db.SaveChanges();
                    try
                    {
                        var obj = db.DbOrderMaster.Where(x => x.OrderId == data.OrderId).FirstOrDefault();

                        obj.Status = "Account settled";
                        obj.UpdatedDate = DateTime.Now;
                        db.DbOrderMaster.Attach(obj);
                        db.Entry(obj).State = EntityState.Modified;
                        db.SaveChanges();
                    }

                    catch
                    {

                    }
                }
                else
                {
                    comp.cash = true;
                    comp.Status = "Partial settled";
                    comp.UpdatedDate = DateTime.Now;
                    db.OrderDispatchedMasters.Attach(comp);
                    db.Entry(comp).State = EntityState.Modified;
                    db.SaveChanges();
                    try
                    {
                        var obj = db.DbOrderMaster.Where(x => x.OrderId == data.OrderId).FirstOrDefault();

                        obj.Status = "Partial settled";
                        obj.UpdatedDate = DateTime.Now;
                        db.DbOrderMaster.Attach(obj);
                        db.Entry(obj).State = EntityState.Modified;
                        db.SaveChanges();
                    }

                    catch
                    {

                    }
                }
                return comp;
            }
            catch (Exception ex)
            {
            }
            return null;
        }
        
        [Route("chequestatus")]
        [HttpGet, HttpPut]
        public dynamic chequestatus(OrderDispatchedMaster data)
        {
            var comp = db.OrderDispatchedMasters.Where(x => x.OrderId == data.OrderId).FirstOrDefault();
            try
            {
                if (data.CashAmount == 0 && data.ElectronicAmount == 0)
                {
                    comp.cheq = true;
                    comp.Status = "Account settled";
                    comp.UpdatedDate = DateTime.Now;
                    db.OrderDispatchedMasters.Attach(comp);
                    db.Entry(comp).State = EntityState.Modified;
                    db.SaveChanges();

                    try
                    {
                        var obj = db.DbOrderMaster.Where(x => x.OrderId == data.OrderId).FirstOrDefault();

                        obj.Status = "Account settled";
                        obj.UpdatedDate = DateTime.Now;
                        db.DbOrderMaster.Attach(obj);
                        db.Entry(obj).State = EntityState.Modified;
                        db.SaveChanges();
                    }

                    catch
                    {

                    }

                }
                else if (data.CashAmount == 0 && data.ElectronicAmount != 0 && data.electronic == true)
                {
                    comp.cheq = true;
                    comp.Status = "Account settled";
                    comp.UpdatedDate = DateTime.Now;
                    db.OrderDispatchedMasters.Attach(comp);
                    db.Entry(comp).State = EntityState.Modified;
                    db.SaveChanges();
                    try
                    {
                        var obj = db.DbOrderMaster.Where(x => x.OrderId == data.OrderId).FirstOrDefault();

                        obj.Status = "Account settled";
                        obj.UpdatedDate = DateTime.Now;
                        db.DbOrderMaster.Attach(obj);
                        db.Entry(obj).State = EntityState.Modified;
                        db.SaveChanges();
                    }

                    catch
                    {

                    }



                }
                else if (data.CashAmount != 0 && data.ElectronicAmount == 0 && data.cash == true)
                {
                    comp.cheq = true;
                    comp.Status = "Account settled";
                    comp.UpdatedDate = DateTime.Now;
                    db.OrderDispatchedMasters.Attach(comp);
                    db.Entry(comp).State = EntityState.Modified;
                    db.SaveChanges();

                    try
                    {
                        var obj = db.DbOrderMaster.Where(x => x.OrderId == data.OrderId).FirstOrDefault();

                        obj.Status = "Account settled";
                        obj.UpdatedDate = DateTime.Now;
                        db.DbOrderMaster.Attach(obj);
                        db.Entry(obj).State = EntityState.Modified;
                        db.SaveChanges();
                    }

                    catch
                    {

                    }



                }
                else if (data.CashAmount != 0 && data.ElectronicAmount != 0 && data.cash == true && data.electronic == true)
                {
                    comp.cheq = true;
                    comp.Status = "Account settled";
                    comp.UpdatedDate = DateTime.Now;
                    db.OrderDispatchedMasters.Attach(comp);
                    db.Entry(comp).State = EntityState.Modified;
                    db.SaveChanges();
                    try
                    {
                        var obj = db.DbOrderMaster.Where(x => x.OrderId == data.OrderId).FirstOrDefault();

                        obj.Status = "Account settled";
                        obj.UpdatedDate = DateTime.Now;
                        db.DbOrderMaster.Attach(obj);
                        db.Entry(obj).State = EntityState.Modified;
                        db.SaveChanges();
                    }

                    catch
                    {

                    }

                }
                else
                {
                    comp.cheq = true;
                    comp.Status = "Partial settled";
                    comp.UpdatedDate = DateTime.Now;
                    db.OrderDispatchedMasters.Attach(comp);
                    db.Entry(comp).State = EntityState.Modified;
                    db.SaveChanges();
                    try
                    {
                        var obj = db.DbOrderMaster.Where(x => x.OrderId == data.OrderId).FirstOrDefault();
                        obj.Status = "Partial settled";
                        obj.UpdatedDate = DateTime.Now;
                        db.DbOrderMaster.Attach(obj);
                        db.Entry(obj).State = EntityState.Modified;
                        db.SaveChanges();
                    }

                    catch
                    {

                    }

                }
                return comp;
            }
            catch (Exception ex)
            {
            }

            return null;
        }
        
        [Route("electronicstatus")]
        [HttpGet, HttpPut]
        public dynamic put(OrderDispatchedMaster data)
        {
            var comp = db.OrderDispatchedMasters.Where(x => x.OrderId == data.OrderId).FirstOrDefault();
            try
            {


                if (data.CashAmount == 0 && data.CheckAmount == 0)
                {
                    comp.electronic = true;
                    comp.Status = "Account settled";
                    comp.UpdatedDate = DateTime.Now;
                    db.OrderDispatchedMasters.Attach(comp);
                    db.Entry(comp).State = EntityState.Modified;
                    db.SaveChanges();
                    try
                    {
                        var obj = db.DbOrderMaster.Where(x => x.OrderId == data.OrderId).FirstOrDefault();

                        obj.Status = "Account settled";
                        obj.UpdatedDate = DateTime.Now;
                        db.DbOrderMaster.Attach(obj);
                        db.Entry(obj).State = EntityState.Modified;
                        db.SaveChanges();
                    }

                    catch
                    {

                    }
                }

                else if (data.CashAmount == 0 && data.CheckAmount != 0 && data.cheq == true)
                {
                    comp.electronic = true;
                    comp.Status = "Account settled";
                    comp.UpdatedDate = DateTime.Now;
                    db.OrderDispatchedMasters.Attach(comp);
                    db.Entry(comp).State = EntityState.Modified;
                    db.SaveChanges();
                    try
                    {
                        var obj = db.DbOrderMaster.Where(x => x.OrderId == data.OrderId).FirstOrDefault();

                        obj.Status = "Account settled";
                        obj.UpdatedDate = DateTime.Now;
                        db.DbOrderMaster.Attach(obj);
                        db.Entry(obj).State = EntityState.Modified;
                        db.SaveChanges();
                    }

                    catch
                    {

                    }
                }
                else if (data.CashAmount != 0 && data.CheckAmount == 0 && data.cash == true)
                {
                    comp.electronic = true;
                    comp.Status = "Account settled";
                    comp.UpdatedDate = DateTime.Now;
                    db.OrderDispatchedMasters.Attach(comp);
                    db.Entry(comp).State = EntityState.Modified;
                    db.SaveChanges();
                    try
                    {
                        var obj = db.DbOrderMaster.Where(x => x.OrderId == data.OrderId).FirstOrDefault();

                        obj.Status = "Account settled";
                        obj.UpdatedDate = DateTime.Now;
                        db.DbOrderMaster.Attach(obj);
                        db.Entry(obj).State = EntityState.Modified;
                        db.SaveChanges();
                    }

                    catch
                    {

                    }
                }
                else if (data.CashAmount != 0 && data.CheckAmount != 0 && data.cheq == true && data.cash == true)
                {
                    comp.electronic = true;
                    comp.Status = "Account settled";
                    comp.UpdatedDate = DateTime.Now;
                    db.OrderDispatchedMasters.Attach(comp);
                    db.Entry(comp).State = EntityState.Modified;
                    db.SaveChanges();
                    try
                    {
                        var obj = db.DbOrderMaster.Where(x => x.OrderId == data.OrderId).FirstOrDefault();

                        obj.Status = "Account settled";
                        obj.UpdatedDate = DateTime.Now;
                        db.DbOrderMaster.Attach(obj);
                        db.Entry(obj).State = EntityState.Modified;
                        db.SaveChanges();
                    }

                    catch
                    {

                    }
                }
                else
                {
                    comp.electronic = true;
                    comp.Status = "Partial settled";
                    comp.UpdatedDate = DateTime.Now;
                    db.OrderDispatchedMasters.Attach(comp);
                    db.Entry(comp).State = EntityState.Modified;
                    db.SaveChanges();
                    try
                    {
                        var obj = db.DbOrderMaster.Where(x => x.OrderId == data.OrderId).FirstOrDefault();

                        obj.Status = "Partial settled";
                        obj.UpdatedDate = DateTime.Now;
                        db.DbOrderMaster.Attach(obj);
                        db.Entry(obj).State = EntityState.Modified;
                        db.SaveChanges();
                    }

                    catch
                    {

                    }
                }

                return comp;
            }
            catch (Exception ex)
            {
            }
            return null;
        }
        
        [Route("Bounce")]
        [HttpGet, HttpPut]
        public dynamic Bounce(OrderDispatchedMaster data)
        {
            var comp = db.OrderDispatchedMasters.Where(x => x.OrderId == data.OrderId).FirstOrDefault();
            try
            {
                var stt = "Partial receiving -Bounce";
                comp.cheq = true;
                comp.Status = stt;
                comp.UpdatedDate = DateTime.Now;
                comp.BounceCheqAmount = 200;
                db.OrderDispatchedMasters.Attach(comp);
                db.Entry(comp).State = EntityState.Modified;
                db.SaveChanges();
                try
                {
                    var obj = db.DbOrderMaster.Where(x => x.OrderId == data.OrderId).FirstOrDefault();

                    obj.Status = "Partial receiving -Bounce";
                    obj.UpdatedDate = DateTime.Now;
                    db.DbOrderMaster.Attach(obj);
                    db.Entry(obj).State = EntityState.Modified;
                    db.SaveChanges();
                }

                catch
                {

                }



                return comp;
            }
            catch (Exception ex)
            {
            }
            return null;
        }
        
        [Route("history")]
        [HttpGet]
        public PaggingData salessettlementhistory(int list, int page)
        {

            try
            {
                PaggingData data = new PaggingData();
                var total_count = db.OrderDispatchedMasters.Where(x => x.Deleted == false && (x.Status == "Account settled" || x.Status == "Partial receiving -Bounce")).Count();
                var ordermaster = db.OrderDispatchedMasters.Where(x => x.Deleted == false && (x.Status == "Account settled" || x.Status == "Partial receiving -Bounce")).OrderByDescending(x => x.OrderId).Skip((page - 1) * list).Take(list).ToList();
                data.ordermaster = ordermaster;
                data.total_count = total_count;
                return data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [Route("Historyexportsales")]
        [HttpGet]
        public HttpResponseMessage Historyexportsales() //get all export sales settlemt 
        {
            try
            {
                var saleSHistory = db.OrderDispatchedMasters.Where(x => x.Status == "Account settled" || x.Status == "Partial receiving -Bounce").ToList();
                return Request.CreateResponse(HttpStatusCode.OK, saleSHistory);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
        
        [Route("historysearch")]
        [HttpGet]
        public dynamic historysearch(DateTime? start, DateTime? end, int? OrderId, double totalAmount)
        {

            try
            {
                if (OrderId != 0 && OrderId > 0)
                {
                    var data = db.OrderDispatchedMasters.Where(x => x.Deleted == false && (x.OrderId == OrderId) && (x.Status == "Account settled" || x.Status == "Partial receiving -Bounce")).ToList();

                    return data;
                }
                else if ((OrderId > 0) && start != null)
                {
                    var data = db.OrderDispatchedMasters.Where(x => x.Deleted == false && (x.OrderId == OrderId) && (x.Status == "Account settled" || x.Status == "Partial receiving -Bounce") && (x.CreatedDate > start && x.CreatedDate <= end)).ToList();

                    return data;
                }
                else if (totalAmount != 0 && totalAmount > 0)
                {
                    var data = db.OrderDispatchedMasters.Where(x => x.Deleted == false && (x.GrossAmount == totalAmount) && (x.Status == "Account settled" || x.Status == "Partial receiving -Bounce")).ToList();

                    return data;
                }
                else {
                    var data = db.OrderDispatchedMasters.Where(x => x.Deleted == false && (x.Status == "Account settled" || x.Status == "Partial receiving -Bounce") && (x.CreatedDate > start && x.CreatedDate < end)).ToList();

                    return data;
                }

            }
            catch (Exception ex)
            {

                return false;
            }
        }


        [Route("SearchSkcode")]
        [HttpGet]
        public dynamic SearchSkcode(string skcode)
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

                var data = db.OrderDispatchedMasters.Where(x => x.Deleted == false && (x.Skcode == skcode && x.Status == "Partial receiving -Bounce")).ToList();
                if (data.Count()>0)
                {
                    return true;
                   
                }
                else {
                  return false;
                   
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error in Category " + ex.Message);
                logger.Info("End  Category: ");
                return null;
            }

        }

    }
}
