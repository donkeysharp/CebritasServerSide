using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cebritas.BusinessLogic.Entities;
using Cebritas.BusinessLogic.PlacesModule;

namespace Cebritas.DataAccess.Repositories {
    public class PlaceRepository : GenericRepository<Place>, IPlaceRepository {
        public IEnumerable<Place> GetByParentCategory(long parentCategoryId) {
            /* Get all son categories from parent and only
             * get ids within an array of numbers e.g. [2,3,4] */
            var leafCategories = (from category in context.Categories
                                  where category.ParentId == parentCategoryId
                                  select category.Id).ToArray();
            IEnumerable<Place> result;
            if (leafCategories.Length > 0) {
                /* It will get those places whos id is within the
                 * leafCategories array */
                result = (from place in context.Places
                          where leafCategories.Contains(place.CategoryId)
                          select place).ToList();
            } else {
                result = (from place in context.Places
                          where place.CategoryId == parentCategoryId
                          select place).ToList();
            }

            return result;
        }
    }
}