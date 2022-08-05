using RecruitmentApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RecruitmentApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
           var userLogin = Session["userLogin"] as User;
            if (userLogin!=null)
                return View();

            return RedirectToAction("Index", "Login");
        }

        public int Topla()
        {
            int x = 4; int y = 5;
            return x + y;
        }

        public string Yaz()
        {
            string mesaj = "AutoAccepted";
            return mesaj;
        }


    }
}