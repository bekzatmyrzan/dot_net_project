﻿using CourseProject.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace CourseProject
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {

            //Database.SetInitializer(new SchoolKidInitializer());
            Database.SetInitializer<SchoolKidContext>(new DropCreateDatabaseIfModelChanges<SchoolKidContext>());

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}