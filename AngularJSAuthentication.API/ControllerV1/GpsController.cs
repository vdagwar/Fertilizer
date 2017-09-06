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
    [RoutePrefix("api/GpsController")]
    public class GpsController : ApiController
    {
        iAuthContext context = new AuthContext();
        private static Logger logger = LogManager.GetCurrentClassLogger();

        [Route("")]
        [AcceptVerbs("GET")]
        public List<GpsCoordinate> Get()
        {
            logger.Info("start get all GpsCoordinate: ");
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                int compid = 0, userid = 0;
                List<GpsCoordinate> displist = new List<GpsCoordinate>();
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
                displist = db.GpsCoordinateDb.Where(x => x.Deleted == false).ToList();
                logger.Info("End  UnitMaster: ");
                return displist;
            }
            catch (Exception ex)
            {
                logger.Error("Error in get all GpsCoordinate " + ex.Message);
                logger.Info("End get all GpsCoordinate: ");
                return null;
            }
        }

        [Route("data")]
        public MapData Get(string id, DateTime sDate, DateTime eDate)
        {
            MapData mapData = new MapData();
            logger.Info("start GpsCoordinate: ");
            if (id != null)
            {
                int Id = Convert.ToInt32(id);
                AuthContext db = new AuthContext();
                mapData.deliveryboy = db.GpsCoordinateDb.Where(x => x.DeliveryBoyId == Id && x.CreatedDate > sDate && x.CreatedDate <= eDate).OrderBy(o=>o.CreatedDate).ToList();
                People p = db.Peoples.Where(x => x.PeopleID == Id).SingleOrDefault();
                var list = db.OrderDispatchedMasters.Where(x => x.DboyMobileNo == p.Mobile && x.Deliverydate >= sDate && x.Deliverydate <= eDate ).ToList();//&& (x.Status == "Ready to Dispatch" || x.Status == "Delivery Redispatch")
                List<GpsCoordinate> displist = new List<GpsCoordinate>();
                foreach (OrderDispatchedMaster m in list)
                {
                    Customer c = db.Customers.Where(x => x.CustomerId == m.CustomerId).SingleOrDefault();
                    GpsCoordinate gc = new GpsCoordinate() 
                    {
                        DeliveryBoyId = Id,
                        lat = c.lat,
                        lg = c.lg,
                        CustomerName = c.ShopName,
                        isDestination = true,
                        status = m.Status
                    };

                    displist.Add(gc);
                }
                mapData.customer = displist;
                return mapData;
            }
            return null;
        }

        [Route("GpsUpdate")]
        [AcceptVerbs("post")]
        public GpsCoordinate post(GpsCoordinate obj)
           {
            try
            {
                return context.Addgps(obj);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}



