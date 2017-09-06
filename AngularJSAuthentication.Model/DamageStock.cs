using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularJSAuthentication.Model
{
   public class DamageStock
    {
        [Key]
        public int DamageStockId { get; set; }
        public int Warehouseid { get; set; }
        public string WarehouseName { get; set; }
        public int ItemId { get; set; }
        public string ItemNumber { get; set; }
        public string ItemName { get; set; }
        public double UnitPrice { get; set; }
        public string ReasonToTransfer { get; set; }
        public int DamageInventory { get; set; }
        public bool Deleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

    }
}
