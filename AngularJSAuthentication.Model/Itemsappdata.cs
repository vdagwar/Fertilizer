using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularJSAuthentication.Model
{
    public class Itemsappdata
    {
        
       // public int ItemId { get; set; }
       // public int Cityid { get; set; }
       // public string CityName { get; set; }
        public int Categoryid { get; set; }
        public int BaseCategoryId { get; set; }
        public int SubCategoryId { get; set; }
        public int SubsubCategoryid { get; set; }
        //public int warehouse_id { get; set; }
        public int SupplierId { get; set; }
       // public string SUPPLIERCODES { get; set; }
        public string CategoryName { get; set; }
        public string SubcategoryName { get; set; }
        public string SubsubcategoryName { get; set; }
       // public string itemname { get; set; }
        //public string itemcode { get; set; }
        //public double price { get; set; }
       // public string LogoUrl { get; set; }
        public string CatLogoUrl { get; set; }
        //public int MinOrderQty { get; set; }
        //public double Discount { get; set; }
        //public double UnitPrice { get; set; }
        //item promotion
        //public string title { get; set; }
        //public string Description { get; set; }
        //public DateTime? StartDate { get; set; }
        //public DateTime? EndDate { get; set; }
        //public double PramotionalDiscount { get; set; }
        //public double TotalTaxPercentage { get; set; }
        //public string WarehouseName { get; set; }
       

    }
}
