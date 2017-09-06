using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularJSAuthentication.Model
{
 
    public class TrnMoveStock
    {
        public int PurchaseInvoiceId { get; set; }
        public int SupplierId { get; set; }
        public int WarehouseId { get; set; }
        public int CategoryId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
