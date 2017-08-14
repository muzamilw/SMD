using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SMD.MIS.Areas.Franchise.Controllers
{
    public class RegisterUsersController : Controller
    {
        // GET: Franchise/RegisterUsers
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult UsersDashboard()
        {
            return View();
        }
    }
}