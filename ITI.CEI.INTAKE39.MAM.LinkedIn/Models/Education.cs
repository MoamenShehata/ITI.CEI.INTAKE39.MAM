using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ITI.CEI.INTAKE39.MAM.LinkedIn.Models
{
    [Table("Education")]
    public class Education
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "School Is Required")]
        [StringLength(30)]
        public string School { get; set; }

        [Required(ErrorMessage = "StartDate Is Required")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "EndDate Is Required")]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "Degree Is Required")]
        [StringLength(30)]
        public string Degree { get; set; }

        public ApplicationUser LinkedInUser { get; set; }

        [ForeignKey("LinkedInUser")]
        public string FK_LinkedInUserId { get; set; }
    }
}