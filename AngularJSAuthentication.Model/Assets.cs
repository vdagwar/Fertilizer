using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AngularJSAuthentication.Model
{
    public class Assets
    {
        [Key]
        public int AssetId { get; set; }
        public int CompanyId { get; set; }
        public int AssetCategoryId { get; set; }
        public string AssetCategoryName { get; set; }
        public string ModelNumber { get; set; }
        public string SerialNumber { get; set; }
        public string PONumber { get; set; }
        public decimal AssetCost { get; set; }
        public DateTime PurchaseDate { get; set; }
        public string WarrantyPeriod { get; set; }
        public string VendorContactNo { get; set; }
        public string VendorAddress { get; set; }
        public string FileUrl { get; set; }
        public string LastOwnership { get; set; }
        public DateTime LastUseDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdateBy { get; set; }
        public bool Deleted { get; set; }

    }
}
