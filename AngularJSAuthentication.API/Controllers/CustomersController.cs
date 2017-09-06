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
using static AngularJSAuthentication.API.ControllerV1.SubCatItemController;

namespace AngularJSAuthentication.API.Controllers
{
    [RoutePrefix("api/Customers")]
    public class CustomersController : ApiController
    {
        iAuthContext context = new AuthContext();
        public static Logger logger = LogManager.GetCurrentClassLogger();

        [Authorize]
        [Route("")]
        public IEnumerable<Customer> Get()
        {
            logger.Info("start Get Customer: ");
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
                logger.Info("End  Customer: ");
                var customer = from c in context.AllCustomers.OrderByDescending(c => c.CreatedDate) select c;
                return customer;
            }
            catch (Exception ex)
            {
                logger.Error("Error in Customer " + ex.Message);
                logger.Info("End  Customer: ");
                return null;
            }
        }

        [Route("")]
        public Customer Get(string Mobile)
        {
            logger.Info("start City: ");
            Customer customer = new Customer();
            try
            {
                customer = context.GetCustomerbyId(Mobile);
                logger.Info("End  Customer: ");
                return customer;
            }
            catch (Exception ex)
            {
                logger.Error("Error in Customer " + ex.Message);
                logger.Info("End  Customer: ");
                return null;
            }
        }

        [Route("InActive")]
        public List<Customer> GetInActive()
        {
            logger.Info("start customer: ");
            AuthContext db = new AuthContext();
            try
            {
                List<Customer> customer = db.Customers.Where(x => x.Active == false).ToList();
                logger.Info("End  Customer: ");
                return customer;
            }
            catch (Exception ex)
            {
                logger.Error("Error in Customer " + ex.Message);
                logger.Info("End  Customer: ");
                return null;
            }
        }

