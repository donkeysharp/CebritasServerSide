using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cebritas.BusinessLogic.Entities;

namespace Cebritas.BusinessLogic.PlacesModule.Services {
    public interface ICategoryService {
        Category Get(long id);
        Category Get(string code);
        IEnumerable<Category> List();
        IEnumerable<Category> GetParentCategories();
        IEnumerable<Category> GetParentCategories(long excludeCategoryId);
        IEnumerable<Category> GetByName(string name, string spanishName);
        Category Insert(Category category);
        int Update(Category category);
        int Delete(long id);
    }
}