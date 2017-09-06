using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularJSAuthentication.Model
{
   public class Project
    {
        [Key]
        public int ProjectID { get; set; }

        public int CompanyId { get; set; }
        public string ProjectCode { get; set; }
        public string ProjectName { get; set; }
        public string Discription { get; set; }
        public double Budget { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string ApproverName { get; set; }
        public string ApproverEmail { get; set; }
        public double ConsultantRate { get; set; }
        public double EmpRate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdateBy { get; set; }
        public bool Deleted { get; set; }
        public int CustomerId { get; set; }
        public string Status { get; set; }
       public string ProjectManager { get; set; }
        public string SalesPerson { get; set; }
        public double  PercentageCompleted { get; set; }
    }
}
