using System.Web;
using System.Web.Mvc;
using WorkflowProcess.Filters;

namespace WorkflowProcess
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new CustomErrorHandler());
        }
    }
}
