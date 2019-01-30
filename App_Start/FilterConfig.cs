using System.Web;
using System.Web.Mvc;

namespace Vidly
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            // here we can register global filters

            filters.Add(new HandleErrorAttribute());

            filters.Add(new AuthorizeAttribute()); // now all users must be authorized to this app

            filters.Add(new RequireHttpsAttribute()); // my application will not be available on http channel
        }
    }
}
