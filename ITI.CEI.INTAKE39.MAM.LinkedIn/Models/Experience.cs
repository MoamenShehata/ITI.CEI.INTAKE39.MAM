using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ITI.CEI.INTAKE39.MAM.LinkedIn.Models
{
    [Table("Experience")]
    public class Experience
    {
        public int Id { get; set; }

        public string Place { get; set; }

        public string Position { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public ApplicationUser LinkedInUser { get; set; }

        [ForeignKey("LinkedInUser")]
        public int FK_LinkedInUserId { get; set; }
    }
}