using GenricEcommers.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AngularJSAuthentication.Model
{
    public class Notification
    {
        [Key]
        public int Id { get; set; }
        public string title { get; set; }
        public string Message { get; set; }
        public string Pic { get; set; }
        public string NotifiedTo { get; set; }
        public string CityName { get; set; }
        public string ClusterName { get; set; }
        public string WarehouseName { get; set; }
        public DateTime NotificationTime { get; set; }
        public DateTime? From { get; set; }
        public DateTime? TO { get; set; }
        public bool Deleted { get; set; }
        // [NotMapped]
        // public dynamic ids { get; set; }
       [NotMapped]
        public List<dbinf> ids { get; set; }
    }
    public class dbinf
    {
         [NotMapped]
        public int id { get; set; }
    }
}