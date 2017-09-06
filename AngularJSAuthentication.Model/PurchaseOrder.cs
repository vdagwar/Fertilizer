using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularJSAuthentication.Model
{
    public class PurchaseOrder
    {
        [Key]
        public int PurchaseOrderId { get; set; }        
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public string itemNumber { get; set; }
        public string PurchaseSku { get; set; }
        public int ConversionFactor { get; set; }
        public double finalqty { get; set; }
        public int TotalQuantity { get; set; }
        public double Price { get; set; }
        public double TaxAmount { get; set; }
        public double TotalAmountIncTax { get; set; }
        public string Status { get; set; }
        public int CityId { get; set; }
        public string CityName { get; set; }
        public int SupplierId { get; set; }
        public string SupplierName { get; set; }
        public int? Warehouseid { get; set; }
        public string WarehouseName { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdateBy { get; set; }
        public bool Deleted { get; set; }
    }   
    public class PurchaseOrderMaster
    {
        [Key]
        public int PurchaseOrderId { get; set; }
        public int SupplierId { get; set; }
        public string SupplierName { get; set; }  
        public int? Warehouseid { get; set; }
        public string WarehouseName { get; set; }
        public string Status { get; set; }
        public double? discount1 { get; set; }
        public double Gr1_Amount { get; set; }
        public DateTime? Gr1_Date { get; set; }
        public double? discount2 { get; set; }
        public double Gr2_Amount { get; set; }
        public DateTime? Gr2_Date { get; set; }
        public double? discount3 { get; set; }
        public double Gr3_Amount { get; set; }
        public DateTime? Gr3_Date { get; set; }
        public double? discount4 { get; set; }
        public double Gr4_Amount { get; set; }
        public DateTime? Gr4_Date { get; set; }
        public double? discount5 { get; set; }
        public double Gr5_Amount { get; set; }
        public DateTime? Gr5_Date { get; set; }
        public double TotalAmount { get; set; }
        public DateTime CreationDate { get; set; }
        public string CreatedBy { get; set; }
        public bool Acitve { get; set; }

        public bool Gr1_Process { get; set; }
        public bool Gr2_Process { get; set; }
        public bool Gr3_Process { get; set; }
        public bool Gr4_Process { get; set; }
        public bool Gr5_Process { get; set; }
        public bool Gr_Process { get; set; }

        [NotMapped]
        public List<PurchaseOrderDetailRecived> purDetails { get; set; }
    }
    public class PurchaseOrderDetail
    {
        [Key]
        public int PurchaseOrderDetailId { get; set; }
        public int PurchaseOrderId { get; set; }
        public int SupplierId { get; set; }
        public int? Warehouseid { get; set; }
        public string WarehouseName { get; set; }
        
        public string SupplierName { get; set; }
        public int ItemId { get; set; }
        public string HSNCode { get; set; }
        public string SKUCode { get; set; }
        public string ItemName { get; set; }
        public double Price { get; set; }
        public int MOQ { get; set; }
        public int TotalQuantity { get; set; }
        public string SellingSku { get; set; }
        public string PurchaseSku { get; set; }
        public double TaxAmount { get; set; }
       
        public double TotalAmountIncTax { get; set; }
        public string Status { get; set; }
        public DateTime CreationDate { get; set; }
        public string CreatedBy { get; set; }
        public int ConversionFactor { get; set; }
        public string PurchaseName { get; set; }
        public double PurchaseQty { get; set; }
        [NotMapped]
        public int QtyRecived { get; set; }

    }
}
