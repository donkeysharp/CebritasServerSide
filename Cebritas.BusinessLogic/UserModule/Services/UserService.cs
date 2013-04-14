using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Cebritas.BusinessLogic.Entities;
using Cebritas.General;
using Cebritas.General.Cryptography;

namespace Cebritas.BusinessLogic.UserModule.Services {
    public class UserService : IUserService {
        private IUserRepository db;

        private UserService(IUserRepository db) {
            this.db = db;
        }

        public static UserService CreateUserService(IUserRepository db) {
            return new UserService(db);
        }

        public Usuario Get(long id) {
            return db.Get(id);
        }

        public Usuario GetByUsername(string username) {
            return db.Filter(x => x.Email.Equals(username)).FirstOrDefault();
        }

        public Usuario GetByEmail(string email) {
            return GetByUsername(email);
        }

        public Usuario GetByAuthenticationCode(string authenticationCode) {
            IList<Usuario> result = (IList<Usuario>)db.Filter(x => x.AuthenticationCode.Equals(authenticationCode));
            return result.Count > 0 ? result.First() : null;
        }

        public IEnumerable<Usuario> Filter(System.Linq.Expressions.Expression<Func<Usuario, bool>> filter = null, Func<IQueryable<Usuario>, IOrderedQueryable<Usuario>> orderBy = null) {
            return db.Filter(filter, orderBy);
        }

        public Usuario Insert(Usuario user) {
            user.Validate();
            if (GetByUsername(user.Email) != null) {
                throw new CebraException(Messages.USER_ALREADY_EXISTS);
            }
            user.Password = Cebritas.General.Cryptography.HashSumUtil.GetHashSum(user.Password, General.Cryptography.HashSumType.SHA1);
            user.Active = true;
            user.AuthenticationCode = Cebritas.General.Cryptography.SecurityTokenGenerator.GenerateAuthenticationToken(user.Email);

            string email = user.Email;
            string activationCode = Cebritas.General.Cryptography.SecurityTokenGenerator.GenerateRandomActivationCode();

            // Verifica que el codigo de activacion sea unico
            List<Usuario> users = (List<Usuario>)Filter(x => x.ActivationCode.Equals(activationCode));
            while (true) {
                if (users.Count == 0) {
                    break; // Es unico
                }
                users = (List<Usuario>)Filter(x => x.ActivationCode.Equals(activationCode));
            }
            user.ActivationCode = activationCode;

            // TODO: How to send email :P fucking SPAM system
            /*string smtpServer = ConfigurationUtil.Get(Constants.SMTP_SERVER);
            int smtpPort = Convert.ToInt32(ConfigurationUtil.Get(Constants.SMTP_PORT));
            string smtpSender = ConfigurationUtil.Get(Constants.SMTP_SENDER);
            string smtpPassword = ConfigurationUtil.Get(Constants.SMTP_PASSWORD);

            Cebritas.General.Networking.EmailUtil emailSender = new General.Networking.EmailUtil(smtpServer, smtpPort, smtpSender, smtpPassword);
            emailSender.AddRecipient(email);

            StringBuilder sb = new StringBuilder();
            sb.Append("Para confirmar su cuenta visite el siguiente link: ");
            sb.Append("http://localhost:51024/user/activate?activationCode=");
            sb.Append(activationCode);

            emailSender.SetBody(sb.ToString());
            emailSender.SendMessage("Activar cuenta");*/

            return db.Insert(user);
        }

        public int Delete(long id) {
            Usuario item = new Usuario() { Id = id };
            return db.Delete(item);
        }

        public int Update(Usuario user) {
            user.Validate();
            if (Get(user.Id) == null) {
                throw new CebraException(Messages.USER_DOES_NOT_EXIST);
            }
            user.Password = HashSumUtil.GetHashSum(user.Password, HashSumType.SHA1);
            return db.Update(user);
        }

        public void ChangeStatus(long id, bool isActive) {
            Usuario user = Get(id);
            if (user == null) {
                throw new CebraException(Messages.USER_DOES_NOT_EXIST);
            }
            user.Active = isActive;
            db.Update(user);
            db.SaveChanges();
        }

        public Usuario AuthenticateUser(string email, string password) {
            Usuario user = GetByEmail(email);
            if (user != null && user.Password.Equals(HashSumUtil.GetHashSum(password, HashSumType.SHA1)) && user.Active) {
                return user;
            }
            return null;
        }
    }
}