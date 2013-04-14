using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cebritas.BusinessLogic.Entities;

namespace Cebritas.BusinessLogic.UserModule.Services {
    public interface IAccessTokenService {
        AccessToken Get(long id);
        AccessToken GetByToken(string token);
        AccessToken Insert(Usuario user);
        int Delete(long id);
        int DeleteByToken(string token);
    }
}