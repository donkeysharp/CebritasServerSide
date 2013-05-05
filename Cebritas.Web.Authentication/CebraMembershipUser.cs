using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;
using Cebritas.General;

namespace Cebritas.Web.Authentication {
    public class CebraMembershipUser : MembershipUser {
        #region "Private Members"
        private string membershipProvider;
        private long userId;
        // username and mail will be the same for both users
        private string username;
        private string email;
        private string name;
        private string authenticationCode;
        private bool active;

        private bool isLockedOut = false;
        private bool isApproved = false;

        private static int userNameMaxLength = 50;
        #endregion "Private Members"

        #region "Constructors"
        public CebraMembershipUser(string username)
            : base("Cebritas.Web.Authentication.CebraMembershipUser", username,
                   username.GetHashCode(), username, string.Empty, string.Empty,
                   true, false, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now) {
            this.username = username;
            this.email = username;
        }

        public CebraMembershipUser(string membershipProvider, long userId, string username) {
            if (string.IsNullOrEmpty(membershipProvider)) {
                throw new CebraException("Membership provider name is null or empty");
            }
            if (string.IsNullOrEmpty(username)) {
                throw new CebraException("Username is null or empty");
            }

            this.membershipProvider = membershipProvider;
            this.username = username;
            this.email = username;
        }

        public CebraMembershipUser(string membershipProvider, long userId, string username, string name, string authenticationCode)
            : this(membershipProvider, userId, username) {
            if (string.IsNullOrEmpty(membershipProvider)) {
                throw new CebraException("Membership provider name is null or empty");
            }
            if (string.IsNullOrEmpty(username)) {
                throw new CebraException("Username is null or empty");
            }

            this.authenticationCode = authenticationCode;
            this.name = name;
        }
        #endregion "Constructors"

        #region "Properties"
        public string MembershipProvider {
            get { return membershipProvider; }
        }
        public long UserId {
            get { return userId; }
            set { userId = value; }
        }
        public override string UserName {
            get { return this.username; }
        }
        public override string Email {
            get { return this.email; }
            set {
                this.email = value;
                this.username = value;
            }
        }
        public string Name {
            get { return name; }
            set { this.name = value; }
        }
        public string AuthenticationCode {
            get { return this.authenticationCode; }
            set { this.authenticationCode = value; }
        }
        public bool IsActive {
            get { return this.active; }
            set { this.active = value; }
        }
        #endregion "Properties"

        #region "Not implemented properites"
        public override bool IsLockedOut {
            get { return this.isLockedOut; }
        }
        public override bool IsApproved {
            get { return this.isApproved; }
            set { this.isApproved = value; }
        }
        public static int UserNameMaxLenght {
            get { return userNameMaxLength; }
        }
        #endregion "Not implemented properites"

        #region "Public Methods"
        public override bool ChangePassword(string oldPassword, string newPassword) {
            return false;
        }
        public override string GetPassword() {
            return string.Empty;
        }
        public override string GetPassword(string passwordAnswer) {
            return string.Empty;
        }
        #endregion "Public Methods"
    }
}