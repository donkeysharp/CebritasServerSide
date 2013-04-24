using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cebritas.BusinessLogic.Entities;

namespace Cebritas.BusinessLogic.PlacesModule.Services {
    public class PlaceService : IPlaceService {
        private IPlaceRepository db;

        public static PlaceService CreatePlaceService(IPlaceRepository db) {
            return new PlaceService(db);
        }

        private PlaceService(IPlaceRepository db) {
            this.db = db;
        }

        public Place Get(long id) {
            return db.Get(id);
        }

        public Place Get(string code) {
            IList<Place> result = db.Filter(x => x.Code.Equals(code)).ToList();
            if (result != null && result.Count > 0) {
                return result[0];
            }
            return null;
        }

        public IEnumerable<Place> Filter(System.Linq.Expressions.Expression<Func<Place, bool>> filter = null, Func<IQueryable<Place>, IOrderedQueryable<Place>> orderBy = null) {
            return db.Filter(filter, orderBy);
        }

        public Place Insert(Place place) {
            place.Code = General.Cryptography.SecurityTokenGenerator.GenerateGuid();

            return db.Insert(place);
        }

        public int Update(Place place) {
            return db.Update(place);
        }

        public int Delete(long id) {
            Place place = new Place() { Id = id };
            return db.Delete(place);
        }
    }
}