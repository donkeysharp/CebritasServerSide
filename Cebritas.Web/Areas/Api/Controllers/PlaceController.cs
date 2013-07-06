using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cebritas.BusinessLogic.Entities;
using Cebritas.BusinessLogic.PlacesModule.Services;
using Cebritas.DataAccess.Repositories;
using Cebritas.General;
using Cebritas.Web.Areas.Api.Models;

namespace Cebritas.Web.Areas.Api.Controllers {
    public class PlaceController : RestControllerBase {
        /// <summary>
        /// Update a place's rating
        /// </summary>
        /// <param name="code">Place code</param>
        /// <param name="rating">Selected rating</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult RateProblem(string code, int? rating) {
            IPlaceService placeService = PlaceService.CreatePlaceService(new PlaceRepository());
            if (string.IsNullOrEmpty(code)) {
                throw new CebraException(Constants.HTTP_BAD_REQUEST, string.Format(Messages.ERROR_PARAM_REQUIRED, "code"));
            }
            if (!rating.HasValue) {
                throw new CebraException(Constants.HTTP_BAD_REQUEST, string.Format(Messages.ERROR_PARAM_REQUIRED, "rating"));
            }
            if (rating.Value <= 0) {
                rating = 1;
            }
            if (rating.Value > 5) {
                rating = 5;
            }

            Place place = placeService.Get(code);
            if (place == null) {
                throw new CebraException(Constants.HTTP_BAD_REQUEST, Messages.ERROR_PLACE_NOT_FOUND);
            }
            place.Rating = place.Rating + ", " + rating.Value;
            placeService.Update(place);

            return SuccessResult(null, Messages.OK);
        }
        /// <summary>
        /// Get all categories based in the longest parent category code
        /// this categories are in a 15 kilometers radius. In the future
        /// according to the lat and lng sended it should filter by region so
        /// it can reduce query cost (google maps services -geocoding)
        /// </summary>
        /// <param name="code">Root category code</param>
        /// <param name="latitude">User's position latitude</param>
        /// <param name="longitude">User's position longitude</param>
        /// <param name="nearDistanceRadius">If it is defined is the radius it will use</param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetPlacesByCategory(string code, double? latitude, double? longitude, double? radius) {
            ValidateGetPlacesByCategory(latitude, longitude);
            IPlaceService placeService = PlaceService.CreatePlaceService(new PlaceRepository());
            ICategoryService categoryService = CategoryService.CreateCategoryService(new CategoryRepository());

            Category parentCategory = categoryService.Get(code);
            IEnumerable<Place> places = placeService.GetByParentCategory(parentCategory.Id, latitude.Value, longitude.Value, radius);

            List<PlaceViewModel> result = new List<PlaceViewModel>();
            PlaceViewModel item;
            foreach (Place place in places) {
                item = new PlaceViewModel();
                EntityToViewModel(place, item);

                result.Add(item);
            }

