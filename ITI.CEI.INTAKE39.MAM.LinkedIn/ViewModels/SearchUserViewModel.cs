using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ITI.CEI.INTAKE39.MAM.LinkedIn.Models;

namespace ITI.CEI.INTAKE39.MAM.LinkedIn.ViewModels
{
    public class SearchUserViewModel
    {
        public string FName { get; set; }
        public string Lname { get; set; }
        public IEnumerable<ApplicationUser> ListOfUsers { get; set; }
    }
}