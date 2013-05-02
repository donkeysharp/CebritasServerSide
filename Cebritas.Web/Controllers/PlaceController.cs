using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cebritas.BusinessLogic.Entities;
using Cebritas.BusinessLogic.PlacesModule.Services;
using Cebritas.DataAccess.Repositories;
using Cebritas.General;
using Cebritas.Web.Models.PlacesModule;

namespace Cebritas.Web.Controllers {
    public class PlaceController : CebraControllerBase {
        public ActionResult List() {
            IPlaceService placeService = PlaceService.CreatePlaceService(new PlaceRepository());
            List<Place> placeList = (List<Place>)placeService.List();

            return View("ListForm", placeList);
        }

        public ActionResult Add() {
            ICategoryService categoryService = CategoryService.CreateCategoryService(new CategoryRepository());
            IEnumerable<Category> categoryList = categoryService.List();
            ViewBag.Categories = new SelectList(categoryList, "Id", "Name");

            return View("EditForm", new PlaceModel());
        }

        [HttpPost]
        public ActionResult Save(PlaceModel placeModel) {
            IPlaceService placeService = PlaceService.CreatePlaceService(new PlaceRepository());
            ICategoryService categoryService = CategoryService.CreateCategoryService(new CategoryRepository());
            Place place;
            if (placeModel.Id == 0) {
                place = new Place();
                ModelToEntity(placeModel, place);

                place = placeService.Insert(place);
            } else {
                place = placeService.Get(placeModel.Id);
                ModelToEntity(placeModel, place);

                placeService.Update(place);
            }

            IEnumerable<Category> categoryList = categoryService.List();
            ViewBag.Categories = new SelectList(categoryList, "Id", "Name");

            return RedirectToAction("edit", new { Id = place.Id });
        }

        public ActionResult Edit(long? id) {
            if (!id.HasValue) {
                throw new CebraException("An Id parameter must be specified");
            }
            IPlaceService placeService = PlaceService.CreatePlaceService(new PlaceRepository());
            ICategoryService categoryService = CategoryService.CreateCategoryService(new CategoryRepository());

            Place place = placeService.Get(id.Value);
            PlaceModel placeModel;
            if (place != null) {
                placeModel = new PlaceModel();
                EntityToModel(place, placeModel);
            } else {
                throw new CebraException("Place doesn't exist");
            }

            IEnumerable<Category> categoryList = categoryService.List();
            ViewBag.Categories = new SelectList(categoryList, "Id", "Name");

            return View("EditForm", placeModel);
        }

        public ActionResult Delete(long? id) {
            if (!id.HasValue) {
                throw new CebraException("An Id parameter must be specified");
            }
            IPlaceService placeService = PlaceService.CreatePlaceService(new PlaceRepository());
            placeService.Delete(id.Value);

            return RedirectToAction("List", "Place");
        }

        private void EntityToModel(Place place, PlaceModel model) {
            try {
                CultureInfo culture = new CultureInfo("en-US");
                model.Id = place.Id;
                model.Name = place.Name;
                model.Address = place.Address;
                model.WebSite = place.WebSite;
                model.MinPrice = place.MinPrice.ToString(culture);
                model.MaxPrice = place.MaxPrice.ToString(culture);
                model.Latitude = place.Latitude.ToString(culture);
                model.Longitude = place.Longitude.ToString(culture);
                model.Parking = place.Parking;
                model.Holidays = place.Holidays;
                model.SmokingArea = place.SmokingArea;
                model.KidsArea = place.KidsArea;
                model.Delivery = place.Delivery;
                model.CategoryId = place.CategoryId;
            } catch (Exception ex) {
                throw new CebraException(ex.Message);
            }
        }

        private void ModelToEntity(PlaceModel model, Place place) {
            try {
                CultureInfo culture = new CultureInfo("en-US");
                place.Name = model.Name;
                place.Address = model.Address;
                place.WebSite = model.WebSite;
                place.MinPrice = double.Parse(model.MinPrice, culture);
                place.MaxPrice = double.Parse(model.MaxPrice, culture);
                place.Latitude = double.Parse(model.Latitude, culture);
                place.Longitude = double.Parse(model.Longitude, culture);
                place.Parking = model.Parking;
                place.Holidays = model.Holidays;
                place.SmokingArea = model.SmokingArea;
                place.KidsArea = model.KidsArea;
                place.Delivery = model.Delivery;
                place.CategoryId = model.CategoryId;
            } catch (Exception ex) {
                throw new CebraException(ex.Message);
            }
        }
    }
}