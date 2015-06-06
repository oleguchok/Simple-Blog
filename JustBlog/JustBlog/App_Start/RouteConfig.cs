﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace JustBlog
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Login",
                "Login",
                new { controller = "Admin", action = "Login"}
            );

            routes.MapRoute(
                "Logout",
                "Logout",
                new { controller = "Admin", action = "Logout" }
            );

            routes.MapRoute(
                "Default",
                "{action}/{id}",
                new { controller = "Blog", action = "Posts", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                "Category",
                "Category/{category}",
                new { controller = "Blog", action = "Category" }
            );

            routes.MapRoute(
                "Tag",
                "Tag/{tag}",
                new { controller = "Blog", action = "Tag" }
            );

            routes.MapRoute(
                "Post",
                "Archive/{year}/{month}/{title}",
                new { controller = "Blog", action = "Post" }
            );
        }
    }
}
