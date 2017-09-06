using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace GenricEcommers.Models
{
    public class News
    {
        [Key]
        public int NewsId { get; set; }
        public string NewsName { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsAvailable { get; set; }
    }
}