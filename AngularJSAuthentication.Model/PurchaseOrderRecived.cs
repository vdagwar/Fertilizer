using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularJSAuthentication.Model
{
    public class PurchaseOrderMasterRecived
    {
        [Key]
        public int PurchaseOrderMasterRecivedId { get; set; }
        public int PurchaseOrderId { get; set; }
        public int SupplierId { get; set; }
        public string SupplierName { get; set; }  
        public int? Warehouseid { get; set; }
        public string WarehouseName { get; set; }
        public string Status { get; set; }
        public DateTime CreationDate { get; set; }
        public string CreatedBy { get; set; }
        public bool Acitve { get; set; }
        public string Comments { get; set; }
    }
    public class PurchaseOrderDetailRecived
    {
        [Key]
        public int PurchaseOrderDetailRecivedId { get; set; }
        public int PurchaseOrderMasterRecivedId { get; set; }
        public int PurchaseOrderDetailId { get; set; }
        public int PurchaseOrderId { get; set; }
        public int SupplierId { get; set; }
        public int? Warehouseid { get; set; }
        public string WarehouseName { get; set; }
        [NotMapped]
        public bool isDeleted { get; set; }
        public string SupplierName { get; set; }
        public int ItemId { get; set; }
        public string HSNCode { get; set; }
        public string SKUCode { get; set; }
        public string PurchaseSku { get; set; }
        public string ItemName { get; set; }
        public double Price { get; set; }
        public double PriceRecived { get; set; }
        public int MOQ { get; set; }
        public int TotalQuantity { get; set; }
        public double TaxAmount { get; set; }
        public double TotalTaxPercentage { get; set; }
        public int PurchaseQty { get; set; }
        public double TotalAmountIncTax { get; set; }
        public string Status { get; set; }
        public DateTime CreationDate { get; set; }
        public string CreatedBy { get; set; }
        public int? QtyRecived1 { get; set; }
        public int? QtyRecived2 { get; set; }
        public int? QtyRecived3 { get; set; }
        public int? QtyRecived4 { get; set; }
        public int? QtyRecived5 { get; set; }
        public double? Price1 { get; set; }
        public double? Price2 { get; set; }
        public double? Price3 { get; set; }
        public double? Price4 { get; set; }
        public double? Price5 { get; set; }
        public double? dis1 { get; set; }
        public double? dis2 { get; set; }
        public double? dis3 { get; set; }
        public double? dis4 { get; set; }
        public double? dis5 { get; set; }
        public double? QtyRecived
        {
            get
            {
                return (QtyRecived1 + QtyRecived2 + QtyRecived3 + QtyRecived4 + QtyRecived5);
            }
        }
    }
}
