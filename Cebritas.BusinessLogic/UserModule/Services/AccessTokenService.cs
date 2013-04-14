using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cebritas.BusinessLogic.Entities;
using Cebritas.General;

namespace Cebritas.BusinessLogic.UserModule.Services {
    public class AccessTokenService : IAccessTokenService {
        private IAccessTokenRepository db;

        private AccessTokenService(IAccessTokenRepository db) {
            this.db = db;
        }

        public static AccessTokenService CreateAccessTokenService(IAccessTokenRepository db) {
            return new AccessTokenService(db);
        }

        public AccessToken Get(long id) {
            return db.Get(id);
        }

        public AccessToken GetByToken(string token) {
            return db.Filter(x => x.Token.Equals(token)).FirstOrDefault();
        }

        public AccessToken Insert(Usuario user) {
            AccessToken accessToken = new AccessToken();

            string token = Cebritas.General.Cryptography.SecurityTokenGenerator.GenerateAccessToken(user.Email);
            AccessToken temp = GetByToken(token);
            while (true) {
                if (temp == null) {
                    break;
                }
                token = Cebritas.General.Cryptography.SecurityTokenGenerator.GenerateAccessToken(user.Email);
                temp = GetByToken(token);
            }
            accessToken.User = user;
            accessToken.Token = token;

            return db.Insert(accessToken);
        }

        public int Delete(long id) {
            AccessToken accessToken = new AccessToken() { Id = id };
            return db.Delete(accessToken);
        }

        public int DeleteByToken(string token) {
            AccessToken accessToken = GetByToken(token);
            return db.Delete(accessToken);
        }
    }
}