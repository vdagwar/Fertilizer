using GenricEcommers.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AngularJSAuthentication.Model
{
    public class PurchaseReturn
    {
        [Key]
        public int PurchaseReturnId { get; set; }
        public int supplierId { get; set; }
        public string supplierCode { get; set; }
        public string supplierName { get; set; }
        public int ItemId { get; set; }
        public int WarehouseId { get; set; }
        public string itemname { get; set; }
        public string PurchaseSku { get; set; }
        public string itemNumber { get; set; }
        public int Qyantity { get; set; }
        public string Status { get; set; }
        public int TotalQuantity { get; set; }
        public double UnitPrice { get; set; }
        public double TotalAmount { get; set; }
        public DateTime CreationDate { get; set; }
        public bool Deleted { get; set; }
    }
}
