using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularJSAuthentication.Model
{
  public  class Video
    {
        public int videoId { get; set; }
        public string title { get; set; }
        public string discription { get; set; }
        public string url { get; set; }
        public string imageUrl { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdateBy { get; set; }
    }
}
