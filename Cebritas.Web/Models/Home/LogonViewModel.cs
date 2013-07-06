using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cebritas.Web.Models.Home {
    public class LogonViewModel {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}