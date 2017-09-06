using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AngularJSAuthentication.Model
{
    public class DeliveryIssuance
    {
        [Key]
        public int DeliveryIssuanceId { get; set; }
        public int Cityid { get; set; }
        public string city { get; set; }
        public string DisplayName { get; set; }  
        public int CompanyId { get; set; }
        public int PeopleID { get; set; }
        public int VehicleId { get; set; }
        public string VehicleNumber { get; set; }
        public string Status { get; set; }
        public string RejectReason { get; set; }
        public string OrderdispatchIds { get; set; }
        public string OrderIds { get; set; }
        public bool Acceptance { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public float IdealTime { get; set; }
        public float TravelDistance { get; set; }

        //details
        public virtual ICollection<IssuanceDetails> details  { get; set; }
        [NotMapped]
        public  List<OrderDispatchedMaster> AssignedOrders { get; set; }

    }
    public class IssuanceDetails {
        [Key]
        public int IssuanceDetailsId { get; set; }
        public int OrderDispatchedMasterId { get; set; }
        public int OrderDispatchedDetailsId { get; set; }
        public string OrderQty { get; set; }
        public string OrderId { get; set; }
        public int qty { get; set; }
        public string itemNumber { get; set; }
        public int ItemId { get; set; }
        public string itemname { get; set; }
    }
}
