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


    }
}


