using AngularJSAuthentication.Model;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace AngularJSAuthentication.API.Controllers
{
    [RoutePrefix("api/Orders")]
    public class OrdersController : ApiController
    {
        AuthContext context = new AuthContext();
        public static Logger logger = LogManager.GetCurrentClassLogger();


        [Authorize]
        [Route("")]
        public IHttpActionResult Get()
        {
            //ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;

            //var Name = ClaimsPrincipal.Current.Identity.Name;
            //var Name1 = User.Identity.Name;

            //var userName = principal.Claims.Where(c => c.Type == "sub").Single().Value;

            return Ok(Order.CreateOrders());
        }


        [Route("")]
        public IEnumerable<OrderMaster> Get(int salespersonid)
        {
            logger.Info("start OrderMaster: ");
            List<OrderMaster> ass = new List<OrderMaster>();
            try
            {

                ass = context.OrderMasterbySalesPersonId(salespersonid).ToList();
                logger.Info("End OrderMaster: ");
                return ass;
            }
            catch (Exception ex)
            {
                logger.Error("Error in OrderMaster " + ex.Message);
                logger.Info("End  OrderMaster: ");
                return null;
            }
        }

        [Route("")]
        public IEnumerable<OrderMaster> Get(string Mobile)
        {
            logger.Info("start OrderMaster: ");
            List<OrderMaster> ass = new List<OrderMaster>();
            try
            {

                ass = context.OrderMasterbymobile(Mobile).ToList();
                logger.Info("End OrderMaster: ");
                return ass;
            }
            catch (Exception ex)
            {
                logger.Error("Error in OrderMaster " + ex.Message);
                logger.Info("End  OrderMaster: ");
                return null;
            }
        }

    }


    #region Helpers

    public class Order
    {
        public int OrderID { get; set; }
        public string CustomerName { get; set; }
        public string ShipperCity { get; set; }
        public Boolean IsShipped { get; set; }


        public static List<Order> CreateOrders()
        {
            List<Order> OrderList = new List<Order> 
            {
                new Order {OrderID = 10248, CustomerName = "Taiseer Joudeh", ShipperCity = "Amman", IsShipped = true },
                new Order {OrderID = 10249, CustomerName = "Ahmad Hasan", ShipperCity = "Dubai", IsShipped = false},
                new Order {OrderID = 10250,CustomerName = "Tamer Yaser", ShipperCity = "Jeddah", IsShipped = false },
                new Order {OrderID = 10251,CustomerName = "Lina Majed", ShipperCity = "Abu Dhabi", IsShipped = false},
                new Order {OrderID = 10252,CustomerName = "Yasmeen Rami", ShipperCity = "Kuwait", IsShipped = true}
            };

            return OrderList;
        }
    }

    #endregion
}
