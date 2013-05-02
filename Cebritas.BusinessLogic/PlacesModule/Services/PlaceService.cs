using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cebritas.BusinessLogic.Entities;
using Cebritas.General;
using Cebritas.General.Geo;

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

        public IEnumerable<Place> List() {
            return db.Filter(null, x => x.OrderBy(y => y.Category.Name));
        }

        public Place Insert(Place place) {
            place.Code = General.Cryptography.SecurityTokenGenerator.GenerateGuid();

            return db.Insert(place);
        }

        public int Update(Place place) {
            return db.Update(place);
        }

        public int Delete(long id) {
            Place place = Get(id);
            return db.Delete(place);
        }

        public IEnumerable<Place> GetByCategoryId(long categoryId) {
            return db.Filter(x => x.CategoryId == categoryId);
        }

        public IEnumerable<Place> GetByCategoryCode(string categoryCode) {
            return db.Filter(x => x.Code.Equals(categoryCode));
        }

        public IEnumerable<Place> GetByCategoryIdNear(long categoryId, double latitude, double longitude) {
            return FilterNearPlaces(GetByCategoryId(categoryId), latitude, longitude);
        }

        public IEnumerable<Place> GetByCategoryCodeNear(string categoryCode, double latitude, double longitude) {
            return FilterNearPlaces(GetByCategoryCode(categoryCode), latitude, longitude);
        }

        private List<Place> FilterNearPlaces(IEnumerable<Place> placeList, double latitude, double longitude) {
            List<Place> result = new List<Place>();
            foreach (Place place in placeList) {
                double distance = GeoCodeCalc.CalcDistance(place.Latitude, place.Longitude, latitude, longitude, GeoCodeCalcMeasurement.Kilometers);
                if (distance <= Constants.MAX_NEAR_PLACE_DISTANCE) {
                    result.Add(place);
                }
            }
            return result;
        }
    }
}