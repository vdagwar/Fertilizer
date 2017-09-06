using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace AngularJSAuthentication.Model
{
    public class TaxMaster
    {
       
        [Key]
        public int TaxID { get; set; }
        public string TaxName { get; set; }
        public string TAlias { get; set; }
        public double TPercent { get; set; }
        public string TDiscription { get; set; }


        //-----------------------------------------------//
        public int CompanyId { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool Deleted { get; set; }
    }
}
