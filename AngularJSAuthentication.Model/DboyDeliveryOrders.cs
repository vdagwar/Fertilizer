using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularJSAuthentication.Model
{
    public class DboyDeliveryOrders
    {
        [Key]
        public int DboyDeliveryOrdersId { get; set; }
        public int DeliveryBoyID { get; set; }
        public string DeliveryBoyName { get; set; }
        public DateTime DeliveryDate { get; set; }
        public ICollection<OrderMaster> orderDetails { get; set; }

    }
}
