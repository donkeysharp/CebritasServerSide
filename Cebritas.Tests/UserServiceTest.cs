using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cebritas.BusinessLogic.Entities;
using Cebritas.BusinessLogic.UserModule.Services;
using Cebritas.DataAccess.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cebritas.Tests {
    [TestClass]
    public class UserServiceTest {
        [TestMethod]
        public void InsertUserTest() {
            IUserService service = UserService.CreateUserService(new UserRepository());
            IRoleService roleService = RoleService.CreateRoleService(new RoleRepository());
            Usuario x = new Usuario();
            x.Active = false;
            x.Email = "danieloo_123@hotmail.com";
            x.Name = "Daniel Mendoza";
            x.Rol = roleService.GetByName("media");
            x.Password = "none";
            x.Country = "BO";

            Usuario result = service.Insert(x);
            Assert.AreEqual<string>(x.Email, result.Email);
        }

        [TestMethod]
        public void UpdateUserTest() {
            UserService service = UserService.CreateUserService(new UserRepository());
            RoleService roleService = RoleService.CreateRoleService(new RoleRepository());
            Usuario x = service.Get(1);
            x.Name = "Sergio Gabriel Guillen Mantilla";
            Role r = roleService.Get(2);
            x.Rol = r;
            int rowsAffected = service.Update(x);
            Assert.AreEqual<int>(1, rowsAffected);
        }
    }
}