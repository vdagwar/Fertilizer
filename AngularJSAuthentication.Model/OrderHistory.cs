using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularJSAuthentication.Model
{
    public class OrderHistory
    {
        public double totalcash { get; set; }
        public int Delivered { get; set; }
        public int Redispatched { get; set; }
        public int Canceled { get; set; }
        public DeliveryIssuance deliveryIssuance { get; set; }
        public virtual ICollection<OrderDispatchedMaster> Orders { get; set; }
        

    }
   
}
