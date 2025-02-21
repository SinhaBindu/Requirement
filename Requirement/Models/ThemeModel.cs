using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Requirement.Models
{
    public class ThemeModel
    {
        public ThemeModel()
        {
            ThemeId_pk = 0;
        }
        public int ThemeId_pk { get; set; }
        [Display(Name = "Theme Code")]
        public string ThemeCode { get; set; }


        [Display(Name = "Theme Name")]
        public string ThemeName { get; set; }



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

        public int? OrderBy { get; set; }
    }
}