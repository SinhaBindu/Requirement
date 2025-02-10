using Requirement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public ActionResult Registration(EmpReg_Model model)
        {
            try
            {
                //if (!ModelState.IsValid)
                //{
                //    return Json(new { success = false, message = "All fields are required." });
                //}

                var Empreg = new Tbl_EmpReg
                {
                    Emp_Code = model.Emp_Code,
                    Name = model.Name,
                    Designation = model.Designation,
                    Location = model.Location,
                    Role = model.Role,
                    Username = model.Username,
                    Password = model.Password,
                    IsActive = true,
                    CreatedOn = DateTime.Now
                };
                db.Tbl_EmpReg.Add(Empreg);
                db.SaveChanges();

                return Json(new { success = true, message = "New registration successful!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"An error occurred: {ex.Message}" });
            }
        }

    }
}