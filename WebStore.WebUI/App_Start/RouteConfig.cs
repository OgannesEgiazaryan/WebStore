using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebStore.WebUI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(null,
                "",
                new
                {
                    controller = "Web",
                    action = "List",
                    category = (string)null,
                    page = 1
                }
            );

            routes.MapRoute(
                name: null,
                url: "Page{page}",
                defaults: new { controller = "Web", action = "List", category = (string)null },
                constraints: new { page = @"\d+" }
            );

            routes.MapRoute(null,
                "{category}",
                new { controller = "Web", action = "List", page = 1 }
            );

            routes.MapRoute(null,
                "{category}/Page{page}",
                new { controller = "Web", action = "List" },
                new { page = @"\d+" }
            );

            routes.MapRoute(null, "{controller}/{action}");

            routes.MapPageRoute("Admin_Soft", "admin/Admin_Soft", "~/Pages/Admin/Index.cshtml");
            routes.MapPageRoute("Admin_Category", "admin/Admin_Category", "~/Pages/Admin/Index1");
            routes.MapPageRoute("Admin_Event", "admin/Admin_Event", "~/Pages/Admin/Index2.cshtml");
            routes.MapPageRoute("Admin_Order", "admin/Admin_Order", "~/Pages/Admin/Index3.cshtml");
            routes.MapPageRoute("Admin_Review", "admin/Admin_Review", "~/Pages/Admin/Index4.cshtml");
            routes.MapPageRoute("Admin_Seller", "admin/Admin_Seller", "~/Pages/Admin/Index5.cshtml");

        }
    }
}