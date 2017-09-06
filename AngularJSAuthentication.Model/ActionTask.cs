using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AngularJSAuthentication.Model
{
    public class ActionTask
    {
        [Key]
        public int ActionTaskid { get; set; }
        public string ShopName { get; set; }
        public string Skcode { get; set; }
        public int WarehouseId { get; set; }
        public int CustomerId { get; set; }
        public int PeopleID { get; set; }
        public string PeopleName { get; set; }
        public string Action { get; set; }  
        public bool active { get; set; }
        public int CompanyId { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public DateTime CompletionDate { get; set; }
        public DateTime CreatedDate { get; set; }                
        public DateTime UpdatedDate { get; set; }
        public bool Deleted { get; set; }        
    }
}
