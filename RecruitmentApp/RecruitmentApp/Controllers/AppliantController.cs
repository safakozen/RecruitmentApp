using RecruitmentApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RecruitmentApp.Controllers
{
    public class AppliantController : Controller
    {
        RecruitmentAppEntities db = new RecruitmentAppEntities();
        public ActionResult Index()
        {
            return View();
        }

        public bool IdentificationNumberVerification(string tc)
        {
            Int64 ATCNO, BTCNO, TcNo;
            long C1, C2, C3, C4, C5, C6, C7, C8, C9, Q1, Q2;

            TcNo = Int64.Parse(tc);

            ATCNO = TcNo / 100;
            BTCNO = TcNo / 100;

            C1 = ATCNO % 10; ATCNO = ATCNO / 10;
            C2 = ATCNO % 10; ATCNO = ATCNO / 10;
            C3 = ATCNO % 10; ATCNO = ATCNO / 10;
            C4 = ATCNO % 10; ATCNO = ATCNO / 10;
            C5 = ATCNO % 10; ATCNO = ATCNO / 10;
            C6 = ATCNO % 10; ATCNO = ATCNO / 10;
            C7 = ATCNO % 10; ATCNO = ATCNO / 10;
            C8 = ATCNO % 10; ATCNO = ATCNO / 10;
            C9 = ATCNO % 10; ATCNO = ATCNO / 10;
            Q1 = ((10 - ((((C1 + C3 + C5 + C7 + C9) * 3) + (C2 + C4 + C6 + C8)) % 10)) % 10);
            Q2 = ((10 - (((((C2 + C4 + C6 + C8) + Q1) * 3) + (C1 + C3 + C5 + C7 + C9)) % 10)) % 10);

            return ((BTCNO * 100) + (Q1 * 10) + Q2 == TcNo);
        }

        [HttpPost]
        public string Index(Appliant model)
        {
            string years, experience, skill, tc;
            years = model.Years;
            int yearsValidation = Convert.ToInt32(years); //String type is converted to int
            
            experience = model.ExperienceYear;
            int experienceValidation = Convert.ToInt32(experience);

            skill = model.Skill;
            int skillValidation = Convert.ToInt32(skill);

            tc = model.TC;
            bool returnValue = false;

            if (tc.Length == 11)
            {
                returnValue = IdentificationNumberVerification(tc);
            }

            if (!returnValue)
            {
                return "AutoRejected";
            }

            if (yearsValidation >= 18 &&
                skillValidation >= 75 &&
                experienceValidation >= 1 &&
                returnValue == true)
            {
                model.ResultsId = 1;
                model.StatusId = 4;
                model.CreatedDate = DateTime.Now;
                db.Appliant.Add(model);
                db.SaveChanges();
                return "AutoAccepted";
            }
            else if (yearsValidation < 18 &&
                yearsValidation > 18 &&
                skillValidation < 25 &&
                returnValue == false &&
                experienceValidation == 0)
            {
                model.ResultsId = 2;
                return "AutoRejected";
            }
            else if (yearsValidation < 18 &&
                skillValidation > 25 &&
                experienceValidation >= 1 &&
                returnValue == true)
            {
                model.ResultsId = 3;
                model.StatusId = 4;
                model.CreatedDate = DateTime.Now;
                db.Appliant.Add(model);
                db.SaveChanges();
                return "TransferredToHR";
            }
            else if (skillValidation >= 25 &&
                skillValidation <= 50 &&
                yearsValidation > 18 &&
                returnValue == true &&
                experienceValidation == 1 ||
                experienceValidation == 2)
            {
                model.ResultsId = 4;
                model.StatusId = 4;
                model.CreatedDate = DateTime.Now;
                db.Appliant.Add(model);
                db.SaveChanges();
                return "TransferredToLead";
            }
            else if (skillValidation >= 50 &&
                skillValidation <= 75 &&
                yearsValidation > 18 &&
                returnValue == true &&
                experienceValidation >= 2)
            {
                model.ResultsId = 5;
                model.StatusId = 4;
                model.CreatedDate = DateTime.Now;
                db.Appliant.Add(model);
                db.SaveChanges();
                return "TransferredToCTO";
            }
            else
            {
                return "sana bir proje verelim.";
            }
        }
    }
}
