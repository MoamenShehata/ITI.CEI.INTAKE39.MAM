using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public List<Experience> Experiences { get; set; }
        public List<Skill> Skills { get; set; }
        public List<Education> Educations { get; set; }
        [UIHint("file_cover")]
        public HttpPostedFileBase CoverFile { get; set; }
        [UIHint("file_profile")]
        public HttpPostedFileBase ProfileFile { get; set; }
    }
}