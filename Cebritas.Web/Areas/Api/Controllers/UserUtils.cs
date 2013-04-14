using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cebritas.BusinessLogic.Entities;
using Cebritas.BusinessLogic.UserModule.Services;
using Cebritas.DataAccess.Repositories;
using Cebritas.General;

namespace Cebritas.Web.Areas.Api.Controllers {
    public class UserUtils {
        public static Usuario ValidateSession(string authenticationCode, string accessToken) {
            IUserService userService = UserService.CreateUserService(new UserRepository());
            IAccessTokenService accessTokenService = AccessTokenService.CreateAccessTokenService(new AccessTokenRepository());

            Usuario currentUser = userService.GetByAuthenticationCode(authenticationCode);
            if (currentUser != null) {
                AccessToken currentAccessToken = accessTokenService.GetByToken(accessToken);
                if (currentAccessToken != null) {
                    if (currentAccessToken.UserId == currentUser.Id) {
                        return currentUser;
                    } else {
                        return null;
                    }
                } else {
                    return null;
                }
            } else {
                return null;
            }
        }
        public static Usuario ValidateSessionThrowable(string authenticationCode, string accessToken) {
            Usuario currentUser = ValidateSession(authenticationCode, accessToken);
            if (currentUser != null) {
                return currentUser;
            } else {
                throw new CebraException(Messages.INVALID_SESSION);
            }
        }
    }
}