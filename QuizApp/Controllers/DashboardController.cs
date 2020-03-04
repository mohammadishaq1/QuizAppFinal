using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuizApp.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Dashboard
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


    }
}