            return SuccessResult(result, Messages.OK);
        }

        /// <summary>
        /// Get places that have a similar name as the query parameter
        /// based in user's position
        /// </summary>
        /// <param name="query"></param>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetPlacesByQuery(string query, double? latitude, double? longitude) {
            ValidateParamsGetPlacesByQuery(query, latitude, longitude);
            IPlaceService placeService = PlaceService.CreatePlaceService(new PlaceRepository());
            IEnumerable<Place> places = placeService.GetByQuery(query, latitude.Value, longitude.Value);

            List<PlaceViewModel> result = new List<PlaceViewModel>();
            PlaceViewModel item;
            foreach (Place place in places) {
                item = new PlaceViewModel();
                EntityToViewModel(place, item);

                result.Add(item);
            }
            return SuccessResult(result, Messages.OK);
        }
        /// <summary>
        /// Get all places that belong to a root category and its price
        /// is between range of given min and max prices
        /// </summary>
        /// <param name="code"></param>
        /// <param name="minPrice"></param>
        /// <param name="maxPrice"></param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetPlacesByPrice(string code, double? latitude, double? longitude, int? minPrice, int? maxPrice) {
            Action<string, double?, double?, int?, int?> validate = (_code, _lat, _long, _minPrice, _maxPrice) => {
                if (string.IsNullOrEmpty(_code)) {
                    throw new CebraException(Constants.HTTP_BAD_REQUEST, string.Format(Messages.ERROR_PARAM_REQUIRED, "code"));
                }
                if (!_lat.HasValue || !_long.HasValue) {
                    throw new CebraException(Constants.HTTP_BAD_REQUEST, Messages.FORMATO_COORDENADAS_INCORRECTO);
                }
                if (!_minPrice.HasValue || !_maxPrice.HasValue) {
                    throw new CebraException(Constants.HTTP_BAD_REQUEST, Messages.ERROR_WALLET_PRICES);
                }
            };
            validate(code, latitude, longitude, minPrice, maxPrice);

            IPlaceService placeService = PlaceService.CreatePlaceService(new PlaceRepository());
            ICategoryService categoryService = CategoryService.CreateCategoryService(new CategoryRepository());

            Category category = categoryService.Get(code);
            IEnumerable<Place> places = placeService.GetByPrices(category.Id, latitude.Value, longitude.Value, minPrice.Value, maxPrice.Value);

            List<PlaceViewModel> result = new List<PlaceViewModel>();
            PlaceViewModel item;
            foreach (Place place in places) {
                item = new PlaceViewModel();
                EntityToViewModel(place, item);

                result.Add(item);
            }
            return SuccessResult(result, Messages.OK);
        }
        /// <summary>
        /// Get places whose name is like query and its price
        /// is between the range of the given parameters
        /// </summary>
        /// <param name="query"></param>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        /// <param name="minPrice"></param>
        /// <param name="maxPrice"></param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetPlacesByPriceAndQuery(string query, double? latitude, double? longitude, int? minPrice, int? maxPrice) {
            Action<string, double?, double?, int?, int?> validate = (_query, _lat, _long, _minPrice, _maxPrice) => {
                if (string.IsNullOrEmpty(_query)) {
                    throw new CebraException(Constants.HTTP_BAD_REQUEST, string.Format(Messages.ERROR_PARAM_REQUIRED, "query"));
                }
                if (!_lat.HasValue || !_long.HasValue) {
                    throw new CebraException(Constants.HTTP_BAD_REQUEST, Messages.FORMATO_COORDENADAS_INCORRECTO);
                }
                if (!_minPrice.HasValue || !_maxPrice.HasValue) {
                    throw new CebraException(Constants.HTTP_BAD_REQUEST, Messages.ERROR_WALLET_PRICES);
                }
            };
            validate(query, latitude, longitude, minPrice, maxPrice);

            IPlaceService placeService = PlaceService.CreatePlaceService(new PlaceRepository());
            IEnumerable<Place> places = placeService.GetByPriceAndQuery(latitude.Value, longitude.Value, minPrice.Value, maxPrice.Value, query);

            List<PlaceViewModel> result = new List<PlaceViewModel>();
            PlaceViewModel item;
            foreach (Place place in places) {
                item = new PlaceViewModel();
                EntityToViewModel(place, item);

                result.Add(item);
            }
            return SuccessResult(result, Messages.OK);
        }

        #region "Utils"
        private void ValidateParamsGetPlacesByQuery(string query, double? latitude, double? longitude) {
            if (string.IsNullOrEmpty(query)) {
                throw new CebraException(Constants.HTTP_BAD_REQUEST, string.Format(Messages.ERROR_PARAM_REQUIRED, "query"));
            }
            ValidateGetPlacesByCategory(latitude, longitude);
        }

        private void ValidateGetPlacesByCategory(double? latitude, double? longitude) {
            if (!latitude.HasValue || !longitude.HasValue) {
                throw new CebraException(Constants.HTTP_BAD_REQUEST, Messages.FORMATO_COORDENADAS_INCORRECTO);
            }
        }

        private void GetRating(string items, out int rating, out int ratingLength) {
            string[] rates = items.Split(new char[] { ' ', ',', '-' }, StringSplitOptions.RemoveEmptyEntries);
            int temp, sum = 0;
            ratingLength = 0;
            for (int i = 0; i < rates.Length; ++i) {
                try {
                    temp = Convert.ToInt32(rates[i]);
                    sum += temp;
                    ratingLength++;
                } catch (Exception) { }
            }
            if (ratingLength != 0) {
                rating = sum / ratingLength;
            } else {
                rating = 1;
            }
        }

        private void EntityToViewModel(Place place, PlaceViewModel item) {
            int rating, ratingLength;
            GetRating(place.Rating, out rating, out ratingLength);

            item.Code = place.Code;
            item.Name = place.Name;
            item.Address = place.Address;
            item.WebSite = place.WebSite;
            item.MinPrice = place.MinPrice;
            item.MaxPrice = place.MaxPrice;
            item.Parking = place.Parking;
            item.Holidays = place.Holidays;
            item.SmokingArea = place.SmokingArea;
            item.KidsArea = place.KidsArea;
            item.Delivery = place.Delivery;
            item.Rating = rating;
            item.RatingCount = ratingLength;
            item.Latitude = place.Latitude;
            item.Longitude = place.Longitude;
            if (place.Category != null) {
                item.CategoryCode = place.Category.Code;
            } else {
                item.CategoryCode = string.Empty;
            }
        }

        #endregion "Utils"
    }
}