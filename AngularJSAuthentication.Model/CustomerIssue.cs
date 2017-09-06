using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularJSAuthentication.Model
{
    public class CustomerIssue
    {
        [Key]
        public int CS_id { get; set; }
        public int CustomerId { get; set; }
        public string ShopName { get; set; }
        public string Mobile { get; set; }
        public int PeopleID { get; set; }
        public string PeopleName { get; set; }
        public string Issue { get; set; }
        public string Status { get; set; }
        public DateTime CreatedDate { get; set;}
        public DateTime UpdatedDate { get; set;}
        public DateTime CompletionDate { get; set;}
        public bool Active { get; set; }
        public bool Deleted { get; set; }



    }
}
