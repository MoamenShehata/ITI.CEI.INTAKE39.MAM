﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ITI.CEI.INTAKE39.MAM.LinkedIn.Models
{
    [Table("Education")]
    public class Education
    {
        public int Id { get; set; }

        public string School { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Degree { get; set; }

        public ApplicationUser LinkedInUser { get; set; }

        [ForeignKey("LinkedInUser")]
        public string FK_LinkedInUserId { get; set; }
    }
}