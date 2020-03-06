using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuizApp.Models;
namespace QuizApp.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Dashboard

        QuizAppEntities db = new QuizAppEntities();
        /////////////////////////////////Admin login and logout/////////////////////////////////////////////////
        public ActionResult Index()
        {
           
            if (Session["ad_id"] == null)
            {
                return RedirectToAction("aLogin","Login");
            }
            return View();
        }
        [HttpGet]
        public ActionResult Logout()
        {
            Session.Abandon();
            Session.RemoveAll();
            return RedirectToAction("aLogin", "Login");
        }

        /////////////////////////////////Add Category/////////////////////////////////////////////////
        public ActionResult addCategory()
        {
            if(Session["ad_id"] == null)
            {
                return RedirectToAction("alogin", "Login");
            }

            List<Category> listOfCategory = db.Categories.ToList();
            ViewData["listOfCategory"] = listOfCategory;
            return View();
        }
        [HttpPost]
        public ActionResult addCategory(Category c)
        {
            Category objCategory = new Category();
              objCategory.Category_Name = c.Category_Name;
            db.Categories.Add(objCategory);
            db.SaveChanges();
            return RedirectToAction("addCategory");

            
        }

        /////////////////////////////////Add Question/////////////////////////////////////////////////

        public ActionResult AddQueston()
        {
            
            List<Category> listOfCategory = db.Categories.ToList();
            ViewBag.list = new SelectList(listOfCategory, "Category_Id", "Category_Name");
            return View();
        }
        [HttpPost]
        public ActionResult AddQueston(Question q)
        {
           
            Question objQuestion = new Question();
            objQuestion.Question_Name = q.Question_Name;
            objQuestion.OptionA = q.OptionA;
            objQuestion.OptionB = q.OptionB;
            objQuestion.OptionC = q.OptionC;
            objQuestion.OptionD = q.OptionD;
            objQuestion.CorrectOption = q.CorrectOption;
            objQuestion.Category_Id = q.Category_Id;
            db.Questions.Add(objQuestion);
            db.SaveChanges();

            TempData["msg"] = "Question Add";
            TempData.Keep();
            return RedirectToAction("AddQueston");

            
        }

        /////////////////////////////////View Questions/////////////////////////////////////////////////

        public ActionResult ViewQuestion(int? id)
        {
            if (Session["ad_id"] == null)
            {
                return RedirectToAction("alogin","login");
            }

            if (id == null)
            {
                return RedirectToAction("Index");
            }


            return View(db.Questions.Where(x=>x.Category_Id == id).ToList());
        }

        public ActionResult Report()
        {
            if (Session["ad_id"] == null)
            {
                return RedirectToAction("slogin", "Login");
            }


            return View(db.Reports.ToList());
        }
    }
}


