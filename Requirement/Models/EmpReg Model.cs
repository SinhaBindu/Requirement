using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Requirement.Models
{
    public class EmpReg_Model
    {
        public int Id_pk { get; set; }
        //[Required]
        [Display(Name = "Emp Code")]
        public int Emp_Code { get; set; }
        //[Required]
        [Display(Name = "Name")]
        public string Name { get; set; }
        //[Required]
        [Display(Name = "Designation")]
        public string Designation { get; set; }
        //[Required]
        [Display(Name = "Location")]
        public string Location { get; set; }
        //[Required]
        [Display(Name = "Role")]
        public int Role { get; set; }
        //[Required]
        [Display(Name = "Username")]
        public string Username { get; set; }
        //[Required]
        [Display(Name = "Password")]
        public string Password { get; set; }
        //[Required]
        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }

        [Display(Name = "Created By")]
        public string CreatedBy { get; set; }

        [Display(Name = "Created On")]
        public DateTime CreatedOn { get; set; }

        [Display(Name = "Updated By")]
        public string UpdatedBy { get; set; }

        [Display(Name = "Updated On")]
        public DateTime UpdatedOn { get; set; }
    }
}