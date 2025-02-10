using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Requirement.Models
{
    public class JD_Model
    {
        public JD_Model()
        {
            Emp_Id_fk = Guid.Empty;
        }
        [Key]
        public Guid Emp_Id_fk { get; set; }

        //[Required]
        [Display(Name = "Job Tittle")]
        public string JobTittle{ get; set; }

        //[Required]
        [Display(Name = "Number of positions")]
        public int NoofPositions { get; set; }

        //[Required]
        [Display(Name = "Location")]
        public string Location { get; set; }

        //[Required]
        [Display(Name = "Grade")]
        public string Grade { get; set; }

        //[Required]
        [Display(Name = "Salary Range(Competitive salary commensurate with experience)")]
        public string SalRange { get; set; }

        //[Required]
        [Display(Name = "Type")]
        public string Type { get; set; }

        //[Required]
        [Display(Name = "Qualification required")]
        public string QualificationReq { get; set; }

        //[Required]
        [Display(Name = "Skills and Attributes")]
        public string SkillsAndAttribute { get; set; }

        //[Required]
        [Display(Name = "Application closure date")]
        public string ApplicationClosureDate { get; set; }


        //[Required]
        [Display(Name = "Key Roles and Responsibilities")]
        public string KeyRolesAndRes { get; set; }

        //[Required]
        [Display(Name = "Reporting to")]
        public string ReportingTo { get; set; }

        //[Required]
        [Display(Name = "About the Project ")]
        public string AboutProject { get; set; }

        //[Required]
        [Display(Name = "Experience required")]
        public string ExperiencedReq { get; set; }


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

        [Display(Name = "Is Deleted")]
        public bool IsDeleted { get; set; }

        [Display(Name = "Deleted On")]
        public DateTime? DeletedOn { get; set; }
    }
}