﻿using System.Web;
using System.Web.Mvc;

namespace CountryApiApp_CC
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
