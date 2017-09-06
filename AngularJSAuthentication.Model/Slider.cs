using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace GenricEcommers.Models
{
    public class Slider
    {
        [Key]
        public int SliderId { get; set; }
        public bool isWeb { get; set; }
        public int SequianceNumber { get; set; }
        public string Type { get; set; }
        public string Pic { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
