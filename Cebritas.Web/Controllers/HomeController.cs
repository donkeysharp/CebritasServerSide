using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Cebritas.Web.Authentication.Security;
using Cebritas.Web.Models.Home;

namespace Cebritas.Web.Controllers {
    public class HomeController : CebraControllerBase {
        [HttpGet]
        public ActionResult Index() {
            return View();
        }
        [HttpGet]
        public ActionResult Logon() {
            return View();
        }
        [HttpPost]
        public ActionResult Logon(LogonViewModel logonViewModel, string returnUrl) {
            // TODO: Authenticate
            if (ModelState.IsValid) {
                if (Membership.ValidateUser(logonViewModel.Email, logonViewModel.Password)) {
                    FormsAuthentication.SetAuthCookie(logonViewModel.Email, true);
                    // In case it will redirect
                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                        && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\")) {
                        return Redirect(returnUrl);
                    } else {
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            return View();
        }
        [HttpGet]
        public ActionResult Logout() {
            SessionManager.CloseSession();
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}