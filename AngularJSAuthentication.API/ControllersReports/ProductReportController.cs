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
    [RoutePrefix("api/ProductReport")]
    public class ProductReportController : ApiController
    {
        AuthContext db = new AuthContext();
        public static Logger logger = LogManager.GetCurrentClassLogger();

        [Route("day")]
        [HttpGet]
        public dynamic GetDay(DateTime? datefrom, DateTime? dateto, int type, string ids, int value, int itemId)
        {
            logger.Info("start OrderMaster: ");
            List<MainProductReports> MainReport = new List<MainProductReports>();
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
                    sdt twolist = new sdt();
                    twolist = getdata(datefrom, dateto, type, value, id, itemId);
                    MainProductReports MainReport1 = new MainProductReports();
                    List<ProductMasterlist> report = new List<ProductMasterlist>();
                    List<ProductMasterlist> report2 = new List<ProductMasterlist>();
                    //list = res;
                    if (twolist.finalsdt.Count == twolist.finalsdt1.Count)
                    {
                        List<ProductMasterlist> uniqueDay = new List<ProductMasterlist>();
                        List<ProductMasterlist> uniqueDay2 = new List<ProductMasterlist>();
                      
                        foreach (var a in twolist.finalsdt)
                        {
                            ProductMasterlist l = uniqueDay.Where(x => x.createdDate.Date == a.createdDate.Date).SingleOrDefault();
                            if (l == null)
                            {
                                a.TotalAmount = a.TotalAmount;
                                a.totalOrderQty = a.qty;
                                uniqueDay.Add(a);
                            }
                            else
                            {
                                l.TotalAmount = l.TotalAmount + a.TotalAmount;
                                l.totalOrderQty = l.totalOrderQty + a.qty;
                            }
                        }
                        for (var day = start.Date; day.Date <= end.Date; day = day.AddDays(1))
                        {
                            ProductMasterlist c = new ProductMasterlist();
                            c.name = twolist.finalsdt[0].name;
                            c.day = day.Day;
                            c.month = day.Month;
                            c.year = day.Year;
                            c.createdDate = day;
                            var total = uniqueDay.Where(a => a.createdDate.Date == day).FirstOrDefault();
                            if (total == null)
                            {
                                c.TotalAmount = 0.00;
                                c.totalOrderQty = 0;
                            }
                            else
                            {
                                c.TotalAmount = total.TotalAmount;
                                c.totalOrderQty = total.totalOrderQty;
                            }
                            report.Add(c);
                        }
                     
                        // for 2nd list

                        foreach (var ab in twolist.finalsdt1)
                            {
                                ProductMasterlist ll = uniqueDay2.Where(x => x.createdDate.Date == ab.createdDate.Date).SingleOrDefault();
                                if (ll == null)
                                {
                                    ab.TotalAmount = ab.TotalAmount;
                                    ab.totalOrderQty = ab.qty;

                                    uniqueDay2.Add(ab);
                                }
                                else
                                {
                                    ll.TotalAmount = ll.TotalAmount + ab.TotalAmount;
                                    ll.totalOrderQty = ll.totalOrderQty + ab.qty;
                                }
                            }

                        for (var days = start.Date; days.Date <= end.Date; days = days.AddDays(1))
                        {
                            ProductMasterlist cc = new ProductMasterlist();

                            cc.name = twolist.finalsdt1[0].name;
                            cc.day = days.Day;
                            cc.month = days.Month;
                            cc.year = days.Year;
                            cc.createdDate = days;
                            var totall = uniqueDay2.Where(a => a.createdDate.Date == days).FirstOrDefault();
                            if (totall == null)
                            {
                                cc.TotalAmount = 0.00;
                                cc.totalOrderQty = 0;
                            }
                            else
                            {
                                cc.TotalAmount = totall.TotalAmount;
                                cc.totalOrderQty = totall.totalOrderQty;
                            }
                            report2.Add(cc);

                           
                        }
                        MainReport1.reports = report;

                        MainReport1.reports2 = report2;

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

     
        public sdt getdata(DateTime? datefrom, DateTime? dateto, int type, int value, int id, int itemId)
        {
            sdt twolist = new sdt();
            twolist.finalsdt = new List<ProductMasterlist>();
            twolist.finalsdt1 = new List<ProductMasterlist>();
            List<ProductMasterlist> report = new List<ProductMasterlist>();

            if (datefrom != null && dateto != null)
            {
                if (type == 1)
                {
                    
                     if (value == 3)
                    {
                        var data = (from i in db.DbOrderDetails
                                    where i.CreatedDate >= datefrom && i.CreatedDate <= dateto && i.CustomerId == id
                                    join j in db.itemMasters on i.ItemId equals j.ItemId
                                    join c in db.Customers on i.CustomerId equals c.CustomerId
                                    join k in db.SubsubCategorys on j.SubsubCategoryid equals k.SubsubCategoryid
                                    select new ProductMasterlist
                                    {
                                        cityid = i.CityId,
                                        warehouseid = i.Warehouseid,
                                        name = c.Skcode,
                                        id = k.SubsubCategoryid,
                                        OrderId = i.OrderId,
                                        qty = i.qty,
                                        retaileId = i.CustomerId,
                                        TotalAmount = i.TotalAmt,
                                        createdDate = i.CreatedDate,
                                        updatedDate = i.UpdatedDate
                                    }).OrderBy(x => x.createdDate).ToList();
                        twolist.finalsdt = data.Where(x => x.id == itemId).ToList();

                        var data1 = (from i in db.OrderDispatchedDetailss
                                     where i.CreatedDate >= datefrom && i.CreatedDate <= dateto && i.CustomerId == id
                                    join j in db.itemMasters on i.ItemId equals j.ItemId
                                    join c in db.Customers on i.CustomerId equals c.CustomerId
                                    join k in db.SubsubCategorys on j.SubsubCategoryid equals k.SubsubCategoryid
                                    select new ProductMasterlist
                                    {
                                        cityid = i.CityId,
                                        warehouseid = i.Warehouseid,
                                        name = c.Skcode,
                                        id = k.SubsubCategoryid,
                                        OrderId = i.OrderId,
                                        qty = i.qty,
                                        retaileId = i.CustomerId,
                                        TotalAmount = i.TotalAmt,
                                        createdDate = i.CreatedDate,
                                        updatedDate = i.UpdatedDate
                                    }).OrderBy(x => x.createdDate).ToList();
                        twolist.finalsdt1 = data1.Where(x => x.id == itemId).ToList();
                    }
                    else if (value == 4)
                    {
                        twolist.finalsdt = (from i in db.DbOrderDetails
                                where i.CreatedDate >= datefrom && i.CreatedDate <= dateto && i.ItemId == itemId && i.CustomerId == id
                                join c in db.Customers on i.CustomerId equals c.CustomerId
                                select new ProductMasterlist
                                {
                                    cityid = i.CityId,
                                    warehouseid = i.Warehouseid,
                                    name = c.Skcode,
                                    id = i.ItemId,
                                    qty = i.qty,
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
                    
                  if (value == 3)
                    {
                        var data = (from i in db.DbOrderDetails
                                    where i.CreatedDate >= datefrom && i.CreatedDate <= dateto && i.Warehouseid == id
                                    join j in db.itemMasters on i.ItemId equals j.ItemId
                                    join k in db.SubsubCategorys on j.SubsubCategoryid equals k.SubsubCategoryid
                                    select new ProductMasterlist
                                    {
                                        cityid = i.CityId,
                                        warehouseid = i.Warehouseid,
                                        name = i.WarehouseName,
                                        id = k.SubsubCategoryid,
                                        OrderId = i.OrderId,
                                        qty = i.qty,
                                        retaileId = i.CustomerId,
                                        TotalAmount = i.TotalAmt,
                                        createdDate = i.CreatedDate,
                                        updatedDate = i.UpdatedDate
                                    }).OrderBy(x => x.createdDate).ToList();
                        twolist.finalsdt = data.ToList();
                        var data1 = (from i in db.OrderDispatchedDetailss
                                     where i.CreatedDate >= datefrom && i.CreatedDate <= dateto && i.Warehouseid == id
                                    join j in db.itemMasters on i.ItemId equals j.ItemId
                                    join k in db.SubsubCategorys on j.SubsubCategoryid equals k.SubsubCategoryid
                                    select new ProductMasterlist
                                    {
                                        cityid = i.CityId,
                                        warehouseid = i.Warehouseid,
                                        name = i.WarehouseName,
                                        id = k.SubsubCategoryid,
                                        OrderId = i.OrderId,
                                        qty = i.qty,
                                        retaileId = i.CustomerId,
                                        TotalAmount = i.TotalAmt,
                                        createdDate = i.CreatedDate,
                                        updatedDate = i.UpdatedDate
                                    }).OrderBy(x => x.createdDate).ToList();
                        twolist.finalsdt1 = data1.ToList();
                    }
                    else if (value == 4)
                    {
                        twolist.finalsdt = (from i in db.DbOrderDetails
                                where i.CreatedDate >= datefrom && i.CreatedDate <= dateto && i.ItemId == itemId && i.Warehouseid == id
                                select new ProductMasterlist
                                {
                                    cityid = i.CityId,
                                    warehouseid = i.Warehouseid,
                                    name = i.WarehouseName,
                                    id = i.ItemId,
                                    qty = i.qty,
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
                   
                    if (value == 3)
                    {
                        var data = (from i in db.DbOrderDetails
                                    where i.CreatedDate >= datefrom && i.CreatedDate <= dateto && i.CityId == id
                                    join j in db.itemMasters on i.ItemId equals j.ItemId
                                    join k in db.SubsubCategorys on j.SubsubCategoryid equals k.SubsubCategoryid
                                    select new ProductMasterlist
                                    {
                                        cityid = i.CityId,
                                        warehouseid = i.Warehouseid,
                                        name = i.City,
                                        id = k.SubsubCategoryid,
                                        OrderId = i.OrderId,
                                        qty = i.qty,
                                        retaileId = i.CustomerId,
                                        TotalAmount = i.TotalAmt,
                                        createdDate = i.CreatedDate,
                                        updatedDate = i.UpdatedDate
                                    }).OrderBy(x => x.createdDate).ToList();
                        twolist.finalsdt = data.Where(x => x.id == itemId).ToList();

                        var data1 = (from i in db.OrderDispatchedDetailss
                                     where i.CreatedDate >= datefrom && i.CreatedDate <= dateto && i.CityId == id
                                    join j in db.itemMasters on i.ItemId equals j.ItemId
                                    join k in db.SubsubCategorys on j.SubsubCategoryid equals k.SubsubCategoryid
                                    select new ProductMasterlist
                                    {
                                        cityid = i.CityId,
                                        warehouseid = i.Warehouseid,
                                        name = i.City,
                                        id = k.SubsubCategoryid,
                                        OrderId = i.OrderId,
                                        qty = i.qty,
                                        retaileId = i.CustomerId,
                                        TotalAmount = i.TotalAmt,
                                        createdDate = i.CreatedDate,
                                        updatedDate = i.UpdatedDate
                                    }).OrderBy(x => x.createdDate).ToList();
                        twolist.finalsdt1 = data1.Where(x => x.id == itemId).ToList();
                    }
                    else if (value == 4)
                    {
                        twolist.finalsdt = (from i in db.DbOrderDetails
                                where i.CreatedDate >= datefrom && i.CreatedDate <= dateto && i.ItemId == itemId && i.Warehouseid == id
                                select new ProductMasterlist
                                {
                                    cityid = i.CityId,
                                    warehouseid = i.Warehouseid,
                                    name = i.City,
                                    id = i.ItemId,
                                    qty = i.qty,
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
                    
                   if (value == 3)
                    {
                        var data = (from i in db.DbOrderDetails
                                    where i.CreatedDate >= datefrom && i.CreatedDate <= dateto
                                    join j in db.itemMasters on i.ItemId equals j.ItemId
                                    join e in db.DbOrderMaster on i.OrderId equals e.OrderId
                                    join k in db.SubsubCategorys on j.SubsubCategoryid equals k.SubsubCategoryid
                                    select new ProductMasterlist
                                    {
                                        salespersonid = e.SalesPersonId,
                                        name = e.SalesPerson,
                                        cityid = i.CityId,
                                        qty = i.qty,
                                        warehouseid = i.Warehouseid,
                                        id = k.SubsubCategoryid,
                                        OrderId = i.OrderId,
                                        retaileId = i.CustomerId,
                                        TotalAmount = i.TotalAmt,
                                        createdDate = i.CreatedDate,
                                        updatedDate = i.UpdatedDate
                                    }).OrderBy(x => x.createdDate).ToList();
                        twolist.finalsdt = data.Where(x => x.id == itemId && x.salespersonid == id).ToList();
                        var data1 = (from i in db.OrderDispatchedDetailss
                                     where i.CreatedDate >= datefrom && i.CreatedDate <= dateto
                                    join j in db.itemMasters on i.ItemId equals j.ItemId
                                    join e in db.DbOrderMaster on i.OrderId equals e.OrderId
                                    join k in db.SubsubCategorys on j.SubsubCategoryid equals k.SubsubCategoryid
                                    select new ProductMasterlist
                                    {
                                        salespersonid = e.SalesPersonId,
                                        name = e.SalesPerson,
                                        cityid = i.CityId,
                                        qty = i.qty,
                                        warehouseid = i.Warehouseid,
                                        id = k.SubsubCategoryid,
                                        OrderId = i.OrderId,
                                        retaileId = i.CustomerId,
                                        TotalAmount = i.TotalAmt,
                                        createdDate = i.CreatedDate,
                                        updatedDate = i.UpdatedDate
                                    }).OrderBy(x => x.createdDate).ToList();
                        twolist.finalsdt1 = data1.Where(x => x.id == itemId && x.salespersonid == id).ToList();
                    }
                    else if (value == 4)
                    {
                        var data = (from i in db.DbOrderDetails
                                    where i.CreatedDate >= datefrom && i.CreatedDate <= dateto && i.ItemId == itemId
                                    join e in db.DbOrderMaster on i.OrderId equals e.OrderId
                                    select new ProductMasterlist
                                    {
                                        salespersonid = e.SalesPersonId,
                                        name = e.SalesPerson,
                                        cityid = i.CityId,
                                        warehouseid = i.Warehouseid,
                                        id = i.ItemId,
                                        qty = i.qty,
                                        OrderId = i.OrderId,
                                        retaileId = i.CustomerId,
                                        TotalAmount = i.TotalAmt,
                                        createdDate = i.CreatedDate,
                                        updatedDate = i.UpdatedDate
                                    }).OrderBy(x => x.createdDate).ToList();
                        twolist.finalsdt = data.Where(x => x.salespersonid == id).ToList();
                    }
                }
                else if (type == 5)
                {
                   
                if (value == 3)
                    {
                        var data = (from i in db.DbOrderDetails
                                    where i.CreatedDate >= datefrom && i.CreatedDate <= dateto
                                    join j in db.itemMasters on i.ItemId equals j.ItemId
                                    join e in db.DbOrderMaster on i.OrderId equals e.OrderId
                                    join k in db.SubsubCategorys on j.SubsubCategoryid equals k.SubsubCategoryid
                                    select new ProductMasterlist
                                    {
                                        clusterid = e.ClusterId,
                                        name = e.ClusterName,
                                        cityid = i.CityId,
                                        warehouseid = i.Warehouseid,
                                        id = k.SubsubCategoryid,
                                        OrderId = i.OrderId,
                                        qty = i.qty,
                                        retaileId = i.CustomerId,
                                        TotalAmount = i.TotalAmt,
                                        createdDate = i.CreatedDate,
                                        updatedDate = i.UpdatedDate
                                    }).OrderBy(x => x.createdDate).ToList();
                        twolist.finalsdt = data.Where(x => x.id == itemId && x.clusterid == id).ToList();
                        var data1 = (from i in db.OrderDispatchedDetailss
                                     where i.CreatedDate >= datefrom && i.CreatedDate <= dateto
                                    join j in db.itemMasters on i.ItemId equals j.ItemId
                                    join e in db.DbOrderMaster on i.OrderId equals e.OrderId
                                    join k in db.SubsubCategorys on j.SubsubCategoryid equals k.SubsubCategoryid
                                    select new ProductMasterlist
                                    {
                                        clusterid = e.ClusterId,
                                        name = e.ClusterName,
                                        cityid = i.CityId,
                                        warehouseid = i.Warehouseid,
                                        id = k.SubsubCategoryid,
                                        OrderId = i.OrderId,
                                        qty = i.qty,
                                        retaileId = i.CustomerId,
                                        TotalAmount = i.TotalAmt,
                                        createdDate = i.CreatedDate,
                                        updatedDate = i.UpdatedDate
                                    }).OrderBy(x => x.createdDate).ToList();
                        twolist.finalsdt1 = data1.Where(x => x.id == itemId && x.clusterid == id).ToList();
                    }
                    else if (value == 4)
                    {
                        var data = (from i in db.DbOrderDetails
                                    where i.CreatedDate >= datefrom && i.CreatedDate <= dateto && i.ItemId == itemId
                                    join e in db.DbOrderMaster on i.OrderId equals e.OrderId
                                    select new ProductMasterlist
                                    {
                                        clusterid = e.ClusterId,
                                        name = e.ClusterName,
                                        qty = i.qty,
                                        cityid = i.CityId,
                                        warehouseid = i.Warehouseid,
                                        id = i.ItemId,
                                        OrderId = i.OrderId,
                                        retaileId = i.CustomerId,
                                        TotalAmount = i.TotalAmt,
                                        createdDate = i.CreatedDate,
                                        updatedDate = i.UpdatedDate
                                    }).OrderBy(x => x.createdDate).ToList();
                        twolist.finalsdt = data.Where(x => x.clusterid == id).ToList();
                    }
                }
            }
            return twolist;
         
        }
    }
    public class ProductMasterlist
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
        public int qty { get; set; }
        public int totalOrder { get; set; }

        public int totalOrderQty { get; set; }
        public int totaldisOrderQty { get; set; }
        public double OrderedFillRate { get; set; }
        public double dispatchedFillRate { get; set; }
        public double TotalAmount { get; set; }
        public double UnitPrice { get; set; }
        public int activeRetailers { get; set; }
        public int activeBrands { get; set; }

        public DateTime createdDate { get; set; }
        public DateTime updatedDate { get; set; }

        public int day { get; set; }
        public int month { get; set; }
        public int year { get; set; }
    }

    public class sdt {
        public List<ProductMasterlist> finalsdt {get; set;}
        public List<ProductMasterlist> finalsdt1 { get; set; }
    }

    public class MainProductReports
    {
        public List<ProductMasterlist> reports { get; set; }
        public List<ProductMasterlist> reports2 { get; set; }
    }
}
