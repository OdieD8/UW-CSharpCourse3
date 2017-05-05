using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace LearningCenter.Controllers
{
    public class ClassesController : Controller
    {
        private MainDbContext db = new MainDbContext();

        // GET: ClassList
        public ActionResult ClassList()
        {
            return View(db.ClassDbContext.ToList());
        }

        // GET: Enroll
        [HttpGet]
        public ActionResult Enroll()
        {
            List<string> list = new List<string>();
            foreach (var item in db.ClassDbContext.ToList())
            {
                list.Add(item.ClassName);
            }

            Models.Class SchoolClass = new Models.Class();
            SchoolClass.Classes = new SelectList(list);

            return View(SchoolClass);
        }

        // POST: Enroll
        [HttpPost]
        public ActionResult Enroll(Models.Class value)
        {
            if (value.SelectedClass[0].ToString() == "")
            {
                ModelState.AddModelError("", "Please select a class");
                return Enroll();
            }
            string selectedValue = value.SelectedClass[0].ToString();
     
            var classIdquery = db.ClassDbContext.SqlQuery($"SELECT * FROM dbo.Class WHERE ClassName = '{selectedValue}'").ToList();
            int classId = Convert.ToInt16(classIdquery[0].ClassId);

            if (Session == null || Session["User"] == null)
            {
                AuthController authController = new AuthController();
                Session["User"] = authController.currentUserCookie;
            }
            string userEmail = Session["User"].ToString();
            var userIdQuery = db.UserDbContext.SqlQuery($"SELECT * FROM [learning-center].[dbo].[User] WHERE UserEmail = '{userEmail}'").ToList();
            int userId = Convert.ToInt16(userIdQuery[0].UserId);

            db.Database.ExecuteSqlCommand($"INSERT INTO dbo.UserClass (ClassId, UserId) VALUES ({classId}, {userId})");



            return View("EnrolledClasses", db.UserDbContext.Where(u => u.UserId == userId).ToList());
        }

        // GET: EnrolledClasses
        [HttpGet]
        public ActionResult EnrolledClasses()
        {
            if (Session == null || Session["User"] == null)
            {
                AuthController authController = new AuthController();
                Session["User"] = authController.currentUserCookie;
            }
            string userEmail = Session["User"].ToString();
            var userIdQuery = db.UserDbContext.SqlQuery($"SELECT * FROM [learning-center].[dbo].[User] WHERE UserEmail = '{userEmail}'").ToList();
            int userId = Convert.ToInt16(userIdQuery[0].UserId);


            return View(db.UserDbContext.Where(u => u.UserId == userId).ToList());
        }
    }
}