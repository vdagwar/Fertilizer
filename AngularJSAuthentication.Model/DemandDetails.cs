using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularJSAuthentication.Model
{
   public class DemandDetails
    {
        [Key]
        public int DemandDetailId { get; set; }
        public int WharehouseId { get; set; }
        public int DemandMasterId { get; set; }
        public int ItemId { get; set; }
        public string ItemCode { get; set; }
       // public string WarehouseName { get; set; }
        public string Description { get; set; }
        public int? Quantity { get; set; }
        public int? CurrentInventry { get; set; }
        public int? NetQuantity { get; set; }
        public int? MOQ { get; set; }
        public int? OrderQuantity { get; set; }
        public DateTime CreatedDate { get; set; }
        [NotMapped]
        public string itemname { get; set; }

    }
    
}
