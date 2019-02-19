using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITI.CEI.INTAKE39.MAM.LinkedIn.Models
{
    public class LinkedInUserProfileViewModel
    {
        public ApplicationUser User { get; set; }
        public Experience experience { get; set; }
        public Education education { get; set; }
        public Skill skill { get; set; }
        public IEnumerable<Experience> Experiences { get; set; }
        public IEnumerable<Skill> Skills { get; set; }
        public IEnumerable<Education> Educations { get; set; }
    }
}