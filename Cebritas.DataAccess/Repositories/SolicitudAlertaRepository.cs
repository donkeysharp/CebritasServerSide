using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cebritas.BusinessLogic.AlertaModule;
using Cebritas.BusinessLogic.Entities;
using Cebritas.General.Geo;

namespace Cebritas.DataAccess.Repositories {
    public class SolicitudAlertaRepository : GenericRepository<SolicitudAlerta>, ISolicitudAlertaRepository {
        /// <summary>
        /// This brings all SolicitudAlerta that are in a radio,
        /// it excludes SolicitudAlerta with the same Id as the alerta
        /// in parameter.
        /// </summary>
        /// <param name="radio"></param>
        /// <param name="alerta"></param>
        /// <returns></returns>
        public IEnumerable<SolicitudAlerta> GetNearAlertas(double radio, SolicitudAlerta alerta) {
            DateTime today = DateTime.Today;
            var temporal = (from x in context.Solicitudes
                            where x.Fecha.Day == today.Day
                                  && x.Fecha.Month == today.Month
                                  && x.Fecha.Year == today.Year
                                  && x.Id != alerta.Id
                            select x).ToList();

            List<SolicitudAlerta> result = new List<SolicitudAlerta>();
            DateTime low, high;
            foreach (SolicitudAlerta obj in temporal) {
                if (obj.Fecha < alerta.Fecha) {
                    low = obj.Fecha;
                    high = alerta.Fecha;
                } else {
                    low = alerta.Fecha;
                    high = obj.Fecha;
                }

                TimeSpan diff = high - low;
                if (diff.Hours == 0) {
                    if (diff.Minutes <= 30) {
                        double distance = GeoCodeCalc.CalcDistance(alerta.Latitud,
                                                                   alerta.Longitud,
                                                                   obj.Latitud,
                                                                   obj.Longitud,
                                                                   GeoCodeCalcMeasurement.Kilometers);
                        distance = distance * 1000.0;
                        if (distance <= radio) {
                            if (obj.UserId != alerta.UserId) {
                                result.Add(obj);
                            }
                        }
                    }
                }
            }

            return result;
        }
    }
}