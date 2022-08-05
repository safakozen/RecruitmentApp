using RecruitmentApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RecruitmentApp.Controllers
{
    public class ResourceController : Controller
    {
        RecruitmentAppEntities db = new RecruitmentAppEntities();
        public ActionResult Index()
        {
            var userLogin = Session["userLogin"] as User;
            if (userLogin!=null)
            {
                //var data = db.Appliant.Where(x=>x.ResultsId)
            }
            return View();

        }
        [HttpPost]
        public ActionResult Index(Appliant model)
        {
            var checkAppliant = db.Appliant.FirstOrDefault(x => x.Name == model.Name);
            if (checkAppliant == null)
            {
                db.Appliant.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}