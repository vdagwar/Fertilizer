using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AngularJSAuthentication.Model;

namespace GenricEcommers.Models
{
    public class DeviceNotification
    {
        [Key]
        public int? Id { get; set; }
        public int? CustomerId { get; set; }
        public string DeviceId { get; set; }
        public string title { get; set; }
        public string Message { get; set; }
        public string ImageUrl { get; set; }
        public DateTime NotificationTime { get; set; }
        public bool Deleted { get; set; }
       
    }
}
