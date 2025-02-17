using Newtonsoft.Json;
using Requirement.Manager;
using Requirement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Requirement.Controllers
{
    public class MasterController : BaseController
    {
        // GET: Master
        private Recruitment_DBEntities db = new Recruitment_DBEntities();
        public ActionResult Index()
        {
            return View();
        }
        #region Master List
        public ActionResult GetStateList(int SelectAll)
        {
            try
            {
                var items = CommonModel.GetALLLocation(SelectAll);
                if (items != null)
                {
                    if (items.Count > 0)
                    {
                        var data = JsonConvert.SerializeObject(items);
                        return Json(new { IsSuccess = true, res = data }, JsonRequestBehavior.AllowGet);
                    }
                }
                return Json(new { IsSuccess = false, res = "" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { IsSuccess = false, res = "There was a communication error." }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion


        #region About Position
        public JsonResult GetTypeOfNames()
        {
            var typeOfNames = db.mst_AboutPosition.Where(j => j.IsActive == true).OrderBy(j => j.OrderBy).Select(j => new { Value = j.AboutPositionId_pk, Text = j.TypeOfName }).ToList();
            return Json(typeOfNames, JsonRequestBehavior.AllowGet);
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
            var tbl = db_.mst_AboutPosition.ToList();
            try
            {
                if (tbl.Count > 0)
                {
                    //var tbldata = JsonConvert.SerializeObject(tbl);
                    var html = ConvertViewToStringHtml("_AboutPositionData", tbl);
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
        #endregion end About Position 

     
    }
}