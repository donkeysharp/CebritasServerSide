using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cebritas.Web.Areas.Api.Models {
    public class SolicitudAlertaViewModel {
        public string Code { get; set; }
        public string Latitud { get; set; }
        public string Longitud { get; set; }
        public string Descripcion { get; set; }
        public string Tipo { get; set; }
        public string TiempoEstimado { get; set; }
    }
}