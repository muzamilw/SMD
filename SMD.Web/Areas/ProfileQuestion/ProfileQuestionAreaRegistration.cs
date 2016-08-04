using System.Web.Mvc;

namespace SMD.MIS.Areas.ProfileQuestion
{
    public class ProfileQuestionAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "ProfileQuestion";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "ProfileQuestion_default",
                "ProfileQuestion/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}