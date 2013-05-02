using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cebritas.BusinessLogic.Entities;
using Cebritas.BusinessLogic.PlacesModule.Services;
using Cebritas.DataAccess.Repositories;

namespace Cebritas.Web.Areas.Api.Controllers {
    public class CategoryController : RestControllerBase {
        public JsonResult GetCategories() {
            ICategoryService categoryService = CategoryService.CreateCategoryService(new CategoryRepository());

            return null;
        }
    }
}