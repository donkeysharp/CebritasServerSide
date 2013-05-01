using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cebritas.Web.Controllers {
    public class CebraControllerBase : Controller {
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