using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Cebritas.BusinessLogic.Entities {
    public class AlertaUrbana {
        public long Id { get; set; }

        public long SolicitudId { get; set; }
        [ForeignKey("SolicitudId")]
        public virtual SolicitudAlerta SolicitudAlerta { get; set; }
    }
}