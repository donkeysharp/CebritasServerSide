using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cebritas.BusinessLogic.Entities;

namespace Cebritas.BusinessLogic.AlertaModule {
    public interface ISolicitudAlertaRepository : IRepository<SolicitudAlerta> {
        IEnumerable<SolicitudAlerta> GetNearAlertas(double radio, SolicitudAlerta alerta);
    }
}