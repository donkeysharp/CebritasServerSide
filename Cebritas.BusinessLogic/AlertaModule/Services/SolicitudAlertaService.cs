using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Cebritas.BusinessLogic.Entities;
using Cebritas.General;
using Cebritas.General.Cryptography;
using Cebritas.General.Geo;

namespace Cebritas.BusinessLogic.AlertaModule.Services {
    public class SolicitudAlertaService : ISolicitudAlertaService {
        private ISolicitudAlertaRepository db;

        public static ISolicitudAlertaService CreateSolicitudAlertaService(ISolicitudAlertaRepository db) {
            return new SolicitudAlertaService(db);
        }

        private SolicitudAlertaService(ISolicitudAlertaRepository db) {
            this.db = db;
        }

        public SolicitudAlerta Get(long id) {
            throw new NotImplementedException();
        }

        public SolicitudAlerta Get(string code) {
            throw new NotImplementedException();
        }

        public List<SolicitudAlerta> ListNear(double latitud, double longitud) {
            throw new NotImplementedException();
        }

        public SolicitudAlerta Insert(SolicitudAlerta solicitudAlerta) {
            ValidateInsert(solicitudAlerta);

            solicitudAlerta.Code = SecurityTokenGenerator.GenerateGuid();
            solicitudAlerta.Fecha = DateTime.Now;

            return db.Insert(solicitudAlerta);
        }

        private void ValidateInsert(SolicitudAlerta solcitudAlerta) {
            if (!solcitudAlerta.Tipo.ToLower().Equals(Constants.ALERTA_TIPO_BLOQUEO)
                && !solcitudAlerta.Tipo.ToLower().Equals(Constants.ALERTA_TIPO_DESFILE)
                && !solcitudAlerta.Tipo.ToLower().Equals(Constants.ALERTA_TIPO_MANIFESTACION)
                && !solcitudAlerta.Tipo.ToLower().Equals(Constants.ALERTA_TIPO_OTRO)
                && !solcitudAlerta.Tipo.ToLower().Equals(Constants.ALERTA_TIPO_TRAFICO)) {
                throw new CebraException(Messages.ALERTA_TIPO_INVALIDO);
            }

            if (!solcitudAlerta.TiempoEstimado.ToLower().Equals(Constants.ALERTA_TIEMPO_15M)
                && !solcitudAlerta.TiempoEstimado.ToLower().Equals(Constants.ALERTA_TIEMPO_30M)
                && !solcitudAlerta.TiempoEstimado.ToLower().Equals(Constants.ALERTA_TIEMPO_1H)
                && !solcitudAlerta.TiempoEstimado.ToLower().Equals(Constants.ALERTA_TIEMPO_2H)
                && !solcitudAlerta.TiempoEstimado.ToLower().Equals(Constants.ALERTA_TIEMPO_3HH)) {
                throw new CebraException(Messages.ALERTA_TIEMPO_ESTIMADO_INVALIDO);
            }
        }

        public IEnumerable<SolicitudAlerta> GetNearAlertas(double radio, SolicitudAlerta alerta) {
            return db.GetNearAlertas(radio, alerta);
        }
    }
}