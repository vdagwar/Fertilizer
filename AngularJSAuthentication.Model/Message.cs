using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularJSAuthentication.Model
{
  public  class Message
    {
        [Key]
        public int MessageId { get; set; }
        public string MessageType { get; set; }
        public string MessageTitle { get; set; }
        public string MessageText { get; set; }
        public bool Deleted { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }

    }
}
