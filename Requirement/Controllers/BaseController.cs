using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//using FP.Helpers;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Requirement;
using Requirement.Models;
using SubSonic.Extensions;
using SubSonic.Schema;

namespace Requirement.Controllers
{
    public class BaseController : Controller
    {
        private ApplicationUserManager _userManager;
        Recruitment_DBEntities db = new Recruitment_DBEntities();

        // GET: Base
        public void Success(string message, bool dismissable = false)
        {
            AddAlert(AlertStyles.Success, message, dismissable);
        }

        public void Information(string message, bool dismissable = false)
        {
            AddAlert(Requirement.Models.AlertStyles.Information, message, dismissable);
        }

        public void Warning(string message, bool dismissable = false)
        {
            AddAlert(AlertStyles.Warning, message, dismissable);
        }

        public void Danger(string message, bool dismissable = false)
        {
            AddAlert(AlertStyles.Danger, message, dismissable);
        }

        private void AddAlert(string alertStyle, string message, bool dismissable)
        {
            var alerts = TempData.ContainsKey(Alert.TempDataKey)
                ? (List<Alert>)TempData[Alert.TempDataKey]
                : new List<Alert>();

            alerts.Add(new Alert
            {
                AlertStyle = alertStyle,
                Message = message,
                Dismissable = dismissable
            });

            TempData[Alert.TempDataKey] = alerts;
        }

        //[HttpGet]
        //public JsonResult GetDepartmentbyhub(int id)
        //{
        //    dbEntities db = new dbEntities();

        //    var list = db.m_Department.Where(x=>x.HubId_Fk==id).OrderBy(o => o.Department).Select(b => new SelectListItem { Value = b.DepartId_pk.ToString(), Text = b.Department.ToString() });
        //    return Json(list, JsonRequestBehavior.AllowGet);

        //}
        //=======================================================================

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public bool RegisterUser(string UserId, string Email, string Password, string Role)
        {
            var user = new ApplicationUser { UserName = UserId, Email = Email };
            var result = UserManager.Create(user, Password);
            if (result.Succeeded)
            {
                var u = UserManager.FindByEmail(Email);
                if (u != null)
                {
                    UserManager.AddToRole(u.Id, Role);
                    // SendMailToApplicant(user.Email, Password);
                }
                return true;
            }
            return false;
        }
        public bool UserRemoveFromRole(string userId, string roleName)
        {
            bool status = false;
            var userInDb = UserManager.Users.SingleOrDefault(u => u.Id == userId);

            if (userInDb == null)
            {
                return status;
            }

            var roleInDb = UserManager.GetRoles(userId).SingleOrDefault();

            if (roleInDb == null)
            { return status; }
            var result = UserManager.RemoveFromRole(userInDb.Id, roleInDb);
            if (result.Succeeded)
            {
                return status = true;
            }
            return status;
        }
        public List<string> GetUserRoles(string userId)
        {
            List<string> roles = UserManager.GetRoles(userId).ToList();
            return roles;
        }
        public bool UserAddInRole(string userId, string roleName)
        {
            bool status = false;
            var userInDb = UserManager.Users.SingleOrDefault(u => u.Id == userId);

            if (userInDb == null)
            {
                return status;
            }

            var result = UserManager.AddToRole(userId, roleName);
            if (result.Succeeded)
            {
                return status = true;
            }
            return status;
        }

        #region Login Check 
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            // Check if user is authenticated
            if (User?.Identity?.IsAuthenticated ?? false)
            {
                // Ensure CUser is available in session
                if (Session["CUser"] == null)
                {
                    var user = GetCurrentUser(); // Fetch from DB
                    Session["CUser"] = user;
                }
            }
            else
            {
                // Redirect unauthenticated users to login page
                filterContext.Result = new RedirectResult("~/Account/Login");
            }
        }

        private UserViewModel GetCurrentUser()
        {
            var user = new UserViewModel();
            StoredProcedure sp = new StoredProcedure("SP_GetCurrentLogin");
            sp.Command.AddParameter("@UN", User.Identity.Name, DbType.String);
            DataTable dt = sp.ExecuteDataSet().Tables[0];
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                user.UserId = dr["UserId"].ToString();
                user.Name = dr["Name"].ToString();
                user.Email = dr["Email"].ToString();
                user.UserName = dr["UserName"].ToString();
                user.PhoneNumber = dr["PhoneNumber"].ToString();
                user.LockoutEnabled = dr["LockoutEnabled"].ToString();
                user.RoleId = Convert.ToInt32(dr["RoleId"]);
                user.RoleName = dr["RoleName"].ToString();
                user.Location = dr["Location"].ToString();
                user.Designation = dr["Designation"].ToString();
            }

