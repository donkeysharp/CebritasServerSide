using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using Cebritas.General;

namespace Cebritas.BusinessLogic.Entities {
    public class SolicitudAlerta {
        public long Id { get; set; }
        public string Code { get; set; }
        public DateTime Fecha { get; set; }
        public double Latitud { get; set; }
        public double Longitud { get; set; }
        public string Descripcion { get; set; }
        public string Tipo { get; set; }
        public string TiempoEstimado { get; set; }
        public bool Activo { get; set; }

        public long UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual Usuario Usuario { get; set; }

        public double GetExtraHours() {
            if (TiempoEstimado.Equals(Constants.ALERTA_TIEMPO_15M)) {
                return 15.0 / 60.0;
            } else if (TiempoEstimado.Equals(Constants.ALERTA_TIEMPO_30M)) {
                return 30.0 / 60.0;
            } else if (TiempoEstimado.Equals(Constants.ALERTA_TIEMPO_1H)) {
                return 1.0;
            } else if (TiempoEstimado.Equals(Constants.ALERTA_TIEMPO_2H)) {
                return 2.0;
            } else if (TiempoEstimado.Equals(Constants.ALERTA_TIEMPO_3HH)) {
                return 5.0;
            } else {
                return 30.0 / 60.0;
            }
        }
    }
}