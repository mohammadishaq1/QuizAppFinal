using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuizApp.Models;
namespace QuizApp.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        QuizAppEntities db = new QuizAppEntities();
        // GET: Admin
        [HttpGet]
        public ActionResult aLogin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult aLogin(Admin a)
        {
            Admin objAdmin = db.Admins.Where(x => x.Admin_Name == a.Admin_Name && x.Admin_Password == a.Admin_Password).SingleOrDefault();
            if (objAdmin != null)
            {
                Session["ad_id"] = objAdmin.Admin_Id;
                return RedirectToAction("Index", "Dashboard");
            }
            else
            {
                ViewBag.msg = "Invalid username or password";
            }
            return View();
        }




        public ActionResult slogin()
        {
            return View();
        }


        [HttpPost]
        public ActionResult slogin(Student s)
        {
            Student objStudent =  db.Students.Where(x => x.Student_Name == s.Student_Name && x.Student_Password == s.Student_Password).SingleOrDefault();
            if (objStudent == null)
            {
                ViewBag.msg = "Invalid Email or password";
            }
            else
            {
                Session["std_id"] = objStudent.Student_Id;
                return RedirectToAction("Index", "StudentDashboard");
            }
            return View();
        }
    }
}