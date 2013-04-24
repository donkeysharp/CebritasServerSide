using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cebritas.BusinessLogic.Entities;
using Cebritas.General;

namespace Cebritas.BusinessLogic.PlacesModule.Services {
    public class CategoryService : ICategoryService {
        private ICategoryRepository db;

        public static CategoryService CreateCategoryService(ICategoryRepository db) {
            return new CategoryService(db);
        }

        private CategoryService(ICategoryRepository db) {
            this.db = db;
        }

        public Category Get(long id) {
            return db.Get(id);
        }

        public Category Get(string code) {
            var result = db.Filter(x => x.Code.Equals(code)).ToList();
            if (result != null && result.Count > 0) {
                return result[0];
            }
            return null;
        }

        public IEnumerable<Category> GetByName(string name, string spanishName) {
            var result = db.Filter(x => x.Name.Equals(name) || x.SpanishName.Equals(spanishName)).ToList();
            return result;
        }

        public IEnumerable<Category> Filter(System.Linq.Expressions.Expression<Func<Entities.Category, bool>> filter = null, Func<IQueryable<Entities.Category>, IOrderedQueryable<Entities.Category>> orderBy = null) {
            return db.Filter(filter, orderBy);
        }

        public Category Insert(Category category) {
            ValidateInsertUpdate(category);

            category.Code = General.Cryptography.SecurityTokenGenerator.GenerateGuid();
            return db.Insert(category);
        }

        public int Update(Category category) {
            ValidateInsertUpdate(category);

            return db.Update(category);
        }

        public int Delete(long id) {
            Category category = new Category() { Id = id };
            return db.Delete(category);
        }

        private void ValidateInsertUpdate(Category category) {
            if (string.IsNullOrEmpty(category.Name) || string.IsNullOrEmpty(category.SpanishName)) {
                throw new CebraException(Messages.CATEGORY_NAMES_ARE_EMPTY);
            }

            List<Category> categoryList = (List<Category>)GetByName(category.Name, category.SpanishName);
            if (categoryList != null && categoryList.Count > 0) {
                if (categoryList[0].Id != category.Id) {
                    throw new CebraException(Messages.CATEGORY_ALREADY_EXISTS);
                }
            }
        }
    }
}