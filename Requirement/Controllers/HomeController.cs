using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Requirement.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Requirement.Controllers
{
    public class HomeController : Controller
    {
        private Recruitment_DBEntities db = new Recruitment_DBEntities();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Registration()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Registration(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var tbLu = db.AspNetUsers.Find(model.Id_pk);
                if (tbLu == null)
                {
                    ModelState.AddModelError("", "User  not found.");
                    return View(model);
                }

                // Find the employee registration in Tbl_EmpReg table
                var tbLe = db.Tbl_EmpReg.Find(model.EmpID_pk);
                if (tbLe == null)
                {
                    ModelState.AddModelError("", "Employee registration not found.");
                    return View(model);
                }
                tbLu.Email = model.Email;
                tbLu.UserName = model.Username;
                tbLu.CreatedBy = model.CreatedBy;
                tbLu.Emp_ID = model.EmpID_pk;
                tbLu.CreatedOn = DateTime.Now;
                tbLu.IsActive = true;

                tbLe.EmpID_pk = Guid.NewGuid();
                //tbLe.UserID_fk = User.Id;
                //tbLe.RoleID_fk = model.RoleID_fk;
                tbLe.Emp_Code = model.Emp_Code;
                tbLe.Name = model.Name;
                tbLe.Designation = model.Designation;
                tbLe.Location = model.Location;
                tbLe.Role = model.Role;
                tbLe.Username = model.Username;
                tbLe.Password = model.Password;
                tbLe.IsActive = true;
                tbLe.CreatedOn = DateTime.Now;

                // Save changes to both tables
                int res = await db.SaveChangesAsync();
                if (res > 0)
                {
                    return Json(new { success = true, message = "Registration successful!" });
                }
                else
                {
                    ModelState.AddModelError("", "Error saving data.");
                }
            }

 
            return Json(new { success = false, message = "Validation failed." });
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