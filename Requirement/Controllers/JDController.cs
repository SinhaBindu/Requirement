using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Requirement.Controllers
{
    public class JDController : Controller
    {
        // GET: JD
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult JDForm()
        {
            return View();
        }
    }
}