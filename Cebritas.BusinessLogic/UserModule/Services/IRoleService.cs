using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cebritas.BusinessLogic.Entities;

namespace Cebritas.BusinessLogic.UserModule.Services {
    public interface IRoleService {
        Role Get(long id);
        string[] GetRoleNames();
        Role GetByName(string name);
        Role Insert(Role role);
        int Delete(long id);
        int Update(Role role);
    }
}