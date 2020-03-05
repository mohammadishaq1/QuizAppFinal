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
        /////////////////////////////////Student submit category/////////////////////////////////////////////////
        [HttpPost]
        public ActionResult Index(int Category_Name)
        {
            if (Session["std_id"] == null)
            {
                return RedirectToAction("slogin", "Login");
            }

            List<Category> listOfCategory = db.Categories.ToList();

            foreach (var item in listOfCategory)
            {
                if (item.Category_Id == Category_Name)
                {
                    List<Question> li = db.Questions.Where(x => x.Category_Id == item.Category_Id).ToList();
                    Queue<Question> queue = new Queue<Question>();
                    foreach(Question a in li)
                    {
                        queue.Enqueue(a);
                    }
                    TempData["questions"] = queue;
                    TempData["score"] = 0;
                    TempData.Keep();
                    return RedirectToAction("QuizStart");
                }
                else
                {
                    //send to 404 page
                    ViewBag.error = "no quiz category found";
                }
               
            }


            return View();
        }

        /////////////////////////////////Quiz Start/////////////////////////////////////////////////
        
        public ActionResult QuizStart()
        {
            Question q = null;
            if (TempData["questions"] != null)
            {
                Queue<Question> qlist = (Queue<Question>)TempData["questions"];
                if (qlist.Count > 0)
                {
                    q = qlist.Peek();
                    qlist.Dequeue();
                    TempData["questions"] = qlist;
                    TempData.Keep();
                }
                else
                {
                    return RedirectToAction("QuizEnd");
                }

            }
            else
            {
                return RedirectToAction("Index");
            }
            return View(q);


        }
        /////////////////////////////////Submit Quiz/////////////////////////////////////////////////
        


        /////////////////////////////////End Quiz Page/////////////////////////////////////////////////

        public ActionResult QuizEnd()
        {

            return View();
        }

    }
}