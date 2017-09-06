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
    [RoutePrefix("api/uniteconomicreport")]
    public class UEReportController : ApiController
    {
        AuthContext db = new AuthContext();
        public static Logger logger = LogManager.GetCurrentClassLogger();

        [Route("")]        
        [HttpGet]
        public dynamic GetMonth(DateTime? datefrom, DateTime? dateto,int Warehouseid, string lab1)
        {
            logger.Info("start OrderMaster: ");
            List<Reportsss> reports = new List<Reportsss>();
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
                var array1 = lab1.Split(',');
                foreach (var lab in array1)
                {
                    Reportsss reports1 = new Reportsss();
                    List<unitReport> uniqueMonth = new List<unitReport>();
                    List<UnitEconomic> list = db.UnitEconomicDb.Where(i => i.CreatedDate >= datefrom && i.CreatedDate <= dateto && i.Warehouseid == Warehouseid && i.Label1.Trim() == lab.Trim()).ToList();
                    List<OrderMaster> order = db.DbOrderMaster.Where(i => i.CreatedDate >= datefrom && i.CreatedDate <= dateto && i.Warehouseid == Warehouseid).ToList();
                    List<orderMasterlist> report = new List<orderMasterlist>();
                    if (list.Count != 0)
                    {                        
                        foreach (var a in order)
                        {
                            unitReport l = uniqueMonth.Where(x => x.createdDate.Month == a.CreatedDate.Month && x.createdDate.Year == a.CreatedDate.Year && x.name.Trim() == lab.Trim()).SingleOrDefault();
                            if (l == null)
                            {
                                unitReport unq = new unitReport();
                                unq.totalOrder += 1;
                                unq.totalSale = a.TotalAmount;
                                unq.name = lab.Trim();
                                unq.createdDate = new DateTime(a.CreatedDate.Year, a.CreatedDate.Month, 1);
                                List<UnitEconomic> unql = list.Where(x => x.CreatedDate.Month == a.CreatedDate.Month && x.CreatedDate.Year == a.CreatedDate.Year).ToList();
                                unq.totalExpence = 0;
                                if (unql.Count != 0)
                                {
                                    foreach (var ab in unql)
                                    {
                                        unq.totalExpence += ab.Amount;
                                    }
                                }
                                uniqueMonth.Add(unq);
                            }
                            else
                            {
                                l.totalOrder += 1;
                                l.totalSale = l.totalSale + a.TotalAmount;
                            }                            
                        }

                        reports1.reportts = uniqueMonth;
                        reports.Add(reports1);
                    }
                }
                return reports;
            }
            catch (Exception ex)
            {
                logger.Error("Error in OrderMaster " + ex.Message);
                logger.Info("End  OrderMaster: ");
                return null;
            }
        } 
    }
    public class unitReport
    {
        public int totalOrder { get; set; }
        public double totalSale { get; set; }
        public double totalExpence { get; set; }
        public string name { get; set; }
        public DateTime createdDate { get; set; }        
    }
    public class Reportsss
    {
        public List<unitReport> reportts { get; set; }
    }
}
