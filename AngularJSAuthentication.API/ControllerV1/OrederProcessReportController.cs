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
    [RoutePrefix("api/OrederProcessReport")]
    public class OrederProcessReportController : ApiController
    {
        AuthContext db = new AuthContext();
        iAuthContext context = new AuthContext();
        public static Logger logger = LogManager.GetCurrentClassLogger();


        [Route("")]
        [HttpGet]
        public OrederProcessReport Get()
        {
            logger.Info("start get all Sales Executive: ");
            OrederProcessReport dbReport = new OrederProcessReport();
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
                AuthContext db = new AuthContext();

                //OrderMaster 
                
                //Issued               
                var Issued = db.OrderDispatchedMasters.Where(i => i.Deleted == false && i.Status == "Issued").ToList();
                dbReport.Issued = Issued.Count;
                //totalamnt
                double sums = 0;
                foreach (var a in Issued)
                {
                    sums = sums + a.GrossAmount;
                }
                dbReport.TotIssuedAmnt = sums;

                //Shiped
                var Shipped = db.OrderDispatchedMasters.Where(s => s.Deleted == false  && s.Status== "Shipped").ToList();
                dbReport.Shipped = Shipped.Count;
                //totalamnt
                double sum1 = 0;
                foreach (var b in Shipped)
                {
                    sum1 = sum1 + b.GrossAmount;
                }
                dbReport.TotShippedAmnt = sum1;


                //Sattle
                var Sattled = db.OrderDispatchedMasters.Where(s => s.Deleted == false && s.Status == "sattled").ToList();
                dbReport.Sattled = Sattled.Count;

                double sum2 = 0;
                foreach (var c in Sattled)
                {
                    sum2 = sum2 + c.GrossAmount;
                }
                dbReport.TotSattledAmnt = sum2;

                //Pending 
                var Pending = db.DbOrderMaster.Where(p => p.Deleted == false && p.Status == "Pending").ToList();
                dbReport.Pending = Pending.Count;

                double sum3 = 0;
                foreach (var d in Pending)
                {
                    sum3 = sum3 + d.GrossAmount;
                }
                dbReport.TotPendingAmnt = sum3;


                //Deliverd
                var Delivered=db.OrderDispatchedMasters.Where(D => D.Deleted == false && D.Status == "Delivered").ToList();
                dbReport.Delivered = Delivered.Count;
                double sum = 0;
                foreach (var a in Delivered)
                 {
                    sum = sum + a.GrossAmount;                  
                 }
                 dbReport.TotDeliveredAmnt = sum;

                //ReadyToDispatch
                var ReadytoDispatch = db.OrderDispatchedMasters.Where(D => D.Deleted == false && D.Status == "Ready to Dispatch").ToList();
                dbReport.Readytodispatch = ReadytoDispatch.Count;
                double sum4 = 0;
                foreach (var e in ReadytoDispatch)
                {
                    sum4 = sum4 + e.GrossAmount;
                }
                dbReport.TotReadytodispatchAmnt = sum4;

                //Order Canceled
                var OrderCanceled = db.OrderDispatchedMasters.Where(k => k.Deleted == false && k.Status == "Order Canceled").ToList();
                dbReport.OrderCancelled = OrderCanceled.Count;
                double sum55 = 0;
                foreach (var f in OrderCanceled)
                {
                    sum55 = sum55 + f.GrossAmount;
                }
                dbReport.TotOrderCancelledAmnt = sum55;


                //DeliveryCanceled
                var DeliveryCanceled = db.OrderDispatchedMasters.Where(k => k.Deleted == false && k.Status == "Delivery Canceled").ToList();
                dbReport.DeliveryCancelled = DeliveryCanceled.Count;
                double sum5 = 0;
                foreach (var f in DeliveryCanceled)
                {
                    sum5 = sum5 + f.GrossAmount;
                }
                dbReport.TotDeliveryCancelledAmnt = sum5;


                //DeliveryRedispatch
                var DeliveryRedispatch = db.OrderDispatchedMasters.Where(m => m.Deleted == false && m.Status == "Delivery Redispatch").ToList();
                dbReport.DeliveredRedispatched = DeliveryRedispatch.Count;
                double sum6 = 0;
                foreach (var g in DeliveryRedispatch)
                {
                    sum6 = sum6 + g.GrossAmount;
                }
                dbReport.TotDeliveredRedispatchedAmnt = sum6;

                //Account settled
                var Accountsettled = db.OrderDispatchedMasters.Where(m => m.Deleted == false && m.Status == "Account settled").ToList();
                dbReport.AccountsettledCount = Accountsettled.Count;
                double sum7 = 0;
                foreach (var h in Accountsettled)
                {
                    sum7 = sum7 + h.GrossAmount;
                }
                dbReport.AccountsettledAmount = sum7;

                return dbReport;
            }
            catch (Exception ex)
            {
                logger.Error("Error in getall  Customer " + ex.Message);
                logger.Info("End get all Customer: ");
                return null;
            }
        }

    }
        public class OrederProcessReport
        {
            public int Pending { get; set; }
            public double TotPendingAmnt { get; set; }

            public int Readytodispatch { get; set; }
            public double TotReadytodispatchAmnt { get; set; }
            public int Issued { get; set; }
            public double TotIssuedAmnt { get; set; }

            public int Shipped { get; set; }
            public double TotShippedAmnt { get; set; }
            public int DeliveryCancelled { get; set; }
            public double TotDeliveryCancelledAmnt { get; set; }

           public int OrderCancelled { get; set; }
           public double TotOrderCancelledAmnt { get; set; }
            public int Delivered { get; set; }
            public double TotDeliveredAmnt { get; set; }
            public int DeliveredRedispatched { get; set; }
            public double TotDeliveredRedispatchedAmnt { get; set; }
            public int Sattled { get; set; }
            public double TotSattledAmnt { get; set; }
        public int AccountsettledCount { get; set; }
        public double AccountsettledAmount { get; set; }
        

    }
    }


