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
        /// Get all categories based in the longes parent category code
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
        public void EntityToViewModel(Place place, PlaceViewModel item) {
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
            item.Rating = place.Rating;
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