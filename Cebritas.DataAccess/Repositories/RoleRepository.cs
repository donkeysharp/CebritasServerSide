﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cebritas.BusinessLogic.Entities;
using Cebritas.BusinessLogic.UserModule;

namespace Cebritas.DataAccess.Repositories {
    public class RoleRepository : GenericRepository<Role>, IRoleRepository {
        public string[] GetRoleNames() {
            IEnumerable<string> roleNames = from role in context.Roles
                                            select role.Name;
            return roleNames.ToArray();
        }
    }
}