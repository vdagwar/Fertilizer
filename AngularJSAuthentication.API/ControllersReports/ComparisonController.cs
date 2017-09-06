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
    [RoutePrefix("api/Comparison")]
    public class ComparisonController : ApiController
    {       
        AuthContext db = new AuthContext();
        public static Logger logger = LogManager.GetCurrentClassLogger();

        [Route("day")]
        [HttpGet]
        public dynamic GetDay(DateTime? datefrom, DateTime? dateto, int type, string ids , int value, int itemId)
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
                    var res = getdata(datefrom, dateto, type, value, id, itemId);
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
                            orderMasterlist n = uniqueCustomer.Where(c => c.retaileId == a.retaileId && (c.createdDate.Month == a.createdDate.Month && c.createdDate.Year == a.createdDate.Year)).SingleOrDefault();
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
        public dynamic GetMonth(DateTime? datefrom, DateTime? dateto, int type, string ids, int value, int itemId)
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
                    var res = getdata(datefrom, dateto, type, value, id, itemId);
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
        public dynamic GetYear(DateTime? datefrom, DateTime? dateto, int type, string ids, int value, int itemId)
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
                    var res = getdata(datefrom, dateto, type, value, id, itemId); 
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
        
        public List<orderMasterlist> getdata(DateTime? datefrom, DateTime? dateto, int type, int value, int id, int itemId)
        {
            List<orderMasterlist> list = new List<orderMasterlist>();
            List<orderMasterlist> report = new List<orderMasterlist>();

            if (datefrom != null && dateto != null)
            {
                if (type ==1)
                {
                    if (value == 1)
                    {
                        var data = (from i in db.DbOrderDetails
                                    where i.CreatedDate >= datefrom && i.CreatedDate <= dateto && i.CustomerId == id
                                    join j in db.itemMasters on i.ItemId equals j.ItemId
                                    join c in db.Customers on i.CustomerId equals c.CustomerId
                                    join k in db.Categorys on j.Categoryid equals k.Categoryid
                                    select new orderMasterlist
                                    {
                                        cityid = i.CityId,
                                        warehouseid = i.Warehouseid,
                                        name = c.Skcode,
                                        id = k.Categoryid,
                                        OrderId = i.OrderId,
                                        retaileId = i.CustomerId,
                                        TotalAmount = i.TotalAmt,
                                        createdDate = i.CreatedDate,
                                        updatedDate = i.UpdatedDate
                                    }).OrderBy(x => x.createdDate).ToList();
                        list = data.Where(x => x.id == itemId).ToList();
                    }
                    else if (value == 2)
                    {
                        var data = (from i in db.DbOrderDetails
                                    where i.CreatedDate >= datefrom && i.CreatedDate <= dateto && i.CustomerId == id
                                    join j in db.itemMasters on i.ItemId equals j.ItemId
                                    join c in db.Customers on i.CustomerId equals c.CustomerId
                                    join k in db.SubCategorys on j.SubCategoryId equals k.SubCategoryId
                                    select new orderMasterlist
                                    {
                                        cityid = i.CityId,
                                        warehouseid = i.Warehouseid,
                                        name = c.Skcode,
                                        id = k.SubCategoryId,
                                        OrderId = i.OrderId,
                                        retaileId = i.CustomerId,
                                        TotalAmount = i.TotalAmt,
                                        createdDate = i.CreatedDate,
                                        updatedDate = i.UpdatedDate
                                    }).OrderBy(x => x.createdDate).ToList();
                        list = data.Where(x => x.id == itemId).ToList();
                    }
                    else if (value == 3)
                    {
                        var data = (from i in db.DbOrderDetails
                                    where i.CreatedDate >= datefrom && i.CreatedDate <= dateto && i.CustomerId == id
                                    join j in db.itemMasters on i.ItemId equals j.ItemId
                                    join c in db.Customers on i.CustomerId equals c.CustomerId
                                    join k in db.SubsubCategorys on j.SubsubCategoryid equals k.SubsubCategoryid
                                    select new orderMasterlist
                                    {
                                        cityid = i.CityId,
                                        warehouseid = i.Warehouseid,
                                        name = c.Skcode,
                                        id = k.SubsubCategoryid,
                                        OrderId = i.OrderId,
                                        retaileId = i.CustomerId,
                                        TotalAmount = i.TotalAmt,
                                        createdDate = i.CreatedDate,
                                        updatedDate = i.UpdatedDate
                                    }).OrderBy(x => x.createdDate).ToList();
                        list = data.Where(x => x.id == itemId).ToList();
                    }
                    else if (value == 4)
                    {
                        list = (from i in db.DbOrderDetails
                                where i.CreatedDate >= datefrom && i.CreatedDate <= dateto && i.ItemId == itemId && i.CustomerId == id
                                join c in db.Customers on i.CustomerId equals c.CustomerId
                                select new orderMasterlist
                                {
                                    cityid = i.CityId,
                                    warehouseid = i.Warehouseid,
                                    name = c.Skcode,
                                    id = i.ItemId,
                                    OrderId = i.OrderId,
                                    retaileId = i.CustomerId,
                                    TotalAmount = i.TotalAmt,
                                    createdDate = i.CreatedDate,
                                    updatedDate = i.UpdatedDate
                                }).OrderBy(x => x.createdDate).ToList();
                    }
                }     
                else if (type == 2)
                {
                    if (value == 1)
                    {
                        var data = (from i in db.DbOrderDetails
                                    where i.CreatedDate >= datefrom && i.CreatedDate <= dateto && i.Warehouseid == id
                                    join j in db.itemMasters on i.ItemId equals j.ItemId
                                    join k in db.Categorys on j.Categoryid equals k.Categoryid
                                    select new orderMasterlist
                                    {
                                        cityid = i.CityId,
                                        warehouseid = i.Warehouseid,
                                        name = i.WarehouseName,
                                        id = k.Categoryid,
                                        OrderId = i.OrderId,
                                        retaileId = i.CustomerId,
                                        TotalAmount = i.TotalAmt,
                                        createdDate = i.CreatedDate,
                                        updatedDate = i.UpdatedDate
                                    }).OrderBy(x => x.createdDate).ToList();
                        list = data.Where(x => x.id == itemId).ToList();
                    }
                    else if (value == 2)
                    {
                        var data = (from i in db.DbOrderDetails
                                    where i.CreatedDate >= datefrom && i.CreatedDate <= dateto && i.Warehouseid == id
                                    join j in db.itemMasters on i.ItemId equals j.ItemId
                                    join k in db.SubCategorys on j.SubCategoryId equals k.SubCategoryId
                                    select new orderMasterlist
                                    {
                                        cityid = i.CityId,
                                        warehouseid = i.Warehouseid,
                                        name = i.WarehouseName,
                                        id = k.SubCategoryId,
                                        OrderId = i.OrderId,
                                        retaileId = i.CustomerId,
                                        TotalAmount = i.TotalAmt,
                                        createdDate = i.CreatedDate,
                                        updatedDate = i.UpdatedDate
                                    }).OrderBy(x => x.createdDate).ToList();
                        list = data.Where(x => x.id == itemId).ToList();
                    }
                    else if (value == 3)
                    {
                        var data = (from i in db.DbOrderDetails
                                    where i.CreatedDate >= datefrom && i.CreatedDate <= dateto && i.Warehouseid == id
                                    join j in db.itemMasters on i.ItemId equals j.ItemId
                                    join k in db.SubsubCategorys on j.SubsubCategoryid equals k.SubsubCategoryid
                                    select new orderMasterlist
                                    {
                                        cityid = i.CityId,
                                        warehouseid = i.Warehouseid,
                                        name = i.WarehouseName,
                                        id = k.SubsubCategoryid,
                                        OrderId = i.OrderId,
                                        retaileId = i.CustomerId,
                                        TotalAmount = i.TotalAmt,
                                        createdDate = i.CreatedDate,
                                        updatedDate = i.UpdatedDate
                                    }).OrderBy(x => x.createdDate).ToList();
                        list = data.Where(x => x.id == itemId).ToList();
                    }
                    else if (value == 4)
                    {
                        list = (from i in db.DbOrderDetails
                                where i.CreatedDate >= datefrom && i.CreatedDate <= dateto && i.ItemId == itemId && i.Warehouseid == id
                                select new orderMasterlist
                                {
                                    cityid = i.CityId,
                                    warehouseid = i.Warehouseid,
                                    name = i.WarehouseName,
                                    id = i.ItemId,
                                    OrderId = i.OrderId,
                                    retaileId = i.CustomerId,
                                    TotalAmount = i.TotalAmt,
                                    createdDate = i.CreatedDate,
                                    updatedDate = i.UpdatedDate
                                }).OrderBy(x => x.createdDate).ToList();
                    }
                }
                else if (type == 3)
                {
                    if (value == 1)
                    {
                        var data = (from i in db.DbOrderDetails
                                    where i.CreatedDate >= datefrom && i.CreatedDate <= dateto && i.CityId == id
                                    join j in db.itemMasters on i.ItemId equals j.ItemId
                                    join k in db.Categorys on j.Categoryid equals k.Categoryid
                                    select new orderMasterlist
                                    {
                                        cityid = i.CityId,
                                        warehouseid = i.Warehouseid,
                                        name = i.City,
                                        id = k.Categoryid,
                                        OrderId = i.OrderId,
                                        retaileId = i.CustomerId,
                                        TotalAmount = i.TotalAmt,
                                        createdDate = i.CreatedDate,
                                        updatedDate = i.UpdatedDate
                                    }).OrderBy(x => x.createdDate).ToList();
                        list = data.Where(x => x.id == itemId).ToList();
                    }
                    else if (value == 2)
                    {
                        var data = (from i in db.DbOrderDetails
                                    where i.CreatedDate >= datefrom && i.CreatedDate <= dateto && i.CityId == id
                                    join j in db.itemMasters on i.ItemId equals j.ItemId
                                    join k in db.SubCategorys on j.SubCategoryId equals k.SubCategoryId
                                    select new orderMasterlist
                                    {
                                        cityid = i.CityId,
                                        warehouseid = i.Warehouseid,
                                        name = i.City,
                                        id = k.SubCategoryId,
                                        OrderId = i.OrderId,
                                        retaileId = i.CustomerId,
                                        TotalAmount = i.TotalAmt,
                                        createdDate = i.CreatedDate,
                                        updatedDate = i.UpdatedDate
                                    }).OrderBy(x => x.createdDate).ToList();
                        list = data.Where(x => x.id == itemId).ToList();
                    }
                    else if (value == 3)
                    {
                        var data = (from i in db.DbOrderDetails
                                    where i.CreatedDate >= datefrom && i.CreatedDate <= dateto && i.CityId == id
                                    join j in db.itemMasters on i.ItemId equals j.ItemId
                                    join k in db.SubsubCategorys on j.SubsubCategoryid equals k.SubsubCategoryid
                                    select new orderMasterlist
                                    {
                                        cityid = i.CityId,
                                        warehouseid = i.Warehouseid,
                                        name = i.City,
                                        id = k.SubsubCategoryid,
                                        OrderId = i.OrderId,
                                        retaileId = i.CustomerId,
                                        TotalAmount = i.TotalAmt,
                                        createdDate = i.CreatedDate,
                                        updatedDate = i.UpdatedDate
                                    }).OrderBy(x => x.createdDate).ToList();
                        list = data.Where(x => x.id == itemId).ToList();
                    }
                    else if (value == 4)
                    {
                        list = (from i in db.DbOrderDetails
                                where i.CreatedDate >= datefrom && i.CreatedDate <= dateto && i.ItemId == itemId && i.Warehouseid == id
                                select new orderMasterlist
                                {
                                    cityid = i.CityId,
                                    warehouseid = i.Warehouseid,
                                    name = i.City,
                                    id = i.ItemId,
                                    OrderId = i.OrderId,
                                    retaileId = i.CustomerId,
                                    TotalAmount = i.TotalAmt,
                                    createdDate = i.CreatedDate,
                                    updatedDate = i.UpdatedDate
                                }).OrderBy(x => x.createdDate).ToList();
                    }
                }
                else if (type == 4)
                {
                    if (value == 1)
                    {
                        var data = (from i in db.DbOrderDetails
                                    where i.CreatedDate >= datefrom && i.CreatedDate <= dateto
                                    join j in db.itemMasters on i.ItemId equals j.ItemId
                                    join e in db.DbOrderMaster on i.OrderId equals e.OrderId
                                    join k in db.Categorys on j.Categoryid equals k.Categoryid
                                    select new orderMasterlist
                                    {
                                        salespersonid = e.SalesPersonId,
                                        name = e.SalesPerson,
                                        cityid = i.CityId,
                                        warehouseid = i.Warehouseid,
                                        id = k.Categoryid,
                                        OrderId = i.OrderId,
                                        retaileId = i.CustomerId,
                                        TotalAmount = i.TotalAmt,
                                        createdDate = i.CreatedDate,
                                        updatedDate = i.UpdatedDate
                                    }).OrderBy(x => x.createdDate).ToList();
                        list = data.Where(x => x.id == itemId && x.salespersonid == id).ToList();
                    }
                    else if (value == 2)
                    {
                        var data = (from i in db.DbOrderDetails
                                    where i.CreatedDate >= datefrom && i.CreatedDate <= dateto
                                    join j in db.itemMasters on i.ItemId equals j.ItemId
                                    join e in db.DbOrderMaster on i.OrderId equals e.OrderId
                                    join k in db.SubCategorys on j.SubCategoryId equals k.SubCategoryId
                                    select new orderMasterlist
                                    {
                                        salespersonid = e.SalesPersonId,
                                        name = e.SalesPerson,
                                        cityid = i.CityId,
                                        warehouseid = i.Warehouseid,
                                        id = k.SubCategoryId,
                                        OrderId = i.OrderId,
                                        retaileId = i.CustomerId,
                                        TotalAmount = i.TotalAmt,
                                        createdDate = i.CreatedDate,
                                        updatedDate = i.UpdatedDate
                                    }).OrderBy(x => x.createdDate).ToList();
                        list = data.Where(x => x.id == itemId && x.salespersonid == id).ToList();
                    }
                    else if (value == 3)
                    {
                        var data = (from i in db.DbOrderDetails
                                    where i.CreatedDate >= datefrom && i.CreatedDate <= dateto 
                                    join j in db.itemMasters on i.ItemId equals j.ItemId
                                    join e in db.DbOrderMaster on i.OrderId equals e.OrderId
                                    join k in db.SubsubCategorys on j.SubsubCategoryid equals k.SubsubCategoryid
                                    select new orderMasterlist
                                    {
                                        salespersonid = e.SalesPersonId,
                                        name = e.SalesPerson,
                                        cityid = i.CityId,
                                        warehouseid = i.Warehouseid,
                                        id = k.SubsubCategoryid,
                                        OrderId = i.OrderId,
                                        retaileId = i.CustomerId,
                                        TotalAmount = i.TotalAmt,
                                        createdDate = i.CreatedDate,
                                        updatedDate = i.UpdatedDate
                                    }).OrderBy(x => x.createdDate).ToList();
                        list = data.Where(x => x.id == itemId && x.salespersonid == id).ToList();
                    }
                    else if (value == 4)
                    {
                        var data = (from i in db.DbOrderDetails
                                where i.CreatedDate >= datefrom && i.CreatedDate <= dateto && i.ItemId == itemId
                                join e in db.DbOrderMaster on i.OrderId equals e.OrderId
                                select new orderMasterlist
                                {
                                    salespersonid = e.SalesPersonId,
                                    name = e.SalesPerson,
                                    cityid = i.CityId,
                                    warehouseid = i.Warehouseid,
                                    id = i.ItemId,
                                    OrderId = i.OrderId,
                                    retaileId = i.CustomerId,
                                    TotalAmount = i.TotalAmt,
                                    createdDate = i.CreatedDate,
                                    updatedDate = i.UpdatedDate
                                }).OrderBy(x => x.createdDate).ToList();
                        list = data.Where(x => x.salespersonid == id).ToList();
                    }
                }
                else if (type == 5)
                {
                    if (value == 1)
                    {
                        var data = (from i in db.DbOrderDetails
                                    where i.CreatedDate >= datefrom && i.CreatedDate <= dateto
                                    join j in db.itemMasters on i.ItemId equals j.ItemId
                                    join e in db.DbOrderMaster on i.OrderId equals e.OrderId
                                    join k in db.Categorys on j.Categoryid equals k.Categoryid
                                    select new orderMasterlist
                                    {
                                        clusterid = e.ClusterId,
                                        name = e.ClusterName,
                                        cityid = i.CityId,
                                        warehouseid = i.Warehouseid,
                                        id = k.Categoryid,
                                        OrderId = i.OrderId,
                                        retaileId = i.CustomerId,
                                        TotalAmount = i.TotalAmt,
                                        createdDate = i.CreatedDate,
                                        updatedDate = i.UpdatedDate
                                    }).OrderBy(x => x.createdDate).ToList();
                        list = data.Where(x => x.id == itemId && x.clusterid == id).ToList();
                    }
                    else if (value == 2)
                    {
                        var data = (from i in db.DbOrderDetails
                                    where i.CreatedDate >= datefrom && i.CreatedDate <= dateto
                                    join j in db.itemMasters on i.ItemId equals j.ItemId
                                    join e in db.DbOrderMaster on i.OrderId equals e.OrderId
                                    join k in db.SubCategorys on j.SubCategoryId equals k.SubCategoryId
                                    select new orderMasterlist
                                    {
                                        clusterid = e.ClusterId,
                                        name = e.ClusterName,
                                        cityid = i.CityId,
                                        warehouseid = i.Warehouseid,
                                        id = k.SubCategoryId,
                                        OrderId = i.OrderId,
                                        retaileId = i.CustomerId,
                                        TotalAmount = i.TotalAmt,
                                        createdDate = i.CreatedDate,
                                        updatedDate = i.UpdatedDate
                                    }).OrderBy(x => x.createdDate).ToList();
                        list = data.Where(x => x.id == itemId && x.clusterid == id).ToList();
                    }
                    else if (value == 3)
                    {
                        var data = (from i in db.DbOrderDetails
                                    where i.CreatedDate >= datefrom && i.CreatedDate <= dateto
                                    join j in db.itemMasters on i.ItemId equals j.ItemId
                                    join e in db.DbOrderMaster on i.OrderId equals e.OrderId
                                    join k in db.SubsubCategorys on j.SubsubCategoryid equals k.SubsubCategoryid
                                    select new orderMasterlist
                                    {
                                        clusterid = e.ClusterId,
                                        name = e.ClusterName,
                                        cityid = i.CityId,
                                        warehouseid = i.Warehouseid,
                                        id = k.SubsubCategoryid,
                                        OrderId = i.OrderId,
                                        retaileId = i.CustomerId,
                                        TotalAmount = i.TotalAmt,
                                        createdDate = i.CreatedDate,
                                        updatedDate = i.UpdatedDate
                                    }).OrderBy(x => x.createdDate).ToList();
                        list = data.Where(x => x.id == itemId && x.clusterid == id).ToList();
                    }
                    else if (value == 4)
                    {
                        var data = (from i in db.DbOrderDetails
                                    where i.CreatedDate >= datefrom && i.CreatedDate <= dateto && i.ItemId == itemId
                                    join e in db.DbOrderMaster on i.OrderId equals e.OrderId
                                    select new orderMasterlist
                                    {
                                        clusterid = e.ClusterId,
                                        name = e.ClusterName,
                                        cityid = i.CityId,
                                        warehouseid = i.Warehouseid,
                                        id = i.ItemId,
                                        OrderId = i.OrderId,
                                        retaileId = i.CustomerId,
                                        TotalAmount = i.TotalAmt,
                                        createdDate = i.CreatedDate,
                                        updatedDate = i.UpdatedDate
                                    }).OrderBy(x => x.createdDate).ToList();
                        list = data.Where(x => x.clusterid == id).ToList();
                    }
                }
            }
            return list;
        }
    }
   
}
