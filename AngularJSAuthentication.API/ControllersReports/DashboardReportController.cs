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
    [RoutePrefix("api/DashboardReport")]
    public class DashboardReportController : ApiController
    {
        iAuthContext context = new AuthContext();
        public static Logger logger = LogManager.GetCurrentClassLogger();
        private static TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
        [Route("")]
        [HttpGet]
        public dashboardReport Get(int Warehouseid)
        {
            logger.Info("start get all Sales Executive: ");
            dashboardReport dbReport = new dashboardReport();
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                int compid = 0, userid = 0;
                List<Customer> displist = new List<Customer>();
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
                AuthContext db = new AuthContext();
                var m = indianTime.Month;
                var y = indianTime.Year;
                var d = indianTime.Day;

                DateTime lM = DateTime.Now.AddMonths(-1);
                int lMonth = lM.Month;
                int lYear = lM.Year;

                DateTime yesterday = DateTime.Now.AddDays(-1);
                int ydMonth = yesterday.Month;
                int ydYear = yesterday.Year;
                int ydDay = yesterday.Day;

                displist = db.Customers.Where(x => x.Deleted == false && x.Warehouseid == Warehouseid).ToList();
                dbReport.TotalCust = displist.Count;
                var CustToday = displist.Where(x => x.CreatedDate.Date == indianTime.Date).ToList();
                dbReport.TodayCust = CustToday.Count;

                var CustMonth = displist.Where(x => x.CreatedDate.Month == m && x.CreatedDate.Year == y).ToList();
                dbReport.MonthCust = CustMonth.Count;

                var CustYesturday = displist.Where(x => x.CreatedDate.Date == yesterday.Date).ToList();
                dbReport.YesturdayCust = CustYesturday.Count;

                var MonthLCust = displist.Where(x => x.CreatedDate.Month == lMonth && x.CreatedDate.Year == lYear).ToList();
                dbReport.LMonthCust = MonthLCust.Count;

                var ACustYesterday = db.DbOrderMaster.Where(o => o.Deleted == false && o.Warehouseid == Warehouseid && o.CreatedDate.Day == ydDay && o.CreatedDate.Month == ydMonth && o.CreatedDate.Year == ydYear).GroupBy(v => v.Skcode).ToList();
                dbReport.AcYesterdayCust = ACustYesterday.Count;

                foreach (var cust in ACustYesterday)
                {
                    if (cust.Count() > 2)
                    {
                        dbReport.AcYesterdayCust2++;
                    }
                }

                var ACustLMonth = db.DbOrderMaster.Where(o => o.Deleted == false && o.Warehouseid == Warehouseid && o.CreatedDate.Month == lMonth && o.CreatedDate.Year == lYear).GroupBy(v => v.Skcode).ToList();
                dbReport.AcLMonthCust = ACustLMonth.Count;

                foreach (var cust in ACustLMonth)
                {
                    if (cust.Count() > 2)
                    {
                        dbReport.AcLMonthCust2++;
                    }
                }
                var ActCust = db.DbOrderMaster.Where(x=> x.Deleted == false && x.Warehouseid == Warehouseid).GroupBy(v => v.Skcode).ToList();
                dbReport.ActiveCust = ActCust.Count;

                foreach (var cust in ActCust)
                {
                    if (cust.Count() > 2)
                    {
                        dbReport.ActiveCust2++;
                    }
                }

                var ACustMonth  = db.DbOrderMaster.Where(o => o.Deleted == false && o.Warehouseid == Warehouseid && o.CreatedDate.Month == m && o.CreatedDate.Year == y).GroupBy(v=>v.Skcode).ToList();
                dbReport.AcMonthCust  = ACustMonth.Count;

                foreach (var cust in ACustMonth)
                {
                    if(cust.Count() > 2)
                    {
                        dbReport.AcMonthCust2 ++;
                    }
                }
                var ACustToday = db.DbOrderMaster.Where(o => o.Deleted == false && o.Warehouseid == Warehouseid && o.CreatedDate.Day == indianTime.Day && o.CreatedDate.Month == m && o.CreatedDate.Year == y).GroupBy(v => v.Skcode).ToList();
                dbReport.AcTodayCust = ACustToday.Count;

                foreach (var cust in ACustToday)
                {
                    if (cust.Count() > 2)
                    {
                        dbReport.AcTodayCust2++;
                    }
                }


                var order = db.DbOrderMaster.Where(o => o.Deleted == false && o.Warehouseid == Warehouseid).ToList();
                dbReport.Ordered = order.Count;
                double TSale = 0.00;
                double MSale = 0.00;
                double DSale = 0.00;

                double MLSale = 0.00;
                double YDSale = 0.00;


                var DOrder = 0;
                var MOrder = 0;
                var MLOrder = 0;
                var DYOrder = 0;
                foreach (var a in order)
                {
                    if (a.CreatedDate.Date == indianTime.Date)
                    {
                        DOrder++;
                        DSale +=  Convert.ToDouble(a.TotalAmount);
                    }

                    if (a.CreatedDate.Date == yesterday.Date)
                    {
                        DYOrder++;
                        YDSale += Convert.ToDouble(a.TotalAmount);
                    }


                    if (a.CreatedDate.Month == lMonth && a.CreatedDate.Year == lYear)
                    {
                        MLSale += Convert.ToDouble(a.TotalAmount);
                        MLOrder++;
                    }

                    if (a.CreatedDate.Month == m && a.CreatedDate.Year == y)
                    {
                        MSale +=  Convert.ToDouble(a.TotalAmount);
                        MOrder++;
                    }
                    TSale +=  Convert.ToDouble(a.TotalAmount);
                }

                dbReport.TodOrdered = DOrder;
                dbReport.ToYOrdered = DYOrder;
                dbReport.MOrdered = MOrder;
                dbReport.TodOSale = DSale;
                dbReport.MLOrdered = MLOrder;
                dbReport.ToYdOSale = YDSale;
                dbReport.MOLSale = MLSale;
                dbReport.MOSale = MSale;
                dbReport.OSale = TSale;
                var DlOr = order.Where(x => x.Status == "sattled").ToList();
                dbReport.Odeliver = DlOr.Count;
                var DDlOrder = DlOr.Where(x => x.CreatedDate.Date == indianTime.Date).ToList();
                dbReport.ToOdeliver = DDlOrder.Count;
                var MDlOrder = DlOr.Where(x => x.CreatedDate.Month == m && x.CreatedDate.Year == y).ToList();
                dbReport.MOdeliver = MDlOrder.Count;
                var YDlOrder = DlOr.Where(x => x.CreatedDate.Date == yesterday.Date).ToList();
                dbReport.ToOYdeliver = YDlOrder.Count;
                var LMDlOrder = DlOr.Where(x => x.CreatedDate.Month == ydDay && x.CreatedDate.Year == ydYear).ToList();
                dbReport.LMOdeliver = LMDlOrder.Count;
                //var DlOrder = order.Where(x => x.Status == "Delivered" || x.Status == "sattled" || x.Status == "Account settled" || x.Status == "Partial settled" || x.Status == "Partial receiving -Bounce" &&  x.Deliverydate.Date > yesterday.Date).ToList();
                var DlOrder = order.Where(x => x.Deliverydate.Date > yesterday.Date && x.Status== "Delivered").ToList();

                var Dl48Order = 0;
                var MDl48Order = 0;
                var YDl48Order = 0;
                var TDl48Order = 0;
                foreach (var a in DlOrder)
                {
                    TimeSpan diff = a.UpdatedDate - a.CreatedDate;
                    double hours = diff.TotalHours;
                    if(hours <= 48)
                    {
                        Dl48Order ++;
                        if (a.Deliverydate.Date == indianTime.Date)
                        {
                            TDl48Order++;
                        }
                        if (a.Deliverydate.Date == yesterday.Date)
                        {
                            YDl48Order++;
                        }
                        if (a.Deliverydate.Month == m && a.Deliverydate.Year == y)
                        {
                            MDl48Order++;
                        }
                    }                    
                }
                dbReport.O48deliver = Dl48Order;
                dbReport.O48Tdeliver = TDl48Order;
                dbReport.O48Ydeliver = YDl48Order;
                dbReport.MO48deliver = MDl48Order; 
                  
                
                var todayInv = 0.0;
                var YdayInv = 0.0;
                var monthInv = 0.0;
                var totalInv = 0.0;
                var todayInvSale = 0.0;
                var monthInvSale = 0.0;
                var totalInvSale = 0.0;
                var YtotalInvSale = 0.0;
                var avgInv = db.AvgInventoryDb.Where(x=>x.warhouseid == Warehouseid).ToList();
                var mday = 0;
                var totDay = 0;
                if (avgInv.Count != 0)
                {
                    if (indianTime.Hour >= 20)
                    {
                        totDay = Convert.ToInt32((indianTime.Date - avgInv[0].date.Date).TotalDays) + 1;
                    }
                    else
                    {
                        totDay = Convert.ToInt32((indianTime.Date - avgInv[0].date.Date).TotalDays);
                    }
                    foreach (var ac in avgInv)
                    {
                        totalInv += ac.totals;
                        totalInvSale += ac.totalSale;
                        if (ac.date.Date == indianTime.Date)
                        {
                            todayInv += ac.totals;
                            todayInvSale += ac.totalSale;
                        }


                        if (ac.date.Date == yesterday.Date)
                        {
                            YdayInv += ac.totals;
                            YtotalInvSale += ac.totalSale;
                        }



                        if (ac.date.Month == m && ac.date.Year == y)
                        {
                            monthInv += ac.totals;
                            monthInvSale += ac.totalSale;
                            if (ac.date.Month == ac.firstdate.Month && ac.date.Year == ac.date.Year)
                            {
                                if (indianTime.Hour >= 20)
                                {
                                    mday = Convert.ToInt32((indianTime.Date - ac.firstdate.Date).TotalDays) + 1;
                                }
                                else
                                {
                                    mday = Convert.ToInt32((indianTime.Date - ac.firstdate.Date).TotalDays);
                                }
                            }
                            else
                            {
                                mday = ac.date.Day;
                            }
                        }
                    }
                    dbReport.ToInvTurn = (todayInv / todayInvSale);

                    dbReport.ToYInvTurn = (YdayInv / YtotalInvSale);
                    dbReport.MInvTurn = (monthInv / monthInvSale);
                    dbReport.InvTurn = (totalInv / totalInvSale);




                    dbReport.ToAvgInv = todayInv;
                    dbReport.ToYAvgInv = YdayInv;
                    dbReport.MAvInv = (monthInv / mday);
                    dbReport.AvgInv = (totalInv / totDay);

                    logger.Info("End Customer: ");
                }
                return dbReport;
            }
            catch (Exception ex)
            {
                logger.Error("Error in getall  Customer " + ex.Message);
                logger.Info("End get all Customer: ");
                return null;
            }
        }

        [Route("")]
        [HttpPost]
        public bool updateInventory()
        {
            AuthContext db = new AuthContext();
            logger.Info("start get all Sales Executive: ");
            try
            {
                var warehouse = db.Warehouses.Where(x => x.Deleted == false).ToList();
                foreach (var w in warehouse)
                {
                    var identity = User.Identity as ClaimsIdentity;
                    int compid = 0, userid = 0;
                    List<Customer> displist = new List<Customer>();
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
                    var totals = 0.00;
                    var totSale = 0.00;
                    var list = db.DbCurrentStock.Where(x => x.Deleted == false && x.CurrentInventory != 0 && x.Warehouseid == w.Warehouseid).ToList();

                   
                    foreach (var data in list)
                    {
                            var item = db.itemMasters.Where(x => x.Number == data.ItemNumber).FirstOrDefault();
                            if (item != null) 
                            {
                                totals = totals + (Convert.ToDouble(data.CurrentInventory) * item.UnitPrice);
                            }
                    }

                    try
                    {
                        var d = indianTime;
                        List<OrderMaster> orderlist = db.DbOrderMaster.Where(x => x.Deleted == false && x.Warehouseid == w.Warehouseid && x.CreatedDate.Day == d.Day && x.CreatedDate.Month == d.Month && x.CreatedDate.Year == d.Year).ToList();
                        foreach (var orderdata in orderlist)
                        {
                            totSale = totSale + orderdata.TotalAmount;
                        }
                        var ddddd = db.AvgInventoryDb.Where(x => x.deleted == false && x.warhouseid == w.Warehouseid).ToList();
                        if (ddddd.Count == 0)
                        {
                            AvgInventory aaaa = new AvgInventory();
                            aaaa.firstdate = indianTime.Date;
                            aaaa.date = indianTime.Date;
                            aaaa.totalSale = totSale;
                            aaaa.totals = totals;
                            aaaa.warhouseid = orderlist[0].Warehouseid;
                            aaaa.active = true;
                            db.AvgInventoryDb.Add(aaaa);
                            db.SaveChanges();
                        }
                        else
                        {
                            var mmdd = ddddd.Where(x => x.date.Date == indianTime.Date).SingleOrDefault();
                            if (mmdd == null)
                            {
                                AvgInventory aaaa = new AvgInventory();
                                aaaa.firstdate = ddddd[0].firstdate;
                                aaaa.date = indianTime.Date;
                                aaaa.totals = totals;
                                aaaa.totalSale = totSale;
                                aaaa.warhouseid = orderlist[0].Warehouseid;
                                aaaa.active = true;
                                db.AvgInventoryDb.Add(aaaa);
                                db.SaveChanges();
                            }
                            else
                            {
                                mmdd.date = indianTime.Date;
                                mmdd.totals = totals;
                                mmdd.totalSale = totSale;
                                db.AvgInventoryDb.Attach(mmdd);
                                db.Entry(mmdd).State = EntityState.Modified;
                                db.SaveChanges();
                            }
                        }
                    }
                    catch (Exception ex) {
                        logger.Error("Error in getall  Customer " + ex.Message);
                    }

                   

                }
                return true;
            }
            catch (Exception ex)
            {
                logger.Error("Error in getall  Customer " + ex.Message);
                logger.Info("End get all Customer: ");
                return false;
            }
        }
    }
    public class dashboardReport
    {
        public int TotalCust { get; set; }
        public int TodayCust { get; set; }
        public int MonthCust { get; set; }
        public int YesturdayCust { get; set; }
        public int LMonthCust { get; set; }
        public int AcTodayCust { get; set; }
        public int AcMonthCust { get; set; }
        public int ActiveCust { get; set; }

        public int AcYesterdayCust { get; set; }
        public int AcLMonthCust { get; set; }

        public int AcYesterdayCust2 { get; set; }
        public int AcLMonthCust2 { get; set; }


        public int AcTodayCust2 { get; set; }
        public int AcMonthCust2 { get; set; }
        public int ActiveCust2 { get; set; }
        public int Ordered { get; set; }
        public int MOrdered { get; set; }
        public int MLOrdered { get; set; }
        public int TodOrdered { get; set; }
        public int ToYOrdered { get; set; }
        public int Odeliver { get; set; }
        public int MOdeliver { get; set; }
        public int LMOdeliver { get; set; }
        public int ToOdeliver { get; set; }
        public int ToOYdeliver { get; set; }
       
        public int O48Tdeliver { get; set; }
        public int O48Ydeliver { get; set; }
        public int O48deliver { get; set; }
        public int MO48deliver { get; set; }
        public double OSale { get; set; }
        public double MOSale { get; set; }
        public double MOLSale { get; set; }
        public double TodOSale { get; set; }
        public double ToYdOSale { get; set; }
        public double ToAvgInv { get; set; }
        public double ToYAvgInv { get; set; }
        public double MAvInv { get; set; }
        public double AvgInv { get; set; }
        public double ToInvTurn { get; set; }
        public double ToYInvTurn { get; set; }
        public double MInvTurn { get; set; }
        public double InvTurn { get; set; }
    }
}
