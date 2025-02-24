﻿using System;
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
            JobDModel = new JobDModel();
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
        [Display(Name = PartDisplayName.ThemeId)]
        public int ThemeId { get; set; }
        //[Required]
        [Display(Name = PartDisplayName.ProjectName)]
        public int ProjectId { get; set; }
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
        [Display(Name = PartDisplayName.SelectionProcessStep1)]
        public int SelectionProcess_Id { get; set; }
        [Display(Name = PartDisplayName.SelectionProcessStep2)]
        public int SelectionProcessStep2 { get; set; }
        [Display(Name = PartDisplayName.SelectionProcessStep3)]
        public int SelectionProcessStep3 { get; set; }
        //[Required]
        [Display(Name = PartDisplayName.LaptopwithSpecifications_Id)]
        public int LaptopwithSpecifications_Id { get; set; }
        //[Required]
        [Display(Name = PartDisplayName.Anyspecificsoftwarewithlicense)]
        public string Anyspecificsoftwarewithlicense { get; set; }
        //[Required]
        [Display(Name = PartDisplayName.InterviewPanel2)]
        public string InterviewPanel2 { get; set; }
        [Display(Name = PartDisplayName.ApprovedBy)]
        public List<string> ApprovedBy { get; set; }
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

        //JD Model Start
        public JobDModel JobDModel { get; set; }
        public List<JobDAboutModel> JobDAboutModel { get; set; }
        public List<JobDKeyRoleModel> JobDKeyRoleModel { get; set; }

    }
    public class MultipleCostModel
    {
        public int MultipleCostId_pk { get; set; }
        public Nullable<System.Guid> NewHireId_fk { get; set; }
        public string MultipleCostName { get; set; }
        public string GrantID { get; set; }
        public string ActivityCode { get; set; }
        public string BudgetCode { get; set; }
        public Nullable<decimal> AllotmentPercent { get; set; }
        public Nullable<decimal> MonthlyAmount { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
    }
    public class JobDModel
    {
        public JobDModel()
        {
            JobId_pk = Guid.Empty;
        }
        public Guid JobId_pk { get; set; }
        public Nullable<System.Guid> NewHireId_fk { get; set; }
        [Display(Name = PartDisplayName.HiringLink)]
        public string HiringLink { get; set; }
        [Display(Name = PartDisplayName.ApplicationClosureDate)]
        public Nullable<System.DateTime> ApplicationClosureDate { get; set; }
        [Display(Name = PartDisplayName.AbouttheProject)]
        public string AbouttheProject { get; set; }
        [Display(Name = PartDisplayName.Remarks)]
        public string Remarks { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public Nullable<int> OrderBy { get; set; }
    }
    public class JobDAboutModel
    {
        public int JobDAboutId_pk { get; set; }
        public Nullable<System.Guid> JobDId_fk { get; set; }
        public Nullable<System.Guid> NewHireId_fk { get; set; }
        public Nullable<int> AboutPositionId { get; set; }
        public string AboutPositionValue { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public Nullable<int> OrderBy { get; set; }
    }
    public class JobDKeyRoleModel
    {
        public int JobDKeyRoleId_pk { get; set; }
        public Nullable<System.Guid> JobDId_fk { get; set; }
        public Nullable<System.Guid> NewHireId_fk { get; set; }
        public Nullable<int> KeyRoleId { get; set; }
        public string KeyValue { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public Nullable<int> OrderBy { get; set; }
    }
    public class PartDisplayName
    {
        public const string NewHiredDate = "New Hired Date";
        public const string HiringTypeId = "Type of Hiring";
        public const string PositionName = "Position Name";
        public const string ThemeId = "Theme";
        public const string ProjectName = "Project Name";
        public const string LocationId = "Location";
        public const string TypeofApplicableId = "Type of Recruitment";
        public const string NoofPositionsId = "No of Positions";
        public const string jobLocationTypeId = "Job Location";
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

        public const string Section_JDHeader = "JOB DESCRIPTION";
        public const string Section_JDAbout = "About Position Detiails";
        public const string Section_JDKeyRole = "JD Key Role Details";
        public const string Section_JDOther = "JD Other Details";
        public const string HiringLink = "How to apply: Interested candidates can apply for the Position by using the following link: ";
        public const string ApplicationClosureDate = "Application closure date";
        public const string AbouttheProject = "About the Project";
        public const string JDRemarks = "Remarks";

        public const string Section_Approval = "Requirement Approval By";
        public const string ApprovedBy = "Approved By";

    }
}