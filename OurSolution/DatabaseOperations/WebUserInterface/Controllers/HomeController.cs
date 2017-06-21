using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebUserInterface.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Instances()
        {
            return View();
        }

        public ActionResult Instance()
        {
            return View();
        }

        public ActionResult Users()
        {
            return View();
        }
        public ActionResult Adduser()
        {
            return View();
        }
        public ActionResult Edituser()
        {
            return View();
        }
        public ActionResult Deleteuser()
        {
            return View();
        }
        public ActionResult Options()
        {
            return View();
        }      
        public ActionResult BaseParameters()
        {
            var script = string.Format(@"var wcfAddress = '{0}';", GlobalSettings.WcfAddress);
            return JavaScript(script);
        }
    }
}