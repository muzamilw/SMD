using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SMD.MIS.Areas.Report.Controllers
{
    public class ReportController : Controller
    {
        // GET: Report/Report
        public ActionResult TransactionReport()
        {
            return View();
        }
        public ActionResult WalletReport()
        {
            return View();
        }
    }
}