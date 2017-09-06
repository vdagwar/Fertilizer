using AngularJSAuthentication.Model;
using GenricEcommers.Models;
using NLog;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Caching;
using System.Security.Claims;
using System.Web.Http;

namespace AngularJSAuthentication.API.ControllerV1
{
    [RoutePrefix("api/CurrencySettle")]
    public class CurrencySettleController : ApiController
    {

        public static Logger logger = LogManager.GetCurrentClassLogger();
        private static TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
        AuthContext context = new AuthContext();
    

        [Route("")]
        [HttpGet]
        public HttpResponseMessage getbyId(int PeopleID)
        {
            try
            {
                var DBoyCurrency = context.getdboysCurrency(PeopleID);
                return Request.CreateResponse(HttpStatusCode.OK, DBoyCurrency);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }


        [Route("checkdata")]
        public List<DBoyCurrency> Getcheckdata( int PeopleID)
        {
            logger.Info("start get all Sales Executive: ");
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                int compid = 0, userid = 0;
                List<DBoyCurrency> displist = new List<DBoyCurrency>();
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
                displist = context.DBoyCurrencyDB.Where(p => p.PeopleId == PeopleID && p.checkrec== false ).ToList();

                logger.Info("End  Sales Executive: ");
                return displist;
            }
            catch (Exception ex)
            {
                logger.Error("Error in getall Sales Executive " + ex.Message);
                logger.Info("End getall Sales Executive: ");
                return null;
            }
        }

        [Route("dueamountget")]
        public List<DBoyCurrency> Get(string status, int PeopleID)
        {
            logger.Info("start get all Sales Executive: ");
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                int compid = 0, userid = 0;
                List<DBoyCurrency> displist = new List<DBoyCurrency>();
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
                displist = context.DBoyCurrencyDB.Where(p => p.PeopleId == PeopleID && p.Dueamountstatus == "Partial Settle").ToList();

                logger.Info("End  Sales Executive: ");
                return displist;
            }
            catch (Exception ex)
            {
                logger.Error("Error in getall Sales Executive " + ex.Message);
                logger.Info("End getall Sales Executive: ");
                return null;
            }
        }



