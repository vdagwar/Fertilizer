using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularJSAuthentication.Model
{
    public class Favorites
    {
        [Key]
        public int favoId { get; set; }
        public int ItemId { get; set; }
        public string customerMobile { get; set; }
        public string ItemName { get; set; }
      
    }
}
