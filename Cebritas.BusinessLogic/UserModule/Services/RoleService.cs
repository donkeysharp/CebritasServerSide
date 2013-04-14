using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cebritas.BusinessLogic.Entities;
using Cebritas.General;

namespace Cebritas.BusinessLogic.UserModule.Services {
    public class RoleService : IRoleService {
        private IRoleRepository db;

        private RoleService(IRoleRepository db) {
            this.db = db;
        }

        public static RoleService CreateRoleService(IRoleRepository db) {
            return new RoleService(db);
        }
        public Role Get(long id) {
            return db.Get(id);
        }

        public Role GetByName(string name) {
            return db.Filter(x => x.Name.Equals(name)).FirstOrDefault();
        }

        public Role Insert(Role role) {
            if (GetByName(role.Name) != null) {
                throw new CebraException(Messages.ROLE_ALREADY_EXISTS);
            }
            return db.Insert(role);
        }

        public int Delete(long id) {
            Role role = new Role() { Id = id };
            return db.Delete(role);
        }

        public int Update(Role role) {
            return db.Update(role);
        }
    }
}