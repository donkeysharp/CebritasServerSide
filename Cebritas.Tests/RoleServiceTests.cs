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
    public class RoleServiceTests {
        [TestMethod]
        public void AddRoleTest() {
            RoleService service = RoleService.CreateRoleService(new RoleRepository());
            Role role1 = new Role();
            role1.Name = "admin";

            Role role2 = new Role();
            role2.Name = "visitor";

            Role role3 = new Role();
            role3.Name = "test";

            Role newRole1 = service.Insert(role1);
            Role newRole2 = service.Insert(role2);
            Role newRole3 = service.Insert(role3);

            // Item saved must be the same as the item sended to method
            Assert.AreEqual<string>(role1.Name, newRole1.Name);
            Assert.AreEqual<string>(role2.Name, newRole2.Name);
            Assert.AreEqual<string>(role3.Name, newRole3.Name);
        }

        [TestMethod]
        public void AddExistingRoleTest() {
            RoleService service = RoleService.CreateRoleService(new RoleRepository());
            Role role = new Role();
            role.Name = "admina";
            try {
                Role newRole = service.Insert(role);
                Assert.Fail("Role already exists, it must not be added");
            } catch (Exception) {
                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        public void UpdateRole1Test() {
            RoleService service = RoleService.CreateRoleService(new RoleRepository());
            Role role = service.GetByName("admina");
            if (role == null) {
                Assert.Fail("Role does not exist");
                return;
            }

            role.Name = "admina2";
            int rowsAffected = service.Update(role);

            Assert.AreEqual<int>(1, rowsAffected);
        }

        [TestMethod]
        public void UpdateRole2Test() {
            RoleService service = RoleService.CreateRoleService(new RoleRepository());
            Role role = new Role() {
                Id = 1,
                Name = "admin"
            };
            role.Name = "AdmiN";

            int rowsAffected = service.Update(role);

            Assert.AreEqual<int>(1, rowsAffected, "There are not modified rows");
        }

        [TestMethod]
        public void DeleteRoleTest() {
            RoleService service = RoleService.CreateRoleService(new RoleRepository());
            int r = service.Delete(3);

            Assert.AreEqual<int>(1, r);
        }
    }
}