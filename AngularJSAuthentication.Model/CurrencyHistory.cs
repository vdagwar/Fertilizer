using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GenricEcommers.Models
{
    public class CurrencyHistory
    {
        [Key]
        public int CurrencyHistoryid { get; set; }
       // public int PeopleId { get; set; }
        public string status { get; set; }
        //public string Stock_status { get; set; }
        public double? OneRupee { get; set; }
        public int? onerscount { get; set; }
        public double? TwoRupee { get; set; }
        public int? tworscount { get; set; }
        public double? FiveRupee { get; set; }
        public int? fiverscount { get; set; }
        public double? TenRupee { get; set; }
        public int? tenrscount { get; set; }
        public double? TwentyRupee { get; set; }
        public int? Twentyrscount { get; set; }
        public double? fiftyRupee { get; set; }
        public int? fiftyrscount { get; set; }
        public double? HunRupee { get; set; }
        public int? hunrscount { get; set; }
        public double? fiveHRupee { get; set; }
        public int? fivehrscount { get; set; }
        public double? twoTHRupee { get; set; }
        public int? twoTHrscount { get; set; }
        public double TotalAmount { get; set; }
        public double checkTotalAmount { get; set; }
        public string checknumber { get; set; }
        public string checkamount { get; set; }
        public double Dueamount { get; set; }
        public string Dueamountstatus { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool Deleted { get; set; }

        public string ArrayIds { get; set; }
        [NotMapped]
        public int DBoyCId { get; set; }
        [NotMapped]
        public int DBoyPeopleId { get; set; }
        [NotMapped]
        public string DboyName { get; set; }
        [NotMapped]
        public List<CurrencyHistory> AssignAmountId { get; set; }

    }
}
