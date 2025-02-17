using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace Requirement.Models
{
    public class EmployeeModel
    {
        public EmployeeModel()
        {
            NewHireId_pk = Guid.Empty;
        }
        [Key]
        public Guid NewHireId_pk { get; set; }
        //[Required]
        [Display(Name = PartDisplayName.NewHiredDate)]
        public DateTime NewHiredDate { get; set; }
        //[Required]
        [Display(Name = PartDisplayName.HiringTypeId)]
        public int HiringTypeId { get; set; }
        //[Required]
        [Display(Name = PartDisplayName.PositionName)]
        public string PositionName { get; set; }
        //[Required]
        [Display(Name = PartDisplayName.ProjectName)]
        public string ProjectName { get; set; }
        //[Required]
        [Display(Name = PartDisplayName.LocationId)]
        public int LocationId { get; set; }
        public string OtherLocation { get; set; }
        //[Required]
        [Display(Name = PartDisplayName.TypeofApplicableId)]
        public int TypeofApplicableId { get; set; }
        //[Required]
        [Display(Name = PartDisplayName.NoofPositionsId)]
        public int NoofPositionsId { get; set; }
        //[Required]
        [Display(Name = PartDisplayName.jobLocationTypeId)]
        public int jobLocationTypeId { get; set; }
        //[Required]
        [Display(Name = PartDisplayName.Remarks)]
        public string Remarks { get; set; }
        //[Required]
        [Display(Name = PartDisplayName.ReportingManager)]
        public string ReportingManager { get; set; }
        //[Required]
        [Display(Name = PartDisplayName.Durationoftheposition_Fdate)]
        public DateTime Durationoftheposition_Fdate { get; set; }
        //[Required]
        [Display(Name = PartDisplayName.Durationoftheposition_Tdate)]
        public DateTime Durationoftheposition_Tdate { get; set; }
        //[Required]
        [Display(Name = PartDisplayName.CostCentreCode_Id)]
        public int CostCentreCode_Id { get; set; }
        //[Required]
        [Display(Name = PartDisplayName.BaseSalRangepermonth_Id)]
        public int BaseSalRangepermonth_Id { get; set; }
        //[Required]
        [Display(Name = PartDisplayName.Grade_Id)]
        public int Grade_Id { get; set; }
        //[Required]
        [Display(Name = PartDisplayName.JD_Availability_Id)]
        public int JD_Availability_Id { get; set; }
        //[Required]
        [Display(Name = PartDisplayName.JD_AvailabilityIfYes_Doc)]
        public HttpPostedFileBase JD_AvailabilityIfYes_Doc { get; set; }
        //[Required]
        [Display(Name = PartDisplayName.Advertisement_Id)]
        public int Advertisement_Id { get; set; }
        //[Required]
        [Display(Name = PartDisplayName.TypeOfInterview)]
        public int TypeOfInterview { get; set; }
        //[Required]
        [Display(Name = PartDisplayName.EmpaneledHead_Id)]
        public int EmpaneledHead_Id { get; set; }
        //[Required]
        [Display(Name = PartDisplayName.InterviewPanel)]
        public string InterviewPanel { get; set; }
        //[Required]
        [Display(Name = PartDisplayName.SelectionProcess_Id)]
        public int SelectionProcess_Id { get; set; }
        //[Required]
        [Display(Name = PartDisplayName.LaptopwithSpecifications_Id)]
        public int LaptopwithSpecifications_Id { get; set; }
        //[Required]
        [Display(Name = PartDisplayName.Anyspecificsoftwarewithlicense)]
        public string Anyspecificsoftwarewithlicense { get; set; }
        //[Required]
        [Display(Name = PartDisplayName.InterviewPanel2)]
        public string InterviewPanel2 { get; set; }
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

        public List<EmployeeModel> Employees { get; set; }
        public List<MultipleCostModel> MultipleCostModel { get; set; }

    }
    public class MultipleCostModel
    {
        public int MultipleCostId_pk { get; set; }
        public Nullable<System.Guid> NewHireId_fk { get; set; }
        public string MultipleCostName { get; set; }
        public string GrantID { get; set; }
        public string ActivityCode { get; set; }
        public string BudgetCode { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
    }
    public class PartDisplayName
    {
        public const string NewHiredDate = "New Hired Date";
        public const string HiringTypeId = "Type of Hiring";
        public const string PositionName = "Position Name";
        public const string ProjectName = "Project Name";
        public const string LocationId = "Location";
        public const string TypeofApplicableId = "Type of Recruitment";
        public const string NoofPositionsId = "No of Positions";
        public const string jobLocationTypeId = "job Location Type";
        public const string Remarks = "Remarks";
        public const string ReportingManager = "Reporting Manager";
        public const string Durationoftheposition_Fdate = "From Date";
        public const string Durationoftheposition_Tdate = "To Date";
        public const string CostCentreCode_Id = "Cost Centre & Code";
        public const string BaseSalRangepermonth_Id = "Base Salary Range per month";
        public const string Grade_Id = "Grade";
        public const string JD_Availability_Id = "JD Availability";
        public const string JD_AvailabilityIfYes_Doc = "Attach File";

        public const string Section_SourcingChannel = "Sourcing Channel:";//Section
        public const string Advertisement_Id = "Advertisement";
        public const string Note_Advertisement = "*Average cost for advertising on Devnet is INR 5k per position";

        public const string EmpaneledHead_Id = "Empaneled Head- Hunting Agency";
        public const string NoteLable_EmpaneledHead = "If yes, additional cost max @12% of CTO+GST will be applicable";//Yes Note

        public const string Section_Interview = "Interview Panel (Suggestions):";//Section
        public const string TypeOfInterview = "Type Of Interview";
        public const string InterviewPanel = "Interview panel(1st Round)";
        public const string InterviewPanel2 = "Interview panel(2nd Round)";

        public const string Section_Selection = "Selection Process";////Section
        public const string SelectionProcessStep1 = "Written test/PPT/Or both written test & PPT/Any other/None ";//Dropdown list
        public const string SelectionProcessStep2 = "Virtual Interview / Face to Face Interview";//Redio
        public const string SelectionProcessStep3 = "Next Level Interview required with Skip Level Supervisor /Budget Holder";//Redio Yes/No

        public const string Section_Infrastructure = "Infrastructure Requirement :";
        public const string LaptopwithSpecifications_Id = "Laptop with Specifications";
        public const string Anyspecificsoftwarewithlicense = "Any specific software with license";
    
    }
}