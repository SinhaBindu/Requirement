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

        public ActionResult JobD()
        {
            return View();
        }

        //[HttpPost]
        //public ActionResult JobD(JD_Model model)
        //{
        //    try
        //    {
        //        string tblcostdataJson = Request.Form["tblcostdata"];
        //        if (string.IsNullOrEmpty(tblcostdataJson))
        //        {
        //            return Json(new { success = false, message = "Invalid data submitted." });
        //        }

        //        // Deserialize JSON into a list of JD_Model objects
        //        var tblcostdata = JsonConvert.DeserializeObject<List<JD_Model>>(tblcostdataJson);

        //        if (tblcostdata == null || tblcostdata.Count == 0)
        //        {
        //            return Json(new { success = false, message = "No data to insert." });
        //        }

        //        var tblclist = new List<Tbl_JobD>();

        //        foreach (var item in tblcostdata)
        //        {
        //            var JobD = new Tbl_JobD
        //            {
        //                TypeOfNameId = item.TypeOfNameId,
        //                TypeOfValue = item.TypeOfValue,
        //                IsActive = true,
        //                CreatedBy = MvcApplication.CUser.UserId,
        //                CreatedOn = DateTime.Now
        //            };

        //            tblclist.Add(JobD);
        //        }

        //        db.Tbl_JobD.AddRange(tblclist);
        //        int result = db.SaveChanges();

        //        if (result > 0)
        //            return Json(new { success = true, message = "Job Description(s) inserted successfully!" });
        //        else
        //            return Json(new { success = false, message = "Failed to insert Job Descriptions." });

        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { success = false, message = "An error occurred while inserting data.", error = ex.Message });
        //    }
        //}


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

        //[HttpPost]
        //public ActionResult UpdateJD(int JobId_pk, int TypeOfNameId, string TypeOfValue)
        //{
        //    try
        //    {
        //        var jobD = db.Tbl_JobD.FirstOrDefault(j => j.JobId_pk == JobId_pk);
        //        if (jobD != null)
        //        {
        //            jobD.TypeOfNameId = TypeOfNameId;
        //            jobD.TypeOfValue = TypeOfValue;
        //            jobD.UpdatedBy = MvcApplication.CUser.UserId;
        //            jobD.UpdatedOn = DateTime.Now;

        //            db.SaveChanges();
        //            return Json(new { success = true, message = "Record updated successfully!" });
        //        }
        //        else
        //        {
        //            return Json(new { success = false, message = "Record not found!" });
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { success = false, message = "Error: " + ex.Message });
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
