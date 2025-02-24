﻿using Newtonsoft.Json;
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
        public ActionResult GetAllUserist(int SelectAll)
        {
            try
            {
                var items = CommonModel.GetUserAll(SelectAll,0,"");
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


        #region For JOBD About Position and Key Role

        public JsonResult GetKeyRole()
        {
            var typeOfNames = db.mst_KeyRole.Where(j => j.IsActive == true).OrderBy(j => j.OrderBy).Select(j => new { Value = j.KeyRoleId_pk, Text = j.KeyRoleName }).ToList();
            return Json(typeOfNames, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetTypeOfNames()//About Master
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

        public ActionResult AddDesignation()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddDesignation(DesignationModel model)
        {
            Recruitment_DBEntities dbe = new Recruitment_DBEntities();
            var tbl = model.Id_pk > 0 ? dbe.mst_Designation.Find(model.Id_pk) : new mst_Designation();
            int res = 0;
            try
            {
                if (string.IsNullOrEmpty(model.Designation))
                {
                    return Json(new { success = false, message = Enums.GetEnumDescription(Enums.eReturnReg.AllFieldsRequired) });
                }
                int maxOrderBy = db.mst_Designation.Select(j => (int?)j.OrderBy).DefaultIfEmpty(0).Max() ?? 0;
                if (model.Id_pk == 0)
                {
                    tbl.Designation = model.Designation.Trim();
                    tbl.IsActive = true;
                    tbl.CreatedBy = MvcApplication.CUser.UserId;
                    tbl.CreatedOn = DateTime.Now;
                    tbl.OrderBy = maxOrderBy + 1;
                    db.mst_Designation.Add(tbl);
                    res = db.SaveChanges();
                }
                else if (model.Id_pk > 0)
                {
                    tbl.Id_pk = model.Id_pk;
                    tbl.Designation = model.Designation.Trim();
                    tbl.IsActive = true;
                    tbl.UpdatedBy = MvcApplication.CUser.UserId;
                    tbl.UpdatedOn = DateTime.Now;
                    res = dbe.SaveChanges();
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
        public ActionResult AddProject()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddProject(ProjectModel model)
        {
            Recruitment_DBEntities dbe = new Recruitment_DBEntities();
            var tbl = model.ProjectId_pk > 0 ? dbe.mst_ProjectMaster.Find(model.ProjectId_pk) : new mst_ProjectMaster();
            int res = 0;
            try
            {
                if (string.IsNullOrEmpty(model.ProjectName))
                {
                    return Json(new { success = false, message = Enums.GetEnumDescription(Enums.eReturnReg.AllFieldsRequired) });
                }
                int maxOrderBy = db.mst_ProjectMaster.Select(j => (int?)j.OrderBy).DefaultIfEmpty(0).Max() ?? 0;
                if (model.ProjectId_pk == 0)
                {
                    tbl.ProjectName = model.ProjectName.Trim();
                    tbl.IsActive = true;
                    tbl.CreatedBy = MvcApplication.CUser.UserId;
                    tbl.CreatedOn = DateTime.Now;
                    tbl.OrderBy = maxOrderBy + 1;
                    dbe.mst_ProjectMaster.Add(tbl);
                    res = dbe.SaveChanges();
                }
                else if (model.ProjectId_pk > 0)
                {
                    tbl.ProjectId_pk = model.ProjectId_pk;
                    tbl.ProjectName = model.ProjectName.Trim();
                    tbl.IsActive = true;
                    tbl.UpdatedBy = MvcApplication.CUser.UserId;
                    tbl.UpdatedOn = DateTime.Now;
                    res = dbe.SaveChanges();
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


        public ActionResult GetDesignationList()
        {
            Recruitment_DBEntities dbe = new Recruitment_DBEntities();
            var tbl = dbe.mst_Designation.ToList();
            try
            {
                if (tbl.Count > 0)
                {
                    //var tbldata = JsonConvert.SerializeObject(tbl);
                    var html = ConvertViewToStringHtml("_DesignationData", tbl);
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

        public ActionResult AddTheme()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddTheme(ThemeModel model)
        {
            Recruitment_DBEntities dbe = new Recruitment_DBEntities();
            var tbl = model.ThemeId_pk > 0 ? dbe.mst_Theme.Find(model.ThemeId_pk) : new mst_Theme();
            int res = 0;
            try
            {
                if (string.IsNullOrEmpty(model.ThemeName))
                {
                    return Json(new { success = false, message = Enums.GetEnumDescription(Enums.eReturnReg.AllFieldsRequired) });
                }
                int maxOrderBy = db.mst_Designation.Select(j => (int?)j.OrderBy).DefaultIfEmpty(0).Max() ?? 0;
                if (model.ThemeId_pk == 0)
                {
                    tbl.ThemeCode = model.ThemeCode;
                    tbl.ThemeName = model.ThemeName.Trim();
                    tbl.IsActive = true;
                    tbl.CreatedBy = MvcApplication.CUser.UserId;
                    tbl.CreatedOn = DateTime.Now;
                    tbl.OrderBy = maxOrderBy + 1;
                    dbe.mst_Theme.Add(tbl);
                    res = dbe.SaveChanges();
                }
                else if (model.ThemeId_pk > 0)
                {
                    tbl.ThemeId_pk = model.ThemeId_pk;
                    tbl.ThemeCode = model.ThemeCode;
                    tbl.ThemeName = model.ThemeName.Trim();
                    tbl.IsActive = true;
                    tbl.UpdatedBy = MvcApplication.CUser.UserId;
                    tbl.UpdatedOn = DateTime.Now;
                    res = dbe.SaveChanges();
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

        public ActionResult GetThemeList()
        {
            Recruitment_DBEntities dbe = new Recruitment_DBEntities();
            var tbl = dbe.mst_Theme.ToList();
            try
            {
                if (tbl.Count > 0)
                {
                    //var tbldata = JsonConvert.SerializeObject(tbl);
                    var html = ConvertViewToStringHtml("_ThemeData", tbl);
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

        public ActionResult GetProjectList()
        {
            Recruitment_DBEntities dbe = new Recruitment_DBEntities();
            var tbl = dbe.mst_ProjectMaster.ToList();
            try
            {
                if (tbl.Count > 0)
                {
                    //var tbldata = JsonConvert.SerializeObject(tbl);
                    var html = ConvertViewToStringHtml("_ProjectData", tbl);
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

        public ActionResult AddKeyRole()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddKeyRole(KeyRoleModel model)
        {
            Recruitment_DBEntities dbe = new Recruitment_DBEntities();
            var tbl = model.KeyRoleId_pk > 0 ? dbe.mst_KeyRole.Find(model.KeyRoleId_pk) : new mst_KeyRole();
            int res = 0;
            try
            {
                if (string.IsNullOrEmpty(model.KeyRoleName))
                {
                    return Json(new { success = false, message = Enums.GetEnumDescription(Enums.eReturnReg.AllFieldsRequired) });
                }
                int maxOrderBy = db.mst_KeyRole.Select(j => (int?)j.OrderBy).DefaultIfEmpty(0).Max() ?? 0;
                if (model.KeyRoleId_pk == 0)
                {
                    tbl.KeyRoleName = model.KeyRoleName.Trim();
                    tbl.IsActive = true;
                    tbl.CreatedBy = MvcApplication.CUser.UserId;
                    tbl.CreatedOn = DateTime.Now;
                    tbl.OrderBy = maxOrderBy + 1;
                    dbe.mst_KeyRole.Add(tbl);
                    res = dbe.SaveChanges();
                }
                else if (model.KeyRoleId_pk > 0)
                {
                    tbl.KeyRoleId_pk = model.KeyRoleId_pk;
                    tbl.KeyRoleName = model.KeyRoleName.Trim();
                    tbl.IsActive = true;
                    tbl.UpdatedBy = MvcApplication.CUser.UserId;
                    tbl.UpdatedOn = DateTime.Now;
                    res = dbe.SaveChanges();
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

        public ActionResult GetKeyRoleList()
        {
            Recruitment_DBEntities dbe = new Recruitment_DBEntities();
            var tbl = dbe.mst_KeyRole.ToList();
            try
            {
                if (tbl.Count > 0)
                {
                    //var tbldata = JsonConvert.SerializeObject(tbl);
                    var html = ConvertViewToStringHtml("_KeyRoleData", tbl);
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

    }
}