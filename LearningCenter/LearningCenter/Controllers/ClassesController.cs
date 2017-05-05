using System;
using System.Collections.Generic;
using System.Linq;
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
            return View(db.Class.ToList());
        }

        // GET: Enroll
        [HttpGet]
        public ActionResult Enroll()
        {
            List<string> list = new List<string>();
            foreach (var item in db.Class.ToList())
            {
                list.Add(item.ClassName);
            }

            LearningCenter.Models.Class SchoolClass = new LearningCenter.Models.Class();
            SchoolClass.Classes = new SelectList(list);

            return View(SchoolClass);
        }

        // POST: Enroll
        [HttpPost]
        public ActionResult Enroll(LearningCenter.Models.Class value)
        {
            string selectedValue = value.SelectedClass.ToString();
     
            var classIdquery = db.Class.SqlQuery($"SELECT ClassId FROM dbo.Class WHERE ClassName = '{selectedValue}").ToList();
            int classId = Convert.ToInt16(classIdquery[0]);

            string userEmail = Session["User"].ToString();
            var userIdQuery = db.User.SqlQuery($"SELECT UserId FROM [learning-center].[dbo].[User] WHERE UserEmail = '{userEmail}'").ToList();
            int userId = Convert.ToInt16(userIdQuery[0]);

            db.Database.ExecuteSqlCommand($"INSERT INTO dbo.UserClass (ClassId, UserId) VALUE ({classId}, {userId})");


            return View(value);
        }
    }
}