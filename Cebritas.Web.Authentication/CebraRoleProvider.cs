using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;
using Cebritas.BusinessLogic.UserModule.Services;
using Cebritas.DataAccess.Repositories;

namespace Cebritas.Web.Authentication {
    public class CebraRoleProvider : RoleProvider {
        #region "Properties"
        private string applicationName;

        public override string ApplicationName {
            get {
                return applicationName;
            }
            set {
                applicationName = value;
            }
        }
        #endregion "Properties"

        public CebraRoleProvider() {
            this.applicationName = "CebraWebApp";
        }

        public override bool IsUserInRole(string username, string roleName) {
            IUserService userService = UserService.CreateUserService(new UserRepository());
            return userService.IsUserInRole(username, roleName);
        }

        public override string[] GetAllRoles() {
            IRoleService roleService = RoleService.CreateRoleService(new RoleRepository());
            return roleService.GetRoleNames();
        }

        public override string[] GetRolesForUser(string username) {
            IUserService userService = UserService.CreateUserService(new UserRepository());
            return userService.GetRolesByUsername(username);
        }

        #region "Not implemented"
        public override void AddUsersToRoles(string[] usernames, string[] roleNames) {
            return;
        }

        public override void CreateRole(string roleName) {
            return;
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole) {
            return false;
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch) {
            return new string[0];
        }

        public override string[] GetUsersInRole(string roleName) {
            return new string[0];
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames) {
            return;
        }

        public override bool RoleExists(string roleName) {
            return false;
        }
        #endregion "Not implemented"
    }
}