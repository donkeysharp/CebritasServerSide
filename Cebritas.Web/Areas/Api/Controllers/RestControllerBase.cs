using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cebritas.General;

namespace Cebritas.Web.Areas.Api.Controllers {
    public class RestControllerBase : Controller {
        /// <summary>
        /// New Success Result
        /// </summary>
        /// <param name="result">Could be a single object or an IEnumerable</param>
        /// <param name="message">Message</param>
        /// <returns></returns>
        protected JsonResult SuccessResult(object result, string message = Messages.OK) {
            int status = Constants.HTTP_OK;
            if (result != null) {
                if (result is IEnumerable) {
                    return GenericResult(status, message, (IEnumerable)result);
                } else {
                    List<object> data = new List<object>();
                    data.Add(result);

                    return GenericResult(status, message, data);
                }
            } else {
                object[] data = new object[0];
                return GenericResult(status, message, data);
            }
        }

        /// <summary>
        /// New Error Result
        /// </summary>
        /// <param name="status">An error status, commonly 400, 403</param>
        /// <param name="message">Message</param>
        /// <returns></returns>
        protected JsonResult ErrorResult(int status, string message = Messages.UNDEFINED_ERROR) {
            object[] data = new object[0];
            return GenericResult(status, message, data);
        }

        /// <summary>
        /// Generic method that returns a json with the format
        /// {
        ///     Status: 200|400|403|500,
        ///     Message: "message",
        ///     Data: []
        /// }
        /// </summary>
        /// <param name="statusCode"></param>
        /// <param name="message"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        protected JsonResult GenericResult(int statusCode, string message, IEnumerable data) {
            return Json(new { Status = statusCode,
                              Message = message,
                              Data = data }, JsonRequestBehavior.AllowGet);
        }

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
            int status = Constants.HTTP_INTERNAL_ERRROR;
            if (ex != null) {
                if (ex is CebraException) {
                    status = ((CebraException)ex).Status;
                    errorMessage = ex.Message;
                }
            }
            // TODO: Remove this concat when remote "debugging" finished
            if(status == Constants.HTTP_INTERNAL_ERRROR) {
                errorMessage += "-------- " + ex.Message + " --- " + ex.StackTrace;
            }

            filterContext.ExceptionHandled = true;
            filterContext.Result = ErrorResult(status, errorMessage);
        }
    }
}