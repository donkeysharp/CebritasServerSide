using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cebritas.BusinessLogic.AlertaModule.Services {
    public class AlertaService : IAlertaService {
        private IAlertaRepository db;

        public static IAlertaService CreateAlertaService(IAlertaRepository db) {
            return new AlertaService(db);
        }

        private AlertaService(IAlertaRepository db) {
            this.db = db;
        }
        public bool IsNearTo(Entities.SolicitudAlerta alerta) {
            return db.IsNearTo(alerta);
        }

        public IEnumerable<Entities.AlertaUrbana> GetToday() {
            return db.GetToday();
        }

        public Entities.AlertaUrbana Insert(Entities.AlertaUrbana alerta) {
            return db.Insert(alerta);
        }

        public IEnumerable<Entities.AlertaUrbana> GetToday(double latitud, double longitud) {
            return db.GetToday(latitud, longitud);
        }
    }
}