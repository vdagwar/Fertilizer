using GenricEcommers.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AngularJSAuthentication.Model
{
    public class GroupNotification
    {
        [Key]
        public int Id { get; set; }
        public string GroupName { get; set;}
            
        public  ICollection<Customer> Customer { get; set; }


    }
}