using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cebritas.BusinessLogic.AlertaModule;
using Cebritas.BusinessLogic.Entities;
using Cebritas.General;
using Cebritas.General.Geo;

namespace Cebritas.DataAccess.Repositories {
    public class AlertaRepository : GenericRepository<AlertaUrbana>, IAlertaRepository {
        public IEnumerable<AlertaUrbana> GetToday() {
            DateTime today = DateTime.Today;
            var res = (from x in context.Alertas
                       where x.SolicitudAlerta.Fecha.Day == today.Day
                             && x.SolicitudAlerta.Fecha.Month == today.Month
                             && x.SolicitudAlerta.Fecha.Year == today.Year
                       select x).ToList();
            return res;
        }

        public bool IsNearTo(SolicitudAlerta alerta) {
            IEnumerable<AlertaUrbana> alertas = GetToday();

            foreach (AlertaUrbana obj in alertas) {
                double distance = GeoCodeCalc.CalcDistance(alerta.Latitud, alerta.Longitud, obj.SolicitudAlerta.Latitud, obj.SolicitudAlerta.Longitud, GeoCodeCalcMeasurement.Kilometers);
                distance = distance * 1000.0;
                if (distance <= Constants.ALERTA_METERS_REPORT_RADIUS) {
                    return true;
                }
            }
            return false;
        }

        public IEnumerable<AlertaUrbana> GetToday(double latitud, double longitud) {
            IEnumerable<AlertaUrbana> today = GetToday();
            List<AlertaUrbana> todayResult = new List<AlertaUrbana>();
            foreach (AlertaUrbana item in today) {
                double distance = GeoCodeCalc.CalcDistance(latitud, longitud, item.SolicitudAlerta.Latitud, item.SolicitudAlerta.Longitud, GeoCodeCalcMeasurement.Kilometers);
                if (distance <= Constants.ALERTA_KILOMETER_NEAR_RADIUS) {
                    todayResult.Add(item);
                }
            }
            return todayResult;
        }
    }
}