﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AngularJSAuthentication.Model
{
    public class Category
    {
        [Key]
        public int Categoryid { get; set; }
        public int CompanyId { get; set; }
        public int Warehouseid { get; set; }
        public string CategoryName { get; set; }
        public string Discription { get; set; }     
        public DateTime CreatedDate { get; set; }                
        public DateTime UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdateBy { get; set; }
        public string LogoUrl { get; set; }
        public string Code { get; set; }
        public int Level { get; set; }
        public int BaseCategoryId { get; set; }
        public bool Deleted { get; set; }
        public bool IsActive { get; set; }

        [NotMapped]
        public string WarehouseName { get; set; }
        
    }
}
