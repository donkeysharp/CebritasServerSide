using System.Web.Mvc;

namespace Cebritas.Web.Areas.Api {
    public class ApiAreaRegistration : AreaRegistration {
        public override string AreaName {
            get { return "Api"; }
        }

        public override void RegisterArea(AreaRegistrationContext context) {
            AlertaUrbanaModuleRoutes(context);
            UserModuleRoutes(context);
            PrecioModuleRoutes(context);
        }

        private void PrecioModuleRoutes(AreaRegistrationContext context) {
            context.MapRoute(
                "GetPrecios",
                "api/precios/get",
                new { controller = "Precio", action = "Get" }
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

        private void AlertaUrbanaModuleRoutes(AreaRegistrationContext context) {
            context.MapRoute(
                "ReportarActividadUrbana",
                "api/alertas/reportar",
                new { controller = "SolicitudAlerta", action = "Reportar" }
            );

            context.MapRoute(
                "ObtenerActividadUrbana",
                "api/alertas/get",
                new { controller = "SolicitudAlerta", action = "GetAlertas" }
            );
        }
    }
}