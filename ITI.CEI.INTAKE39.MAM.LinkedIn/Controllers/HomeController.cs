using System;
using System.Collections.Generic;
using System.IO;
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