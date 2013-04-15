using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cebritas.BusinessLogic.Entities;
using Cebritas.BusinessLogic.PrecioModule.Services;
using Cebritas.DataAccess.Repositories;
using Cebritas.General;
using Cebritas.Web.Areas.Api.Models;

namespace Cebritas.Web.Areas.Api.Controllers {
    public class PrecioController : RestControllerBase {
        [HttpGet]
        public JsonResult Get() {
            IPrecioService service = PrecioService.CreatePrecioService(new PrecioRepository());

            List<PrecioViewModel> precioModelList = new List<PrecioViewModel>();
            IEnumerable<Precio> precioEntityList = service.GetPrecios();

            EntityListToModelList(precioEntityList, precioModelList);

            return SuccessResult(new { Places = precioModelList });
        }

        [HttpGet]
        public JsonResult GetByFirstCategoryId(string firstCategoryId) {
            IPrecioService service = PrecioService.CreatePrecioService(new PrecioRepository());

            List<PrecioViewModel> precioModelList = new List<PrecioViewModel>();
            IEnumerable<Precio> precioEntityList = service.GetByFirstCategoryId(firstCategoryId);

            EntityListToModelList(precioEntityList, precioModelList);

            return SuccessResult(precioModelList);
        }

        [HttpGet]
        public JsonResult GetByVenueId(string venueId) {
            IPrecioService service = PrecioService.CreatePrecioService(new PrecioRepository());

            List<PrecioViewModel> precioModelList = new List<PrecioViewModel>();
            IEnumerable<Precio> precioEntityList = service.GetByVenueId(venueId);

            EntityListToModelList(precioEntityList, precioModelList);

            return SuccessResult(precioModelList);
        }

        [HttpGet]
        public JsonResult GetByMinPriceBetween(string low, string high) {
        // Needs a better review
            try {
                int lowValue, highValue;
                lowValue = Convert.ToInt32(low);
                highValue = Convert.ToInt32(high);

                IPrecioService service = PrecioService.CreatePrecioService(new PrecioRepository());

                List<PrecioViewModel> precioModelList = new List<PrecioViewModel>();
                IEnumerable<Precio> precioEntityList = service.GetMinPriceBetween(lowValue, highValue);

                EntityListToModelList(precioEntityList, precioModelList);

                return SuccessResult(precioModelList);
            } catch (Exception) {
                throw new CebraException(Messages.PRECIOS_INVALID_PARAMETER_FORMAT);
            }
        }

        [HttpGet]
        public JsonResult GetByMaxPriceBetween(string low, string high) {
            try {
                int lowValue, highValue;
                lowValue = Convert.ToInt32(low);
                highValue = Convert.ToInt32(high);

                IPrecioService service = PrecioService.CreatePrecioService(new PrecioRepository());

                List<PrecioViewModel> precioModelList = new List<PrecioViewModel>();
                IEnumerable<Precio> precioEntityList = service.GetMaxPriceBetween(lowValue, highValue);

                EntityListToModelList(precioEntityList, precioModelList);

                return SuccessResult(precioModelList);
            } catch (Exception) {
                throw new CebraException(Messages.PRECIOS_INVALID_PARAMETER_FORMAT);
            }
        }

        private void EntityListToModelList(IEnumerable<Precio> entityList, List<PrecioViewModel> modelList) {
            PrecioViewModel obj;
            foreach (Precio item in entityList) {
                obj = new PrecioViewModel();
                obj.FourSquareFirstCategoryId = item.FourSquareFirstCategoryId;
                obj.FourSquareVenueId = item.FourSquareVenueId;
                obj.MinPrice = item.MinPrice;
                obj.MaxPrice = item.MaxPrice;
                obj.Capacity = item.Capacity;
                obj.Parking = item.Parking.ToString();
                obj.Holidays = item.Holidays.ToString();
                obj.SmokingArea = item.SmokingArea.ToString();
                obj.KidsArea = item.KidsArea.ToString();
                obj.Delivery = item.Delivery.ToString();

                modelList.Add(obj);
            }
        }
    }
}