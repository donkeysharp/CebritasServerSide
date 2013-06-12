using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cebritas.BusinessLogic.Entities;
using Cebritas.BusinessLogic.PlacesModule.Services;
using Cebritas.DataAccess.Repositories;
using Cebritas.Web.Areas.Api.Models;

namespace Cebritas.Web.Areas.Api.Controllers {
    public class CategoryController : RestControllerBase {
        public JsonResult GetCategories() {
            ICategoryService categoryService = CategoryService.CreateCategoryService(new CategoryRepository());
            IEnumerable<Category> categories = categoryService.List();
            List<CategoryViewModel> result = new List<CategoryViewModel>();
            CategoryViewModel viewModel;

            // Convert entities to a viewmodel structure
            // in order to have a custom JSON result
            foreach (Category category in categories) {
                viewModel = new CategoryViewModel();
                EntityToViewModel(category, viewModel);
                viewModel.SubCategories = new List<CategorySingleViewModel>();

                // Add subcategories to structure
                CategorySingleViewModel singleViewModel;
                foreach (Category subCategory in category.SubCategories) {
                    singleViewModel = new CategorySingleViewModel();
                    EntityToViewModel(subCategory, singleViewModel);

                    viewModel.SubCategories.Add(singleViewModel);
                }

                result.Add(viewModel);
            }

            return SuccessResult(result);
        }

        #region "Utils"
        private void EntityToViewModel(Category category, CategorySingleViewModel viewModel) {
            viewModel.Code = category.Code;
            viewModel.Name = category.Name;
            viewModel.SpanishName = category.SpanishName;
            viewModel.Icon = category.Icon;
        }
        #endregion "Utils"
    }
}