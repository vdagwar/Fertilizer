using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularJSAuthentication.Model
{
    public class DayEventViewModel
    {

        public string projectname { get; set; }
        public int projectid { get; set; }
        public string clientproject { get { return clientname + "-" + projectname; } }
        public string clientname { get; set; }
        public int clientid { get; set; }
        public int tasktypeid { get; set; }
        public int taskid { get; set; }
        public int tfstaskid { get; set; }
        public string tasktype { get; set; }
        public double d1 { get; set; }
       public string taskname { get; set; }
        public double total { get; set; }
        public int d1EventId { get; set; }
      
        public string startdate { get; set; }
        public string day
        {
            get
            {
                return "d1";
            }
            set
            {

            }
        }
    }
}
