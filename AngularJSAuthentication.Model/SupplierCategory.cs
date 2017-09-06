using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AngularJSAuthentication.Model
{
    public class SupplierCategory
    {
        [Key]
        public int SupplierCaegoryId { get; set; }
        public int CompanyId { get; set; }
        public string CategoryName {get;set;}
        [NotMapped]
        public String Exception { get; set; }
    }
}
