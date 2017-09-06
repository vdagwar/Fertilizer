using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AngularJSAuthentication.Model
{
    public class WarehouseSubsubCategory
    {
        [Key]
        public int WhSubsubCategoryid { get; set; }
      
        public int WhSubCategoryid { get; set; }   //FOREIGN KEY

        public int WhCategoryid { get; set; }     //FOREIGN KEY

        public int Warehouseid { get; set; }  //FOREIGN KEY
        public int Stateid { get; set; }
        public int Cityid { get; set; }

        public int SubsubCategoryid { get; set; }
        public string SubsubcategoryName { get; set; }


        public string WarehouseName { get; set; }
        public string State { get; set; }
        public string City { get; set; }

        public bool IsVisible { get; set; }
        public int SortOrder { get; set; }

        public int CompanyId { get; set; }
     
   
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdateBy { get; set; }
        public bool Deleted { get; set; }
        


    }
}
