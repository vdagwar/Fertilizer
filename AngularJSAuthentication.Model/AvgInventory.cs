using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularJSAuthentication.Model
{
   public class AvgInventory
    {
        [Key]
        public int id { get; set; }
        public DateTime firstdate { get; set; }
        public DateTime date { get; set; }
        public int warhouseid { get; set; }
        public double totals { get; set; }
        public double totalSale { get; set; }
        public bool active { get; set; }
        public bool deleted { get; set; }
    }
}
