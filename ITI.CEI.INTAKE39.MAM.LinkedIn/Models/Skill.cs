using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ITI.CEI.INTAKE39.MAM.LinkedIn.Models
{
    [Table("Skill")]
    public class Skill
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name Is Required")]
        [StringLength(40)]
        public string Name { get; set; }

        public ApplicationUser LinkedInUser { get; set; }

        [ForeignKey("LinkedInUser")]
        public string FK_LinkedInUserId { get; set; }
    }
}