using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularJSAuthentication.Model
{
    public partial class TrnSaleOrder
    {
        [Key]
        public int SaleOrderId { get; set; }
        public int CustomerId { get; set; }
        public int LocalityId { get; set; }
        public int InvoiceNumber { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string AddressLine4 { get; set; }
        public string ContactNumber1 { get; set; }
        public string ContactNumber2 { get; set; }
        public double GrossAmount { get; set; }
        public double DiscountAmount { get; set; }
        public double TaxAmount { get; set; }
        public double TotalAmount { get; set; }
        public string PinCode { get; set; }
        public int WarehouseId { get; set; }
        public string Status { get; set; }
        public DateTime Date { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }

        public virtual ICollection<TrnSaleOrderDetail> TrnSaleOrderDetails { get; set; }
       
    }
}
