using System.Web.Mvc;

namespace Cebritas.Web.Areas.Api {
    public class ApiAreaRegistration : AreaRegistration {
        public override string AreaName {
            get { return "Api"; }
        }

        public override void RegisterArea(AreaRegistrationContext context) {
            ProblemModuleRoutes(context);
            PlaceModuleRoutes(context);
            WalletModuleRoutes(context);
            UserModuleRoutes(context);
        }

        private void WalletModuleRoutes(AreaRegistrationContext context) {
            context.MapRoute(
                "GetPlacesByPrices",
                "api/wallet/getplacesbetween",
                new { controller = "Place", action = "GetPlacesByPrice" }
            );
        }

        private void PlaceModuleRoutes(AreaRegistrationContext context) {
            context.MapRoute(
                "GetCategoryTree",
                "api/places/getcategories",
                new { controller = "Category", action = "GetCategories" }
            );
            context.MapRoute(
                "GetPlacesByCategory",
                "api/places/getbycategory",
                new { controller = "Place", action = "GetPlacesByCategory" }
            );
            context.MapRoute(
                "GetPlacesByCategoryNear",
                "api/places/getbycategorynear",
                new { controller = "Place", action = "GetPlacesByCategory" }
            );
            context.MapRoute(
                "GetPlacesByQuery",
                "api/places/getbyquery",
                new { controller = "Place", action = "GetPlacesByQuery" }
            );
        }

        private void ProblemModuleRoutes(AreaRegistrationContext context) {
            context.MapRoute(
                "GetProblems",
                "api/problems/get",
                new { controller = "Problem", action = "GetProblems" }
            );
            context.MapRoute(
                "GetProblemsReportedByFriends",
                "api/problems/getbyfriends",
                new { controller = "Problem", action = "GetProblemsReportedByFriends" }
            );
            context.MapRoute(
                "RerpotProblem",
                "api/problems/report",
                new { controller = "Problem", action = "Report" }
            );
        }

        private void UserModuleRoutes(AreaRegistrationContext context) {
            context.MapRoute(
                "RegisterUser",
                "api/usuario/registro",
                new { controller = "User", action = "Register" }
            );
            context.MapRoute(
                "LogInUser",
                "api/usuario/login",
                new { controller = "User", action = "LogIn" }
            );

            context.MapRoute(
                "LogOutUser",
                "api/usuario/logout",
                new { controller = "User", action = "LogOut" }
            );

            context.MapRoute(
                "GetUser",
                "api/usuario/get",
                new { controller = "User", action = "GetUser" }
            );

            context.MapRoute(
                "UpdateUser",
                "api/usuario/update",
                new { controller = "User", action = "UpdateUser" }
            );
        }
    }
}