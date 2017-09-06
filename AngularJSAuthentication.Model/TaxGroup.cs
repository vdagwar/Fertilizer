using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace AngularJSAuthentication.Model
{
    public class TaxGroup
    {
       
        [Key]
        public int GruopID { get; set; }
     
        public string TGrpName { get; set; }
        public string TGrpAlias { get; set; }     
        public string TGrpDiscription { get; set; }
        public int CompanyId { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool Deleted { get; set; }
    }
}
