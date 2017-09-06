using GenricEcommers.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AngularJSAuthentication.Model
{
    public class FreeItem
    {
        [Key]
        public int Id { get; set; }
        public int PurchaseOrderId { get; set; }
        public int supplierId { get; set; }
        public string SupplierName { get; set; }
        public int ItemId { get; set; }
        public int WarehouseId { get; set; }
        public string itemname { get; set; }
        public string itemNumber { get; set; }
        public string PurchaseSku { get; set; }
        public string Status { get; set; }
        public int TotalQuantity { get; set; }
        public DateTime CreationDate { get; set; }
        public bool Deleted { get; set; }
    }
    public class SKFreeItem
    {
        [Key]
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public string SkCode { get; set; }
        public int ItemId { get; set; }
        public double Amount { get; set; }
        public int WarehouseId { get; set; }
        public string itemname { get; set; }
        public string itemNumber { get; set; }
        public string SellingSku { get; set; }
        public string Status { get; set; }
        public int TotalQuantity { get; set; }
        public DateTime CreationDate { get; set; }
        public bool Deleted { get; set; }
    }
}
