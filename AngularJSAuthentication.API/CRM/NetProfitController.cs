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
    [RoutePrefix("api/NetProfit")]
    public class NetProfitController : ApiController
    {
        AuthContext db = new AuthContext();
        public static Logger logger = LogManager.GetCurrentClassLogger();

        [Route("")]
        [HttpGet]
        public dynamic GetDay(DateTime? datefrom, DateTime? dateto, int type, string ids)
         {
            logger.Info("start OrderMaster: ");
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
                List<repo> report = new List<repo>();
                foreach (var iidd in array)
                {
                    int id = Convert.ToInt32(iidd);
                    var res = getdata(datefrom, dateto, type, id);
                    List<netmargin> list = new List<netmargin>();
                    list = res;
                    if (list.Count != 0)
                    {
                        repo r = new repo();
                        netmargin m = new netmargin();
                        foreach (var a in list)
                        {
                            if (a.NetPurchasePrice != 0)
                            {
                                m.SellingPrice += a.QTY * a.SellingPrice;
                                m.NetPurchasePrice += a.QTY * a.NetPurchasePrice;
                            }
                        }
                        if (m.SellingPrice > 0)
                            r.value = (((m.SellingPrice - m.NetPurchasePrice) / (m.SellingPrice)) * 100);
                        else
                            r.value = 0;
                        report.Add(r);
                    }
                }
                return report;
            }
            catch (Exception ex)
            {
                logger.Error("Error in OrderMaster " + ex.Message);
                logger.Info("End  OrderMaster: ");
                return null;
            }
        }

        public List<netmargin> getdata(DateTime? datefrom, DateTime? dateto, int type, int id)
        {
            List<netmargin> list = new List<netmargin>();
            if (datefrom != null && dateto != null)
            {
                if (type == 4)
                {
                    var data = (from i in db.DbOrderDetails
                                where i.CreatedDate >= datefrom && i.CreatedDate <= dateto
                                join j in db.DbOrderMaster on i.OrderId equals j.OrderId
                                select new netmargin
                                {
                                    QTY = i.qty,
                                    NetPurchasePrice = i.NetPurchasePrice,
                                    SellingPrice = i.UnitPrice,
                                    salespersonid = j.SalesPersonId.Value,
                                    SalesPersonName = j.SalesPerson,
                                }).ToList();
                    list = data.Where(x => x.salespersonid == id).ToList();
                }
                else if (type == 2)
                {
                    list = (from i in db.DbOrderDetails
                            where i.CreatedDate >= datefrom && i.CreatedDate <= dateto && i.Warehouseid == id
                            select new netmargin
                            {
                                QTY = i.qty,
                                NetPurchasePrice = i.NetPurchasePrice,
                                SellingPrice = i.UnitPrice,
                                HubId = i.Warehouseid,
                                HubName = i.WarehouseName,
                            }).ToList();
                }
                else if (type == 3)
                {
                    list = (from i in db.DbOrderDetails
                            where i.CreatedDate >= datefrom && i.CreatedDate <= dateto && i.CityId == id
                            select new netmargin
                            {
                                QTY = i.qty,
                                NetPurchasePrice = i.NetPurchasePrice,
                                SellingPrice = i.UnitPrice,
                                CityId = i.CityId.GetValueOrDefault(),
                                CityName = i.City,
                            }).ToList();
                }
                else if (type == 5)
                {
                    var data = (from i in db.DbOrderDetails
                                where i.CreatedDate >= datefrom && i.CreatedDate <= dateto
                                join j in db.DbOrderMaster on i.OrderId equals j.OrderId
                                select new netmargin
                                {
                                    QTY = i.qty,
                                    NetPurchasePrice = i.NetPurchasePrice,
                                    SellingPrice = i.UnitPrice,
                                    clusterId = j.ClusterId,
                                    ClusterName = j.ClusterName,
                                }).ToList();
                    list = data.Where(x => x.clusterId == id).ToList();
                }
            }
            return list;
        }
    }
    public class netmargin
    {
        public int QTY { get; set; }
        public double NetPurchasePrice { get; set; }
        public double SellingPrice { get; set; }
        public int salespersonid { get; set; }
        public int clusterId { get; set; }
        public int HubId { get; set; }
        public string HubName { get; set; }
        public int CityId { get; set; }
        public string CityName { get; set; }
        public string ClusterName { get; set; }
        public string SalesPersonName { get; set; }
    }
    public class repo
    {
        public double value { get; set; }       
    }
}