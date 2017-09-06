using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularJSAuthentication.Model
{
    public class Feedback
    {
        [Key]
        public int feedBackId { get; set; }
        public int customerId { get; set; }
        public string Mobile { get; set; }
        public string shopName { get; set; }
        public string suggestions { get; set; }
        public int satisfactionLevel { get; set; }
        public DateTime createdDate { get; set; }
    }
}
