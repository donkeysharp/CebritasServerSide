using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Cebritas.DataAccess;

namespace Cebritas.Web {
    // Note: For instructions on enabling IIS6 or IIS7 classic mode,
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters) {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes) {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "ListCategories",
                "temp/category/list",
                new { controller = "Category", action = "List" }
            );
            routes.MapRoute(
                "AddCategory",
                "temp/category/add",
                new { controller = "Category", action = "Add" }
            );
            routes.MapRoute("SaveCategory", "temp/category/save",
                new { controller = "Category", action = "Save" }
            );
            routes.MapRoute(
                "EditCategory",
                "temp/category/edit",
                new { controller = "Category", action = "Edit" }
            );
            routes.MapRoute(
                "DeleteCategory",
                "temp/category/delete",
                new { controller = "Category", action = "Delete"}
            );
        }

        protected void Application_Start() {
            // Must call the job scheduler to update alertas
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }
    }
}