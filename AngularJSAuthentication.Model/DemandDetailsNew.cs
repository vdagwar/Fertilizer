using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularJSAuthentication.Model
{
   public class DemandDetailsNew
    {
        public int Warehouseid { get; set; }
        public string WarehouseName { get; set; }
        public string itemNumber { get; set; }
        public string City { get; set; }
        public int? CityId { get; set; }
        public int ItemId { get; set; }
        public string ItemCode { get; set; }
        public string Description { get; set; }
        public int qty { get; set; }
        public int CurrentInventry { get; set; }
        public int NetQuantity { get; set; }
        public int? MinOrderQty { get; set; }
        public int OrderQuantity { get; set; }
        public DateTime CreatedDate { get; set; }
        public string itemname { get; set; }
        public string status { get; set; }
        public string PurchaseSku { get; set; }
        public string PurchaseUnitName { get; set; }

    }
}
