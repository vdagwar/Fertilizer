using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularJSAuthentication.Model
{
    public class DBoySummary
    {       
        public int OrderId { get; set; }
        public int? SalesPersonId { get; set; }
        public string DBoyName { get; set; }
        public string SalesPerson { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string Skcode { get; set; }
        public string ShopName { get; set; }
        public string Status { get; set; }
        public string invoice_no { get; set; }
        public double GrossAmount { get; set; }
        public double cashAmount { get; set; }
        public double chequeAmount { get; set; }
        public string comments { get; set; }
        public string chequeNo { get; set; }
        public int? qty { get; set; }
    }    
}