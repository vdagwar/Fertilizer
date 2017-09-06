using GenricEcommers.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AngularJSAuthentication.Model
{
    public class NotificationByDeviceId
    {
        [Key]
        public int Id { get; set; }
        public string Message { get; set;}
        [NotMapped]
        public string MobileNumber { get; set; }
        public string NotifiedTo { get; set; }
        public string ImageUrl { get; set; }
        public string Title { get; set; }
        public DateTime? NotificationByDeviceIdTime { get; set; }

        public  ICollection<DeviceNotification> deviceNotification { get; set; }


    }
}