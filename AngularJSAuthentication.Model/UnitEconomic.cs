using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AngularJSAuthentication.Model
{
    public class UnitEconomic
    {
        [Key]
        public int unitId { get; set; }
        public int Warehouseid { get; set; }
        public string Label1 { get; set; }
        public string Label2 { get; set; }  
        public string Label3 { get; set; }
        public double Amount { get; set; }
        public string Discription { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool Deleted { get; set; }
        public bool IsActive { get; set; }
    }
}
