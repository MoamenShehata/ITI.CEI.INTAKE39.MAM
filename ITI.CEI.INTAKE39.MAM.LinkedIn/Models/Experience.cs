using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ITI.CEI.INTAKE39.MAM.LinkedIn.Models
{
    [Table("Experience")]
    public class Experience
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title Field Is Mandatory")]
        [StringLength(30)]
        public string Title { get; set; }

        [Required(ErrorMessage = "Company Field Is Required")]
        [StringLength(30)]
        public string Company { get; set; }

        [Required(ErrorMessage = "Location Field Is Required")]
        [StringLength(30)]
        public string Location { get; set; }

        [Required(ErrorMessage = "You Must Input Satrt Date")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "You Must Input End Date")]
        public DateTime EndDate { get; set; }

        public ApplicationUser LinkedInUser { get; set; }

        [ForeignKey("LinkedInUser")]
        public string FK_LinkedInUserId { get; set; }
    }
}