using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LearningCenter.Models;
using System.Security.Claims;
using LearningCenter.CustomLibraries;

namespace LearningCenter.Controllers
{
    [AllowAnonymous]
    public class AuthController : Controller
    {
        // GET: Auth
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LearningCenter.Models.User model)
        {
            using (var db = new MainDbContext())
            {
                var emailCheck = db.User.FirstOrDefault(u => u.UserEmail == model.UserEmail);
                var getPassword = db.User.Where(u => u.UserEmail == model.UserEmail).Select(u => u.UserPassword);
                var materializePassword = getPassword.ToList();
                var password = materializePassword[0];
                var decryptedPassword = CustomDecrypt.Decrypt(password);

                if (model.UserEmail != null && model.UserPassword == decryptedPassword)
                {
                    var getEmail = db.User.Where(u => u.UserEmail == model.UserEmail).Select(u => u.UserEmail);
                    var materializeEmail = getEmail.ToList();
                    var email = materializeEmail[0];

                    var identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, email) }, "ApplicationCookie");
                    Session["User"] = identity.Name;

                    var ctx = Request.GetOwinContext();
                    var authManager = ctx.Authentication;

                    authManager.SignIn(identity);

                    return RedirectToAction("Index", "Home");
                }
            }

            ModelState.AddModelError("", "Invalid Email or Password");
            return View(model);
        }

        public ActionResult Logout()
        {
            Session["User"] = null;
            var ctx = Request.GetOwinContext();
            var authManager = ctx.Authentication;

            authManager.SignOut("ApplicationCookie");
            return RedirectToAction("Login", "Auth");
        }

        public ActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registration(LearningCenter.Models.User model)
        {
            if (ModelState.IsValid)
            {
                using (var db = new MainDbContext())
                {
                    var encryptedPassword = CustomEnrypt.Encrypt(model.UserPassword);
                    var user = db.User.Create();
                    user.UserEmail = model.UserEmail;
                    user.UserPassword = encryptedPassword;
                    db.User.Add(user);
                    db.SaveChanges();
                }
            }
            else
            {
                ModelState.AddModelError("", "Missing Registration Information");
            }
            return View();
        }
    }
}