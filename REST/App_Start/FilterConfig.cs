using System.Web;
using System.Web.Mvc;

namespace REST
{
    /// <summary>
    /// Filter config
    /// </summary>
    public class FilterConfig
    {
        /// <summary>
        /// Add filters
        /// </summary>
        /// <param name="filters">Global filters</param>
        /// <remarks></remarks>
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
