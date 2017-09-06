using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace GenricEcommers.Models
{
    public class Offer
    {
        [Key]
        public int OfferId { get; set; }
        public string OfferCategory { get; set; }
        public string OfferType { get; set; }
        public string OfferName { get; set; }
        public double Amount { get; set; }
        //public string OfferCode { get; set; }
        public string Description { get; set; }
        public double MinAmount { get; set; }
        public double MaxAmount { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdateDate { get; set; }

    }
}