            return user;
        }

        #endregion

        //[HttpPost]
        //public JsonResult ValidateEmployeeEmail(string Email,int EmployeeId = 0)
        //{
        //    UM_DBEntities db = new UM_DBEntities();
        //    bool status = true;
        //    if (EmployeeId == 0)
        //    {
        //        if (db.m_EmployeeList.Any(a => a.Email == Email))
        //        {
        //            status = false;
        //        }
        //    }
        //    return Json(status, JsonRequestBehavior.AllowGet);
        //}
        //public JsonResult ValidateUserName(string LoginId, int EmployeeId = 0)
        //{
        //    UM_DBEntities db = new UM_DBEntities();
        //    bool status = true;
        //    if (EmployeeId == 0)
        //    {
        //        if (db.AspNetUsers.Any(a => a.UserName == LoginId))
        //        {
        //            status = false;
        //        }
        //    }
        //    return Json(status, JsonRequestBehavior.AllowGet);
        //}
        //[HttpPost]
        //public JsonResult ValidatEmployeeCode(int? EmployeeCode,int EmployeeId = 0)
        //{
        //    dbEntities db = new dbEntities();
        //    bool status = true;
        //    if (EmployeeId == 0)
        //    {
        //        if (db.m_EmployeeList.Any(b => b.EmpCode == EmployeeCode))
        //        {
        //            status = false;
        //        }
        //    }
        //    return Json(status, JsonRequestBehavior.AllowGet);
        //}
        public void SendMailToApplicant(string Email, string Password)
        {
            var u = UserManager.FindByEmail(Email);
            if (u != null)
            {
                string code = UserManager.GenerateEmailConfirmationToken(u.Id);
                var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = u.Id, code = code }, protocol: Request.Url.Scheme);
                string MailBody = "Your registration is successful in TAR Web Portal. Your account credential is as follows:- <br/>" +
                "Username/UserID : " + Email + "<br />" +
                "Password : " + Password + "<br />" +
                "Please confirm your account before login by clicking <a href=\"" + callbackUrl + "\">here</a><br/>" +
                "Please change your password after Login <br/><br/>" +
                "<p style=\"color:red;\">This is a system generated mail, don't reply to this mail.</p>";

                UserManager.SendEmail(u.Id, "TAR Web Portal Confirm your account", MailBody);
            }

        }
        //public JsonResult GetSuperwiserDetails(int id)
        //{
        //    UM_DBEntities db = new UM_DBEntities();
        //    var SupEmp = from s in db.m_EmployeeList.Where(x => x.EmpId_pk == id).ToList()
        //                 select s;
        //    return Json(new SelectList(SupEmp.ToArray(), "EmpCode", "Email"), JsonRequestBehavior.AllowGet);
        //}
        public string GetUserRole(string EmailID)
        {
            string rolename = string.Empty;
            var user = UserManager.FindByEmail(EmailID);
            if (user != null)
            {
                rolename = UserManager.GetRoles(user.Id).FirstOrDefault();
            }
            return rolename;
        }
        //[HttpGet]
        //public JsonResult GetDistrictList(string id="10")
        //{
        //    Hunar_DBEntities db = new Hunar_DBEntities();
        //    List<SelectListItem> list = new List<SelectListItem>();
        //    if (!string.IsNullOrEmpty(id))
        //    {
        //        int _stateId = Convert.ToInt32(id);

        //        var block = from b in db.District_Master.Where(x => x.StateId_fk == _stateId)
        //                    select new { b.DistrictName, b.DistrictId_pk };
        //        list = new SelectList(block.Select(m => new { Text = m.DistrictName, Value = m.DistrictId_pk }).OrderBy(m => m.Text), "Value", "Text").ToList();
        //        // var data = db.m_block.Where(x => x.DistrictId_Fk == district.DistId_Pk).ToList().OrderBy(o => o.Block).Select(b => new SelectListItem { Value = b.Block, Text = b.Block.ToString() });
        //        return Json(list, JsonRequestBehavior.AllowGet);
        //    }
        //    else
        //    {
        //        list.Add(new SelectListItem { Text = "Select", Value = "", Selected = true });
        //        return Json(list, JsonRequestBehavior.AllowGet);
        //    }
        //}

        //[HttpGet]
        //public JsonResult GetIndicatorManagement(string id)
        //{
        //    dbEntities db = new dbEntities();
        //    List<SelectListItem> list = new List<SelectListItem>();
        //    if (!string.IsNullOrEmpty(id))
        //    {
        //        var ind = from b in db.vw_v3_Management_InputType.Where(x => x.SubSection == id)
        //                  group b by new { b.indid, b.ind } into x
        //                  select new { ind = x.Key.ind, indid = x.Key.indid };
        //        list = new SelectList(ind.Select(m => new { Text = m.ind, Value = m.indid }).OrderBy(m => m.Text), "Value", "Text").ToList();
        //        return Json(list, JsonRequestBehavior.AllowGet);
        //    }
        //    else
        //    {
        //        return Json(list, JsonRequestBehavior.AllowGet);
        //    }

        //}
    }
}