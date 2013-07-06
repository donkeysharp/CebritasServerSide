using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cebritas.Web.Models.Profile {
    public class ProfileViewModel {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string Country { get; set; }
        public int TimeZone { get; set; }
        public string Description { get; set; }
    }
}