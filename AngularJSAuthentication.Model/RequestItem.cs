using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularJSAuthentication.Model
{
    public class RequestItem
    {
        [Key]
        public int reqId { get; set; }
        public int customerId { get; set; }
        public string shopName { get; set; }
        public string customerMobile { get; set; }
        public string requestedBrand { get; set; }
        public DateTime createdDate { get; set; }
    }
}
