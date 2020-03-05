using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuizApp.Models;
namespace QuizApp.Controllers
{
    public class StudentDashboardController : Controller
    {
        QuizAppEntities db = new QuizAppEntities();
        // GET: StudentDashboard

        /////////////////////////////////Student Choose Category/////////////////////////////////////////////////
        public ActionResult Index()
        {
            if (Session["std_id"] == null)
            {
                return RedirectToAction("slogin", "Login");
            }

            List<Category> listOfCategory = db.Categories.ToList();
            ViewBag.list = new SelectList(listOfCategory, "Category_Id", "Category_Name");

            return View();
        }
       
        [HttpPost]
        public ActionResult Index(int Category_Name)
        {
            List<Category> listOfCategory = db.Categories.ToList();
            
            if (Session["std_id"] == null)
            {
                return RedirectToAction("slogin", "Login");
            }
           
            foreach(var item in listOfCategory)
            {
                if (item.Category_Id == Category_Name)
                {
                    TempData["examid"] = item.Category_Id;
                    TempData.Keep();
                    return RedirectToAction("QuizStart");
                }
                else
                {
                    ViewBag.error = "no quiz category found";
                }
               
            }


            return View();
        }

        /////////////////////////////////Quiz Start/////////////////////////////////////////////////
        
        public ActionResult QuizStart()
        {
            Question q = null;
            if (TempData["qid"] == null)
            {
                int examid = Convert.ToInt32(TempData["examid"].ToString());
                q = db.Questions.First(x => x.Category_Id == examid);
            }
            return View(q);


        }

    }
}