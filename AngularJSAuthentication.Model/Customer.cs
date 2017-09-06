using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AngularJSAuthentication.Model;

namespace AngularJSAuthentication.Model
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }
        public int? CustomerCategoryId { get; set; }
        public string Skcode { get; set; }
        public string ShopName { get; set; }
        public int? Warehouseid { get; set; }
        public string Mobile { get; set; }
        public string WarehouseName { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Description { get; set; }
        public string CustomerType { get; set; }
        public string CustomerCategoryName { get; set; }
        public int? CompanyId { get; set; }
        public string BillingAddress { get; set; }
        public string TypeOfBuissness { get; set; }
        public string ShippingAddress { get; set; }
        public string LandMark { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public int? Cityid { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string BAGPSCoordinates { get; set; }
        public string SAGPSCoordinates { get; set; }
        public string RefNo { get; set; }
        public string OfficePhone { get; set; }
        public string Emailid { get; set; }
        public string Familymember { get; set; }
        public string CreatedBy { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string imei { get; set; }
        public double MonthlyTurnOver { get; set; }

        public int? ExecutiveId { get; set; }
        public string SizeOfShop { get; set; }
        public int? Rating { get; set; }
        public int? ClusterId { get; set; }
        public string ClusterName { get; set; }
        public bool Deleted { get; set; }
        public bool Active { get; set; }
        public double lat { get; set; }
        public double lg { get; set; }

        public string Day { get; set; }
        public int DivisionId { get; set; }
        public int BeatNumber { get; set; }
        public string Rewardspoints { get; set; }
        public string fcmId { get; set; }

        public ICollection<GroupNotification> GroupNotification { get; set; }
        public ICollection<Notification> Notifications { get; set; }       

        [NotMapped]
        public bool check { get; set; }
        [NotMapped]
        public String Exception { get; set; }

        public string level { get; set; }
        public int ordercount { get; set; }

        [NotMapped]
        public int thisordercount { get; set; }
        [NotMapped]
        public int thisordercountCancelled { get; set; }
        [NotMapped]
        public int thisordercountRedispatch { get; set; }
        [NotMapped]
        public int thisordercountdelivered { get; set; }
        [NotMapped]
        public int thisordercountpending { get; set; }
        [NotMapped]
        public double thisordervalue { get; set; }
        [NotMapped]
        public int thisRAppordercount { get; set; }
        [NotMapped]
        public int thisSAppordercount { get; set; }
    }
}