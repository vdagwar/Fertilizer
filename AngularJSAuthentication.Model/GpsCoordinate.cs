using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AngularJSAuthentication.Model
{
    public class GpsCoordinate
    {
        [Key]
        public int gpsId { get; set; }
        public int DeliveryBoyId { get; set; }
        public string StartOrEnd { get; set; }
        public double lat { get; set; }
        public double lg { get; set; } 
        public DateTime CreatedDate { get; set; }
        public bool Deleted { get; set; }

        [NotMapped]
        public bool isDestination { get; set; }

        [NotMapped]
        public string status { get; set; }

        [NotMapped]
        public string CustomerName { get; set; }
    }
    public class MapData
    {
        public List<GpsCoordinate> customer { get; set; }
        
        public List<GpsCoordinate> deliveryboy { get; set; }
    }
}
