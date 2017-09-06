using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace GenricEcommers.Models
{
    public class Coupon
    {
        [Key]

        public int OfferId { get; set; }
        public string OfferCode { get; set; }
        public string OfferType { get; set; }   //Fixed Discount , //CashBack  , //Bogo
        public string OfferName { get; set; }
        public string Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        //coupon and cashback
        public double? Discount { get; set; }
        public double? MinAmount { get; set; }
        //bogo offer and cashback
        public int? SourceItemId { get; set; }
        public string SourceItemName { get; set; }
        //bogo offer
        public int? FreeItemId { get; set; }
        public string FreeItemName { get; set; }
        //cashback
        public string DiscountType { get; set; }   //Percent or Amount 
        //public DateTime CreatedDate { get; set; }
        //public DateTime UpdatedDate { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ItemImage { get; set; }
    }
}
