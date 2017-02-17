using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SMD.MIS.Areas.EmailPromotion.Controllers
{
    public class EmailsController : Controller
    {
        // GET: EmailPromotion/Emails
        public ActionResult Index()
        {
            return View();
        }
    }
}