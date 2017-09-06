using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularJSAuthentication.Model
{
    public class ItemMasterHistory
    {
        [Key]
        public int id { get; set; }
        public int ItemId { get; set; }
        public int Cityid { get; set; }
        public string CityName { get; set; }
        public int Categoryid { get; set; }
        public int SubCategoryId { get; set; }
        public int SubsubCategoryid { get; set; }
        public int warehouse_id { get; set; }
        public int SupplierId { get; set; }
        public string SUPPLIERCODES { get; set; }
        public int CompanyId { get; set; }
        //public int? Id { get; set; }
        public string CategoryName { get; set; }
        public int BaseCategoryid { get; set; }
        public string BaseCategoryName { get; set; }
        public string SubcategoryName { get; set; }
        public string SubsubcategoryName { get; set; }
        public string SupplierName { get; set; }
        public string itemname { get; set; }
        public string itemcode { get; set; }
        public string SellingUnitName { get; set; }  //Selling Unit
        public string PurchaseUnitName { get; set; }  //Purchase unit name
        public double price { get; set; }
        public double VATTax { get; set; }
        public bool active { get; set; }
        public string LogoUrl { get; set; }
        public string CatLogoUrl { get; set; }
        public int MinOrderQty { get; set; }
        public int PurchaseMinOrderQty { get; set; }
        public int GruopID { get; set; }
        public string TGrpName { get; set; }
        public double Discount { get; set; }
        public double UnitPrice { get; set; }
        public string Number { get; set; }
        public string PurchaseSku { get; set; }
        public string SellingSku { get; set; }
        public double? PurchasePrice { get; set; }
        public double? SellingPrice { get; set; }
        public double? GeneralPrice { get; set; }
        public string title { get; set; }
        public string Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public double PramotionalDiscount { get; set; }
        public double TotalTaxPercentage { get; set; }
        public string WarehouseName { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool Deleted { get; set; }
        public bool IsDailyEssential { get; set; }
        public double DisplaySellingPrice { get; set; }
        public string StoringItemName { get; set; }
        public double SizePerUnit { get; set; }
        public double itemarea { get; set; }
        public double NoOfUnit { get; set; }
        public string HindiName { get; set; }
        public string Barcode { get; set; }
        public int? marginPoint { get; set; }
        public int? promoPoint { get; set; }


        [NotMapped]
        public int CurrentStock { get; set; }
    }
}
