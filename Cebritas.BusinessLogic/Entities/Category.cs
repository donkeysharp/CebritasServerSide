﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Cebritas.BusinessLogic.Entities {
    public class Category {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string SpanishName { get; set; }

        public long? ParentId { get; set; }
        [ForeignKey("ParentId")]
        public virtual Category Parent { get; set; }
    }
}