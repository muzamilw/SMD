using System.Web.Mvc;

namespace SMD.MIS.Areas.DAM
{
    public class DAMAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "DAM";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "DAM_default",
                "DAMImages/{action}/{id}",
                new { controller = "Images", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}