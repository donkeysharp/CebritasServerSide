using System;
using System.Web.Mvc;
using Cebritas.BusinessLogic.Entities;
using Cebritas.BusinessLogic.UserModule.Services;
using Cebritas.DataAccess.Repositories;
using Cebritas.General;
using Cebritas.General.Cryptography;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Cebritas.Web.Areas.Api.Controllers {
    public class UserController : RestControllerBase {
        [HttpGet]
        public JsonResult Get(string data) {
            return SuccessResult(data);
        }

        [HttpPost]
        public JsonResult Register(string data) {
            IUserService userService = UserService.CreateUserService(new UserRepository());
            IRoleService roleService = RoleService.CreateRoleService(new RoleRepository());

            Usuario user = DecryptData(data);
            user.Rol = roleService.GetByName("visitor");

            userService.Insert(user);

            return SuccessResult(Messages.OK);
        }

        [HttpPost]
        public JsonResult LogIn(string data) {
            IUserService userService = UserService.CreateUserService(new UserRepository());
            IAccessTokenService accessTokenService = AccessTokenService.CreateAccessTokenService(new AccessTokenRepository());

            Usuario userData = DecryptData(data);
            Usuario user = userService.AuthenticateUser(userData.Email, userData.Password);
            if (user != null && !string.IsNullOrEmpty(user.AuthenticationCode)) {
                AccessToken accessToken = accessTokenService.Insert(user);
                return SuccessResult(new { AuthenticationCode = user.AuthenticationCode, AccessToken = accessToken.Token });
            }

            return ErrorResult(Messages.USER_CREDENTIALS_ARE_INVALID);
        }

        [HttpPost]
        public JsonResult LogOut(string authenticationCode, string accessToken) {
            IUserService userService = UserService.CreateUserService(new UserRepository());
            IAccessTokenService accessTokenService = AccessTokenService.CreateAccessTokenService(new AccessTokenRepository());

            Usuario user = userService.GetByAuthenticationCode(authenticationCode);
            if (user != null) {
                AccessToken accessTokenObj = accessTokenService.GetByToken(accessToken);
                if (accessTokenObj != null && user.Id == accessTokenObj.UserId) {
                    accessTokenService.DeleteByToken(accessToken);

                    return SuccessResult(Messages.SUCCESSFULLY_LOGGED_OUT);
                }
            }
            return ErrorResult(Messages.INVALID_SESSION);
        }

        [HttpGet]
        public JsonResult GetUser(string authenticationCode, string accessToken) {
            IUserService userService = UserService.CreateUserService(new UserRepository());
            IAccessTokenService accessTokenService = AccessTokenService.CreateAccessTokenService(new AccessTokenRepository());

            Usuario user = userService.GetByAuthenticationCode(authenticationCode);
            if (user == null) {
                return ErrorResult(Messages.INVALID_SESSION);
            } else {
                AccessToken accessTokenObj = accessTokenService.GetByToken(accessToken);
                if (accessTokenObj == null || user.Id != accessTokenObj.UserId) {
                    return ErrorResult(Messages.INVALID_SESSION);
                }
            }

            return SuccessResult(new { Email = user.Email, Nombre = user.Name });
        }

        [HttpPut]
        public JsonResult UpdateUser(string authenticationCode, string accessToken, string data) {
            Usuario user = UserUtils.ValidateSessionThrowable(authenticationCode, accessToken);

            IUserService userService = UserService.CreateUserService(new UserRepository());
            IAccessTokenService accessTokenService = AccessTokenService.CreateAccessTokenService(new AccessTokenRepository());

            Usuario requestUser = DecryptData(data);
            user.Name = requestUser.Name;
            user.Password = requestUser.Password;

            userService.Update(user);

            return SuccessResult(Messages.OK);
        }

        private Usuario DecryptData(string data) {
            string xmlPrivateKey = ApiData.GetInstance().PrivateKey;
            RSAUtil rsa = new RSAUtil();
            rsa.XmlPrivateKey = xmlPrivateKey;
            try {
                string jsonString = rsa.DecryptTextToString(data);
                JObject temp = (JObject)JsonConvert.DeserializeObject(jsonString);
                Usuario result = new Usuario();

                result.Name = temp["Nombre"] != null ? temp["Nombre"].ToString() : string.Empty;
                result.Email = temp["Email"] != null ? temp["Email"].ToString() : string.Empty;
                result.Password = temp["Password"] != null ? temp["Password"].ToString() : string.Empty;

                return result;
            } catch (Exception) {
                throw new CebraException(Messages.ERROR_PARSING_DATA);
            }
        }
    }
}