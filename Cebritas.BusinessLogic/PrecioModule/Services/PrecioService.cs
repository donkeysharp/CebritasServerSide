using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cebritas.BusinessLogic.PrecioModule.Services {
    public class PrecioService : IPrecioService {
        private IPrecioRepository db;

        public static PrecioService CreatePrecioService(IPrecioRepository db) {
            return new PrecioService(db);
        }

        private PrecioService(IPrecioRepository db) {
            this.db = db;
        }
        public IEnumerable<Entities.Precio> GetPrecios() {
            return db.Filter();
        }

        public IEnumerable<Entities.Precio> GetByVenueId(string venueId) {
            return db.Filter(x => x.FourSquareVenueId.Equals(venueId));
        }

        public IEnumerable<Entities.Precio> GetByFirstCategoryId(string firstCategoryId) {
            return db.Filter(x => x.FourSquareFirstCategoryId.Equals(firstCategoryId));
        }

        public IEnumerable<Entities.Precio> GetMinPriceBetween(int low, int high) {
            return db.Filter(x => low <= x.MinPrice && x.MinPrice <= high);
        }

        public IEnumerable<Entities.Precio> GetMaxPriceBetween(int low, int high) {
            return db.Filter(x => low <= x.MaxPrice && x.MaxPrice <= high);
        }
    }
}