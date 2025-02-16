using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Requirement.Models;
using Requirement.Manager;
using Newtonsoft.Json;
using static Requirement.Manager.CommonModel;
using System.Data;
using System.IO;

namespace Requirement.Controllers
{
    [Authorize]
    [SessionCheckAttribute]
    public class JDController : Controller
    {
        private Recruitment_DBEntities db = new Recruitment_DBEntities();

        // GET: JD
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetTypeOfNames()
        {
            var typeOfNames = db.mst_AboutPosition.Where(j => j.IsActive == true).OrderBy(j => j.OrderBy).Select(j => new { Value = j.AboutPositionId_pk, Text = j.TypeOfName }).ToList();
            return Json(typeOfNames, JsonRequestBehavior.AllowGet);
        }

        public ActionResult JobD()
        {
            return View();
        }

        [HttpPost]
        public ActionResult JobD(JD_Model model)
        {
            try
            {
                string tblcostdataJson = Request.Form["tblcostdata"];
                if (string.IsNullOrEmpty(tblcostdataJson))
                {
                    return Json(new { success = false, message = "Invalid data submitted." });
                }

                // Deserialize JSON into a list of JD_Model objects
                var tblcostdata = JsonConvert.DeserializeObject<List<JD_Model>>(tblcostdataJson);

                if (tblcostdata == null || tblcostdata.Count == 0)
                {
                    return Json(new { success = false, message = "No data to insert." });
                }

                var tblclist = new List<Tbl_JobD>();

                foreach (var item in tblcostdata)
                {
                    var JobD = new Tbl_JobD
                    {
                        TypeOfNameId = item.TypeOfNameId,
                        TypeOfValue = item.TypeOfValue,
                        IsActive = true,
                        CreatedBy = MvcApplication.CUser.UserId,
                        CreatedOn = DateTime.Now
                    };

                    tblclist.Add(JobD);
                }

                db.Tbl_JobD.AddRange(tblclist);
                int result = db.SaveChanges();

                if (result > 0)
                    return Json(new { success = true, message = "Job Description(s) inserted successfully!" });
                else
                    return Json(new { success = false, message = "Failed to insert Job Descriptions." });

            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "An error occurred while inserting data.", error = ex.Message });
            }
        }


        public ActionResult JDList()
        {
            return View();
        }

        public ActionResult GetJDList()
        {
            DataTable tbllist = SPManager.Get_Usp_GetJDList();
            try
            {
                if (tbllist.Rows.Count > 0)
                {
                    var html = ConvertViewToString("_JDListData", tbllist);
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

        [HttpPost]
        public ActionResult UpdateJD(int JobId_pk, int TypeOfNameId, string TypeOfValue)
        {
            try
            {
                var jobD = db.Tbl_JobD.FirstOrDefault(j => j.JobId_pk == JobId_pk);
                if (jobD != null)
                {
                    jobD.TypeOfNameId = TypeOfNameId;
                    jobD.TypeOfValue = TypeOfValue;
                    jobD.UpdatedBy = MvcApplication.CUser.UserId;
                    jobD.UpdatedOn = DateTime.Now;

                    db.SaveChanges();
                    return Json(new { success = true, message = "Record updated successfully!" });
                }
                else
                {
                    return Json(new { success = false, message = "Record not found!" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error: " + ex.Message });
            }
        }

        public ActionResult JDMaster()
        {
            return View();
        }
        [HttpPost]
        public ActionResult JDMaster(JDMaster model)
        {
            Recruitment_DBEntities db_ = new Recruitment_DBEntities();
            var tbl = model.AboutPositionId_pk > 0 ? db_.mst_AboutPosition.Find(model.AboutPositionId_pk) : new mst_AboutPosition();
            int res = 0;
            try
            {
                if (string.IsNullOrEmpty(model.TypeOfName))
                {
                    return Json(new { success = false, message = Enums.GetEnumDescription(Enums.eReturnReg.AllFieldsRequired) });
                }
                int maxOrderBy = db.mst_AboutPosition.Select(j => (int?)j.OrderBy).DefaultIfEmpty(0).Max() ?? 0;
                if (model.AboutPositionId_pk == 0)
                {
                    tbl.TypeOfName = model.TypeOfName.Trim();
                    tbl.IsActive = true;
                    tbl.CreatedBy = MvcApplication.CUser.UserId;
                    tbl.CreatedOn = DateTime.Now;
                    tbl.OrderBy = maxOrderBy + 1;
                    db.mst_AboutPosition.Add(tbl);
                    res = db.SaveChanges();
                }
                else if (model.AboutPositionId_pk > 0)
                {
                    tbl.AboutPositionId_pk = model.AboutPositionId_pk;
                    tbl.TypeOfName = model.TypeOfName.Trim();
                    tbl.IsActive = true;
                    tbl.UpdatedBy = MvcApplication.CUser.UserId;
                    tbl.UpdatedOn = DateTime.Now;
                    res = db_.SaveChanges();
                }
               
                if (res > 0)
                    return Json(new { success = true, message = Enums.GetEnumDescription(Enums.eReturnReg.Insert) });
                else
                    return Json(new { success = true, message = Enums.GetEnumDescription(Enums.eReturnReg.NotInsert) });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = Enums.GetEnumDescription(Enums.eReturnReg.ExceptionError) });
            }
        }

        public ActionResult GetAboutPositionList()
        {
            Recruitment_DBEntities db_ = new Recruitment_DBEntities();
            var tbl = db.mst_AboutPosition.ToList();
            try
            {
                if (tbl.Count > 0)
                {
                    //var tbldata = JsonConvert.SerializeObject(tbl);
                    var html = ConvertViewToString("_AbboutPositionData", tbl);
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
