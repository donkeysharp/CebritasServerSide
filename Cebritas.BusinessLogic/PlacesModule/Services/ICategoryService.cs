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
        IEnumerable<Category> GetByName(string name, string spanishName);
        IEnumerable<Category> Filter(Expression<Func<Category, bool>> filter = null,
            Func<IQueryable<Category>, IOrderedQueryable<Category>> orderBy = null);
        Category Insert(Category category);
        int Update(Category category);
        int Delete(long id);
    }
}