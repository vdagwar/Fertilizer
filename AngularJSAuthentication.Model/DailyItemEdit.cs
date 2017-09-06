using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularJSAuthentication.Model
{
    public class DailyItemEdit
    {
        [Key]
        public int DailyItemCancelId { get; set; }
        public string CustAddress { get; set; }
        public string CustMobile { get; set; }
        public DateTime EditDate { get; set; }
        public string itemname { get; set; }
        public int Qty { get; set; }
        public int ItemId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool Deleted { get; set; }
       
        
    }
}
