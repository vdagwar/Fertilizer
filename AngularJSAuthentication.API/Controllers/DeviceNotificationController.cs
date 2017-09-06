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

namespace AngularJSAuthentication.API.Controllers
{
    [RoutePrefix("api/DeviceNotificationApi")]
    public class DeviceNotificationController : ApiController
    {
        AuthContext context = new AuthContext();
        private static Logger logger = LogManager.GetCurrentClassLogger();

        [Route("")]
        public IEnumerable<DeviceNotification> Get()
        {
            logger.Info("start DeviceNotification: ");
            List<DeviceNotification> CategoriesList = new List<DeviceNotification>();
            try
            {
                CategoriesList = context.GetAllDeviceNotification().ToList();
                logger.Info("End  DeviceNotification: ");
                return CategoriesList;
            }
            catch (Exception ex)
            {
                logger.Error("Error in DeviceNotification " + ex.Message);
                logger.Info("End  DeviceNotification: ");
                return null;
            }
        }

        [Route("")]
        public bool Get(string RegId, string imei)
        {
            logger.Info("start DeviceNotification: ");
         
            try
            {
               var CategoriesList = context.GetAllDeviceNotification(RegId,imei);
                logger.Info("End  DeviceNotification: ");
                
                return CategoriesList;
            }
            catch (Exception ex)
            {
                logger.Error("Error in DeviceNotification " + ex.Message);
                logger.Info("End  DeviceNotification: ");
                return false;
            }
        }

        [Route("")]
        public DeviceNotification Get(int id)
        {
            logger.Info("start single DeviceNotification: ");
            DeviceNotification deviceNotification = new DeviceNotification();
            try
            {
                logger.Info("in deviceNotification");

                deviceNotification = context.GetByDeviceNotificationId(id);
                logger.Info("End Get DeviceNotification id: " + deviceNotification.DeviceId);
                return deviceNotification;
            }
            catch (Exception ex)
            {
                logger.Error("Error in Get deviceNotification by deviceNotification id " + ex.Message);
                logger.Info("End  single deviceNotification: ");
                return null;
            }
        }

        [ResponseType(typeof(DeviceNotification))]
        [Route("")]
        [AcceptVerbs("POST")]
        public DeviceNotification add(DeviceNotification deviceNotification)
        {
           logger.Info("Add DeviceNotification: ");
            try
            {
                if (deviceNotification == null)
                {
                    throw new ArgumentNullException("deviceNotification");
                }
                context.AddDeviceNotification(deviceNotification);
                logger.Info("End  Add deviceNotification: ");
                return deviceNotification;
            }
            catch (Exception ex)
            {
                logger.Error("Error in Add deviceNotification " + ex.Message);

                return null;
            }
        }

        
        [Route("")]
        [AcceptVerbs("PUT")]
        public DeviceNotification Put(DeviceNotification deviceNotification)
        {
            try
            {
                return context.PutDeviceNotification(deviceNotification);
            }
            catch (Exception ex)
            {
                logger.Error("Error in Put deviceNotification " + ex.Message);
                return null;
            }
        }


        
        [Route("")]
        [AcceptVerbs("Delete")]
        public void Remove(int id)
        {
            logger.Info("DELETE Remove: ");
            try
            {
                context.DeleteDeviceNotification(id);
            }
            catch (Exception ex)
            {
                logger.Error("Error in Remove DeviceNotification " + ex.Message);

            }
        }


    }
}



