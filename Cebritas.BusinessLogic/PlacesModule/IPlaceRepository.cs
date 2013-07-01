using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cebritas.BusinessLogic.Entities;

namespace Cebritas.BusinessLogic.PlacesModule {
    public interface IPlaceRepository : IRepository<Place> {
        IEnumerable<Place> GetByParentCategory(long parentCategoryId);

        IEnumerable<Place> GetByPrice(long parentCategoryId, int minPrice, int maxPrice);

        IEnumerable<Place> GetByPriceAndQuery(int minPrice, int maxPrice, string query);
    }
}