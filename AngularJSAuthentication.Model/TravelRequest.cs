using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularJSAuthentication.Model
{
  public   class TravelRequest
    {
        [Key]
        public int Id { get; set; }

        public int PersonId { get; set; }
      //  public string PersonName { get; set; }
        public int CompanyId { get; set; }
        public DateTime  DepartingDate { get; set; }
        public DateTime ArrivalDate { get; set; }
        public string DepartingCity { get; set; }
        public string ArrivalCity { get; set; }
        public string Details { get; set; }
        public bool HotelRequired { get; set; }
        public bool AirRequired { get; set; }
        public bool TransportRequired { get; set; }

        public string CreateBy { get; set; }
        public DateTime CreatedDate { get; set; }

        public string UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string ReasonForAppDec { get; set; }
        public bool IsApprove { get; set; }
        public bool Deleted { get; set; }
    }
}
