using AngularJSAuthentication.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AngularJSAuthentication.API.Controllers
{
    [RoutePrefix("api/Retailers")]
    public class RetailerController : ApiController
    {
        iAuthContext context = new AuthContext();
        AuthContext cont = new AuthContext();
        //new7.9.15
        [Route("")]
        public List<Customer> Get(int id)
        {
            int id1 = id;
            List<Customer> retailers = (from em in cont.Customers.Where(x => x.Deleted == false).Where(c => c.Warehouseid == id) select em).ToList();
               //  cont.Customers.Where(c=> c.Warehouseid.Equals(id1)).ToList();
            //return context.AllCustomers;
            return retailers;
        }
        [Route("")]
        public List<Customer> Get(int ExecutiveId, int id)
        {
            int id1 = ExecutiveId;
            List<Customer> retailers = (from em in cont.Customers.Where(x => x.Deleted == false).Where(c => c.ExecutiveId == id1) select em).ToList();
            //  cont.Customers.Where(c=> c.Warehouseid.Equals(id1)).ToList();
            //return context.AllCustomers;
            return retailers;
        }
        [Route("")]
        public List<Warehouse> Get(string City,string City1)
        {
            List<Warehouse> citywarehouse = cont.Warehouses.Where(w => w.CityName.Trim().ToLower().Equals(City.Trim().ToLower())).ToList();
            return citywarehouse;

        }
        
    }
}
