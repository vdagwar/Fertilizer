using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GenricEcommers.Models
{
    public class Wallet
    {
        [Key]
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public DateTime? TransactionDate { get; set; }
        public double? TotalAmount { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool Deleted { get; set; }

        [NotMapped]
        public double? CreditAmount { get; set; }

        [NotMapped]
        public double? DebitAmount { get; set; }
        [NotMapped]
        public string Skcode { get; set; }
        [NotMapped]
        public string ShopName { get; set; }
    }
}
