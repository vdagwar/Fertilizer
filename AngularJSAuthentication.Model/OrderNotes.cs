using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AngularJSAuthentication.Model
{
    public class OrderNotes
    {
        [Key]
        public int NoteId { get; set; }
        public string Note { get; set; }
        public int PeopleID { get; set; }
        public int DivisionId { get; set; }
        public int OrderId { get; set; }
        public DateTime CreatedDate { get; set; }                
        public DateTime UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdateBy { get; set; }
        public bool Deleted { get; set; }
    }
}
