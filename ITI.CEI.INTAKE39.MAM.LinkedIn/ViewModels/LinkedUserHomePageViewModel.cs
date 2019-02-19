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


    }
}