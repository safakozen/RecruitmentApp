using RecruitmentApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RecruitmentApp.Controllers
{
    public class LoginController : Controller
    {
        RecruitmentAppEntities db = new RecruitmentAppEntities();
        public ActionResult Index()
        {
            var userLogin = Session["userLogin"] as User;
            if (userLogin != null)
                return RedirectToAction("Index", "Home");
            return View();
        }
        [HttpPost]
        public ActionResult Index(User model)
        {
            var checkUser = db.User.FirstOrDefault(x => x.Email == model.Email && x.Password == model.Password);
            if (checkUser !=null)
            {
                Session["userLogin"] = checkUser;
                return RedirectToAction("Index", "Home");
            }
            return View();

        }
        
    }
}