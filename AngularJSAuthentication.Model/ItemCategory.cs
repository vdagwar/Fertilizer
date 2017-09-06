using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularJSAuthentication.Model
{
   public class ItemCategory
    {
        [Key]
        public int ItemcategoryId { get; set; }
        public int Itemcategorylevel { get; set; }
        public int CompanyId { get; set; }
        public string ParentItemName { get; set; }
        public string ChildItemName { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool Deleted { get; set; }
    }
}
