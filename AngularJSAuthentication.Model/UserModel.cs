using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AngularJSAuthentication.API.Models
{
    public class UserModel
    {

       
        [Display(Name = "DepartmentId")]
        public string DepartmentId { get; set; }


        [Required]
        [Display(Name = "User name")]
        [DefaultValue("")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }

        [Required]
        [Display(Name = "Company Zip")]
        public string CompanyZip { get; set; }


        [Required]
        [Display(Name = "Company Phone")]
        public string CompanyPhone { get; set; }

        [Required]
        [Display(Name = "Company Phone")]
        public int Employees { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        public string PeopleFirstName { get; set; }
        public string PeopleLastName { get; set; }

        public int Warehouseid { get; set; }

        public int Stateid { get; set; }

        public int Cityid { get; set; }
        public string Mobile { get; set; }
        public string Department { get; set; }
        public string Skcode { get; set; }
        public string Permissions { get; set; }
        public bool Active { get; set; }
    }

   
}