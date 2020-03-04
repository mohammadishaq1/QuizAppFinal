using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuizApp.Models;

namespace QuizApp.Controllers
{
    public class RegistrationController : Controller
    {
        QuizAppEntities db = new QuizAppEntities();
        
        // GET: Registration
        [HttpGet]
        public ActionResult Index()
        {
            if (Session["ad_id"] == null)
            {
                return RedirectToAction("aLogin", "Login");
            }
            return View();

        }


        [HttpPost]
        public ActionResult Index (Student s)
        {

            try
            {
                Student objStudent = new Student();
                objStudent.Student_Name = s.Student_Name;
                objStudent.Student_Password = s.Student_Password;
                db.Students.Add(objStudent);
                db.SaveChanges();
                return RedirectToAction("slogin", "Login");
            }
            catch (Exception)
            {

                ViewBag.msg = "Error in registration";
            }
            return View();

        }
    }

    
}