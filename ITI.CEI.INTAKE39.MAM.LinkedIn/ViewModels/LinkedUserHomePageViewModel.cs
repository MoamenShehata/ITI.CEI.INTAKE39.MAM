using ITI.CEI.INTAKE39.MAM.LinkedIn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITI.CEI.INTAKE39.MAM.LinkedIn.ViewModels
{
    public class LinkedUserHomePageViewModel
    {
        public ApplicationUser UserAtHome{ get; set; }
        public List<Post> PostsAtHome { get; set; }
        public List<ApplicationUser> UsersOfPosts { get; set; }
        public Post AddedPost { get; set; }
        public List<Like> LikesOnPosts { get; set; }
        public List<Comment> CommentsOnPosts { get; set; }
        public List<ApplicationUser> UsersOfComments { get; set; }
        public List<ApplicationUser> Friends { get; set; }
        public Dictionary<int,ApplicationUser> PeopleToFollow { get; set; }






    }
}