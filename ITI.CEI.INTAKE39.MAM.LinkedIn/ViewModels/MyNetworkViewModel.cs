﻿using ITI.CEI.INTAKE39.MAM.LinkedIn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITI.CEI.INTAKE39.MAM.LinkedIn.ViewModels
{
    public class MyNetworkViewModel
    {
        public List<ApplicationUser> FriendRequests { get; set; }
        public List<ApplicationUser> Friends { get; set; }
    }
}