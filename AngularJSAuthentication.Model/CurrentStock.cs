using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularJSAuthentication.Model
{
   public class CurrentStock
    {
        [Key]
        public int StockId { get; set; }
        public int? CityId { get; set; }
        public string CityName { get; set; }
        public int Warehouseid { get; set; }
        public string WarehouseName { get; set; }
        public int ItemId { get; set; }
        public string ItemNumber { get; set; }

        //public string PurchaseSku { get; set; }
        //public string SellingSku { get; set; }
        public string ItemName { get; set; }

        public byte[] BarcodeImage { get; set; }
        public string Barcode { get; set; }

        public int CurrentInventory { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime UpdatedDate { get; set; }        
        public string CreatedBy { get; set; }
        public string UpdateBy { get; set; }
        public bool Deleted { get; set; }
        public string ManualReason { get; set; }
    }
}
