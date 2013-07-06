using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cebritas.Web.Models.Profile {
    public class ProfilePasswordViewModel {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string VerifyPassword { get; set; }
    }
}