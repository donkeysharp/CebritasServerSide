using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Cebritas.Web.Authentication.Security;

namespace Cebritas.Web.Controllers {
    public class CebraControllerBase : Controller {
        protected override void OnActionExecuting(ActionExecutingContext filterContext) {
            if (Request.IsAuthenticated) {
                if (SessionManager.GetAuthenticatedUser() == null) {
                    FormsAuthentication.SignOut();
                    Roles.DeleteCookie();
                    Session.Clear();
                }
            }
            base.OnActionExecuting(filterContext);
        }

        protected override void OnException(ExceptionContext filterContext) {
            // base.OnException(filterContext);
            if (filterContext == null) { return; }
            // TODO: Add viewbag the exception result :D
            var exception = filterContext.Exception ?? new Exception("Unknown exception");
            ViewBag.ErrorException = exception.Message;
            filterContext.Result = View("Error");
        }
    }
}