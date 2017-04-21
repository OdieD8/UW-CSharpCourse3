using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HelloWorld.Models;
using System.Web.UI;

namespace HelloWorld.Controllers
{
    [AuthorizeIPAddress]
    [Logging]
    public class HomeController : Controller
    {
        //catches exception in controller
        //protected override void OnException(ExceptionContext filterContext)
        //{
        //    base.OnException(filterContext);
        //}

        // GET: Home
        public ActionResult Index()
        {
            // Error checking
            //int x = 1;
            //x = x / (x - 1);
            return View();
        }

        [HttpGet]
        public ActionResult RsvpForm()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RsvpForm(Models.GuestResponse guestResponse)
        {
            if (ModelState.IsValid)
            {
                return View("Thanks", guestResponse);
            }
            else
            {
                return View();
            }
        }

        //public ActionResult Product()
        //{
        //    var myProduct = new Product
        //    {
        //        ProductId = 1,
        //        Name = "Kayak",
        //        Description = "A boat for one person",
        //        Category = "water-sports",
        //        Price = 200m,
        //    };

        //    return View(myProduct);
        //}

        //public ActionResult Products()
        //{
        //    var products = new Product[]
        //    {
        //        new Product{ ProductId = 1, Name = "First One", Price = 1.11m, ProductCount = 5 },
        //        new Product{ ProductId = 2, Name = "Second One", Price = 2.22m, ProductCount = 1 },
        //        new Product{ ProductId = 3, Name = "Third One", Price = 3.33m, ProductCount = 0 },
        //        new Product{ ProductId = 4, Name = "Fourth One", Price = 4.44m, ProductCount = 2 },
        //    };

        //    return View(products);
        //}

        public ActionResult Product()
        {
            return View(productRepository.Products.First());
        }

        //[OutputCache(Duration = 15, Location = OutputCacheLocation.Any, VaryByParam = "none")]
        public ActionResult Products()
        {
            return View(productRepository.Products);
        }

        private IProductRepository productRepository;

        public HomeController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public PartialViewResult IncrementCount()
        {
            int count = 0;

            // Check if MyCount exists
            if (Session["MyCount"] != null)
            {
                count = (int)Session["MyCount"];
                count++;
            }

            // Create the MyCount session variable
            Session["MyCount"] = count;

            return new PartialViewResult();
        }

        public ActionResult Login()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult Login(Models.LoginModel loginModel)
        {
            Session["UserName"] = loginModel.UserName;
            return RedirectToAction("Index");
        }

        public ActionResult Logoff()
        {
            Session["UserName"] = null;
            return RedirectToAction("Index");
        }

        public PartialViewResult DisplayLoginName()
        {
            return new PartialViewResult();
        }
    }
}