using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TheLearningCenter.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult ClassList()
        {
            return View();
        }

        public ActionResult Classes()
        {
            return View();
        }

        public ActionResult Enroll()
        {
            return View();
        }

        public ActionResult LearnMore()
        {
            return View();
        }
    }
}