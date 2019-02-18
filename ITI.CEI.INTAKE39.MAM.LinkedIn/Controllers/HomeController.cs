using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ITI.CEI.INTAKE39.MAM.LinkedIn.Models;
using ITI.CEI.INTAKE39.MAM.LinkedIn.ViewModels;


namespace ITI.CEI.INTAKE39.MAM.LinkedIn.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _ctxt = new ApplicationDbContext();
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult Wall()
        {
            return View();
        }

        [Authorize]
        public ActionResult ProfilePage()
        {
            string UserName = User.Identity.Name;
            var user =_ctxt.Users.Where(u => u.UserName == UserName).FirstOrDefault();
            LinkedInUserProfileViewModel VM = new LinkedInUserProfileViewModel()
            {
                User = user,
                Experiences = _ctxt.Experiences.Where(e => e.FK_LinkedInUserId == user.Id).ToList(),
                Educations = _ctxt.Educations.Where(e => e.FK_LinkedInUserId == user.Id).ToList(),
                Skills= _ctxt.Skills.Where(e => e.FK_LinkedInUserId == user.Id).ToList(),
            };
            return View(VM);
        }

        [Authorize]
        public ActionResult LoadExperience(int id)
        {
            string UserName = User.Identity.Name;
            var user = _ctxt.Users.Where(u => u.UserName == UserName).FirstOrDefault();
            var experience = _ctxt.Experiences.Find(id);
            LinkedInUserProfileViewModel VM = new LinkedInUserProfileViewModel()
            {
                experience = experience,
                User = user,
                Experiences = _ctxt.Experiences.Where(e => e.FK_LinkedInUserId == user.Id).ToList(),
                Educations = _ctxt.Educations.Where(e => e.FK_LinkedInUserId == user.Id).ToList(),
                Skills = _ctxt.Skills.Where(e => e.FK_LinkedInUserId == user.Id).ToList(),
            };
            return PartialView("_PartialEditExperience", VM);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [AllowAnonymous]
        public ActionResult SearchPage()
        {
            //var users = _ctxt.Users.ToList();
            //List<SearchUser> SUsers = new List<SearchUser> {
                
            //}; 
            return View("SearchPage");
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult SearchPage(SearchUserViewModel users)
        {
            var persons = _ctxt.Users.Where(c=>c.FName.Contains(users.FName)).ToList();
            if (persons.Count==0)
            {
                return HttpNotFound();
            }
            else
            {

            
            /*SearchResults = persons*/;
            return View("SearchPageResults",persons);
            }
        }
    }
}