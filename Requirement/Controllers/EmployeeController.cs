using Requirement.Models;
using Requirement.Manager;
using System;
using System.IO;
using System.Web.Mvc;
using System.Data;


namespace Requirement.Controllers
{
    public class EmployeeController : Controller
    {
        private Recruitment_DBEntities db = new Recruitment_DBEntities();

        // GET: Employee
        public ActionResult EmpReg()
        {
            return View();
        }

        [HttpPost]
        public ActionResult EmpReg(Employee model)
        {
            try
            {
                //if (!ModelState.IsValid)
                //{
                //    return Json(new { success = false, message = "All fields are required." });
                //}

                var NewHire = new tbl_NewHire
                {
                    Emp_Id = Guid.NewGuid(),
                    NewHiredDate = DateTime.Now,
                    HiringType_Id = model.HiringType_Id,
                    PositionName = model.PositionName,
                    ProjectName = model.ProjectName,
                    Location = model.Location,
                    TypeofApplicable_Id = model.TypeofApplicable_Id,
                    NoofPositions_Id = model.NoofPositions_Id,
                    jobLocationType_Id = model.jobLocationType_Id,
                    ReportingManager = model.ReportingManager,
                    Durationoftheposition_Fdate = model.Durationoftheposition_Fdate,
                    Durationoftheposition_Tdate = model.Durationoftheposition_Tdate,
                    CostCentreCode_Id = model.CostCentreCode_Id,
                    BaseSalRangepermonth_Id = model.BaseSalRangepermonth_Id,
                    Grade_Id = model.Grade_Id,
                    JD_Availability_Id = model.JD_Availability_Id,
                    Advertisement_Id = model.Advertisement_Id,
                    EmpaneledHead_Id = model.EmpaneledHead_Id,
                    TypeOfInterview = model.TypeOfInterview,
                    InterviewPanel = model.InterviewPanel,
                    InterviewPanel2 = model.InterviewPanel2,
                    SelectionProcess_Id = model.SelectionProcess_Id,
                    LaptopwithSpecifications_Id = model.LaptopwithSpecifications_Id,
                    Anyspecificsoftwarewithlicense = model.Anyspecificsoftwarewithlicense,
                    IsActive = true,
                    CreatedOn = DateTime.Now
                };
                if (model.JD_Availability_Id == 1 && model.JD_AvailabilityIfYes_Doc != null && model.JD_AvailabilityIfYes_Doc.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(model.JD_AvailabilityIfYes_Doc.FileName);
                    var path = Path.Combine(Server.MapPath("~/Images/NewHired_Images"), fileName);
                    model.JD_AvailabilityIfYes_Doc.SaveAs(path);
                    NewHire.JD_AvailabilityIfYes_Doc = fileName;
                }
                else if (model.JD_Availability_Id == 1)
                {
                    return Json(new { success = false, message = "File upload failed. Please select a file for JD Availability." });
                }
                //if (model.JD_AvailabilityIfYes_Doc != null && model.JD_AvailabilityIfYes_Doc.ContentLength > 0)
                //{
                //    var fileName = Path.GetFileName(model.JD_AvailabilityIfYes_Doc.FileName);
                //    var path = Path.Combine(Server.MapPath("~/Images/NewHired_Images"), fileName);
                //    model.JD_AvailabilityIfYes_Doc.SaveAs(path);
                //    NewHire.JD_AvailabilityIfYes_Doc = fileName;
                //}
                //else
                //{
                //    // Log or handle the case where the file is not uploaded
                //    return Json(new { success = false, message = "File upload failed. Please select a file." });
                //}
                db.tbl_NewHire.Add(NewHire);
                db.SaveChanges();

                return Json(new { success = true, message = "New Hired registration successful!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"An error occurred: {ex.Message}" });
            }
        }

        public ActionResult HiredList()
        {
            return View();
        }

        public ActionResult GetHiredList()
        {
            DataTable tbllist = SPManager.Get_USP_GetHiredList();
            try
            {
                if (tbllist.Rows.Count > 0)
                {
                    var html = ConvertViewToString("_NewHiredData", tbllist);
                    return Json(new { IsSuccess = true, Data = html }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { IsSuccess = false, Data = "No records found." }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { IsSuccess = false, Data = "An error occurred: " + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult MultipleHireF()
        {
            return View();
        }

        private string ConvertViewToString(string viewName, object model)
        {
            ViewData.Model = model;
            using (StringWriter writer = new StringWriter())
            {
                ViewEngineResult vResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                ViewContext vContext = new ViewContext(this.ControllerContext, vResult.View, ViewData, new TempDataDictionary(), writer);
                vResult.View.Render(vContext, writer);
                return writer.ToString();
            }
        }
    }
}