using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Cebritas.BusinessLogic.Entities;
using Cebritas.General;

namespace Cebritas.Web.Authentication.Security {
    public class SessionManager {
        public static void SetAuthenticatedUser(Usuario user) {
            HttpContext.Current.Session[Constants.SESSION_USER] = user;
        }
        public static Usuario GetAuthenticatedUser() {
            if (HttpContext.Current.Session[Constants.SESSION_USER] is Usuario) {
                return (Usuario)HttpContext.Current.Session[Constants.SESSION_USER];
            }
            return null;
        }
        public static void CloseSession() {
            HttpContext.Current.Session[Constants.SESSION_USER] = null;
        }
    }
}