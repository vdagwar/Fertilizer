using GenricEcommers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using System.Web.Http.Description;
using NLog;
using GenricEcommers;
using AngularJSAuthentication.Model;
using System.Text;
using System.IO;

namespace AngularJSAuthentication.API.Controllers
{
    [RoutePrefix("api/Notification")]
    public class NotificationController : ApiController
    {
        AuthContext context = new AuthContext();
        private static Logger logger = LogManager.GetCurrentClassLogger();
       
        [Route("get")]
        [HttpGet]
        public PaggingDatas notifyy(int list, int page)
        {
            try
            {
                PaggingDatas data = new PaggingDatas();
                var total_count = context.NotificationDb.Where(x => x.Message != null).Count();
                var notificationmaster = context.NotificationDb.Where(x => x.Message != null).OrderByDescending(x => x.NotificationTime).Skip((page - 1) * list).Take(list).ToList();
                data.notificationmaster = notificationmaster;
                data.total_count = total_count;
                return data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [Route("getall")]
        [HttpGet]
        public PaggingDatas notifyDelivered(int list, int page, int customerid)
        {
            try
            {
                PaggingDatas data = new PaggingDatas();
                var total_count = context.DeviceNotificationDb.Where(x => x.CustomerId == customerid).Count();
                var notificationmaster = context.DeviceNotificationDb.Where(x => x.CustomerId == customerid).OrderByDescending(x => x.NotificationTime).Skip((page - 1) * list).Take(list).ToList();
                data.notificationmaster = notificationmaster;
                data.total_count = total_count;
                return data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [Route("all")]
        [HttpGet]
        public HttpResponseMessage Get()
        {
            List<Customer> ass = new List<Customer>();
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
                ass = context.Customers.Where(c => c.Deleted == false && c.fcmId != null).ToList();
                logger.Info("End  Return: ");
                return Request.CreateResponse(HttpStatusCode.OK, ass);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }


        [Route("allfcmcust")]
        [HttpGet]
        public HttpResponseMessage Get(int id )
        {
            List<Customer> ass = new List<Customer>();
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
                var list = (from i in context.DbOrderDetails where i.Deleted == false
                             join b in context.itemMasters on i.ItemId equals b.ItemId
                             join c in context.Categorys on b.Categoryid equals c.Categoryid
                             join j in context.Customers on i.CustomerId equals j.CustomerId
                             select new notModel
                             {
                                 categoryId = c.Categoryid,
                                 CustomerId = i.CustomerId,
                                 orderTotal = i.TotalAmt,
                               
                                 fcmId = j.fcmId
                             }).ToList();
                var list1 = list.Where(x => x.categoryId == id).ToList();
                List<notModel> uniqecustomer = new List<notModel>();
                foreach (var a in list1)
                {
                    notModel customer = uniqecustomer.Where(c => c.CustomerId == a.CustomerId).SingleOrDefault();
                    if (customer == null)
                    {
                        a.orderCount = 1;
                        uniqecustomer.Add(a);
                    }
                    else
                    {
                        customer.orderCount++;
                        customer.orderTotal += a.orderTotal;
                    }

                }
                var cust = uniqecustomer.OrderBy(o => o.orderCount).Take(2);
                List<Customer> custlist = new List<Customer>();
                foreach (var b in cust)
                {
                    Customer cu = context.Customers.Where(c => c.CustomerId == b.CustomerId).SingleOrDefault();
                    custlist.Add(cu);
                }

                logger.Info("End  Return: ");
                return Request.CreateResponse(HttpStatusCode.OK, custlist);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        //warehouse

        [Route("allware")]
        [HttpGet]
        public HttpResponseMessage Get(int Warehouseid, string idd)
        {
            List<Customer> ass = new List<Customer>();
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
                ass = context.Customers.Where(c => c.fcmId != null && c.Warehouseid == Warehouseid).ToList();
                logger.Info("End  Return: ");
                return Request.CreateResponse(HttpStatusCode.OK, ass);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }



        // city

        [Route("allcity")]
        [HttpGet]
        public HttpResponseMessage City(int Cityid)
        {
            List<Customer> ass = new List<Customer>();
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
                ass = context.Customers.Where(c => c.fcmId != null && c.Cityid == Cityid).ToList();
                logger.Info("End  Return: ");
                return Request.CreateResponse(HttpStatusCode.OK, ass);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        //cluster

        [Route("allcluster")]
        [HttpGet]
        public HttpResponseMessage Cluster(int ClusterId)
        {
            List<Customer> ass = new List<Customer>();
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
                ass = context.Customers.Where(c => c.fcmId != null && c.ClusterId == ClusterId).ToList();
                logger.Info("End  Return: ");
                return Request.CreateResponse(HttpStatusCode.OK, ass);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [ResponseType(typeof(Notification))]
        [Route("")]
        [AcceptVerbs("POST")]
        public Notification add(Notification notification)
        {
            logger.Info("Add message: ");
            try
            {
                if (notification == null)
                {
                    throw new ArgumentNullException("message");
                }

                List<Customer> customers = new List<Customer>();


                if (notification.ids.Count () > 0) {
                    foreach (var i in notification.ids)
                    {
                       // var olist = context.Customers.Where(x => x.fcmId != null && x.CustomerId == i.id && x.CreatedDate >= notification.From && x.CreatedDate <= notification.TO).SingleOrDefault();
                        var olist = context.Customers.Where(x => x.fcmId != null && x.CustomerId == i.id).SingleOrDefault();

                        customers.Add(olist);
                    }
                }

                else
                {
                    customers = context.Customers.Where(x => x.fcmId != null).ToList();
                }
                    context.AddNotification(notification);

                    foreach (var item in customers)
                    {
                        if (item.fcmId != null)
                        {
                            //Registration Id created by Android App i.e. DeviceId.  
                            string regId;
                            regId = item.Name;
                            //API Key created in Google project  
                            var Key = "AIzaSyA67YsQd7_ZIOBYsAYDFbC4JBUDuDNU3MY";
                            //Project ID created in Google project.  
                            var id = "98292896656";
                            var varMessage = notification.Message;
                            var ImageURL = "https://cdn4.iconfinder.com/data/icons/ionicons/512/icon-image-128.png";

                            WebRequest tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send") as HttpWebRequest;
                            tRequest.Method = "post";

                            var objNotification = new
                            {
                                to = item.fcmId,
                                notification = new
                                {
                                    title = notification.title,
                                    body = notification.Message,
                                    icon = notification.Pic
                                }
                            };
                            string jsonNotificationFormat = Newtonsoft.Json.JsonConvert.SerializeObject(objNotification);
                            Byte[] byteArray = Encoding.UTF8.GetBytes(jsonNotificationFormat);
                            tRequest.Headers.Add(string.Format("Authorization: key={0}", Key));
                            tRequest.Headers.Add(string.Format("Sender: id={0}", id));
                            tRequest.ContentLength = byteArray.Length;
                            tRequest.ContentType = "application/json";
                            using (Stream dataStream = tRequest.GetRequestStream())
                            {
                                dataStream.Write(byteArray, 0, byteArray.Length);
                                using (WebResponse tResponse = tRequest.GetResponse())
                                {
                                    using (Stream dataStreamResponse = tResponse.GetResponseStream())
                                    {
                                        using (StreamReader tReader = new StreamReader(dataStreamResponse))
                                        {
                                            String responseFromFirebaseServer = tReader.ReadToEnd();

                                            FCMResponse response = Newtonsoft.Json.JsonConvert.DeserializeObject<FCMResponse>(responseFromFirebaseServer);
                                            if (response.success == 1)
                                            {
                                                Console.Write(response);
                                                try
                                                {
                                                    DeviceNotification obj = new DeviceNotification();
                                                    obj.CustomerId = item.CustomerId;
                                                    obj.DeviceId = item.fcmId;
                                                    obj.title = notification.title;
                                                    obj.Message = notification.Message;
                                                    obj.ImageUrl = notification.Pic;
                                                    obj.NotificationTime = DateTime.Now;
                                                    context.DeviceNotificationDb.Add(obj);
                                                    int Id = context.SaveChanges();
                                                    // return true;
                                                }
                                                catch (Exception ex)
                                                {
                                                    logger.Error("Error in Add message " + ex.Message);

                                                    return null;
                                                }

                                                       }
                                            else if (response.failure == 1)
                                            {
                                               
                                            }

                                        }
                                    }

                                }
                            }

                        }
                    }

                logger.Info("End  Add message: ");
                return notification;
            }
            catch (Exception ex)
            {
                logger.Error("Error in Add message " + ex.Message);

                return null;
            }
        }
    }

}
public class FCMResponse
{
    public long multicast_id { get; set; }
    public int success { get; set; }
    public int failure { get; set; }
    public int canonical_ids { get; set; }
    public List<FCMResult> results { get; set; }
}
public class FCMResult
{
    public string message_id { get; set; }
}
public class customers
{
    public string fcmId { get; set; }
    public int CustomerId { get; set; }
    public int orderCount { get; set; }
    public double orderTotal { get; set; }
}

public class notModel
{
    public int ItemId { get; set; }
    public int CustomerId { get; set; }
    public int categoryId { get; set; }
    public string fcmId { get; set; }
    public int orderCount { get; set; }
    public double orderTotal { get; set; }
}




