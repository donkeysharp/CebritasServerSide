using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cebritas.BusinessLogic.Entities;
using Cebritas.BusinessLogic.PlacesModule.Services;
using Cebritas.DataAccess.Repositories;
using Cebritas.General;
using Cebritas.Web.Models.CategoryModule;

namespace Cebritas.Web.Controllers {
    public class CategoryController : CebraControllerBase {
        public ActionResult List() {
            ICategoryService categoryService = CategoryService.CreateCategoryService(new CategoryRepository());
            List<Category> categoryList = (List<Category>)categoryService.Filter();

            return View("ListForm", categoryList);
        }

        public ActionResult Add() {
            ICategoryService categoryService = CategoryService.CreateCategoryService(new CategoryRepository());
            IEnumerable<Category> parents = categoryService.Filter(x => x.ParentId == null);
            ViewBag.Parents = new SelectList(parents, "Id", "Name");

            return View("EditForm", new CategoryModel());
        }

        [HttpPost]
        public ActionResult Save(CategoryModel categoryModel) {
            ICategoryService categoryService = CategoryService.CreateCategoryService(new CategoryRepository());
            Category category;
            if (categoryModel.Id == 0) {
                category = new Category();
                category.Name = categoryModel.Name;
                category.SpanishName = categoryModel.SpanishName;
                category.ParentId = categoryModel.ParentId;
                category = categoryService.Insert(category);
            } else {
                category = categoryService.Get(categoryModel.Id);
                category.Name = categoryModel.Name;
                category.SpanishName = categoryModel.SpanishName;
                category.ParentId = categoryModel.ParentId;

                categoryService.Update(category);
            }
            // Sets the request attribute to populate parent categories
            IEnumerable<Category> parents = categoryService.Filter(x => x.ParentId == null);
            ViewBag.Parents = new SelectList(parents, "ParentId", "Name", category.ParentId);

            return RedirectToAction("edit", new { Id = category.Id });
        }

        public ActionResult Edit(long? id) {
            if (!id.HasValue) {
                throw new CebraException("An Id parameter must be specified");
            }
            ICategoryService categoryService = CategoryService.CreateCategoryService(new CategoryRepository());
            Category category = categoryService.Get(id.Value);
            CategoryModel model;
            if (category != null) {
                 model = new CategoryModel(category);
            } else {
                throw new CebraException("Category doesn't exist");
            }

            // Sets the request attribute to populate parent categories
            IEnumerable<Category> parents = categoryService.Filter(x => x.ParentId == null && x.Id != category.Id);
            ViewBag.Parents = new SelectList(parents, "Id", "Name", category.ParentId);

            return View("EditForm", model);
        }

        public ActionResult Delete(long? id) {
            if (!id.HasValue) {
                throw new CebraException("An Id parameter must be specified");
            }
            ICategoryService categoryService = CategoryService.CreateCategoryService(new CategoryRepository());
            categoryService.Delete(id.Value);

            return RedirectToAction("List", "Category");
        }

        private void EntityToModel(Category category, CategoryModel categoryModel) {
            categoryModel.Id = category.Id;
            categoryModel.Name = category.Name;
            categoryModel.SpanishName = category.SpanishName;
            categoryModel.ParentId = category.ParentId;
        }
    }
}