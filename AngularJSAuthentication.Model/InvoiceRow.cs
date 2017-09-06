using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AngularJSAuthentication.Model
{
    public class InvoiceRow
    {
        [Key]
        public int invoicedetail_id { get; set; }
        public int invoice_id { get; set; }
        public string EmployeeId { get; set; }
        public string Desc { get; set; }
        public int Amount { get; set; }
        public string Quantity { get; set; }
        public int unit { get; set; }
        //public string note { get; set; }
        public int deleted { get; set; }
        public string product { get; set; }


    }
}
