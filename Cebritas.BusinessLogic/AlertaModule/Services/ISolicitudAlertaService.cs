using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cebritas.BusinessLogic.Entities;

namespace Cebritas.BusinessLogic.AlertaModule.Services {
    public interface ISolicitudAlertaService {
        SolicitudAlerta Get(long id);
        SolicitudAlerta Get(string code);
        List<SolicitudAlerta> ListNear(double latitud, double longitud);
        SolicitudAlerta Insert(SolicitudAlerta solicitudAlerta);

        IEnumerable<SolicitudAlerta> GetNearAlertas(double radio, SolicitudAlerta alerta);
    }
}