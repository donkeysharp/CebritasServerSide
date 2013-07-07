using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cebritas.Web.Models.UserManagement {
    public class UserViewModel {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public string Country { get; set; }
        public int TimeZoneId { get; set; }
        public string TimeZone { get; set; }
        public long Role { get; set; }
        public string RoleName { get; set; }
        public string Password { get; set; }
        public string VerifyPassword { get; set; }
    }
}