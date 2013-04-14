using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cebritas.BusinessLogic.Entities;
using Cebritas.BusinessLogic.UserModule;

namespace Cebritas.DataAccess.Repositories {
    public class UserRepository : GenericRepository<Usuario>, IUserRepository {
    }
}