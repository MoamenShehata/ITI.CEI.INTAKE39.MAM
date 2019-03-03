using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITI.CEI.INTAKE39.MAM.LinkedIn.Models
{
    public class User_Friends
    {
        public int Id { get; set; }
        public string User_Id { get; set; }
        public string Friend_Id { get; set; }
        public bool Request { get; set; }

    }
}