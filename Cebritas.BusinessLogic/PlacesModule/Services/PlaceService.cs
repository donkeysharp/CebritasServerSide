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

        /// <summary>
        /// It will get all places filtered by the most parent category code
        /// </summary>
        /// <param name="code">Most parent category code</param>
        /// <returns></returns>
        public IEnumerable<Place> GetByParentCategory(long parentCategoryId, double latitude, double longitude, double? radius) {
            // Calculate the radius distance whether or not it is defined
            double nearDistanceRadius;
            if (radius.HasValue) {
                // The given radius cannot be greater than 100000 mts
                if (radius > 0 && radius <= Constants.MAX_NEAR_PLACE_DISTANCE_METERS) {
                    nearDistanceRadius = radius.Value;
                } else {
                    nearDistanceRadius = Constants.MAX_NEAR_PLACE_DISTANCE_METERS;
                }
            } else {
                nearDistanceRadius = Constants.MAX_NEAR_PLACE_DISTANCE_METERS;
            }

            IEnumerable<Place> places = db.GetByParentCategory(parentCategoryId);
            List<Place> result = new List<Place>();
            foreach (Place place in places) {
                double distance = General.Geo.GeoCodeCalc.CalcDistance(latitude, longitude, place.Latitude, place.Longitude, General.Geo.GeoCodeCalcMeasurement.Kilometers);
                distance *= 1000;
                if (distance <= nearDistanceRadius) {
                    result.Add(place);
                }
            }

            return result;
        }
        /// <summary>
        /// Get Places inside a between radius so it accepts a query
        /// with name or spanish name
        /// </summary>
        /// <param name="query"></param>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        /// <returns></returns>
        public IEnumerable<Place> GetByQuery(string query, double latitude, double longitude) {
            IEnumerable<Place> places = db.Filter(x => x.Name.Contains(query));
            List<Place> result = new List<Place>();
            foreach (Place place in places) {
                double distance = GeoCodeCalc.CalcDistance(latitude, longitude, place.Latitude, place.Longitude, GeoCodeCalcMeasurement.Kilometers);
                distance *= 1000; // Converts distance to meters
                if (distance <= Constants.MAX_NEAR_PLACE_DISTANCE_METERS) {
                    result.Add(place);
                }
            }

            return result;
        }

        #region "Common"
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
                if (distance <= Constants.MAX_NEAR_PLACE_DISTANCE_METERS) {
                    result.Add(place);
                }
            }
            return result;
        }

        #endregion "Common"
    }
}