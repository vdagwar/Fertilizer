using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularJSAuthentication.Model
{
    public class DamageOrderMaster
    {
        [Key]
        public int DamageOrderId { get; set; }
        public int CompanyId { get; set; }
        public int? SalesPersonId { get; set; }
        public string SalesPerson { get; set; }
        public string SalesMobile { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string Skcode { get; set; }
        public string ShopName { get; set; }
        public string Status { get; set; }
        public string invoice_no { get; set; }
        public int CustomerCategoryId { get; set; }
        public string CustomerCategoryName { get; set; }
        public string CustomerType { get; set; }
        public string Customerphonenum { get; set; }
        public string BillingAddress { get; set; }
        public string ShippingAddress { get; set; }

        public double TotalAmount { get; set; }
        public double GrossAmount { get; set; }
        public double DiscountAmount { get; set; }
        public double TaxAmount { get; set; }        
        
        public int? CityId { get; set; }
        public int Warehouseid { get; set; }
        public string WarehouseName { get; set; }
        public bool active { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? Deliverydate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool Deleted { get; set; }
        public int ReDispatchCount { get; set; }
        public int DivisionId { get; set; }
        public string ReasonCancle { get; set; }
        public int ClusterId { get; set; }
        public string ClusterName { get; set; }
        public double? deliveryCharge { get; set; }
        public double? WalletAmount { get; set; }
        public double? UsedPoint { get; set; }
        public double? RewardPoint { get; set; }
        public double ShortAmount { get; set; }
        public string comments { get; set; }
        public int? OrderTakenSalesPersonId { get; set; }
        public string OrderTakenSalesPerson { get; set; }
        public string Tin_No { get; set; }
        public string ShortReason { get; set; }
        public virtual ICollection<DamageOrderDetails> DamageorderDetails { get; set; }
        
        public bool orderProcess { get; set; }
        public bool accountProcess { get; set; }
        public bool chequeProcess { get; set; }
        public bool epaymentProcess { get; set; }
    }
    public class DamageOrder
    {

        public int DamageStockId { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public int Warehouseid { get; set; }
        public string WarehouseName { get; set; }
        public int ItemId { get; set; }
        public string ItemNumber { get; set; }
        public string ItemName { get; set; }
        public double UnitPrice { get; set; }
        public double DefaultUnitPrice { get; set; }
        public double TotalAmount { get; set; }
        public int qty { get; set; }
        public string ShippingAddress { get; set; }

    }
}