using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ITI.CEI.INTAKE39.MAM.LinkedIn.Models
{
    [Table("Post")]
    public class Post
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public string MediaURL { get; set; }

        public ApplicationUser LinkedInUser { get; set; }

        [ForeignKey("LinkedInUser")]
        public int FK_LinkedInUserId { get; set; }

        public List<Comment> Comments { get; set; }

        public List<Like> Likes { get; set; }
    }
}