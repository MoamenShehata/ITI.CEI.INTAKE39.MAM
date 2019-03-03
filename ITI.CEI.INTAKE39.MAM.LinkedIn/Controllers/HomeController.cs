using System;
using System.Collections.Generic;
using System.IO;
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

        [Authorize]
        [HttpGet]
        public ActionResult RejectFriendRequest(string FriendId)
        {
            var userId = User.Identity.GetUserId();
            var record =_ctxt.UserFriends.SingleOrDefault(c => c.User_Id == FriendId && c.Friend_Id == userId);
            _ctxt.UserFriends.Remove(record);
            _ctxt.SaveChanges();
            var result = "Request rejected";
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpGet]
        public ActionResult AcceptFriendRequest(string FriendId)
        {
            var userId = User.Identity.GetUserId();
            var friendaccepted = new User_Friends
            {
                User_Id=userId,
                Friend_Id=FriendId,
                Request=true
            };
            _ctxt.UserFriends.Add(friendaccepted);
            //_ctxt.UserFriends.Where(c => c.User_Id == FriendId && c.Friend_Id == userId).ToList().ForEach(c => c.Request = true);
            var record = _ctxt.UserFriends.SingleOrDefault(c => c.User_Id == FriendId && c.Friend_Id == userId);
            record.Request = true;
            _ctxt.SaveChanges();
            var result = "Friend added succuessifully";
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        [Authorize]
        public ActionResult Network ()
        {
            var users = _ctxt.Users.ToList();
            var userId = User.Identity.GetUserId();
            var friendRequests = _ctxt.UserFriends.Where(c => c.Friend_Id == userId && c.Request == false).ToList();
            var allConnections = _ctxt.UserFriends.Where(c => c.User_Id == userId && c.Request == true).ToList();

            var f = from u in users
                    from d in friendRequests
                    where u.Id == d.User_Id
                    select u;
            var flist = f.ToList();



            var a = from u in users
                    from d in allConnections
                    where u.Id == d.Friend_Id
                    select u;

            var alist = a.ToList();

            var friendsViewModel = new MyNetworkViewModel
            {
                FriendRequests = flist,
                Friends = alist
            };
            return View("Network",friendsViewModel);
        }

        [Authorize]
        [HttpGet]
        public ActionResult FriendRequest(string userId,string FriendId)
        {

            var friendrequest = new User_Friends {
                User_Id = userId,
                Friend_Id = FriendId,
                Request = false,
            };
            _ctxt.UserFriends.Add(friendrequest);
            _ctxt.SaveChanges();
            var result = "Request is sent successefully";
            return Json(result, JsonRequestBehavior.AllowGet);
        }

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
            if (Addedpost.Content != null)
            {
                var AddedpostToDatabase = new Post
                {
                    FK_LinkedInUserId = userId,
                    Content = Addedpost.Content

                };
                _ctxt.Posts.Add(AddedpostToDatabase);
                _ctxt.SaveChanges();
            }


            return RedirectToAction("Wall");
        }

        [Authorize]
        public ActionResult Wall()
        {
            var users = _ctxt.Users.ToList();
            
            var posts = _ctxt.Posts.ToList();
            var userId = User.Identity.GetUserId();
            var user = _ctxt.Users.SingleOrDefault(c => c.Id == userId);
            var userOfPosts = _ctxt.Users.ToList();
            var Likes = _ctxt.Likes.ToList();
            var comments = _ctxt.Comments.ToList();
            var userofComments = _ctxt.Users.ToList();

            var listOfPeoplefollowed = _ctxt.UserFriends.Where(c => c.User_Id == userId).ToList();
            //foreach (var friend in listOfPeoplefollowed)
            //{
            //    if (users)
            //    {

            //    }
            //}

            List<ApplicationUser> listOfPeopleToFollow = new List<ApplicationUser>();
            if (listOfPeoplefollowed.Count()==0)
            {
                for (int i = 0; i < users.Count(); i++)
                {
                    if (users[i].Id != userId)
                    {
                        listOfPeopleToFollow.Add(users[i]);
                    }
                }
                
            }
            else
            {
                for (int i = 0; i < users.Count; i++)
                {
                    if (users[i].Id != userId)
                    {
                        for (int j = 0; j < listOfPeoplefollowed.Count; j++)
                        {
                            if (users[i].Id != listOfPeoplefollowed[j].Friend_Id)
                            {
                                if (j == listOfPeoplefollowed.Count - 1)
                                {
                                    listOfPeopleToFollow.Add(users[i]);
                                }
                                continue;
                            }
                            else
                            {
                                break;
                            }


                        }

                    }
                    else
                    {
                        continue;
                    }
                }
            }
        


            //var peopleToFollow = from u in users
            //                     from f in FriendTable

            //                     where u.Id !=userId && u.Id!=f.Friend_Id && u.Id !=f.User_Id
            //                     select u;


            Dictionary<int, ApplicationUser> friendstobe = new Dictionary<int, ApplicationUser>();
            int idx = 0;
            int idz = -1;
            foreach (var item in listOfPeopleToFollow)
            {
                if (_ctxt.UserFriends.Where(c=>c.User_Id==item.Id && c.Request==false && c.Friend_Id==userId).ToList().Count()!=0)
                {
                    friendstobe.Add(idz, item);
                    idz--;
                }
                else
                {
                    friendstobe.Add(idx, item);
                    idx++;
                }
                
            }

            var friendsOfUser = _ctxt.UserFriends.ToList();
            var postsathome = (from p in posts
                              join u in friendsOfUser
                              on p.FK_LinkedInUserId equals u.Friend_Id
                              where u.User_Id == userId 
                              select p).Concat(_ctxt.Posts.Where(c => c.FK_LinkedInUserId == userId).ToList()).ToList();
            //var selfposts = _ctxt.Posts.Where(c => c.FK_LinkedInUserId == userId).ToList();
            
            var userViewModel = new LinkedUserHomePageViewModel
            {
                UserAtHome = user,
                PostsAtHome = postsathome,
                UsersOfPosts = userOfPosts,
                LikesOnPosts = Likes,
                CommentsOnPosts = comments,
                UsersOfComments = userofComments,
                PeopleToFollow=friendstobe
                
                
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
            var persons = _ctxt.Users.Where(c=>c.FName.Contains(users.FName)).ToList();
            var VM = new LoginViewModel() { users = persons };
            if (persons.Count==0)
            {
                return HttpNotFound();
            }
            else
            {

            
            /*SearchResults = persons*/;
            return View("SearchPageResults",VM);
            }
        }
    }
}