        [Route("Forgrt")]
        public HttpResponseMessage GetForgrt(string Mobile)
        {
            logger.Info("start City: ");
            Customer customer = new Customer();
            try
            {
                customer = context.GetCustomerbyId(Mobile);
                if (customer != null)
                {
                    new Sms().sendOtp(customer.Mobile, "Hi " + customer.ShopName + " \n\t You Recently requested a forget password on ShopKirana. Your account Password is '" + customer.Password + "'\n If you didn't request then ingore this message\n\t\t Thanks\n\t\t Shopkirana.com");
                    return Request.CreateResponse(HttpStatusCode.OK, true);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, false);
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error in Customer " + ex.Message);
                logger.Info("End  Customer: ");
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpGet]
        [Route("serach")]
        public HttpResponseMessage serach(string key)
        {
            AuthContext db = new AuthContext();
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
                var customer = db.Customers.Where(c => (c.Skcode.Contains(key) || c.ShopName.Contains(key) || c.Mobile.Contains(key)) && c.Deleted == false).ToList();
                return Request.CreateResponse(HttpStatusCode.OK, customer);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [Route("export")]
        [HttpGet]
        public dynamic export()
        {
            logger.Info("start City: ");
            dynamic customer = null;
            AuthContext db = new AuthContext();
            try
            {
                customer = (from i in db.Customers
                            join j in db.Peoples on i.ExecutiveId equals j.PeopleID into ps
                            from j in ps.DefaultIfEmpty()
                            join k in db.Clusters on i.ClusterId equals k.ClusterId into ps1
                            from k in ps1.DefaultIfEmpty()
                            select new
                            {
                                RetailerId = i.CustomerId,
                                RetailersCode = i.Skcode,
                                ShopName = i.ShopName,
                                RetailerName = i.Name,
                                Mobile = i.Mobile,
                                Address = i.BillingAddress,
                                Area = i.LandMark,
                                Warehouse = i.WarehouseName,
                                ExecutiveId = i.ExecutiveId,
                                ExecutiveName = j.DisplayName,
                                Emailid = i.Emailid,
                                ClusterId = i.ClusterId,
                                ClusterName = k.ClusterName,
                                Day = i.Day,
                                latitute = i.lat,
                                longitute = i.lg,
                                BeatNumber = i.BeatNumber,
                                Active = i.Active,
                                Deleted = i.Deleted
                            }).OrderBy(x => x.RetailerId).ToList();

                logger.Info("End  Customer: ");
                return customer;
            }
            catch (Exception ex)
            {
                logger.Error("Error in Customer " + ex.Message);
                logger.Info("End  Customer: ");
                return null;
            }
        }
        [ResponseType(typeof(Customer))]
        [Route("")]
        public IEnumerable<Customer> Get(string Cityid, string mobile, string skcode, DateTime? datefrom, DateTime? dateto)
        {
            logger.Info("start OrderMaster: ");
            List<Customer> ass = new List<Customer>();
            try
            {
                if (mobile == "undefined" || mobile == null) { mobile = ""; }
                if (skcode == "undefined" || skcode == null) { skcode = ""; }
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
                ass = context.filteredCustomerMaster(Cityid, datefrom, dateto, mobile, skcode).ToList();
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

        [Authorize]
        [ResponseType(typeof(Customer))]
        [Route("")]
        [AcceptVerbs("POST")]
        public Customer add(Customer item)
        {
            logger.Info("start addCustomer: ");
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
                item.CompanyId = compid;
                if (item == null)
                {
                    throw new ArgumentNullException("item");
                }
                context.AddCustomer(item);
                logger.Info("User ID : {0} , Company Id : {1}", compid, userid);
                logger.Info("End  addCustomer: ");
                return item;
            }
            catch (Exception ex)
            {
                logger.Error("Error in addCustomer " + ex.Message);
                return null;
            }
        }

        [Authorize]
        [ResponseType(typeof(Customer))]
        [Route("")]
        [AcceptVerbs("PUT")]
        public Customer Put(Customer item)
        {
            logger.Info("start putCustomer: ");
            try
            {
                return context.PutCustomer(item);
            }
            catch (Exception ex)
            {
                logger.Error("Error in put Customer " + ex.Message);
                return null;
            }
        }

        [ResponseType(typeof(Customer))]
        [Route("")]
        [AcceptVerbs("Delete")]
        public void Remove(int id)
        {
            logger.Info("start delete Customer: ");
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
                context.DeleteCustomer(id);
                logger.Info("End  delete Customer: ");
            }
            catch (Exception ex)
            {
                logger.Error("Error in delete Customer " + ex.Message);
            }
        }




        [Route("favourite")]
        [HttpPost]
        public HttpResponseMessage getFavItems(favourite FIt)
        {
            var item = FIt.items;
            try
            {
                List<factoryItemdata> itemlist = new List<factoryItemdata>();
                foreach (var a in item)
                {
                    try
                    {
                        int id = Convert.ToInt32(a.ItemId);
                        AuthContext db = new AuthContext();
                        var it = db.itemMasters.Where(i => i.ItemId == id).SingleOrDefault();
                        if (it != null)
                        {
                            factoryItemdata itm = new factoryItemdata();
                            itm.Categoryid = it.Categoryid;
                            itm.Discount = it.Discount;
                            itm.ItemId = it.ItemId;
                            itm.itemname = it.SellingUnitName;
                            itm.LogoUrl = it.SellingSku;
                            itm.MinOrderQty = it.MinOrderQty;
                            itm.price = it.price;
                            itm.SubCategoryId = it.SubCategoryId;
                            itm.SubsubCategoryid = it.SubsubCategoryid;
                            itm.TotalTaxPercentage = it.TotalTaxPercentage;
                            itm.UnitPrice = it.UnitPrice;
                            itm.VATTax = it.VATTax;
                            itm.SellingUnitName = it.SellingUnitName;
                            itm.SellingSku = it.SellingSku;
                            itm.HindiName = it.HindiName;
                            itm.marginPoint = it.marginPoint;
                            itm.promoPerItems = it.promoPerItems;

                            itemlist.Add(itm);
                        }
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex.Message);
                    }
                }
                return Request.CreateResponse(HttpStatusCode.OK, itemlist);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }




    public class favourite
    {
        public List<favItem> items { get; set; }
    }
    public class favItem
    {
        public int ItemId  { get; set; }
    }
}