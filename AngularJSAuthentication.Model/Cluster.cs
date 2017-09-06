using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularJSAuthentication.Model
{
   public class Cluster
    {
        [Key]
        public int ClusterId { get; set; }
        public string ClusterName { get; set; }
        public string WarehouseName { get; set; }
        public int? Warehouseid { get; set; }
        public int? CompanyId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public bool Active { get; set; }
        public bool Deleted { get; set; }
    }
}
