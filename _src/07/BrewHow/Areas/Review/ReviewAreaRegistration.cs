using System.Web.Mvc;

namespace BrewHow.Areas.Review
{
    public class ReviewAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Review";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Review_default",
                "Review/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
