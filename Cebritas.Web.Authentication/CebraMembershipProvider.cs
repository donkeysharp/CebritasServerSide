using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using Cebritas.BusinessLogic.Entities;
using Cebritas.BusinessLogic.UserModule.Services;
using Cebritas.DataAccess.Repositories;
using Cebritas.General;
using Cebritas.Web.Authentication.Security;

namespace Cebritas.Web.Authentication {
    public class CebraMembershipProvider : MembershipProvider {

        #region "Private Members"
        private bool enablePasswordReset;
        private bool enablePasswordRetrieval;
        private bool requiresQuestionAndAnswer;
        private bool requiresUniqueEmail;
        private string applicationName;
        private string passwordStrengthRegularExpression;
        private int maxAllowedPasswordLength;
        private int maxInvalidPasswordAttempts;
        private int minRequiredNonAlphanumericCharacters;
        private int minRequiredPasswordLength;
        private int passwordAttemptWindow;
        private MembershipPasswordFormat passwordFormat;

        #endregion "Private Members"

        #region "Application Name"
        public override string ApplicationName {
            get { return applicationName; }
            set { applicationName = value; }
        }

        #endregion "Application Name"

        #region "Password Properties"
        public override bool EnablePasswordReset {
            get { return enablePasswordReset; }
        }
        public override bool EnablePasswordRetrieval {
            get { return enablePasswordRetrieval; }
        }
        public int MaxAllowedPasswordLength {
            get { return maxAllowedPasswordLength; }
        }
        public override int MaxInvalidPasswordAttempts {
            get { return maxInvalidPasswordAttempts; }
        }
        public override int MinRequiredNonAlphanumericCharacters {
            get { return minRequiredNonAlphanumericCharacters; }
        }
        public override int MinRequiredPasswordLength {
            get { return minRequiredPasswordLength; }
        }
        public override int PasswordAttemptWindow {
            get { return passwordAttemptWindow; }
        }
        public override MembershipPasswordFormat PasswordFormat {
            get { return passwordFormat; }
        }
        public override string PasswordStrengthRegularExpression {
            get { return passwordStrengthRegularExpression; }
        }
        public override bool RequiresQuestionAndAnswer {
            get { return requiresQuestionAndAnswer; }
        }

        #endregion "Password Properties"

        #region "Email Properties"
        public override bool RequiresUniqueEmail {
            get { return requiresUniqueEmail; }
        }

        #endregion "Email Properties"

        #region "Configuration Methods"
        private string GetConfigValue(string configValue, string defaultValue) {
            if (string.IsNullOrEmpty(configValue))
                return defaultValue;
            return configValue;
        }

        #endregion "Configuration Methods"

        #region "Initialization"
        public override void Initialize(string name, System.Collections.Specialized.NameValueCollection config) {
            if (config == null)
                throw new CebraException("\'config\' parameter is null");

            if (string.IsNullOrEmpty(name))
                name = "CebraWebApp";

            base.Initialize(name, config);

            applicationName = GetConfigValue(config["applicationName"], "/");
            enablePasswordReset = Convert.ToBoolean(GetConfigValue(config["enablePasswordReset"], "false"));
            enablePasswordRetrieval = Convert.ToBoolean(GetConfigValue(config["enablePasswordRetrieval"], "false"));
            requiresQuestionAndAnswer = Convert.ToBoolean(GetConfigValue(config["requiresQuestionAndAnswer"], "false"));
            requiresUniqueEmail = Convert.ToBoolean(GetConfigValue(config["requiresUniqueEmail"], "false"));
            passwordStrengthRegularExpression = GetConfigValue(config["passwordStrengthRegularExpression"], "");
            maxInvalidPasswordAttempts = Convert.ToInt32(GetConfigValue(config["maxInvalidPasswordAttempts"], "5"));
            passwordAttemptWindow = Convert.ToInt32(GetConfigValue(config["passwordAttemptWindow"], "10"));
            minRequiredNonAlphanumericCharacters = Convert.ToInt32(GetConfigValue(config["minRequiredNonAlphanumericCharacters"], "0"));
            minRequiredPasswordLength = Convert.ToInt32(GetConfigValue(config["minRequiredPasswordLength"], "1"));
            maxAllowedPasswordLength = Convert.ToInt32(GetConfigValue(config["maxAllowedPasswordLength"], "10"));

            string pwdFormat = config["passwordFormat"];
            if (string.IsNullOrEmpty(pwdFormat))
                pwdFormat = "clear";

            switch (pwdFormat) {
                case "hashed": throw new CebraException("It doesn\'t support password hashing");
                case "encrypted": passwordFormat = MembershipPasswordFormat.Encrypted;
                    break;
                case "clear": passwordFormat = MembershipPasswordFormat.Clear;
                    break;
                default: throw new Exception("Password format not found");
            }
        }

        #endregion "Initialization"

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline) {
            CebraMembershipUser authUser = new CebraMembershipUser("no_user");
            return authUser;
        }

        public override string GetUserNameByEmail(string email) {
            // For the Cebra Project email is the same user and email
            IUserService userService = UserService.CreateUserService(new UserRepository());
            Usuario user = userService.GetByEmail(email);
            if (user != null) {
                return email;
            }
            return null;
        }

        public override bool ChangePassword(string username, string oldPassword, string newPassword) {
            return false;
        }

        public override bool ValidateUser(string username, string password) {
            IUserService userService = UserService.CreateUserService(new UserRepository());
            // username and email is the same
            try {
                Usuario user = userService.AuthenticateUser(username, password);
                if (user != null) {
                    SessionManager.SetAuthenticatedUser(user);
                    return true;
                }
                return false;
            } catch (Exception) {
                return false;
            }
        }

        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status) {
            CebraMembershipUser authUser = new CebraMembershipUser(username);
            status = MembershipCreateStatus.Success;
            return authUser;
        }

        public override MembershipUser GetUser(string username, bool userIsOnline) {
            IUserService userService = UserService.CreateUserService(new UserRepository());
            Usuario user = userService.GetByEmail(username);
            if (user != null) {
                CebraMembershipUser authUser = new CebraMembershipUser(username, user.Id, username, user.Name, user.AuthenticationCode);
                authUser.IsActive = true;

                return authUser;
            }
            return null;
        }

        #region "Non-Implemented Methods"
        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer) {
            throw new NotImplementedException();
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData) {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords) {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords) {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords) {
            throw new NotImplementedException();
        }

        public override int GetNumberOfUsersOnline() {
            throw new NotImplementedException();
        }

        public override string GetPassword(string username, string answer) {
            throw new NotImplementedException();
        }

        public override string ResetPassword(string username, string answer) {
            throw new NotImplementedException();
        }

        public override bool UnlockUser(string userName) {
            throw new NotImplementedException();
        }

        public override void UpdateUser(MembershipUser user) {
            throw new NotImplementedException();
        }

        #endregion "Non-Implemented Methods"
    }
}