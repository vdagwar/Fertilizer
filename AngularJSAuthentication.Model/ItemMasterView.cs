using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularJSAuthentication.Model
{
    class ItemMasterView
    {
        public class ItemMaster
        {
            [Key]
            public int ItemId { get; set; }
            public int UnitId { get; set; }
            public int Categoryid { get; set; }
            public int SubCategoryId { get; set; }
            public int SubsubCategoryid { get; set; }
            public int warehouse_id { get; set; }
            public int SupplierId { get; set; }
            public int CompanyId { get; set; }
            public string CategoryName { get; set; }
            public string SubcategoryName { get; set; }
            public string SubsubcategoryName { get; set; }
            public string SupplierName { get; set; }
            public string itemname { get; set; }
            public string itemcode { get; set; }
            public string UnitName { get; set; }
            public double price { get; set; }
            public double VATTax { get; set; }
            public bool active { get; set; }
            public string LogoUrl { get; set; }
            public string WarehouseName { get; set; }
            public DateTime CreatedDate { get; set; }
            public DateTime UpdatedDate { get; set; }
            public bool Deleted { get; set; }

        }
    }
}
