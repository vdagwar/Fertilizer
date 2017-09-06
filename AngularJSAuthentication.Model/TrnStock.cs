using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularJSAuthentication.Model
{
    public partial class TrnStock
    {
        [Key]
        public int StockId { get; set; }
        public int WarehouseId { get; set; }
        public int CategoryId { get; set; }
        public int ProductId { get; set; }
        public string BarCode { get; set; }
        public double PurchasePrice { get; set; }
        public double SalePrice { get; set; }
        public int UnitId { get; set; }
        public int Quantity { get; set; }
      
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}
