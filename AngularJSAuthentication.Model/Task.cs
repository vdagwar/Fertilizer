using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularJSAuthentication.Model
{
   public class ProjectTask
    {
        [Key]
        public int TaskId { get; set; }

        public int CompanyId { get; set; }
        public int TaskTypeId { get; set; }
        public int CustomerId { get; set; }
        public int ProjectID { get; set; }
        public int PeopleID { get; set; }
        
        public string Discription { get; set; }
        public string Name { get; set; }
        public string CustomerName { get; set; }
        public string ProjectName { get; set; }
        public double AllocatedHours { get; set; }
        public string Assignee { get; set; }
        public int Priority { get; set; }
        [DefaultValue("false")]
        public bool Completed { get; set; }        
        //public int Severity { get; set; }
        //public string AssignedTo { get; set; }
        //public string Priority { get; set; }        
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }        
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdateBy { get; set; }
        public bool Deleted { get; set; }


    }
}
