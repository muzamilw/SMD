using System.Web.Mvc;

namespace SMD.MIS.Areas.Billing.Controllers
{
    /// <summary>
    /// Manage Billing
    /// </summary>
    public class ManageController : Controller
    {
        //
        // GET: /Billing/Manage/
        public ActionResult Invoice()
        {
            return View();
        }
        public ActionResult EmailDashboard()
        {
            return View();
        }
	}
}