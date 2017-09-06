using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularJSAuthentication.Model
{
  
    public class ItemCollection
    {
        public string strCompanyId { get; set; }
        public string col0_valCompanyId { get { return "CompanyId"; } }

        public string strCategoryName { get; set; }
        public string col3_valCategoryName { get { return "CategoryName"; } }

        public string strSubcategoryName { get; set; }
        public string col4_valstrcategoryName { get; set; }

        public string strSubsubcategoryName { get; set; }
        public string col5_valSubsubcategoryName { get; set; }

        public string strSupplierName { get; set; }
        public string col11_valSupplierName { get; set; }

        public string stritemname { get; set; }
        public string col6_valstritemname { get; set; }

        public string stritemcode { get; set; }
        public string col7_valitemcode { get; set; }

        public string strUnitName { get; set; }
        public string col10_valUnitName { get; set; }

        public double strprice { get; set; }
        public double col8_valprice { get; set; }

        public double strVATTax { get; set; }
        public double col9_valVATTax { get; set; }

        public string strLogoUrl { get; set; }
        public string col12_valLogoUrl { get; set; }

        public int strMinOrderQty { get; set; }
        public int valMinOrderQty { get; set; }

        public string strTGrpName { get; set; }
        public string col1_valTGrpName { get { return "TGrpName"; } }

        public float strTotalTaxPercentage { get; set; }
        public float valTotalTaxPercentage { get; set; }

        public string strWarehouseName { get; set; }
        public string col2_valWarehouseName { get; set; }
    }
}
