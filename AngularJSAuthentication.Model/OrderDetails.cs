using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularJSAuthentication.Model
{
    public class OrderDetails
    {
        [Key]
        public int OrderDetailsId { get; set; }
        public int  OrderId { get; set; }   
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public String City { get; set; }
        public string Mobile { get; set; }
        public DateTime OrderDate { get; set; }
        public int CompanyId { get; set; }
        public int? CityId { get; set; }
        public int Warehouseid { get; set; }
        public string WarehouseName { get; set; }
        public string CategoryName { get; set; }
        public string SubcategoryName { get; set; }
        public string SubsubcategoryName { get; set; }

        public int ItemId { get; set; }
        public string Itempic { get; set; }
        public string itemname { get; set; }
        public string itemcode { get; set; }
        public string itemNumber { get; set; }
        public string HSNCode { get; set; }
        public string Barcode { get; set; }
      
        public double price { get; set; }
        public double UnitPrice { get; set; }
        public double Purchaseprice { get; set; }

        public int MinOrderQty { get; set; }
        public double MinOrderQtyPrice { get; set; }
        public int qty { get; set; }
        // new calculation shopkirana
        // to show no of qty in peti
        public int Noqty { get; set; }
        public double AmtWithoutTaxDisc { get; set; }
        public double AmtWithoutAfterTaxDisc { get; set; }
        public double TotalAmountAfterTaxDisc { get; set; }


        public double NetAmmount { get; set; }
        public double DiscountPercentage { get; set; }
        public double DiscountAmmount { get; set; }
        public double NetAmtAfterDis { get; set; }
        public double TaxPercentage { get; set; }
        public double TaxAmmount { get; set; }
        public double SGSTTaxPercentage { get; set; }
        public double SGSTTaxAmmount { get; set; }
        public double CGSTTaxPercentage { get; set; }
        public double CGSTTaxAmmount { get; set; }
        public double TotalAmt { get; set; }
       
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool Deleted { get; set; }
        public string Status { get; set; }
        public double SizePerUnit { get; set; }
        public int? marginPoint { get; set; }
        public int? promoPoint { get; set; }
        public double NetPurchasePrice { get; set; }
        [NotMapped]
        public double CurrentStock { get; set; }
    }
}
