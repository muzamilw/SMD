using System.Web.Mvc;

namespace SMD.MIS.Areas.Ads.Controllers
{
    public class CouponsController : Controller
    {
        // GET: Coupons/Coupons
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CouponReviews()
        {
            return View();
        }
        /// <summary>
        /// Approval/rejection of adds
        /// </summary>
        //public ActionResult AddAproval()
        //{
        //    return View();
        //}
    }
}