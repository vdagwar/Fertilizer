using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularJSAuthentication.Model
{
   public class AllInvoice
    {



        [Key]
        public int id { get; set; }
        // public int CompanyId { get; set; }
        public int InvoiceID { get; set; }
        public int CustomerId { get; set; }
        public string currency { get; set; }
        //public string task { get; set; }
        public DateTime lastdate { get; set; }
        public DateTime Issuedate { get; set; }
        //public string CreatedBy { get; set; }
        //public string UpdateBy { get; set; }
        public bool Deleted { get; set; }

       
        //public string client { get; set; }
       
       // public Task TaskID { get; set; }
        //public int quantity { get; set; }
        public int amount { get; set; }        
        // public string desc { get; set; }
        public int discount { get; set; }
        public string note { get; set; }
        public string Customer { get; set; }
        public int PONUmber { get; set; }
        public string Subject { get; set; }
        //public string product { get; set; }
        //public int Quantoty { get; set; }        
        public int tax { get; set; }
       // public DateTime issuedate { get; set; }
        public string duedate { get; set; }
        public bool save { get; set; }

    }
}
