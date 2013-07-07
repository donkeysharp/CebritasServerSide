using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cebritas.BusinessLogic.Entities;
using Cebritas.BusinessLogic.UserModule.Services;
using Cebritas.DataAccess.Repositories;
using Cebritas.General;
using Cebritas.General.Geo;
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
        public JsonResult GetRegionalInfo() {
            IEnumerable<CebraTimeZone> timeZones = TimeUtil.GetTimeZones();
            IEnumerable<Country> countries = Regions.GetCountries();
            return Json(new { timezones = timeZones, countries = countries }, JsonRequestBehavior.AllowGet);
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
                item.Role = user.RoleId;
                item.RoleName = user.Rol.Name;
                item.TimeZoneId = user.TimeZone;
                item.Description = user.Information;
                item.TimeZone = TimeUtil.GetTimeZone(user.TimeZone).DisplayName;

                result.Add(item);
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult CreateUser(UserViewModel userViewModel) {
            IUserService userService = UserService.CreateUserService(new UserRepository());
            Usuario user = new Usuario();
            user.Name = userViewModel.Name;
            user.Email = userViewModel.Email;
            user.RoleId = userViewModel.Role;
            user.Country = userViewModel.Country;
            user.TimeZone = userViewModel.TimeZoneId;
            user.Password = userViewModel.Password;
            user.Information = userViewModel.Description;

            userService.Insert(user);

            return RedirectToAction("Index", "AccountPanel");
        }
        [HttpPost]
        public ActionResult UpdateUser(UserViewModel userViewModel) {
            IUserService userService = UserService.CreateUserService(new UserRepository());
            Usuario user = userService.GetByAuthenticationCode(userViewModel.Code);

            if (user != null) {
                user.Name = userViewModel.Name;
                user.RoleId = userViewModel.Role;
                user.Country = userViewModel.Country;
                user.TimeZone = userViewModel.TimeZoneId;
                user.Information = userViewModel.Description;
                bool updatePassword = false;
                if (!string.IsNullOrEmpty(userViewModel.Password)) {
                    user.Password = userViewModel.Password;
                    updatePassword = true;
                }

                userService.Update(user, updatePassword);
            }
            return RedirectToAction("Index", "AccountPanel");;
        }
        [HttpPost]
        public ActionResult DeleteUser(string code) {
            return null;
        }
    }
}