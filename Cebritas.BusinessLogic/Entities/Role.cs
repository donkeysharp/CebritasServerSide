using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cebritas.BusinessLogic.Entities {
    public class Role {
        public long Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Usuario> Users { get; set; }
    }
}