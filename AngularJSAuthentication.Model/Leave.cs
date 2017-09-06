using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AngularJSAuthentication.Model
{
    public class Leave
    {
        [Key]
        public int LeaveID { get; set; }
        public int CompanyId { get; set; }
        public string EmployeeName { get; set; }
        public string Department { get; set; }
        public string Email { get; set; }        
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdateBy { get; set; }
        public string CellNo { get; set; }   
        public string LeaveType { get; set; }     
        public string Reason { get; set; }
        public string ReasonForAppDec { get; set; }
        public bool IsApprove { get; set; }
        public bool Deleted { get; set; }        
    }
}
