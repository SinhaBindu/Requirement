using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Requirement.Models
{
    public class Multiple_Model
    {
        public Multiple_Model()
        {
            Emp_Id = Guid.Empty;
        }
        [Key]
        public Guid Emp_Id { get; set; }

        //[Required]
        [Display(Name = PartDisplayNamemultiple.NewHiredDate)]
        public DateTime NewHiredDate { get; set; }

        //[Required]
        [Display(Name = PartDisplayNamemultiple.HiringType_Id)]
        public int HiringType_Id { get; set; }

        //[Required]
        [Display(Name = PartDisplayNamemultiple.PositionName)]
        public string PositionName { get; set; }
        
        //[Required]
        [Display(Name = PartDisplayNamemultiple.Location)]
        public string Location { get; set; }

        //[Required]
        [Display(Name = PartDisplayNamemultiple.TypeofApplicable_Id)]
        public int TypeofApplicable_Id { get; set; }

        //[Required]
        [Display(Name = PartDisplayNamemultiple.NoofPositions_Id)]
        public int NoofPositions_Id { get; set; }

        //[Required]
        [Display(Name = PartDisplayNamemultiple.ReportingManager)]
        public string ReportingManager { get; set; }

        //[Required]
        [Display(Name = PartDisplayNamemultiple.Durationoftheposition_Fdate)]
        public DateTime Durationoftheposition_Fdate { get; set; }

        //[Required]
        [Display(Name = PartDisplayNamemultiple.Durationoftheposition_Tdate)]
        public DateTime Durationoftheposition_Tdate { get; set; }

        //[Required]
        [Display(Name = PartDisplayNamemultiple.CostCentreCode_Id)]
        public int CostCentreCode_Id { get; set; }

        //[Required]
        [Display(Name = PartDisplayNamemultiple.CTCSalRangepermonth_Id)]
        public int CTCSalRangepermonth_Id { get; set; }

        //[Required]
        [Display(Name = PartDisplayNamemultiple.Grade_Id)]
        public int Grade_Id { get; set; }

        //[Required]
        [Display(Name = PartDisplayNamemultiple.JD_Availability_Id)]
        public int JD_Availability_Id { get; set; }

        //[Required]
        [Display(Name = PartDisplayNamemultiple.JD_AvailabilityIfYes_Doc)]
        public HttpPostedFile JD_AvailabilityIfYes_Doc { get; set; }

        //[Required]
        [Display(Name = PartDisplayNamemultiple.Advertisement_Id)]
        public int Advertisement_Id { get; set; }

        //[Required]
        [Display(Name = PartDisplayNamemultiple.EmpaneledHead_Id)]
        public int EmpaneledHead_Id { get; set; }

        //[Required]
        [Display(Name = PartDisplayNamemultiple.InterviewPanel1)]
        public int InterviewPanel1 { get; set; }

        //[Required]
        [Display(Name = PartDisplayNamemultiple.InterviewPanel2)]
        public int InterviewPanel2 { get; set; }

        //[Required]
        [Display(Name = PartDisplayNamemultiple.SelectionProcess_Id)]
        public int SelectionProcess_Id { get; set; }

        //[Required]
        [Display(Name = PartDisplayNamemultiple.LaptopwithSpecifications_Id)]
        public int LaptopwithSpecifications_Id { get; set; }

        //[Required]
        [Display(Name = PartDisplayNamemultiple.Anyspecificsoftwarewithlicense)]
        public string Anyspecificsoftwarewithlicense { get; set; }

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

        public List<Multiple_Model> Multiple { get; set; }


    }
    public class PartDisplayNamemultiple
    {
        public const string NewHiredDate = "New Hired Date";
        public const string HiringType_Id = "Type of Haring";
        public const string PositionName = "Position Name";
        public const string Location = "Location";
        public const string TypeofApplicable_Id = "Applicable Type";
        public const string NoofPositions_Id = "No of Positions";
        public const string ReportingManager = "Reporting Manager";
        public const string Durationoftheposition_Fdate = "From Date";
        public const string Durationoftheposition_Tdate = "To Date";
        public const string CostCentreCode_Id = "Cost Centre & Code";
        public const string CTCSalRangepermonth_Id = "CTC Salary Range per month";
        public const string Grade_Id = "Grade";
        public const string JD_Availability_Id = "JD Availability";
        public const string JD_AvailabilityIfYes_Doc = "Upload Files";
        public const string Advertisement_Id = "Advertisement";
        public const string EmpaneledHead_Id = "Empaneled Head-Hunting Agency";
        public const string InterviewPanel1 = "1st Panel";
        public const string InterviewPanel2 = "2nd Panel";
        public const string SelectionProcess_Id = "Selection Process";
        public const string LaptopwithSpecifications_Id = "Laptop with Specifications";
        public const string Anyspecificsoftwarewithlicense = "Any specific software with license";


    }
}