using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ITI.CEI.INTAKE39.MAM.LinkedIn.Models;
using ITI.CEI.INTAKE39.MAM.LinkedIn.ViewModels;
using Microsoft.AspNet.Identity;
using System.Data.Entity;

namespace ITI.CEI.INTAKE39.MAM.LinkedIn.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _ctxt = new ApplicationDbContext();
        public ActionResult Index()
        {
            return View();
        }

        //[HttpGet]
        //public ActionResult Editcomment (int postid,int commentid)
        //{

        //    return View();
        //}


        //public ActionResult Editcomment (string updatedcommentText)
        //{

        //}
        [Authorize]
        [HttpGet]
        public ActionResult Editcomment(int postid,string oldtext,string newtext)
        {
            var userId = User.Identity.GetUserId();
           var oldcomment= _ctxt.Comments.SingleOrDefault(c => c.FK_LinkedInUserId == userId && c.FK_PostId == postid && c.Text == oldtext);
            if (oldcomment!=null)
            {
                oldcomment.Text = newtext;
                _ctxt.SaveChanges();
            }
            var user = _ctxt.Users.SingleOrDefault(m => m.Id == userId);
            string resultName = user.FName + " " + user.LName;
            return Json(resultName, JsonRequestBehavior.AllowGet);
        }



        [Authorize]
        [HttpGet]
        public ActionResult Comment (int postid, string commentText)
        {

            var userId = User.Identity.GetUserId();
            var commentAdded = new Comment
            {
                Text=commentText,
                FK_LinkedInUserId=userId,
                FK_PostId=postid 
            };
            _ctxt.Comments.Add(commentAdded);
             _ctxt.SaveChanges();
            var comments = _ctxt.Comments.ToList();
            int id = comments[comments.Count - 1].Id;
            var user = _ctxt.Users.SingleOrDefault(m => m.Id == userId);
            var result = new
            {
                Name = user.FName + " " + user.LName,
                commentId=id,
            }; 
            return Json(result, JsonRequestBehavior.AllowGet); 

        }



        [Authorize]
        [HttpGet]
        public int Like (int postid)
        {
            var userId = User.Identity.GetUserId();
            var test = _ctxt.Likes.SingleOrDefault(m => m.FK_PostId == postid && m.FK_LinkedInUserId == userId);
           
            if (test==null)
            {
                var likeAdded = new Like
                {
                    FK_LinkedInUserId = userId,
                    FK_PostId = postid
                };

                _ctxt.Likes.Add(likeAdded);
              _ctxt.SaveChanges();
              var noOflikesOnPost = _ctxt.Likes.Where(m => m.FK_PostId == postid).ToList().Count();
                
                return noOflikesOnPost;
            }
            else
            {
                _ctxt.Likes.Remove(test);
                _ctxt.SaveChanges();
                var noOflikesOnPost = _ctxt.Likes.Where(m => m.FK_PostId == postid).ToList().Count();
                return noOflikesOnPost;
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult Submit (Post Addedpost)
        {
            var userId = User.Identity.GetUserId();
            var AddedpostToDatabase = new Post
            {
               FK_LinkedInUserId = userId,
               Content=Addedpost.Content

            };
            _ctxt.Posts.Add(AddedpostToDatabase);
            _ctxt.SaveChanges();

            return RedirectToAction("Wall");
        }

        [Authorize]
        public ActionResult Wall()
        {
            var posts = _ctxt.Posts.ToList();
            var userId = User.Identity.GetUserId();
            var user = _ctxt.Users.SingleOrDefault(c => c.Id == userId);
            var userOfPosts = _ctxt.Users.ToList();
            var Likes = _ctxt.Likes.ToList();
            var comments = _ctxt.Comments.ToList();
            var userofComments = _ctxt.Users.ToList();


            var userViewModel = new LinkedUserHomePageViewModel
            {
                UserAtHome = user,
                PostsAtHome = posts,
                UsersOfPosts=userOfPosts,
                LikesOnPosts=Likes,
                CommentsOnPosts=comments,
                UsersOfComments=userofComments,
                
                
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