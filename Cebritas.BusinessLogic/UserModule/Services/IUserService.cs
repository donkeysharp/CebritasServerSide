using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cebritas.BusinessLogic.Entities;

namespace Cebritas.BusinessLogic.UserModule.Services {
    public interface IUserService {
        Usuario Get(long id);
        IEnumerable<Usuario> Filter(Expression<Func<Usuario, bool>> filter = null,
                           Func<IQueryable<Usuario>, IOrderedQueryable<Usuario>> orderBy = null);

        Usuario GetByUsername(string username);
        Usuario GetByEmail(string email);
        Usuario GetByAuthenticationCode(string authenticationCode);
        string[] GetRolesByUsername(string username);
        void ChangeStatus(long id, bool isActive);
        Usuario Insert(Usuario user);
        int Delete(long id);
        int Update(Usuario user, bool updatePassword = false);

        Usuario AuthenticateUser(string email, string password);

        bool IsUserInRole(string username, string roleName);
    }
}