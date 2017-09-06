using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularJSAuthentication.Model
{
    public class DailyEssential
    {
        [Key]
        public int DailyEssentialId { get; set; }
        public string CustAddress { get; set; }
        public string CustMobile { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime StartDate { get; set; }
        public bool Monday { get; set; }
        public bool Tuesday { get; set; }
        public bool Wednesday { get; set; }
        public bool Thursday { get; set; }
        public bool Friday { get; set; }
        public bool Saturday { get; set; }
        public bool Sunday { get; set; }

        public string itemname { get; set; }
        public string LogoUrl { get; set; }
        public double UnitPrice { get; set; }
        public int DailyItemQty1 { get; set; }
        public int DailyItemQty2 { get; set; }
        public int DailyItemQty3 { get; set; }
        public int DailyItemQty4 { get; set; }
        public int DailyItemQty5 { get; set; }
        public int DailyItemQty6 { get; set; }
        public int DailyItemQty7 { get; set; }
        public int ItemId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool Deleted { get; set; }
       
        
    }
}
