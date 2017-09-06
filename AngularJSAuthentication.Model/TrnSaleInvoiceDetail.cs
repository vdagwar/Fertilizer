using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularJSAuthentication.Model
{
    public class TrnSaleInvoiceDetail
    {
        [Key]

        public int SaleInvoiceDetailId { get; set; }
        public int SaleInvoiceId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public string Remark { get; set; }
        public double Amount { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
}
