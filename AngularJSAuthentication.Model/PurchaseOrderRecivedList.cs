using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularJSAuthentication.Model
{
    public class PurchaseOrderRecivedList
    {
        public DateTime OrderDate { get; set; }
        public int? Warehouseid { get; set; }
        public string WarehouseName { get; set; }
        public int? Cityid { get; set; }
        public int? SupplierId { get; set; }
        public string SupplierName { get; set; }
        public int OrderId { get; set; }
        public int OrderDetailsId { get; set; }
        public string PurchaseSku { get; set; }
        public string SellingSku { get; set; }


        public int? ItemId { get; set; }
        public string SKUCode { get; set; }
        public string ItemName { get; set; }
        public int PurchaseMinOrderQty { get; set; }
        public string Unit { get; set; }
        public string Discription { get; set; }

        public int qty { get; set; }
        public int requiredqty { get; set; }

        public int CurrentInventory { get; set; }
        public int NetPurchaseQty { get; set; }// TotalPurchaseQty=CurrentInventory-qty;
        public double? Price { get; set; }
        public double NetAmmount { get; set; }
        public double TaxPercentage { get; set; }
        public double TaxAmount { get; set; }
        public double TotalAmountIncTax { get; set; }
        public string Status { get; set; }


        public DateTime CreationDate { get; set; }
        public bool Deleted { get; set; }

    }

}
