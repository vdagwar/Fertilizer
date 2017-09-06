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
using System.Web.Configuration;

namespace AngularJSAuthentication.API.Controllers
{
    [RoutePrefix("api/NotificationByDeviceId")]
    public class NotificationByDeviceIdController : ApiController
    {
        AuthContext context = new AuthContext();
        private static Logger logger = LogManager.GetCurrentClassLogger();

        //[Authorize]
        [Route("")]
        public IEnumerable<DeviceNotification> Get()
        {
            logger.Info("start Message: ");
            List<DeviceNotification> MessageList = new List<DeviceNotification>();
            try
            {
                MessageList = context.GetAllNotification().ToList();
                logger.Info("End  Message: ");
                return MessageList;
            }
            catch (Exception ex)
            {
                logger.Error("Error in Message " + ex.Message);
                logger.Info("End  Message: ");
                return null;
            }
        }

        [Route("")]
        public IEnumerable<DeviceNotification> Get(int CustomerId)
        {
            logger.Info("start Message: ");
            List<DeviceNotification> MessageList = new List<DeviceNotification>();
            try
            {
               // MessageList = context.GetAllNotificationByCustomerId(CustomerId).ToList();
                logger.Info("End  Message: ");
                return MessageList;
            }
            catch (Exception ex)
            {
                logger.Error("Error in Message " + ex.Message);
                logger.Info("End  Message: ");
                return null;
            }
        }


    }
}



