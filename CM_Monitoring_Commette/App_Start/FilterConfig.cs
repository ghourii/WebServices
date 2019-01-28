using System.Web;
using System.Web.Mvc;

namespace CM_Monitoring_Commette
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
