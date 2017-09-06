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
    [RoutePrefix("api/CRM")]
    public class CRMController : ApiController
    {
        AuthContext context = new AuthContext();
        private static Logger logger = LogManager.GetCurrentClassLogger();

        [Authorize]
        [Route("getcust")]
        public HttpResponseMessage get(int id, int idtype, DateTime start, DateTime end,string subsubcatname) //get customers 
        {
            try
            {
                
                logger.Info("getcustomerby level");
                var identity = User.Identity as ClaimsIdentity;
                int compid = 0, userid = 0;
                // Access claims
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
                if (compid > 0) {
                    var objsend = getcrmdashboarddata(id, idtype, start, end, "Garner");

                    //if (months == 0) {
                    //    var firstDayOfMonth = new DateTime(start.Year, start.Month, 1);
                    //    var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
                    //    var crmdata = getcrmdashboarddata(id, idtype, firstDayOfMonth, lastDayOfMonth, "Garner");
                    //    if (crmdata != null)
                    //    {
                    //        crmmnthdata.Add(crmdata);
                    //    }
                    //}


                    return Request.CreateResponse(HttpStatusCode.OK, objsend);

                }
                return Request.CreateResponse(HttpStatusCode.OK, "No data");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        //[Authorize]
        //[Route("getcust")]
        //public HttpResponseMessage get(string level, int clusterid) //get customers 
        //{
        //    try
        //    {
        //        DateTime start = DateTime.Now.AddDays(-30).Date;
        //        DateTime end = DateTime.Now.Date;
        //        logger.Info("getcustomerby level");
        //        var identity = User.Identity as ClaimsIdentity;
        //        int compid = 0, userid = 0;
        //        // Access claims
        //        foreach (Claim claim in identity.Claims)
        //        {
        //            if (claim.Type == "compid")
        //            {
        //                compid = int.Parse(claim.Value);
        //            }
        //            if (claim.Type == "userid")
        //            {
        //                userid = int.Parse(claim.Value);
        //            }
        //        }
        //        logger.Info("User ID : {0} , Company Id : {1}", compid, userid);

        //        if (level == "Aware/Intereseted")
        //        {//active but not ordered
        //            var customers = context.Customers.Where(x => x.ClusterId == clusterid && x.Active == true && x.ordercount == 0).ToList();
        //            return Request.CreateResponse(HttpStatusCode.OK, customers);
        //        }
        //        else if (level == "Ordered")//ordered once
        //        {
        //            var customers = context.Customers.Where(x => x.ClusterId == clusterid && x.Active == true && x.ordercount == 1).ToList();
        //            return Request.CreateResponse(HttpStatusCode.OK, customers);
        //        }
        //        else if (level == "Keep/DropCustomer") //keepor drop
        //        {
        //            var customers = context.Customers.Where(x => x.ClusterId == clusterid && x.Active == true).ToList();
        //            return Request.CreateResponse(HttpStatusCode.OK, customers);
        //        }
        //        else if (level == "EngagedCustomer")//>3 orders
        //        {
        //            var clist = getcustomerordercounts(clusterid, start, end);
        //            return Request.CreateResponse(HttpStatusCode.OK, clist);
        //        }
        //        else if (level == "RelyingCustomer")//>5 orders
        //        {

        //        }
        //        else if (level == "EnabledCustomer")//>10 orders
        //        {

        //        }
        //        else if (level == "EmpoweredCustomer")//>15 orders
        //        {

        //        }


        //        return Request.CreateResponse(HttpStatusCode.OK, "None");
        //    }
        //    catch (Exception ex)
        //    {
        //        return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
        //    }
        //}

        public List<CRMMonthdata> getcrmdashboarddata(int id, int idtype, DateTime start, DateTime end, string subsubcatname)
        {
            List<CRMMonthdata> crmmnthdata = new List<CRMMonthdata>();
            int months = (end.Year - start.Year) * 12 + end.Month - start.Month;
            for (int i = 0; i <= months; i++)
            {
                DateTime st = start.Date.AddMonths(i);
                var firstDayOfMonth = new DateTime(st.Year, st.Month, 1);
                var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
                CRMMonthdata mdata = new CRMMonthdata();
                try
                {                    
                    mdata.dt = firstDayOfMonth;
                    CRMDashboard l0 = new CRMDashboard(); l0.level = 0;
                    CRMDashboard l1 = new CRMDashboard(); l1.level = 1;
                    CRMDashboard l2 = new CRMDashboard(); l2.level = 2;
                    CRMDashboard l3 = new CRMDashboard(); l3.level = 3;
                    CRMDashboard l4 = new CRMDashboard(); l4.level = 4;
                    CRMDashboard l5 = new CRMDashboard(); l5.level = 5;

                    l0.customercount = 0;
                    l0.skbrandordercount = 0;
                    l0.brandsordered = 0;
                    l0.RetailerAppordercount = 0;
                    l0.SalesmanAppordercount = 0;
                    l0.volume = 0;
                    l1.customercount = 0;
                    l1.skbrandordercount = 0;
                    l1.brandsordered = 0;
                    l1.RetailerAppordercount = 0;
                    l1.SalesmanAppordercount = 0;
                    l1.volume = 0;
                    l2.customercount = 0;
                    l2.skbrandordercount = 0;
                    l2.brandsordered = 0;
                    l2.RetailerAppordercount = 0;
                    l2.SalesmanAppordercount = 0;
                    l2.volume = 0;
                    l3.customercount = 0;
                    l3.skbrandordercount = 0;
                    l3.brandsordered = 0;
                    l3.RetailerAppordercount = 0;
                    l3.SalesmanAppordercount = 0;
                    l3.volume = 0;
                    l4.customercount = 0;
                    l4.skbrandordercount = 0;
                    l4.brandsordered = 0;
                    l4.RetailerAppordercount = 0;
                    l4.SalesmanAppordercount = 0;
                    l4.volume = 0;
                    l5.customercount = 0;
                    l5.skbrandordercount = 0;
                    l5.brandsordered = 0;
                    l5.RetailerAppordercount = 0;
                    l5.SalesmanAppordercount = 0;
                    l5.volume = 0;
                    //get Shopkirana brands list
                    List<SubsubCategory> brands = context.SubsubCategorys.Where(x => x.SubcategoryName.Contains(subsubcatname) && x.Deleted == false).ToList();
                    //get customers where by id 
                    List<Customer> cs = new List<Customer>();
                    if (idtype == 1)
                    {
                        cs = context.Customers.Where(x => x.Cityid == id && x.Active == true).ToList();
                    }
                    else if (idtype == 2)
                    {
                        cs = context.Customers.Where(x => x.Warehouseid == id && x.Active == true).ToList();
                    }
                    else if (idtype == 3)
                    {
                        cs = context.Customers.Where(x => x.Cityid == id && x.Active == true).ToList();
                    }
                    else if (idtype == 4)
                    {
                        cs = context.Customers.Where(x => x.ExecutiveId == id && x.Active == true).ToList();
                    }
                    else if (idtype == 5)
                    {
                        cs = context.Customers.Where(x => x.ClusterId == id && x.Active == true).ToList();
                    }

                    List<CrmCustomer> cclist = new List<CrmCustomer>();
                    foreach (var c in cs)
                    {
                        List<OrderMaster> orders = context.DbOrderMaster.Where(x => x.CustomerId == c.CustomerId && x.CreatedDate >= firstDayOfMonth && x.CreatedDate <= lastDayOfMonth).ToList();
                        int ordercnt = orders.Count();
                        int brandcnt = 0;
                        double volume = 0;
                        int RetailerAppordercount = 0;
                        int SalesmanAppordercount = 0;
                        int skbrandordercount = 0;
                        int canceled = 0;
                        int del = 0;
                        int redisp = 0;
                        int pending = 0;

                        CrmCustomer ccc = new CrmCustomer();

                        List<brandorderedlist> brandordered = new List<brandorderedlist>();
                        List<brandorderedlist> SKbrandordered = new List<brandorderedlist>();
                        foreach (var o in orders)
                        {
                            volume = volume + o.GrossAmount;
                            if (o.OrderTakenSalesPersonId == 0)
                            {
                                RetailerAppordercount = RetailerAppordercount + 1;
                            }
                            else
                            {
                                SalesmanAppordercount = SalesmanAppordercount + 1;
                            }
                            bool skod = false;
                            if (o.Status == "Pending")
                            {
                                pending = pending + 1;
                            }
                            else if (o.Status == "Delivery Redispatch")
                            {
                                redisp = redisp + 1;
                            }
                            else if (o.Status == "Delivered" || o.Status == "sattled" || o.Status == "Account settled" || o.Status == "Partial receiving -Bounce" || o.Status == "Partial settled")
                            {
                                del = del + 1;
                            }
                            else if (o.Status == "Order Canceled" || o.Status == "Delivery Canceled")
                            {
                                canceled = canceled + 1;
                            }

                            foreach (var od in o.orderDetails)
                            {
                                var item = context.itemMasters.Where(x => x.ItemId == od.ItemId).SingleOrDefault();
                                bool check = brands.Any(x => x.SubsubCategoryid == item.SubsubCategoryid); //check if SK brand
                                if (check)
                                {
                                    if (!SKbrandordered.Any(x => x.brandordered == item.SubsubcategoryName)) //check for SK brands count
                                    {
                                        SKbrandordered.Add(new brandorderedlist { brandordered = item.SubsubcategoryName });  //Skbrand = Skbrand + 1;
                                    }
                                    skod = true;
                                }
                                if (!brandordered.Any(x => x.brandordered == item.SubsubcategoryName))//check for all brands count
                                {
                                    brandordered.Add(new brandorderedlist { brandordered = item.SubsubcategoryName });
                                    brandcnt = brandcnt + 1;
                                }
                            }
                            if (skod) { skbrandordercount = skbrandordercount + 1; }
                        }
                        ccc.ReportDate = mdata.dt;
                        ccc.CustomerId = c.CustomerId;
                        ccc.WarehouseName = c.Warehouseid.GetValueOrDefault();
                        ccc.Skcode = c.Skcode;
                        ccc.ShopName = c.ShopName;
                        ccc.Mobile = c.Mobile;
                        ccc.thisordercount = ordercnt;
                        ccc.thisordervalue = volume;
                        ccc.thisRAppordercount = RetailerAppordercount;
                        ccc.thisSAppordercount = SalesmanAppordercount;
                        ccc.thisordercountCancelled = canceled;
                        ccc.thisordercountdelivered = del;
                        ccc.thisordercountRedispatch = redisp;
                        ccc.thisordercountpending = pending;

                        cclist.Add(ccc);


                        decimal app = 0;
                        if (ordercnt > 0)
                        {
                            app = (RetailerAppordercount / ordercnt) * 100;
                        }
                        if (ordercnt >= 15 && skbrandordercount >= 15 && volume >= 50000 && brandcnt >= 50 && app >= 60)
                        {
                            l5.customercount = l5.customercount + 1;
                            l5.skbrandordercount = l5.skbrandordercount + skbrandordercount;
                            l5.brandsordered = l5.brandsordered + brandcnt;
                            l5.RetailerAppordercount = l5.RetailerAppordercount + RetailerAppordercount;
                            l5.SalesmanAppordercount = l5.SalesmanAppordercount + SalesmanAppordercount;
                            l5.volume = l5.volume + volume;
                        }
                        else if (ordercnt >= 10 && skbrandordercount >= 8 && volume >= 30000 && brandcnt >= 30 && app >= 20)
                        {
                            l4.customercount = l4.customercount + 1;
                            l4.skbrandordercount = l4.skbrandordercount + skbrandordercount;
                            l4.brandsordered = l4.brandsordered + brandcnt;
                            l4.RetailerAppordercount = l4.RetailerAppordercount + RetailerAppordercount;
                            l4.SalesmanAppordercount = l4.SalesmanAppordercount + SalesmanAppordercount;
                            l4.volume = l4.volume + volume;
                        }
                        else if (ordercnt >= 5 && skbrandordercount >= 2 && volume >= 20000 && brandcnt >= 10 && app > 0)
                        {
                            l3.customercount = l3.customercount + 1;
                            l3.skbrandordercount = l3.skbrandordercount + skbrandordercount;
                            l3.brandsordered = l3.brandsordered + brandcnt;
                            l3.RetailerAppordercount = l3.RetailerAppordercount + RetailerAppordercount;
                            l3.SalesmanAppordercount = l3.SalesmanAppordercount + SalesmanAppordercount;
                            l3.volume = l3.volume + volume;
                        }
                        else if (ordercnt >= 3 && skbrandordercount > 0 && volume >= 10000 && brandcnt >= 5)
                        {
                            l2.customercount = l2.customercount + 1;
                            l2.skbrandordercount = l2.skbrandordercount + skbrandordercount;
                            l2.brandsordered = l2.brandsordered + brandcnt;
                            l2.RetailerAppordercount = l2.RetailerAppordercount + RetailerAppordercount;
                            l2.SalesmanAppordercount = l2.SalesmanAppordercount + SalesmanAppordercount;
                            l2.volume = l2.volume + volume;
                        }
                        else if (ordercnt > 0)
                        {
                            l1.customercount = l1.customercount + 1;
                            l1.skbrandordercount = l1.skbrandordercount + skbrandordercount;
                            l1.brandsordered = l1.brandsordered + brandcnt;
                            l1.RetailerAppordercount = l1.RetailerAppordercount + RetailerAppordercount;
                            l1.SalesmanAppordercount = l1.SalesmanAppordercount + SalesmanAppordercount;
                            l1.volume = l1.volume + volume;
                        }
                        else
                        {
                            l0.customercount = l0.customercount + 1;
                            l0.skbrandordercount = l0.skbrandordercount + skbrandordercount;
                            l0.brandsordered = l0.brandsordered + brandcnt;
                            l0.RetailerAppordercount = l0.RetailerAppordercount + RetailerAppordercount;
                            l0.SalesmanAppordercount = l0.SalesmanAppordercount + SalesmanAppordercount;
                            l0.volume = l0.volume + volume;
                        }
                    }
                    mdata.L0 = l0;
                    mdata.L1 = l1;
                    mdata.L2 = l2;
                    mdata.L3 = l3;
                    mdata.L4 = l4;
                    mdata.L5 = l5;
                    mdata.customer = cclist;

                }
                catch (Exception ex)
                { return null; }

                if (mdata != null)
                {
                   crmmnthdata.Add(mdata);
                }
            }

            return crmmnthdata;
        }
    }

    public class brandorderedlist
    {
        public string brandordered { get; set; }
    }

    public class CRMDashboard {
        public int level { get; set; }
        public int customercount { get; set; }
        public int skbrandordercount { get; set; }
        public double volume { get; set; }
        public int brandsordered { get; set; }
        public int RetailerAppordercount { get; set; }
        public int SalesmanAppordercount { get; set; }
    }
    public class CRMMonthdata {
        public CRMDashboard L0 { get; set; }
        public CRMDashboard L1 { get; set; }
        public CRMDashboard L2 { get; set; }
        public CRMDashboard L3 { get; set; }
        public CRMDashboard L4 { get; set; }
        public CRMDashboard L5{ get; set; }
        public DateTime dt { get; set; }
        public List<CrmCustomer> customer { get; set; }

    }
    public class CrmCustomer
    { 
        public int CustomerId { get; set; }
        public DateTime ReportDate { get; set; }
        public int WarehouseName { get; set; }
        public string Skcode { get; set; }
        public string ShopName { get; set; }
        public string Mobile { get; set; }
        public int thisordercount { get; set; }
        public double thisordervalue { get; set; }
        public int thisordercountpending { get; set; }
        public int thisordercountdelivered { get; set; }
        public int thisordercountRedispatch { get; set; }
        public int thisordercountCancelled { get; set; }
        public int thisRAppordercount { get; set; }
        public int thisSAppordercount { get; set; }
    }
}