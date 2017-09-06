using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularJSAuthentication.Model
{
    public class FinalOrderDispatchedMaster
    {
        [Key]
        public int FinalOrderDispatchedMasterId { get; set; }        
        public int OrderDispatchedMasterId { get; set; }
        public int OrderId { get; set; }
        public int CompanyId { get; set; }
        public int? SalesPersonId { get; set; }
        public string SalesPerson { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
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

        public string DboyName { get; set; }
        public string DboyMobileNo { get; set; }
        public int? CityId { get; set; }
        public int Warehouseid { get; set; }
        public string WarehouseName { get; set; }
        public bool active { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime Deliverydate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool Deleted { get; set; }
        public int DivisionId { get; set; }
        public double PaymentAmount { get; set; }
        
        public string CheckNo { get; set; }
        public double CheckAmount { get; set; }
        public string ElectronicPaymentNo { get; set; }
        public double ElectronicAmount { get; set; }
        public double CashAmount { get; set; }
        public double deliveryCharge { get; set; }

        public double ShortAmount { get; set; }
        public string ShortReason { get; set; }
        // public virtual ICollection<OrderDispatchedDetails> orderDetails { get; set; }
        [NotMapped]
        public List<FinalOrderDispatchedMaster> AssignedOrders { get; set; }
    }
}
