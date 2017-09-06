using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularJSAuthentication.Model
{
   public class Area
    {
        [Key]
        public int areaId { get; set; }
        public string AreaName { get; set; }
        public string AreaCode { get; set; }
        public string CityName { get; set; }
        public int CityId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool Active { get; set; }
        public bool Deleted { get; set; }
    }
}
