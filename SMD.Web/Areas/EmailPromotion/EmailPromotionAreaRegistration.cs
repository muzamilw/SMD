using System.Web.Mvc;

namespace SMD.MIS.Areas.EmailPromotion
{
    public class EmailPromotionAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "EmailPromotion";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "EmailPromotion_default",
                "EmailPromotion/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}