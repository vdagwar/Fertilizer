using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularJSAuthentication.Model
{
   public class CurrentStockHistory
    {
        [Key]
        public int id { get; set; }
        public int StockId { get; set; }
        public int? CityId { get; set; }
        public string CityName { get; set; }
        public int Warehouseid { get; set; }
        public string WarehouseName { get; set; }
        public int ItemId { get; set; }
        public string ItemNumber { get; set; }
        public string ItemName { get; set; }
        public byte[] BarcodeImage { get; set; }
        public string Barcode { get; set; }
        public int CurrentInventory { get; set; }
        public int? InventoryIn { get; set; }
        public int? InventoryOut { get; set; }
        public int? TotalInventory { get; set; }
        public int? DamageInventoryOut { get; set; }
        public int? PurchaseInventoryOut { get; set; }
        public int? OrderCancelInventoryIn { get; set; }
        public int? OdOrPoId { get; set; }
        public int? ManualInventoryIn { get; set; }
        public string ManualReason { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? updationDate { get; set; }
        public bool Deleted { get; set; }
    }
}
