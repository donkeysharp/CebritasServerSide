using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cebritas.BusinessLogic.Entities;
using Cebritas.BusinessLogic.UserModule;

namespace Cebritas.DataAccess.Repositories {
    public class UserRepository : GenericRepository<Usuario>, IUserRepository {
        public string[] GetRolesByUsername(string username) {
            IEnumerable<string> roleNames = from user in context.Users
                                            where user.Email.Equals(username)
                                            select user.Rol.Name;
            return roleNames.ToArray();
        }
    }
}