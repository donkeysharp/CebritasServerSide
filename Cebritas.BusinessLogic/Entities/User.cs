using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using Cebritas.General;

namespace Cebritas.BusinessLogic.Entities {
    public class Usuario {
        public long Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string AuthenticationCode { get; set; }
        public string ActivationCode { get; set; }
        public bool Active { get; set; }
        public string Country { get; set; }

        public long RoleId { get; set; }
        [ForeignKey("RoleId")]
        public virtual Role Rol { get; set; }

        public virtual ICollection<AccessToken> Tokens { get; set; }

        public void Validate() {
            if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Password)) {
                throw new CebraException(Messages.SOME_FIELDS_ARE_NOT_IN_VALID_FORMAT);
            }
        }
    }
}