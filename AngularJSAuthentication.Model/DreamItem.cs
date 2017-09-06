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
    public class DreamOrder
    {
        [Key]
        public int Order_Id { get; set; }
        public int CustomerId { get; set; }
        public string Skcode { get; set; }
        public string ShopName { get; set; }
        public string CustomerMobile { get; set; }
        public string ShippingAddress { get; set; }
        public int? Warehouseid { get; set; }
        public string WarehouseName { get; set; }
        public double? WalletPoint { get; set; }
        public string Status { get; set; }
        public int? CompanyId { get; set; }
        public int? SalesPersonId { get; set; }
        public int? CityId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? Deliverydate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool Deleted { get; set; }
        public int ReDispatchCount { get; set; }
        public string ReasonCancle { get; set; }
        public string comments { get; set; }

        public string DboyName { get; set; }
        public string DboyMobileNo { get; set; }
        public virtual ICollection<DreamItem> DreamItemDetails { get; set; }
    }
    public class DreamItem
    {
        [Key]
        public int Id { get; set; }
        public int Order_Id { get; set; }
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public int OrderQty { get; set; }
        public string Discription { get; set; }
        public string Status { get; set; }
        public string Skcode { get; set; }
        public string ShopName { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool Deleted { get; set; }
    }
}