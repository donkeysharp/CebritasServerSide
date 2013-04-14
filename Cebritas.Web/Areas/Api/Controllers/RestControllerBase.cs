using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cebritas.General;

namespace Cebritas.Web.Areas.Api.Controllers {
    public class RestControllerBase : Controller {
        protected JsonResult ErrorResult(string message = Messages.UNDEFINED_ERROR) {
            return Json(new { Error = message }, JsonRequestBehavior.AllowGet);
        }

        protected JsonResult SuccessResult(object result) {
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        protected JsonResult SuccessResult(string message = Messages.OK) {
            return SuccessResult(new { Success = message });
        }

        protected override void OnException(ExceptionContext filterContext) {
            if (filterContext == null) { return; }

            var ex = filterContext.Exception;
            string errorMessage = Messages.THERE_WAS_A_PROBLEMO_JEFE;
            if (ex != null) {
                if (ex is CebraException) {
                    errorMessage = ex.Message;
                }
            }

            filterContext.ExceptionHandled = true;
            filterContext.Result = ErrorResult(errorMessage);
        }
    }
}