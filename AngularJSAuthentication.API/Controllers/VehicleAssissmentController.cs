using AngularJSAuthentication.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Caching;
using System.Security.Claims;
using System.Web.Http;


namespace AngularJSAuthentication.API.Controllers
{
    [RoutePrefix("api/vehicleassissment")]
    public class VehicleAssissmentControllerController : ApiController
    {
        AuthContext context = new AuthContext();

        [Route("")]
        [HttpGet]
        public HttpResponseMessage Get(string ids)//get all 
        {

            try
            {
                List<DBoySummary> SUmmarylist = new List<DBoySummary>();
                string[] idss = ids.Split(',');

            
                foreach (var od in idss)
                {
                    var oid = Convert.ToInt16(od);
                    
                    var orderdipatchmaster = context.OrderDispatchedMasters.Where(x => x.OrderId == oid).SingleOrDefault();
                  

                    DBoySummary Os = new DBoySummary();

                    if (orderdipatchmaster != null)
                    {
                        Os.chequeNo = orderdipatchmaster.CheckNo;
                        Os.CustomerId = orderdipatchmaster.CustomerId;
                        Os.CustomerName = orderdipatchmaster.CustomerName;
                        Os.DBoyName = orderdipatchmaster.DboyName;
                        Os.GrossAmount = orderdipatchmaster.GrossAmount;
                        Os.OrderId = orderdipatchmaster.OrderId;
                        Os.SalesPerson = orderdipatchmaster.SalesPerson;
                        Os.SalesPersonId = orderdipatchmaster.SalesPersonId;
                        Os.Status = orderdipatchmaster.Status;
                        Os.Skcode = orderdipatchmaster.Skcode;
                        Os.invoice_no = orderdipatchmaster.invoice_no;
                        Os.ShopName = orderdipatchmaster.ShopName;
                        Os.cashAmount = orderdipatchmaster.CashAmount;
                        Os.chequeAmount = orderdipatchmaster.CheckAmount;
                        Os.comments = orderdipatchmaster.comments;
                        SUmmarylist.Add(Os);
                    }

                }

                return Request.CreateResponse(HttpStatusCode.OK, SUmmarylist);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [Route("")]
        [HttpGet]
        public HttpResponseMessage Getorders(string ids,string testt)//get all 
        {

            try
            {
                string[] idss = ids.Split(',');

                List<OrderDispatchedMaster> Ordeersss = new List<OrderDispatchedMaster>();
                foreach (var od in idss)
                {
                    var oid = Convert.ToInt16(od);

                    var orderdipatchmaster = context.OrderDispatchedMasters.Where(x => x.OrderId == oid).SingleOrDefault();
                    if (orderdipatchmaster != null) {
                        Ordeersss.Add(orderdipatchmaster);
                    }
                }

                return Request.CreateResponse(HttpStatusCode.OK, Ordeersss);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

    }
}



