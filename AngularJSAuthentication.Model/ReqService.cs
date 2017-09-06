using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AngularJSAuthentication.Model
{
    public class ReqService
    {
        [Key]
        public int ReqServiceId { get; set; }
        public string ShopName { get; set; }
        public string Skcode { get; set; }
 
        public string ItemName { get; set; }
        public int WarehouseId { get; set; }
        public int PeopleID { get; set; }
        public string PeopleName { get; set; }      
        public DateTime CreatedDate { get; set; }
        public bool Deleted { get; set; }     
  
    }
}
