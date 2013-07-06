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
using Cebritas.Web.Models.Profile;

namespace Cebritas.Web.Controllers {
    [Authorize]
    public class ProfileController : CebraControllerBase {
        public ActionResult Index() {
            Usuario usuario = SessionManager.GetAuthenticatedUser();
            ProfileViewModel viewModel = new ProfileViewModel();
            viewModel.Name = usuario.Name;
            viewModel.Email = usuario.Email;
            viewModel.Role = usuario.Rol.Name;
            viewModel.Country = usuario.Country;
            viewModel.TimeZone = usuario.TimeZone;
            viewModel.Description = usuario.Information;

            ViewBag.Countries = new SelectList(Regions.GetCountries(), "Code", "Name");
            ViewBag.TimeZones = new SelectList(TimeUtil.GetTimeZones(), "Id", "Name");

            return View(viewModel);
        }
        [HttpPost]
        public ActionResult UpdateProfile(ProfileViewModel profileViewModel) {
            IUserService userService = UserService.CreateUserService(new UserRepository());
            Usuario usuario = SessionManager.GetAuthenticatedUser();
            usuario.Name = profileViewModel.Name;
            usuario.Country = profileViewModel.Country;
            usuario.TimeZone = profileViewModel.TimeZone;
            usuario.Information = profileViewModel.Description;

            userService.Update(usuario);

            return RedirectToAction("Index", "Profile");
        }
        [HttpPost]
        public ActionResult UpdatePassword(ProfilePasswordViewModel passwordViewModel) {
            Usuario user = SessionManager.GetAuthenticatedUser();
            IUserService userService = UserService.CreateUserService(new UserRepository());
            string oldPassword = General.Cryptography.HashSumUtil.GetHashSum(passwordViewModel.OldPassword, General.Cryptography.HashSumType.SHA1);
            if (user.Password.Equals(oldPassword)) {
                if (passwordViewModel.NewPassword.Equals(passwordViewModel.VerifyPassword)) {
                    user.Password = passwordViewModel.NewPassword;
                    userService.Update(user, true);
                } else {
                    TempData.Add("ErrorsPassword", true);
                }
            } else {
                TempData.Add("ErrorsPassword", true);
            }
            return RedirectToAction("Index", "Profile");
        }
    }
}