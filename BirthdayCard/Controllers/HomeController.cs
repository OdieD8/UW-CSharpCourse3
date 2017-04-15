using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BirthdayCard.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Models.CardContents cardContents)
        {
            if (ModelState.IsValid)
            {
                return View("CardSent", cardContents);
            }
            else
            {
                return View();
            }
        }
    }
}