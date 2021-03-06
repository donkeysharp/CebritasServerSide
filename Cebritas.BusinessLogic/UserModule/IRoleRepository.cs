﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cebritas.BusinessLogic.Entities;

namespace Cebritas.BusinessLogic.UserModule {
    public interface IRoleRepository : IRepository<Role> {
        string[] GetRoleNames();
    }
}