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
        public string currentUserCookie { get; set; }

        // GET: Auth
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Models.User model)
        {
            using (var db = new MainDbContext())
            {
                var emailCheck = db.UserDbContext.FirstOrDefault(u => u.UserEmail == model.Email);
                var getPassword = db.UserDbContext.Where(u => u.UserEmail == model.Email).Select(u => u.UserPassword);
                var materializePassword = getPassword.ToList();
                var password = materializePassword[0];
                var decryptedPassword = CustomDecrypt.Decrypt(password);

                if (model.Email != null && model.Password == decryptedPassword)
                {
                    var getEmail = db.UserDbContext.Where(u => u.UserEmail == model.Email).Select(u => u.UserEmail);
                    var materializeEmail = getEmail.ToList();
                    var email = materializeEmail[0];

                    var identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, email) }, "ApplicationCookie");
                    Session["User"] = identity.Name;
                    currentUserCookie = identity.Name;

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
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registration(Models.User model)
        {
            if (ModelState.IsValid)
            {
                using (var db = new MainDbContext())
                {
                    var encryptedPassword = CustomEnrypt.Encrypt(model.Password);
                    var user = db.UserDbContext.Create();
                    user.UserEmail = model.Email;
                    user.UserPassword = encryptedPassword;
                    db.UserDbContext.Add(user);
                    db.SaveChanges();
                }
                return RedirectToAction("Login", "Auth");
            }
            else
            {
                ModelState.AddModelError("", "Missing Registration Information or Password Mismatch");
                return View(model);
            }
            
        }
    }
}