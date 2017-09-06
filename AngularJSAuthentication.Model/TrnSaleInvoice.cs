using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularJSAuthentication.Model
{
    public partial class TrnSaleInvoice
    {
        [Key]
        public int SaleInvoiceId { get; set; }
        public int CustomerId { get; set; }
        public string PaymentMode { get; set; }
        public string PaymentDetail { get; set; }
        public string Remark { get; set; }
        public double GrossAmount { get; set; }
        public double DiscountAmount { get; set; }
        public double TaxAmount { get; set; }
        public double TotalAmount { get; set; }
     
        public DateTime Date { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }

        public virtual ICollection<TrnSaleInvoiceDetail> SaleInvoiceDetails { get; set; }
    }
}
