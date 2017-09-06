using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularJSAuthentication.Model
{
   public class DemandMaster
    {
        [Key]
        public int DemandMasterId { get; set; }
        public int CityId { get; set; }
        public int WharehouseId { get; set; }
        public DateTime CreatedDate { get; set; }
        [NotMapped]
        public List<DemandDetails> demand { get; set; }


    }
}
