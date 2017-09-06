using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularJSAuthentication.Model
{
    public partial class TrnPurchaseInvoice
    {
        [Key]
        public int PurchaseInvoiceId { get; set; }
        public int SupplierId { get; set; }

        [NotMapped]
        public String SupplierName {get;set;}
        [NotMapped]
        public String Address { get; set; }
        [NotMapped]
        public String ContactNumber1 { get; set; }

        public string PaymentMode { get; set; }
        public string PaymentDetail { get; set; }
        public string Remark { get; set; }
        public double GrossAmount { get; set; }
        public double DiscountAmount { get; set; }
        public double TaxAmount { get; set; }
        public double TotalAmount { get; set; }
        public DateTime Date { get; set; }


        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        
        
        public virtual ICollection<TrnPurchaseInvoiceDetail> PurchaseOrderDetails { get; set; }
    }
}
