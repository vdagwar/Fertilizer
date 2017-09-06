using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularJSAuthentication.Model
{
   public class WarehouseSupplier
    {
       [Key]
        public int Whsupid { get; set; }
        public int Stateid { get; set; }
        public string StateName { get; set; }   
        public int Cityid { get; set; }
        public string CityName { get; set; }
        public int Warehouseid { get; set; }
        public string WarehouseName { get; set; }
        public int SupplierId { get; set; }
        public string SupplierName { get; set; }
        public int CompanyId { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool Deleted { get; set; }

    }
}
