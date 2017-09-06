using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace GenricEcommers.Models
{
    public class DeliveryCharge
    {
        [Key]
        public int id { get; set; }
        public double? min_Amount { get; set; }
        public double? max_Amount { get; set; }   
        public double? del_Charge { get; set; }
        public int? warhouse_Id { get; set; }
        public int? cluster_Id { get; set; }
        public string warhouse_Name { get; set; }
        public string cluster_Name { get; set; }
        public bool IsActive { get; set; }
        public bool isDeleted { get; set; }
    }
}
