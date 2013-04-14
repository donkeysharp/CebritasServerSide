using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cebritas.BusinessLogic.AlertaModule.Services;
using Cebritas.BusinessLogic.Entities;
using Cebritas.BusinessLogic.UserModule.Services;
using Cebritas.DataAccess.Repositories;
using Cebritas.General;
using Cebritas.General.Geo;
using Cebritas.Web.Areas.Api.Models;

namespace Cebritas.Web.Areas.Api.Controllers {
    public class SolicitudAlertaController : RestControllerBase {
        [HttpGet]
        public JsonResult GetAlertas(string latitud, string longitud) {
            double lat, longi;
            ValidateLatitudAndLongitud(latitud, longitud, out lat, out longi);
            // This method doesn't need user validation, logged off or no registered user can get alerts
            ISolicitudAlertaService service = SolicitudAlertaService.CreateSolicitudAlertaService(new SolicitudAlertaRepository());
            IAlertaService alertaService = AlertaService.CreateAlertaService(new AlertaRepository());

            IEnumerable<AlertaUrbana> alertas = alertaService.GetToday(lat, longi);
            List<SolicitudAlertaViewModel> alertaJson = new List<SolicitudAlertaViewModel>();
            SolicitudAlertaViewModel obj;
            foreach (AlertaUrbana item in alertas) {
                obj = new SolicitudAlertaViewModel();
                obj.Code = item.SolicitudAlerta.Code;
                obj.Latitud = item.SolicitudAlerta.Latitud.ToString().Replace(",", ".");
                obj.Longitud = item.SolicitudAlerta.Longitud.ToString().Replace(",", ".");
                obj.Descripcion = item.SolicitudAlerta.Descripcion;
                obj.Tipo = item.SolicitudAlerta.Tipo;
                obj.TiempoEstimado = item.SolicitudAlerta.TiempoEstimado;

                alertaJson.Add(obj);
            }
            return SuccessResult(new { Alertas = alertaJson });
        }

        private void ValidateLatitudAndLongitud(string latitud, string longitud, out double lat, out double longi) {
            if (string.IsNullOrEmpty(latitud) || string.IsNullOrEmpty(longitud) || latitud.Contains(",") || longitud.Contains(",")) {
                throw new CebraException(Messages.ALERTA_FORMATO_COORDENADAS_INCORRECTO);
            }
            try {
                CultureInfo culture = new CultureInfo("en-US");
                lat = double.Parse(latitud, culture);
                longi = double.Parse(longitud, culture);
            } catch (Exception) {
                throw new CebraException(Messages.ALERTA_FORMATO_COORDENADAS_INCORRECTO);
            }
        }

        [HttpPost]
        public JsonResult Reportar(string authenticationCode, string accessToken, SolicitudAlertaViewModel solicitudAlerta) {
            Usuario user = UserUtils.ValidateSessionThrowable(authenticationCode, accessToken);

            ISolicitudAlertaService solicitudAlertaService = SolicitudAlertaService.CreateSolicitudAlertaService(new SolicitudAlertaRepository());
            SolicitudAlerta solicitudAlertaObj = new SolicitudAlerta();
            ViewModelToEntity(solicitudAlertaObj, solicitudAlerta);
            solicitudAlertaObj.UserId = user.Id;
            solicitudAlertaObj.Activo = true;

            SolicitudAlerta alerta = solicitudAlertaService.Insert(solicitudAlertaObj);

            List<SolicitudAlerta> alertasCercanas;
            alertasCercanas = (List<SolicitudAlerta>)solicitudAlertaService.GetNearAlertas(Constants.ALERTA_METERS_REPORT_RADIUS, alerta);
            if (alertasCercanas.Count >= 2) {
                IAlertaService alertaService = AlertaService.CreateAlertaService(new AlertaRepository());
                // True if there is a confirmed alert near the not confirmed one so it will
                // no report a new one in the same place
                if (!alertaService.IsNearTo(alerta)) {
                    AlertaUrbana alertaUrbanaFinal = new AlertaUrbana();
                    alertaUrbanaFinal.SolicitudId = alerta.Id;

                    alertaService.Insert(alertaUrbanaFinal);
                }
            }

            return SuccessResult(Messages.OK);
        }

        private void ViewModelToEntity(SolicitudAlerta solicitudAlertaObj, SolicitudAlertaViewModel solicitudAlerta) {
            if (solicitudAlerta.Latitud.Contains(",") || solicitudAlerta.Longitud.Contains(",")) {
                throw new CebraException(Messages.ALERTA_FORMATO_COORDENADAS_INCORRECTO);
            }

            try {
                double latitud, longitud;
                CultureInfo culture = new CultureInfo("en-US");
                latitud = double.Parse(solicitudAlerta.Latitud, culture);
                longitud = double.Parse(solicitudAlerta.Longitud, culture);

                solicitudAlertaObj.Latitud = latitud;
                solicitudAlertaObj.Longitud = longitud;
                solicitudAlertaObj.Descripcion = solicitudAlerta.Descripcion;
                solicitudAlertaObj.Tipo = solicitudAlerta.Tipo;
                solicitudAlertaObj.TiempoEstimado = solicitudAlerta.TiempoEstimado;
            } catch (Exception) {
                throw new CebraException(Messages.ALERTA_FORMATO_COORDENADAS_INCORRECTO);
            }
        }
    }
}