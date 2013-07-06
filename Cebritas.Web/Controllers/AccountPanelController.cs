using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cebritas.BusinessLogic.Entities;
using Cebritas.BusinessLogic.UserModule.Services;
using Cebritas.DataAccess.Repositories;
using Cebritas.General;
using Cebritas.Web.Authentication.Security;
using Cebritas.Web.Models.UserManagement;

namespace Cebritas.Web.Controllers {
    [Authorize(Roles="admin")]
    public class AccountPanelController : CebraControllerBase {
        [HttpGet]
        public ActionResult Index() {
            return View();
        }
        [HttpGet]
        public JsonResult GetUsers() {
            Usuario currentUser = SessionManager.GetAuthenticatedUser();
            IUserService userService = UserService.CreateUserService(new UserRepository());
            IEnumerable<Usuario> users = userService.Filter();
            List<UserViewModel> result = new List<UserViewModel>();
            UserViewModel item;
            foreach (Usuario user in users) {
                item = new UserViewModel();
                item.Code = user.AuthenticationCode;
                item.Name = user.Name;
                item.Email = user.Email;
                item.Country = user.Country;
                item.TimeZone = TimeUtil.GetTimeZone(user.TimeZone).DisplayName;

                result.Add(item);
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}