        [Route("")]
        [HttpGet]
        public HttpResponseMessage getAppOrders(string M, string mob) //get orders for delivery
        {
            try
            {
                if (M == "all")
                {
                    var DBoyorders = context.getallOrderofboy(mob);

                    return Request.CreateResponse(HttpStatusCode.OK, DBoyorders);
                }
                else
                {
                    var DBoyorders = context.getAcceptedOrders(mob);
                    return Request.CreateResponse(HttpStatusCode.OK, DBoyorders);
                }


            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [Route("")]
        [HttpPost]
        public HttpResponseMessage DBCurrency(DBoyCurrency obj, int PeopleID) //Order change delivery boy
        {
            try
            {

                var DBoyorders = context.DboyCu(obj, PeopleID);
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
        //[Route("Stock")]
        //[HttpPost]
        //public dynamic DBCurrencyStock(dynamic objlist) 
        //{
        //    try
        //    {

        //        var DBoyorders = context.stockadd(objlist);
        //        if (DBoyorders == null)
        //        {
        //            return null;
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
        //    }
        //    return true;
        //}
        [Route("Stock")]
        [HttpPost]
        public dynamic DBCurrencyStock(CurrencyStock objlist) //Order change delivery boy
        {
            try
            {
               


                if (objlist != null)
                {
                    var existDatas = context.CurrencyStockDB.Where(x => x.Deleted == false).FirstOrDefault();

                    if (existDatas == null)
                    {
                        objlist.UpdatedDate = indianTime;
                        objlist.CreatedDate = indianTime;
                        // objlist.DboyName = deliveryBoy.DisplayName;
                        objlist.status = "Delivered Boy Currency Inserted InCST";
                        context.CurrencyStockDB.Add(objlist);
                        int id = context.SaveChanges();
                    }
                }


                }
            catch (Exception ex)
            {

                return null;
            }
            return objlist;

        }

        [Route("Stockhistory")]
        [HttpPost]
        public dynamic DBCurrencyStockhistory(CurrencyHistory objlist, int PeopleID) //Order change delivery boy
          {
            try
            {
                var deliveryBoy = context.Peoples.Where(x => x.PeopleID == PeopleID && x.Deleted == false).FirstOrDefault();
                if (objlist != null)
                {
                    var existDatas = context.CurrencyHistoryDB.Where(x => x.Deleted == false).FirstOrDefault();

                    if (existDatas == null)
                    {
                        objlist.UpdatedDate = indianTime;
                        objlist.CreatedDate = indianTime;
                        objlist.DboyName = deliveryBoy.DisplayName;
                        objlist.status = "Delivered Boy Currency Inserted InCST";
                        context.CurrencyHistoryDB.Add(objlist);
                        int id = context.SaveChanges();
                    }
                    else
                    {
                        var existData = context.CurrencyHistoryDB.Where(x => x.CurrencyHistoryid == existDatas.CurrencyHistoryid && x.Deleted == false).FirstOrDefault();

                        //CurrencyStock CST = new CurrencyStock();
                        existData.OneRupee += objlist.OneRupee;
                        existData.onerscount += objlist.onerscount;
                        existData.TwoRupee += objlist.TwoRupee;
                        existData.tworscount += objlist.tworscount;
                        existData.FiveRupee += objlist.FiveRupee;
                        existData.fiverscount += objlist.fiverscount;
                        existData.TenRupee += objlist.TenRupee;
                        existData.tenrscount += objlist.tenrscount;
                        existData.TwentyRupee += objlist.TwentyRupee;
                        existData.Twentyrscount += objlist.Twentyrscount;
                        existData.fiftyRupee += objlist.fiftyRupee;
                        existData.fiftyrscount += objlist.fiftyrscount;
                        existData.HunRupee += objlist.HunRupee;
                        existData.hunrscount += objlist.hunrscount;
                        existData.fiveHRupee += objlist.fiveHRupee;
                        existData.fivehrscount += objlist.fivehrscount;
                        existData.twoTHRupee += objlist.twoTHRupee;
                        existData.twoTHrscount += objlist.twoTHrscount;
                        existData.TotalAmount += objlist.TotalAmount;
                        //existData.TotalAmount += objlist.Dueamount;
                        //existData.Dueamount += objlist.Dueamount;
                        existData.UpdatedDate = indianTime;
                        context.CurrencyHistoryDB.Attach(existData);
                        context.Entry(existData).State = EntityState.Modified;
                        context.SaveChanges();
                    }
                    if (deliveryBoy != null)
                    {
                        //objlist.ArrayIds = "";
                        //foreach (var o in objlist.AssignAmountId)
                        //{
                        //    if (objlist.ArrayIds == "")
                        //    {
                        //        objlist.ArrayIds = Convert.ToString(o.DBoyCId);
                        //    }
                        //    else
                        //    {
                        //        objlist.ArrayIds = objlist.ArrayIds + "," + Convert.ToString(o.DBoyCId);
                        //    }
                        //}
                        foreach (var o in objlist.AssignAmountId)
                        {
                            DBoyCurrency db = context.DBoyCurrencyDB.Where(x => x.DBoyCId ==o.DBoyCId).FirstOrDefault();
                            db.Status = "Delivered Boy Currency Settled";
                            db.UpdatedDate = indianTime;
                            db.Dueamount = objlist.Dueamount;
                            db.Dueamountstatus = objlist.Dueamountstatus;
                       
                            //db.TotalAmount = db.TotalAmount - objlist.Dueamount;
                            context.DBoyCurrencyDB.Attach(db);
                            context.Entry(db).State = EntityState.Modified;
                            context.SaveChanges();

                        }
                    }

                }
            }
            catch (Exception ex)
            {

                return null;
            }
            return true;

        }




        [Route("Stockhistorydue")]
        [HttpPost]
        public dynamic DBCurrencyStockhistorydue(CurrencyHistory objlist, int PeopleID) //Order change delivery boy
        {
            try
            {
                var deliveryBoy = context.Peoples.Where(x => x.PeopleID == PeopleID && x.Deleted == false).FirstOrDefault();
                if (objlist != null)
                {
                    var existDatas = context.CurrencyHistoryDB.Where(x => x.Deleted == false).FirstOrDefault();

                    if (existDatas == null)
                    {
                        objlist.UpdatedDate = indianTime;
                        objlist.CreatedDate = indianTime;
                        objlist.DboyName = deliveryBoy.DisplayName;
                        objlist.status = "Delivered Boy Currency Inserted InCST";
                        context.CurrencyHistoryDB.Add(objlist);
                        int id = context.SaveChanges();
                    }
                    else
                    {
                        var existData = context.CurrencyHistoryDB.Where(x => x.CurrencyHistoryid == existDatas.CurrencyHistoryid && x.Deleted == false).FirstOrDefault();

                        //CurrencyStock CST = new CurrencyStock();
                        existData.OneRupee += objlist.OneRupee;
                        existData.onerscount += objlist.onerscount;
                        existData.TwoRupee += objlist.TwoRupee;
                        existData.tworscount += objlist.tworscount;
                        existData.FiveRupee += objlist.FiveRupee;
                        existData.fiverscount += objlist.fiverscount;
                        existData.TenRupee += objlist.TenRupee;
                        existData.tenrscount += objlist.tenrscount;
                        existData.TwentyRupee += objlist.TwentyRupee;
                        existData.Twentyrscount += objlist.Twentyrscount;
                        existData.fiftyRupee += objlist.fiftyRupee;
                        existData.fiftyrscount += objlist.fiftyrscount;
                        existData.HunRupee += objlist.HunRupee;
                        existData.hunrscount += objlist.hunrscount;
                        existData.fiveHRupee += objlist.fiveHRupee;
                        existData.fivehrscount += objlist.fivehrscount;
                        existData.twoTHRupee += objlist.twoTHRupee;
                        existData.twoTHrscount += objlist.twoTHrscount;
                        existData.TotalAmount += objlist.TotalAmount;
                        existData.TotalAmount += objlist.Dueamount;
                        //existData.Dueamount += objlist.Dueamount;
                        existData.UpdatedDate = indianTime;
                        context.CurrencyHistoryDB.Attach(existData);
                        context.Entry(existData).State = EntityState.Modified;
                        context.SaveChanges();
                    }
                    if (deliveryBoy != null)
                    {
                        DBoyCurrency db = context.DBoyCurrencyDB.Where(x => x.DBoyCId == objlist.DBoyCId).FirstOrDefault();
                      
                        db.Dueamountstatus = " Settled ";
                    
                        context.DBoyCurrencyDB.Attach(db);
                        context.Entry(db).State = EntityState.Modified;
                        context.SaveChanges();

                       
                    }

                }
            }
            catch (Exception ex)
            {

                return null;
            }
            return true;

        }


        [Route("Checkdeatil")]
        [HttpPost]
        public dynamic CheckdeatilStock(CheckCurrency objlist,int PeopleID) //Order change delivery boy
        {
            try
            {
                var deliveryBoy = context.Peoples.Where(x => x.PeopleID == PeopleID && x.Deleted == false).FirstOrDefault();

                if (objlist != null)
                {
                        objlist.UpdatedDate = indianTime;
                        objlist.CreatedDate = indianTime;
                        //objlist.DboyName = deliveryBoy.DisplayName;
                        objlist.status = "Delivered Boy Check Inserted InCST";
                        context.CheckCurrencyDB.Add(objlist);
                        int id = context.SaveChanges();
                    
                }
                if (deliveryBoy != null)
                {
                    DBoyCurrency db = context.DBoyCurrencyDB.Where(x => x.DBoyCId == objlist.DBoyCId).FirstOrDefault();

                    if (db != null)
                    {
                        db.checkrec = true;
                        context.DBoyCurrencyDB.Attach(db);
                        context.Entry(db).State = EntityState.Modified;
                        context.SaveChanges();
                    } else { }
                   


                }


            }
            catch (Exception ex)
            {

                return null;
            }
            return objlist;

        }



    } 
}
