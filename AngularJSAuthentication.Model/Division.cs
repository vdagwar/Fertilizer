using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AngularJSAuthentication.Model
{
    public class Division
    {
        [Key]
        public int DivisionId { get; set; }
        public string DivisionName { get; set; }
        public string Description { get; set; }  
        public DateTime CreatedDate { get; set; }                
        public DateTime UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdateBy { get; set; }
        public bool Deleted { get; set; }
    }
}
