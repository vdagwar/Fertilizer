using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularJSAuthentication.Model
{
  public  class Supplier
    {
        [Key]
        public int SupplierId { get; set; }
        public int CompanyId { get; set; }
        public int SupplierCaegoryId { get; set;}
        public bool Deleted { get; set; }
        public string CategoryName { get; set; }
        public string Brand { get; set; }
        public string Name { get; set; }
        public string SUPPLIERCODES { get; set; }
        //change
        public string OfficePhone { get; set; }
        public string Bank_Name { get; set; }
        public string Bank_AC_No { get; set; }
        public string Bank_Ifsc { get; set; }
        public string PhoneNumber { get; set; }
        public string TINNo { get; set; }

        public int Avaiabletime { get; set; }
        
        public int rating { get; set; }
        public string BillingAddress { get; set; } //Address 
        public string ShippingAddress { get; set; }//Location
        public string Comments { get; set; }
       

        public string MobileNo { get; set; }
        public string EmailId { get; set; }
        public string WebUrl { get; set; }
        public string SalesManager { get; set; }
        public string ContactPerson { get; set; }
        public string ContactImage { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        [NotMapped]
        public String Exception { get; set; }
    }
}
