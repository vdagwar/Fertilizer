using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularJSAuthentication.Model
{
   public  class TaskType
    {
        [Key]
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public string Name { get; set; }

        public string Desc { get; set; }
        public string Category { get; set; }
        public User user { get; set; }
        public string CreateBy { get; set; }
        public DateTime CreatedDate { get; set; }

        public string UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }

    }
}
