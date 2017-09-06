using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularJSAuthentication.Model
{
    public class ShortSetttle
    {
        [Key]
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public int Warehouseid { get; set; }
        public string WarehouseName { get; set; }
        public string DboyName { get; set; }
        public string DboyMobileNo {get; set;}
        public string Status { get; set; }
        public double ShortAmount { get; set; }
        public double DiscountAmount { get; set; }
        public double GrossAmount { get; set; }
        public bool Deleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ShortReason { get; set; }
    }   
}