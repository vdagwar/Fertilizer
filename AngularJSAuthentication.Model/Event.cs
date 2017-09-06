using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularJSAuthentication.Model
{
  public  class Event
    {
      [Key]
      public int Id { get; set; }

      public int CompanyId { get; set; }
      public int CustomerId { get; set; }
      public int PeopleID { get; set; }
      public DateTime EventDate { get; set; }
      public int TaskId { get; set; }
      public int TFSTaskId { get; set; }
      public int ProjectId { get; set; }
      public string ProjectName { get; set; }
      public int ClientId { get; set; }
      public string ClientName { get; set; }
      public int TaskType { get; set; }
      public string Details { get; set; }
      public string TaskTypeName { get; set; }
      public double Hours { get; set; }
      public bool Approved { get; set; }
      public bool ApprovedBy { get; set; }
      public DateTime? CreatedDate { get; set; }

      public DateTime? UpdatedDate { get; set; }

      public string CreatedBy { get; set; }
      public string UpdatedBy { get; set; }
     
    }
}
