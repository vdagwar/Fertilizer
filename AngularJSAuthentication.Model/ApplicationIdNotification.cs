using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace GenricEcommers.Models
{
    public class ApplicationIdNotification
    {
        [Key]
        public int AppId { get; set; }
       
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
