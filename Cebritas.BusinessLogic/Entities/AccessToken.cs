using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Cebritas.BusinessLogic.Entities {
    public class AccessToken {
        public long Id { get; set; }
        public string Token { get; set; }

        public long UserId { get; set; }
        [ForeignKey("UserId")]
        public Usuario User { get; set; }
    }
}