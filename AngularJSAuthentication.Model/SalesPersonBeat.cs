using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AngularJSAuthentication.Model;

namespace AngularJSAuthentication.Model
{
    public class SalesPersonBeat
    {
        [Key]
        public int id { get; set; }
        public int SalesPersonId { get; set; }
        public string Skcode { get; set; }
        public string ShopName { get; set; }
        public string SalespersonName { get; set; }
        public string status { get; set; }
        public string Comment { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}