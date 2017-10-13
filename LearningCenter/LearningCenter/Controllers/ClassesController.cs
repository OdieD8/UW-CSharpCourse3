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
            if (Session == null || Session["User"] == null)
            {
                return RedirectToAction("Login", "Auth");
            }

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
            if (Session == null || Session["User"] == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            if (value.SelectedClass[0].ToString() == "")
            {
                ModelState.AddModelError("", "Please select a class");
                return Enroll();
            }
            string selectedValue = value.SelectedClass[0].ToString();
     
            var classIdquery = db.ClassDbContext.SqlQuery($"SELECT * FROM dbo.Class WHERE ClassName = '{selectedValue}'").ToList();
            int classId = Convert.ToInt16(classIdquery[0].ClassId);

            int userId = GetUserId();

            try
            {
                db.Database.ExecuteSqlCommand($"INSERT INTO dbo.UserClass (ClassId, UserId) VALUES ({classId}, {userId})");
            }
            catch (Exception)
            {
                TempData["errorMessage"] = "You are already enrolled in this class";
                return RedirectToAction("Error", "Classes");
            }

            return View("EnrolledClasses", db.UserDbContext.Where(u => u.UserId == userId).ToList());
        }

        // GET: EnrolledClasses
        [HttpGet]
        public ActionResult EnrolledClasses()
        {
            if (Session == null || Session["User"] == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            int userId = GetUserId();
            return View(db.UserDbContext.Where(u => u.UserId == userId).ToList());
        }

        // POST: Drop Class
        [HttpPost]
        public ActionResult EnrolledClasses(FormCollection collection)
        {
            if (Session == null || Session["User"] == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            int userId = GetUserId();
            string collectionValue = collection.Get("classId").ToString().Trim();
            int collectionValueLength = collectionValue.Length;
            int classId = Convert.ToInt16(collectionValue.Substring(0, collectionValueLength - 1));

            try
            {
                db.Database.ExecuteSqlCommand($"DELETE FROM dbo.UserClass WHERE ClassId = {classId} AND UserId = {userId}");
            }
            catch (Exception)
            {
                TempData["errorMessage"] = "Error dropping class";
                return RedirectToAction("Error", "Classes");
            }

            return View("EnrolledClasses", db.UserDbContext.Where(u => u.UserId == userId).ToList());
        }

        private int GetUserId()
        {
            string userEmail = Session["User"].ToString();
            var userIdQuery = db.UserDbContext.SqlQuery($"SELECT * FROM [learning-center].[dbo].[User] WHERE UserEmail = '{userEmail}'").ToList();
            int userId = Convert.ToInt16(userIdQuery[0].UserId);
            return userId;
        }

        [HttpGet]
        public ActionResult Error()
        {
            return View();
        }
    }
}