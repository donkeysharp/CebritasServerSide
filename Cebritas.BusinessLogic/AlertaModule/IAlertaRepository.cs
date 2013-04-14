using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cebritas.BusinessLogic.Entities;

namespace Cebritas.BusinessLogic.AlertaModule {
    public interface IAlertaRepository : IRepository<AlertaUrbana> {
        IEnumerable<AlertaUrbana> GetToday();
        IEnumerable<AlertaUrbana> GetToday(double latitud, double longitud);
        bool IsNearTo(SolicitudAlerta alerta);
    }
}