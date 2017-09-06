using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AngularJSAuthentication.Model
{
    public class Warehouse
    {
        [Key]
        public int Warehouseid { get; set; }
        public string WarehouseName { get; set; }

        public string aliasName { get; set; }  
        public bool active { get; set; }
        public int CompanyId { get; set; }

        public int Stateid { get; set; }
        public string StateName { get; set; }
        public int Cityid { get; set; }
        public string CityName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        
        public DateTime CreatedDate { get; set; }                
        public DateTime UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdateBy { get; set; }
        public bool Deleted { get; set; }
        public int? GruopID { get; set; }
        public string TGrpName { get; set; }
       

        public virtual ICollection<WarehouseCategory> warehouseCategory { get; set; }

    }
}
