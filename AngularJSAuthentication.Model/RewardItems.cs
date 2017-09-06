using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace GenricEcommers.Models
{
    public class RewardItems
    {
        [Key]
        public int rItemId { get; set; }
        public string rName { get; set; }
        public int rPoint { get; set; }
        public string rItem { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}