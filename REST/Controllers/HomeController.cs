using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace REST.Controllers
{
    /// <summary>
    /// Api controller for home page
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// Action result for index page
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }
    }
}
