using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularJSAuthentication.Model
{
    public class ItemPramotions
    {
        [Key]
        public int PramotionId { get; set; }
        public int Warehouseid { get; set; }
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public string title { get; set; }
        public double PramotionalDiscount { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsActive { get; set; }
        public bool Deleted { get; set; }

    }
}
