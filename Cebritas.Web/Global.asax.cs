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
            RegisterPlacesModule(routes);
            RegisterWebModules(routes);
        }

        private static void RegisterWebModules(RouteCollection routes) {
            routes.MapRoute(
                "HomePage",
                "",
                new { controller = "Home", action = "Index" },
                new string[] { "Cebritas.Web.Controllers" }
            );
            routes.MapRoute(
                "Logon",
                "logon/",
                new { controller = "Home", action = "Logon" },
                new string[] { "Cebritas.Web.Controllers" }
            );
            routes.MapRoute(
                "Profile",
                "profile/",
                new { controller = "Profile", action = "Index" },
                new string[] { "Cebritas.Web.Controllers" }
            );
            routes.MapRoute(
                "Reporter",
                "reporter/",
                new { controller = "UserReport", action = "Index" },
                new string[] { "Cebritas.Web.Controllers" }
            );
            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}",
                new { controller = "Home", action = "Index" }
            );
        }

        private static void RegisterPlacesModule(RouteCollection routes) {
            // For Category CRUD
            routes.MapRoute(
                "ListCategories",
                "temp/category/list",
                new { controller = "Category", action = "List" },
                new string[] { "Cebritas.Web.Controllers" }
            );
            routes.MapRoute(
                "AddCategory",
                "temp/category/add",
                new { controller = "Category", action = "Add" },
                new string[] { "Cebritas.Web.Controllers" }
            );
            routes.MapRoute("SaveCategory", "temp/category/save",
                new { controller = "Category", action = "Save" },
                new string[] { "Cebritas.Web.Controllers" }
            );
            routes.MapRoute(
                "EditCategory",
                "temp/category/edit",
                new { controller = "Category", action = "Edit" },
                new string[] { "Cebritas.Web.Controllers" }
            );
            routes.MapRoute(
                "DeleteCategory",
                "temp/category/delete",
                new { controller = "Category", action = "Delete" },
                new string[] { "Cebritas.Web.Controllers" }
            );

            // For Place CRUD
            routes.MapRoute(
                "ListPlaces",
                "temp/places/list",
                new { controller = "Place", action = "List" },
                new string[] { "Cebritas.Web.Controllers" }
            );
            routes.MapRoute(
                "AddPlace",
                "temp/places/add",
                new { controller = "Place", action = "Add" },
                new string[] { "Cebritas.Web.Controllers" }
            );
            routes.MapRoute("SavePlace", "temp/places/save",
                new { controller = "Place", action = "Save" },
                new string[] { "Cebritas.Web.Controllers" }
            );
            routes.MapRoute(
                "EditPlace",
                "temp/places/edit",
                new { controller = "Place", action = "Edit" },
                new string[] { "Cebritas.Web.Controllers" }
            );
            routes.MapRoute(
                "DeletePlace",
                "temp/places/delete",
                new { controller = "Place", action = "Delete" },
                new string[] { "Cebritas.Web.Controllers" }
            );
        }
        public void Application_OnBeginRequest(object sender, EventArgs e) {
        }
        protected void Application_Start() {
            // Must call the job scheduler to update alertas
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }
    }
}