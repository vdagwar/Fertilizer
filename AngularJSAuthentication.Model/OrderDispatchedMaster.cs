using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularJSAuthentication.Model
{
    public class OrderDispatchedMaster
    {
        [Key]
        public int OrderDispatchedMasterId { get; set; }
        public int OrderId { get; set; }
        public int CompanyId { get; set; }
        public int? SalesPersonId { get; set; }
        public string SalesPerson { get; set; }
        public string SalesMobile { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string ShopName { get; set; }
        public string Skcode { get; set; }
        public string Status { get; set; }
        public string invoice_no { get; set; }
        public string Trupay { get; set; }
        public int CustomerCategoryId { get; set; }
        public string CustomerCategoryName { get; set; }
        public string CustomerType { get; set; }
        public string Customerphonenum { get; set; }
        public string BillingAddress { get; set; }
        public string ShippingAddress { get; set; }
        public string comments { get; set; }
		public double deliveryCharge { get; set; }
        public double TotalAmount { get; set; }
        public double GrossAmount { get; set; }
        public double DiscountAmount { get; set; }
        public double TaxAmount { get; set; }
        public double SGSTTaxAmmount { get; set; }
        public double CGSTTaxAmmount { get; set; }
        public string CheckNo { get; set; }
        public double CheckAmount { get; set; }
        public string ElectronicPaymentNo { get; set; }
        public double ElectronicAmount { get; set; }
        public double CashAmount { get; set; }
        public double PaymentAmount { get; set; }
        public double RecivedAmount { get; set; }
        public string DboyName { get; set; }
        public string DboyMobileNo { get; set; }
        public int ReDispatchCount { get; set; }

        public int? CityId { get; set; }
        public int Warehouseid { get; set; }
        public string WarehouseName { get; set; }
        public bool active { get; set; }
        public DateTime OrderedDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime Deliverydate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool Deleted { get; set; }
        public int DivisionId { get; set; }
        public int ClusterId { get; set; }
        public string Signimg { get; set; }
        public string ClusterName { get; set; }
        [NotMapped]
        public DateTime OrderDate { get; set; }
        [NotMapped]
        public bool check { get; set; }
        [NotMapped]
        public double lat { get; set; }
        [NotMapped]
        public double lg { get; set; }

        public bool cash { get; set; }
        public bool electronic { get; set; }
        public bool cheq { get; set; }
        public double BounceCheqAmount { get; set; }
        public double? WalletAmount { get; set; }
        public double? RewardPoint { get; set; }
        public double ShortAmount { get; set; }
        public int? OrderTakenSalesPersonId { get; set; }
        public string OrderTakenSalesPerson { get; set; }
        public string Tin_No { get; set; }
        public virtual ICollection<OrderDispatchedDetails> orderDetails { get; set; }

        [NotMapped]
        public List<OrderDispatchedMaster> AssignedOrders { get; set; }

    }

    public class OrderDispatchedMasterDTO {
        public int OrderDispatchedMasterId { get; set; }
        public int OrderId { get; set; }
        public int OrderSeq { get; set; }
        public int CompanyId { get; set; }
        public int? SalesPersonId { get; set; }
        public string SalesPerson { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string ShopName { get; set; }
        public string Skcode { get; set; }
        public string Status { get; set; }
        public string invoice_no { get; set; }
        public int CustomerCategoryId { get; set; }
        public string CustomerCategoryName { get; set; }
        public string CustomerType { get; set; }
        public string Customerphonenum { get; set; }
        public string BillingAddress { get; set; }
        public string ShippingAddress { get; set; }
        public string comments { get; set; }

        public double TotalAmount { get; set; }
        public double GrossAmount { get; set; }
        public double DiscountAmount { get; set; }
        public double TaxAmount { get; set; }
        //public double RecivedAmount { get; set; }

        public string CheckNo { get; set; }
        public double CheckAmount { get; set; }
        public string ElectronicPaymentNo { get; set; }
        public double ElectronicAmount { get; set; }
        public double CashAmount { get; set; }
        public double PaymentAmount { get; set; }
        public double RecivedAmount { get; set; }

        public string DboyName { get; set; }
        public string DboyMobileNo { get; set; }
        public int ReDispatchCount { get; set; }

        public int? CityId { get; set; }
        public int Warehouseid { get; set; }
        public string WarehouseName { get; set; }
        public bool active { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime Deliverydate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool Deleted { get; set; }
        public int DivisionId { get; set; }
        public double deliveryCharge { get; set; }
        [NotMapped]
        public DateTime OrderDate { get; set; }
        [NotMapped]
        public bool check { get; set; }
        [NotMapped]
        public double lat { get; set; }
        [NotMapped]
        public double lg { get; set; }
        [NotMapped]
        public int ClusterId { get; set; }
        [NotMapped]
        public string ClusterName { get; set; }
        public virtual ICollection<OrderDispatchedDetails> orderDetails { get; set; }
    }
}
