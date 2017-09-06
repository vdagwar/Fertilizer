using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AngularJSAuthentication.Model
{
    public class Vehicle
    {
        [Key]
        public int VehicleId { get; set; }
        public string VehicleName { get; set; }
        public string VehicleNumber { get; set; }
        public double Capacity { get; set; }
        public bool isActive { get; set; }
        public int CompanyId { get; set; }
        public string City { get; set; }
        public int Cityid { get; set; }
        public int Warehouseid { get; set; }
        public string WarehouseName { get; set; }
        public string OwnerName { get; set; }
        public string OwnerAddress { get; set; }

        //public int ClusterId { get; set; }
        //public string ClusterName { get; set; }
        public DateTime CreatedDate { get; set; }                
        public DateTime UpdatedDate { get; set; }
        public bool isDeleted { get; set; }
    }
}
