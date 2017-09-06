using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularJSAuthentication.Model
{
    public class IDetail
    {
        public int ItemId { get; set; }
        public int qty { get; set; }
    }
  public  class ShoppingCart
    {
        public string Customerphonenum { get; set; }
        public List<IDetail> itemDetails { get; set; }
        public int? SalesPersonId { get; set; }
        public string CustomerName { get; set; }
        public string Trupay { get; set; }
        public string ShopName { get; set; }
        public string CustomerType { get; set; }
        public string ShippingAddress { get; set; }
        public string Skcode { get; set; }
        public double deliveryCharge { get; set; }
        public double WalletAmount { get; set; }
        public double? UsedPoint { get; set; }
        public int DreamPoint { get; set; }
        public double TotalAmount { get; set; }
        public DateTime? CreatedDate { get; set; }

    }
}
