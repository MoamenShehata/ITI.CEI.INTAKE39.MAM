using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ITI.CEI.INTAKE39.MAM.LinkedIn.Models;
using ITI.CEI.INTAKE39.MAM.LinkedIn.ViewModels;
using Microsoft.AspNet.Identity;

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
            var posts = _ctxt.Posts.ToList();
            var userId = User.Identity.GetUserId();
            var user = _ctxt.Users.SingleOrDefault(c => c.Id == userId);
            var userOfPosts = _ctxt.Users.ToList();

            var userViewModel = new LinkedUserHomePageViewModel
            {
                UserAtHome = user,
                PostsAtHome = posts,
                UsersOfPosts=userOfPosts
                
            };
            return View("Wall",userViewModel);
        }

        [AllowAnonymous]
        public ActionResult SearchPage()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult SearchPage(SearchUserViewModel users)
        {
            var persons = _ctxt.Users.Where(c => c.FName.Contains(users.FName) && c.LName.Contains(users.Lname)).ToList();
            if (persons.Count==0)
            {
                return HttpNotFound();
            }
            else
            {
                return View("SearchPageResults",persons);
            }
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
    }
}