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
    [RoutePrefix("api/Report")]
    public class ReportController : ApiController
    {       
        AuthContext db = new AuthContext();
        public static Logger logger = LogManager.GetCurrentClassLogger();
        private static TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
        [Authorize]        
        [Route("first")]
        [HttpGet]
        public dynamic reportfirst(string type, int Warehouseid)
        {
            dataSelect result =new dataSelect();
            logger.Info("start Get Report1: ");
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

                var d = indianTime.Day;
                var m = indianTime.Month;
                var y = indianTime.Year;
                var TotalOrder = db.DbOrderMaster.Where(x => x.Deleted == false && x.Warehouseid == Warehouseid && (x.Status != "sattled" && x.Status != "Partial settled" && x.Status != "Account settled")).ToList();

                var data = TotalOrder.Where(x => x.CreatedDate.Day == d && x.CreatedDate.Month == m && x.CreatedDate.Year == y).ToList();
                result.totalOrder = data.Count;
                foreach (var a in data)
                {
                    result.totalSale += a.TotalAmount;
                    if (a.Status == "Delivery Canceled" || a.Status == "Order Canceled")
                    {
                        result.cancelOrder += 1;
                    }
                }
                var data1 = TotalOrder.Where(x => x.Status == "Pending").ToList();
                result.pendingOrder = data1.Count;
                foreach (var b in data1)
                {
                    result.PendingSale += b.TotalAmount;                    
                }
                var data2 = TotalOrder.Where(x => x.CreatedDate.AddDays(2).Date < indianTime.Date && x.Status == "Pending").ToList();
                result.pendingOrder_2 = data2.Count;
                foreach (var c in data2)
                {
                    result.PendingSale2 += c.TotalAmount;
                }
                var data3 = TotalOrder.Where(x => x.UpdatedDate.Day == d && x.UpdatedDate.Month == m && x.UpdatedDate.Year == y && x.Status == "Delivered").ToList();
                result.totalDelivered = data3.Count;
                foreach (var f in data3)
                {
                    result.deliveredSale += f.TotalAmount;
                }
                var data4 = TotalOrder.Where(x => x.CreatedDate.AddDays(2).Date < indianTime.Date && ((x.Status == "Ready to Dispatch" || x.Status == "Issued" || x.Status == "Assigned" || x.Status == "Shipped" || x.Status == "Delivery Redispatch") && x.CreatedDate.AddDays(2).Date < indianTime.Date)).ToList();
                //data4= data4.Where(x => x.CreatedDate.AddDays(2).Date < indianTime.Date).ToList();
                result.notDelivered = data4.Count;
                foreach (var g in data4)
                {
                    result.notDeliveredSale += g.TotalAmount;
                }
                return result;
            }
            catch (Exception ex)
            {
                logger.Error("Error in Customers Report  " + ex.Message);
                logger.Info("End  Customers Report: ");
                return null;
            }
        }

        [Route("select")]
        [HttpGet]
        public dynamic Get(int value)
        {
            dynamic result = null;
            logger.Info("start Get Report2: ");
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
                
                if (value == 1)
                {
                    result = db.Customers.Where(x => x.Deleted == false).ToList();
                }
                else if (value == 2)
                {
                    result = db.Warehouses.Where(x => x.Deleted == false).ToList();
                }
                else if (value == 3)
                {
                    result = db.Cities.Where(x => x.Deleted == false).ToList();
                }
                else if(value == 4)
                {
                    result = db.Peoples.Where(x => x.Department == "Sales Executive" && x.Deleted == false).ToList();
                }
                else if (value == 5)
                {
                    result = db.Clusters.Where(x => x.Deleted == false).ToList();
                }
                return result;
            }
            catch (Exception ex)
            {
                logger.Error("Error in Customers Report  " + ex.Message);
                logger.Info("End  Customers Report: ");
                return null;
            }
        }

        [Route("Catogory")]
        [HttpGet]
        public dynamic GetCatogery(int value)
        {
            dynamic result = null;
            logger.Info("start Get Report2: ");
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

                if (value == 1)
                {
                    result = db.Categorys.Where(x => x.IsActive == true).OrderBy(x => x.CategoryName).ToList();
                }
                else if (value == 2)
                {
                    result = (from i in db.SubCategorys
                              where i.IsActive == true && i.Deleted == false
                              select new 
                              {
                                  SubCategoryId = i.SubCategoryId,
                                  SubcategoryName = i.CategoryName.Trim() + " " + i.SubcategoryName.Trim(),
                                  Categoryid = i.Categoryid,
                                  Warehouseid = i.Warehouseid,
                                  CategoryName = i.CategoryName
                              }).OrderBy(x => x.SubcategoryName).ToList();
                }
                else if (value == 3)
                {
                    result = (from i in db.SubsubCategorys
                              where i.IsActive == true && i.Deleted== false
                              join j in db.SubCategorys on i.SubCategoryId equals j.SubCategoryId
                                         select new 
                                         {
                                             SubsubCategoryid = i.SubsubCategoryid,
                                             SubsubcategoryName = i.CategoryName.Trim() + " (" + j.SubcategoryName.Trim() + ")" + i.SubsubcategoryName.Trim(),
                                             Categoryid = i.Categoryid,
                                             Warehouseid = i.Warehouseid,
                                             CategoryName = i.CategoryName,
                                             SubCategoryId = i.SubCategoryId
                                         }).OrderBy(x => x.SubsubcategoryName).ToList();
                }
                else if (value == 4)
                {
                    result = db.itemMasters.Where(x => x.active == true).OrderBy(x => x.itemname).ToList();
                }
                return result;
            }
            catch (Exception ex)
            {
                logger.Error("Error in Customers Report  " + ex.Message);
                logger.Info("End  Customers Report: ");
                return null;
            }
        }

        [Route("day")]
        [HttpGet]
        public dynamic GetDay(DateTime? datefrom, DateTime? dateto, int type, int value, string ids)
        {
            logger.Info("start OrderMaster: ");            
            List<MainReports> MainReport = new List<MainReports>();
            DateTime start = DateTime.Parse("01-01-2017 00:00:00");
            DateTime end = DateTime.Today.AddDays(1);
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

                if (datefrom == null)
                {
                    datefrom = DateTime.Parse("01-01-2017 00:00:00");
                    dateto = DateTime.Today.AddDays(1);
                }
                else
                {
                    start = datefrom.GetValueOrDefault();
                    end = dateto.GetValueOrDefault();
                }
                var array = ids.Split(',');
                foreach (var iidd in array)
                {
                    int id = Convert.ToInt32(iidd);
                    var res = getdata(datefrom, dateto, value, id);
                    MainReports MainReport1 = new MainReports();
                    List<orderMasterlist> list = new List<orderMasterlist>();
                    List<orderMasterlist> report = new List<orderMasterlist>();
                    list = res;
                    if (list.Count != 0)
                    {
                        List<orderMasterlist> uniqueDay = new List<orderMasterlist>();
                        List<orderMasterlist> uniqueOrdered = new List<orderMasterlist>();
                        List<orderMasterlist> uniqueCustomer = new List<orderMasterlist>();
                        foreach (var a in list)
                        {
                            orderMasterlist l = uniqueDay.Where(x => x.createdDate.Date == a.createdDate.Date).SingleOrDefault();
                            if (l == null)
                            {
                                a.TotalAmount = a.TotalAmount;
                                uniqueDay.Add(a);
                            }
                            else
                            {
                                l.TotalAmount = l.TotalAmount + a.TotalAmount;
                            }
                            orderMasterlist m = uniqueOrdered.Where(c => c.OrderId == a.OrderId && c.createdDate.Date == a.createdDate.Date).SingleOrDefault();
                            if (m == null)
                            {
                                uniqueOrdered.Add(a);
                            }
                            orderMasterlist n = uniqueCustomer.Where(c => c.retaileId == a.retaileId && c.createdDate.Date == a.createdDate.Date).SingleOrDefault();
                            if (n == null)
                            {
                                uniqueCustomer.Add(a);
                            }
                        }
                        for (var day = start.Date; day.Date <= end.Date; day = day.AddDays(1))
                        {
                            orderMasterlist c = new orderMasterlist();

                            c.name = list[0].name;
                            c.day = day.Day;
                            c.month = day.Month;
                            c.year = day.Year;
                            c.createdDate = day;
                            var total = uniqueDay.Where(a => a.createdDate.Date == day).FirstOrDefault();
                            if (total == null)
                            {
                                c.TotalAmount = 0.00;
                            }
                            else
                            {
                                c.TotalAmount = total.TotalAmount;
                            }
                            List<orderMasterlist> order = uniqueOrdered.Where(a => a.createdDate.Date == day).ToList();
                            if (order.Count == 0)
                            {
                                c.totalOrder = 0;
                            }
                            else
                            {
                                c.totalOrder = order.Count;
                            }
                            List<orderMasterlist> retailer = uniqueCustomer.Where(a => a.createdDate.Date == day).ToList();
                            if (retailer.Count == 0)
                            {
                                c.activeRetailers = 0;
                            }
                            else
                            {
                                c.activeRetailers = retailer.Count;
                            }
                            report.Add(c);
                        }
                        MainReport1.reports = report;
                        MainReport.Add(MainReport1);
                    }
                }
                return MainReport;
            }
            catch (Exception ex)
            {
                logger.Error("Error in OrderMaster " + ex.Message);
                logger.Info("End  OrderMaster: ");
                return null;
            }
        }

        [Route("month")]
        [HttpGet]
        public dynamic GetMonth(DateTime? datefrom, DateTime? dateto, int type, int value, string ids)
        {
            logger.Info("start OrderMaster: ");
            List<MainReports> MainReport = new List<MainReports>();
            DateTime start = DateTime.Parse("01-01-2017 00:00:00");
            DateTime end = DateTime.Today.AddDays(1);
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

                if (datefrom == null)
                {
                    datefrom = DateTime.Parse("01-01-2017 00:00:00");
                    dateto = DateTime.Today.AddDays(1);
                }
                else
                {
                    start = datefrom.GetValueOrDefault();
                    end = dateto.GetValueOrDefault();
                }
                var array = ids.Split(',');
                foreach (var iidd in array)
                {
                    int id = Convert.ToInt32(iidd);
                    var res = getdata(datefrom, dateto, value, id);
                    MainReports MainReport1 = new MainReports();
                    List<orderMasterlist> list = new List<orderMasterlist>();
                    List<orderMasterlist> report = new List<orderMasterlist>();
                    list = res;
                    if (list.Count != 0)
                    {
                        List<orderMasterlist> uniqueMonth = new List<orderMasterlist>();
                        List<orderMasterlist> uniqueOrdered = new List<orderMasterlist>();
                        List<orderMasterlist> uniqueCustomer = new List<orderMasterlist>();
                        foreach (var a in list)
                        {
                            orderMasterlist l = uniqueMonth.Where(x => x.createdDate.Month == a.createdDate.Month && x.createdDate.Year == a.createdDate.Year).SingleOrDefault();
                            if (l == null)
                            {
                                a.TotalAmount = a.TotalAmount;
                                uniqueMonth.Add(a);
                            }
                            else
                            {
                                l.TotalAmount = l.TotalAmount + a.TotalAmount;
                            }
                            orderMasterlist m = uniqueOrdered.Where(c => c.OrderId == a.OrderId && (c.createdDate.Month == a.createdDate.Month && c.createdDate.Year == a.createdDate.Year)).SingleOrDefault();
                            if (m == null)
                            {
                                uniqueOrdered.Add(a);
                            }
                            orderMasterlist n = uniqueCustomer.Where(c => c.retaileId == a.retaileId && (c.createdDate.Month == a.createdDate.Month && c.createdDate.Year == a.createdDate.Year)).SingleOrDefault();
                            if (n == null)
                            {
                                uniqueCustomer.Add(a);
                            }
                        }

                        var d = start.Date;
                        var day = d.Month;
                        var year = d.Year;
                        do
                        {
                            orderMasterlist c = new orderMasterlist();
                            c.name = list[0].name;
                            c.month = d.Month;
                            c.year = d.Year;
                            c.createdDate = new DateTime(d.Year, d.Month, 1);
                            var total = uniqueMonth.Where(a => a.createdDate.Month == c.month && a.createdDate.Year == c.year).FirstOrDefault();
                            if (total == null)
                            {
                                c.TotalAmount = 0.00;
                            }
                            else
                            {
                                c.TotalAmount = total.TotalAmount;
                            }
                            List<orderMasterlist> order = uniqueOrdered.Where(a => a.createdDate.Month == c.month && a.createdDate.Year == c.year).ToList();
                            if (order.Count == 0)
                            {
                                c.totalOrder = 0;
                            }
                            else
                            {
                                c.totalOrder = order.Count;
                            }
                            List<orderMasterlist> retailer = uniqueCustomer.Where(a => a.createdDate.Month == c.month && a.createdDate.Year == c.year).ToList();
                            if (retailer.Count == 0)
                            {
                                c.activeRetailers = 0;
                            }
                            else
                            {
                                c.activeRetailers = retailer.Count;
                            }
                            report.Add(c);
                            d = d.AddMonths(1);
                            day = d.Month;
                            year = d.Year;
                        } while (day <= end.Month && year <= end.Year);
                        MainReport1.reports = report;
                        MainReport.Add(MainReport1);
                    }
                }
                return MainReport;
            }
            catch (Exception ex)
            {
                logger.Error("Error in OrderMaster " + ex.Message);
                logger.Info("End  OrderMaster: ");
                return null;
            }
        }

        [Route("year")]
        [HttpGet]
        public dynamic GetYear(DateTime? datefrom, DateTime? dateto, int type, int value, string ids)
        {
            logger.Info("start OrderMaster: ");
            List<MainReports> MainReport = new List<MainReports>();
            DateTime start = DateTime.Parse("01-01-2017 00:00:00");
            DateTime end = DateTime.Today.AddDays(1);
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

                if (datefrom == null)
                {
                    datefrom = DateTime.Parse("01-01-2017 00:00:00");
                    dateto = DateTime.Today.AddDays(1);
                }
                else
                {
                    start = datefrom.GetValueOrDefault();
                    end = dateto.GetValueOrDefault();
                }
                var array = ids.Split(',');
                foreach (var iidd in array)
                {
                    int id = Convert.ToInt32(iidd);
                    var res = getdata(datefrom, dateto, value, id);
                    MainReports MainReport1 = new MainReports();
                    List<orderMasterlist> list = new List<orderMasterlist>();
                    List<orderMasterlist> report = new List<orderMasterlist>();
                    list = res;
                    if (list.Count != 0)
                    {
                        List<orderMasterlist> uniqueYear = new List<orderMasterlist>();
                        List<orderMasterlist> uniqueOrdered = new List<orderMasterlist>();
                        List<orderMasterlist> uniqueCustomer = new List<orderMasterlist>();
                        foreach (var a in list)
                        {
                            orderMasterlist l = uniqueYear.Where(x => x.createdDate.Year == a.createdDate.Year).SingleOrDefault();
                            if (l == null)
                            {
                                a.TotalAmount = a.TotalAmount;
                                uniqueYear.Add(a);
                            }
                            else
                            {
                                l.TotalAmount = l.TotalAmount + a.TotalAmount;
                            }
                            orderMasterlist m = uniqueOrdered.Where(c => c.OrderId == a.OrderId && c.createdDate.Year == a.createdDate.Year).SingleOrDefault();
                            if (m == null)
                            {
                                uniqueOrdered.Add(a);
                            }
                            orderMasterlist n = uniqueCustomer.Where(c => c.retaileId == a.retaileId && c.createdDate.Year == a.createdDate.Year).SingleOrDefault();
                            if (n == null)
                            {
                                uniqueCustomer.Add(a);
                            }
                        }
                        var d = start.Date;
                        var day = d.Year;
                        do
                        {
                            orderMasterlist c = new orderMasterlist();
                            c.name = list[0].name;
                            c.year = d.Year;
                            c.createdDate = new DateTime(d.Year, 1, 1);
                            var total = uniqueYear.Where(a => a.createdDate.Year == c.year).FirstOrDefault();
                            if (total == null)
                            {
                                c.TotalAmount = 0.00;
                            }
                            else
                            {
                                c.TotalAmount = total.TotalAmount;
                            }
                            List<orderMasterlist> order = uniqueOrdered.Where(a => a.createdDate.Year == c.year).ToList();
                            if (order.Count == 0)
                            {
                                c.totalOrder = 0;
                            }
                            else
                            {
                                c.totalOrder = order.Count;
                            }
                            List<orderMasterlist> retailer = uniqueCustomer.Where(a => a.createdDate.Year == c.year).ToList();
                            if (retailer.Count == 0)
                            {
                                c.activeRetailers = 0;
                            }
                            else
                            {
                                c.activeRetailers = retailer.Count;
                            }
                            report.Add(c);
                            d = d.AddYears(1);
                            day = d.Year;
                        } while (day <= end.Year);

                        MainReport1.reports = report;
                        MainReport.Add(MainReport1);
                    }
                }
                return MainReport;
            }
            catch (Exception ex)
            {
                logger.Error("Error in OrderMaster " + ex.Message);
                logger.Info("End  OrderMaster: ");
                return null;
            }
        }
        
        public List<orderMasterlist> getdata(DateTime? datefrom, DateTime? dateto, int value, int id)
        {
            List<orderMasterlist> list = new List<orderMasterlist>();
            List<orderMasterlist> report = new List<orderMasterlist>();

            if (datefrom != null && dateto != null)
            {
                if (value == 1)
                {
                    var data = (from i in db.DbOrderDetails
                                where i.CreatedDate >= datefrom && i.CreatedDate <= dateto
                                join j in db.itemMasters on i.ItemId equals j.ItemId
                                join k in db.Categorys on j.Categoryid equals k.Categoryid
                                select new orderMasterlist
                                {
                                    cityid = i.CityId,
                                    warehouseid = i.Warehouseid, 
                                    name = k.CategoryName,
                                    id = k.Categoryid,
                                    OrderId = i.OrderId,
                                    retaileId = i.CustomerId,
                                    TotalAmount = i.TotalAmt,
                                    createdDate = i.CreatedDate,
                                    updatedDate = i.UpdatedDate
                                }).OrderBy(x => x.createdDate).ToList();
                    list = data.Where(x => x.id == id).ToList();
                }
                else if (value == 2)
                {
                    var data = (from i in db.DbOrderDetails
                                where i.CreatedDate >= datefrom && i.CreatedDate <= dateto
                                join j in db.itemMasters on i.ItemId equals j.ItemId
                                join k in db.SubCategorys on j.SubCategoryId equals k.SubCategoryId
                                select new orderMasterlist
                                {
                                    cityid = i.CityId,
                                    warehouseid = i.Warehouseid,
                                    name = k.CategoryName.Trim() + " " + k.SubcategoryName.Trim(),
                                    id = k.SubCategoryId,
                                    OrderId = i.OrderId,
                                    retaileId = i.CustomerId,
                                    TotalAmount = i.TotalAmt,
                                    createdDate = i.CreatedDate,
                                    updatedDate = i.UpdatedDate
                                }).OrderBy(x => x.createdDate).ToList();
                    list = data.Where(x => x.id == id).ToList();
                }
                else if (value == 3)
                {
                    var data = (from i in db.DbOrderDetails
                                where i.CreatedDate >= datefrom && i.CreatedDate <= dateto
                                join j in db.itemMasters on i.ItemId equals j.ItemId
                                join k in db.SubsubCategorys on j.SubsubCategoryid equals k.SubsubCategoryid
                                select new orderMasterlist
                                {
                                    cityid = i.CityId,
                                    warehouseid = i.Warehouseid,
                                    name = k.CategoryName.Trim() + " (" + k.SubcategoryName.Trim() + ")" + k.SubsubcategoryName.Trim(),
                                    id = k.SubsubCategoryid,
                                    OrderId = i.OrderId,
                                    retaileId = i.CustomerId,
                                    TotalAmount = i.TotalAmt,
                                    createdDate = i.CreatedDate,
                                    updatedDate = i.UpdatedDate
                                }).OrderBy(x => x.createdDate).ToList();
                    list = data.Where(x => x.id == id).ToList();
                }
                else if (value == 4)
                {
                    list = (from i in db.DbOrderDetails
                            where i.CreatedDate >= datefrom && i.CreatedDate <= dateto && i.ItemId == id
                            select new orderMasterlist
                            {
                                cityid = i.CityId,
                                warehouseid = i.Warehouseid,
                                name = i.itemname,
                                id = i.ItemId,
                                OrderId = i.OrderId,
                                retaileId = i.CustomerId,
                                TotalAmount = i.TotalAmt,
                                createdDate = i.CreatedDate,
                                updatedDate = i.UpdatedDate
                            }).OrderBy(x => x.createdDate).ToList();
                }
                         
            }
            return list;
        }
    }
    public class dataSelect
    {
        public int totalOrder { get; set; }
        public double totalSale { get; set; }
        public int pendingOrder { get; set; }
        public double PendingSale { get; set; }
        public int notDelivered { get; set; }
        public double notDeliveredSale { get; set; }
        public int pendingOrder_2 { get; set; }
        public double PendingSale2 { get; set; }
        public int totalDelivered { get; set; }
        public double deliveredSale { get; set; }
        public int cancelOrder { get; set; }
        public int activeRetailers { get; set; }
        public int activeBrands { get; set; }
    }
    public class orderMasterlist
    {
        public int id { get; set; }
        public string name { get; set; }
        public int OrderId { get; set; }
        public int retaileId { get; set; }
        public int brandId { get; set; }
        public int? cityid { get; set; }
        public int warehouseid { get; set; }
        public int? salespersonid { get; set; }
        public int clusterid { get; set; }

        public int totalOrder { get; set; }
        public double TotalAmount { get; set; }
        public int activeRetailers { get; set; }
        public int activeBrands { get; set; }

        public DateTime createdDate { get; set; }
        public DateTime updatedDate { get; set; }

        public int day { get; set; }
        public int month { get; set; }
        public int year { get; set; }
    }

    public class MainReports
    {
        public List<orderMasterlist> reports { get; set; }
    }
}
