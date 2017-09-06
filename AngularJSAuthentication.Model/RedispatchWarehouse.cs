using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularJSAuthentication.Model
{
    public class RedispatchWarehouse
    {
        [Key]
        public int RedispatchWarehouseId { get; set; }
        public int OrderDispatchedMasterId { get; set; }
        public int OrderId { get; set; }
        public int CompanyId { get; set; }
        public int DeliveryBoyId { get; set; }
        public string comments { get; set; }        
        public string DboyName { get; set; }
        public string DboyMobileNo { get; set; }
        public string Status { get; set; }
        public int ReDispatchCount { get; set; }
        public int Warehouseid { get; set; }
        public bool active { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool Deleted { get; set; }
        

    }
}
