using System.Web.Mvc;

namespace Cebritas.Web.Areas.Api {
    public class ApiAreaRegistration : AreaRegistration {
        public override string AreaName {
            get { return "Api"; }
        }

        public override void RegisterArea(AreaRegistrationContext context) {
            ProblemModuleRoutes(context);
            UserModuleRoutes(context);
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