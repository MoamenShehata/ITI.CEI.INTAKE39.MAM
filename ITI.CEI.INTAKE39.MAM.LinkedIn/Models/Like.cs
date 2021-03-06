﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ITI.CEI.INTAKE39.MAM.LinkedIn.Models
{
    [Table("Like")]
    public class Like
    {
        public int Id { get; set; }

        public ApplicationUser LinkedInUser { get; set; }

        public Post Post { get; set; }

        [ForeignKey("LinkedInUser")]
        public string FK_LinkedInUserId { get; set; }

        [ForeignKey("Post")]
        public int FK_PostId { get; set; }
    }
}