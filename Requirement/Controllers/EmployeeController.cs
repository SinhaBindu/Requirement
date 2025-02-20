using Requirement.Models;
using Requirement.Manager;
using System;
using System.IO;
using System.Web.Mvc;
using System.Data;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Drawing.Drawing2D;
using static Requirement.Manager.CommonModel;
using Requirement.Controllers;
using Microsoft.Ajax.Utilities;


namespace Requirement.Controllers
{
    [Authorize]
    [SessionCheckAttribute]
    public class EmployeeController : BaseController
    {
        private Recruitment_DBEntities db = new Recruitment_DBEntities();
        int result = 0;
        // GET: Employee

        public ActionResult JDEditor()
        {
            return View();
        }

        public ActionResult AddNewHire()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddNewHire(EmployeeModel model)
        {
            try
            {
                //if (!ModelState.IsValid)
                //{
                //    return Json(new { success = false, message = Enums.GetEnumDescription(Enums.eReturnReg.AllFieldsRequired) });
                //}

                var tbl = new tbl_NewHire();
                tbl.NewHireId_pk = Guid.NewGuid();
                tbl.NewHiredDate = DateTime.Now;
                tbl.HiringTypeId = model.HiringTypeId;
                tbl.PositionName = model.PositionName;
                tbl.ThemeId = model.ThemeId;
                tbl.ProjectId = model.ProjectId;
                tbl.LocationId = model.LocationId;
                tbl.OtherLocation = model.OtherLocation;
                tbl.TypeofApplicableId = model.TypeofApplicableId;
                tbl.NoofPositionsId = model.NoofPositionsId;
                tbl.jobLocationTypeId = model.jobLocationTypeId;
                tbl.ReportingManager = model.ReportingManager;
                tbl.Durationoftheposition_Fdate = model.Durationoftheposition_Fdate;
                tbl.Durationoftheposition_Tdate = model.Durationoftheposition_Tdate;
                tbl.CostCentreCode_Id = model.CostCentreCode_Id;
                tbl.BaseSalRangepermonth_Id = model.BaseSalRangepermonth_Id;
                tbl.Grade_Id = model.Grade_Id;
                tbl.JD_Availability_Id = model.JD_Availability_Id;
                tbl.Advertisement_Id = model.Advertisement_Id;
                tbl.EmpaneledHead_Id = model.EmpaneledHead_Id;
                tbl.TypeOfInterview = model.TypeOfInterview;
                tbl.InterviewPanel = model.InterviewPanel;
                tbl.InterviewPanel2 = model.InterviewPanel2;
                tbl.SelectionProcess_Id = model.SelectionProcess_Id;
                tbl.LaptopwithSpecifications_Id = model.LaptopwithSpecifications_Id;
                tbl.Anyspecificsoftwarewithlicense = model.Anyspecificsoftwarewithlicense;
                tbl.IsActive = true;
                tbl.CreatedOn = DateTime.Now;
                tbl.CreatedBy = User.Identity.Name;

                //Multiple Mode
                string tblcostdataJson = Request.Form["tblcostdata"];
                if (!string.IsNullOrWhiteSpace(tblcostdataJson) && model.HiringTypeId == 2)
                {
                    // Deserialize the JSON string into a list of CostData objects
                    var tblcostdata = JsonConvert.DeserializeObject<List<MultipleCostModel>>(tblcostdataJson);
                    if (tblcostdata != null && tblcostdata.Count > 0 && tbl != null)
                    {
                        var tblclist = new List<tbl_NewHireMultipleCost>();
                        foreach (var item in tblcostdata)
                        {
                            var tblc = new tbl_NewHireMultipleCost
                            {
                                NewHireId_fk = tbl.NewHireId_pk,
                                MultipleCostName = item.MultipleCostName,
                                GrantID = item.GrantID,
                                ActivityCode = item.ActivityCode,
                                BudgetCode = item.BudgetCode,
                                AllotmentPercent = item.AllotmentPercent,
                                MonthlyAmount = item.MonthlyAmount,
                                IsActive = true,
                                CreatedBy = MvcApplication.CUser.UserId,
                                CreatedOn = tbl.CreatedOn
                            };
                            tblclist.Add(tblc);
                        }
                        db.tbl_NewHireMultipleCost.AddRange(tblclist);
                    }
                }

                if (model.ApprovedBy != null && model.ApprovedBy.Any())
                {
                    //var tblS=new tbl_NewHireStatus();
                    //tblS.NewHireId_fk=tbl.NewHireId_pk;
                    //db.tbl_NewHireStatus.Add(tblS);
                }
                //JD Details Save
                if (model.JD_Availability_Id == 1)
                {
                    var tbljd = model.JobDModel.JobId_pk != Guid.Empty ? db.tbl_JobD.Find(model.JobDModel.JobId_pk) : new tbl_JobD();
                    var tbljdaboutist = new List<tbl_JobDAbout>();
                    var tbljdkeyroleist = new List<Tbl_JobDKeyRole>();
                    if (tbljd != null)
                    {
                        tbljd.ApplicationClosureDate = model.JobDModel.ApplicationClosureDate;
                        tbljd.HiringLink = !(string.IsNullOrWhiteSpace(model.JobDModel.HiringLink)) ? model.JobDModel.HiringLink.Trim() : null;
                        tbljd.AbouttheProject = !(string.IsNullOrWhiteSpace(model.JobDModel.AbouttheProject)) ? model.JobDModel.AbouttheProject.Trim() : null;
                        tbljd.Remarks = !(string.IsNullOrWhiteSpace(model.JobDModel.Remarks)) ? model.JobDModel.Remarks.Trim() : null;
                        tbljd.IsActive = true;
                        if (tbljd.JobId_pk == Guid.Empty)
                        {
                            tbljd.JobId_pk = Guid.NewGuid();
                            tbljd.NewHireId_fk = tbl.NewHireId_pk;
                            tbljd.CreatedBy = MvcApplication.CUser.UserId;
                            tbljd.CreatedOn = DateTime.Now;

                        }
                        else if (tbljd.JobId_pk != Guid.Empty)
                        {
                            tbljd.UpdatedBy = MvcApplication.CUser.UserId;
                            tbljd.UpdatedOn = DateTime.Now;
                        }
                        db.tbl_JobD.Add(tbljd);
                    }
                    string JobDAboutModelJson = Request.Form["tbldataabout"];
                    if (!string.IsNullOrWhiteSpace(JobDAboutModelJson))
                    {
                        var jdaboutlist = JsonConvert.DeserializeObject<List<JobDAboutModel>>(JobDAboutModelJson);

                        if (jdaboutlist != null && jdaboutlist.Count > 0 && tbl != null && tbljd != null)
                        {
                            foreach (var item in jdaboutlist.ToList())
                            {
                                var tblabout = new tbl_JobDAbout
                                {
                                    JobDId_fk = tbljd.JobId_pk,
                                    NewHireId_fk = tbl.NewHireId_pk,
                                    AboutPositionId = item.AboutPositionId,
                                    AboutPositionValue = item.AboutPositionValue,
                                    OrderBy = item.OrderBy,
                                    IsActive = true,
                                    CreatedBy = tbl.CreatedBy,
                                    CreatedOn = tbl.CreatedOn,
                                };
                                tbljdaboutist.Add(tblabout);
                            }
                            db.tbl_JobDAbout.AddRange(tbljdaboutist);
                        }
                    }

                    string JobDKeyRoleModelJson = Request.Form["tbldatakeyrole"];
                    if (!string.IsNullOrWhiteSpace(JobDKeyRoleModelJson))
                    {
                        var jdkeyrolelist = JsonConvert.DeserializeObject<List<JobDKeyRoleModel>>(JobDKeyRoleModelJson);

                        if (jdkeyrolelist != null && jdkeyrolelist.Count > 0 && tbl != null && tbljd != null)
                        {
                            foreach (var item in jdkeyrolelist.ToList())
                            {
                                var tblkeyrole = new Tbl_JobDKeyRole
                                {
                                    JobDId_fk = tbljd.JobId_pk,
                                    NewHireId_fk = tbl.NewHireId_pk,
                                    KeyRoleId = item.KeyRoleId,
                                    KeyValue = item.KeyValue,
                                    OrderBy = item.OrderBy,
                                    IsActive = true,
                                    CreatedBy = tbl.CreatedBy,
                                    CreatedOn = tbl.CreatedOn
                                };
                                tbljdkeyroleist.Add(tblkeyrole);
                            }
                            db.Tbl_JobDKeyRole.AddRange(tbljdkeyroleist);

                        }

                    }

                }

                if (model.JD_Availability_Id == 1 && model.JD_AvailabilityIfYes_Doc != null && model.JD_AvailabilityIfYes_Doc.ContentLength > 0)
                {
                    FileModel modelfile = CommonModel.saveFile(model.JD_AvailabilityIfYes_Doc, "NewHire", "GID" + tbl.NewHireId_pk + (DateTime.Now.ToString("ddMMMyyyyHHss")));
                    string fileExtension = Path.GetExtension(model.JD_AvailabilityIfYes_Doc.FileName).ToLower();
                    string URLPath = "";
                    // Check if the file extension is allowed
                    if (fileExtension == ".zip" || fileExtension == ".png" || fileExtension == ".jpg" || fileExtension == ".pdf" || fileExtension == ".doc" || fileExtension == ".jpeg")
                    {
                        if (modelfile.IsvalidFile)
                        {
                            URLPath = modelfile.FilePathFull;
                        }
                        else
                        {
                            return Json(new { success = false, message = "File size is too large. Only files up to 20MB are allowed." });
                        }
                    }
                    else
                    {
                        return Json(new { success = false, message = "Invalid file extension. Only zip, png, jpg, pdf, doc, and jpeg files are allowed." });
                    }
                    //var fileName = Path.GetFileName(model.JD_AvailabilityIfYes_Doc.FileName);
                    //var path = Path.Combine(Server.MapPath("~/Images/NewHired_Images"), fileName);
                    //model.JD_AvailabilityIfYes_Doc.SaveAs(path);
                    tbl.JD_AvailabilityIfYes_Doc = URLPath;
                }
                db.tbl_NewHire.Add(tbl);
                result = db.SaveChanges();

                if (result > 0)
                    return Json(new { success = true, message = Enums.GetEnumDescription(Enums.eReturnReg.Insert) });
                else
                    return Json(new { success = true, message = Enums.GetEnumDescription(Enums.eReturnReg.NotInsert) });

            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    message = Enums.GetEnumDescription(Enums.eReturnReg.ExceptionError)
                });
            }
        }

        public ActionResult HireList()
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

        //public ActionResult MultipleHireF(Multiple_Model model)
        //{
        //    try
        //    {
        //        // Validate the model
        //        if (!ModelState.IsValid)
        //        {
        //            return Json(new { success = false, message = "All fields are required." });
        //        }
        //        var Emp_Id = Guid.NewGuid();
        //        var MultipleHire = new tbl_NewHireMultiple_old
        //        {
        //            NewHiredDate = DateTime.Now,
        //            HiringType_Id = model.HiringType_Id,
        //            PositionName = model.PositionName,
        //            Location = model.Location,
        //            TypeofApplicable_Id = model.TypeofApplicable_Id,
        //            NoofPositions_Id = model.NoofPositions_Id,
        //            ReportingManager = model.ReportingManager,
        //            Durationoftheposition_Fdate = model.Durationoftheposition_Fdate,
        //            Durationoftheposition_Tdate = model.Durationoftheposition_Tdate,
        //            CTCSalRangepermonth_Id = model.CTCSalRangepermonth_Id,
        //            Grade_Id = model.Grade_Id,
        //            IsActive = true,
        //            CreatedOn = DateTime.Now
        //        };

        //        db.tbl_NewHireMultiple_old.Add(MultipleHire);
        //        db.SaveChanges();


        //        var accountants = model.Accountant;
        //        var grantIDs = model.GrantID;
        //        var activityCodes = model.ActivityCode;
        //        var budgetCodes = model.BudgetCode;

        //        for (int i = 0; i < maxLength; i++)
        //        {
        //            var multipleCost = new tbl_NewHireMultipleCost
        //            {
        //                MultipleCostName = i < accountants.Count ? accountants[i] : null,
        //                GrantID = i < grantIDs.Count ? grantIDs[i] : null,
        //                ActivityCode = i < activityCodes.Count ? activityCodes[i] : null,
        //                BudgetCode = i < budgetCodes.Count ? budgetCodes[i] : null,
        //                IsActive = true,
        //                CreatedBy = User.Identity.Name,
        //                CreatedOn = DateTime.Now
        //            };

        //            db.tbl_NewHireMultipleCost.Add(multipleCost);
        //        }

        //        db.SaveChanges();

        //        return Json(new { success = true, message = "New hire and multiple costs saved successfully!" });
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { success = false, message = $"An error occurred: {ex.Message}" });
        //    }
        //}



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