﻿using System.Web;
using System.Web.Mvc;

namespace ITI.CEI.INTAKE39.MAM.LinkedIn
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
