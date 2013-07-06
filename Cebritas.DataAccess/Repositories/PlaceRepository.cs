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

        /// <summary>
        /// Get all places that belong to a root category
        /// and has its prices are in a given range
        /// </summary>
        /// <param name="code"></param>
        /// <param name="minPrice"></param>
        /// <param name="maxPrice"></param>
        /// <returns></returns>
        public IEnumerable<Place> GetByPrice(long parentCategoryId, int minPrice, int maxPrice) {
            var leafCategories = (from category in context.Categories
                                  where category.ParentId == parentCategoryId
                                  select category.Id).ToArray();

            IEnumerable<Place> result;
            if (leafCategories.Length > 0) {
                /* It will get those places whos id is within the
                 * leafCategories array */
                result = (from place in context.Places
                          where leafCategories.Contains(place.CategoryId)
                                && ((minPrice <= place.MinPrice && place.MinPrice <= maxPrice)|| (minPrice <= place.MaxPrice && place.MaxPrice <= maxPrice))

                          select place).ToList();
            } else {
                result = (from place in context.Places
                          where place.CategoryId == parentCategoryId
                                && ((minPrice <= place.MinPrice && place.MinPrice <= maxPrice) || (minPrice <= place.MaxPrice && place.MaxPrice <= maxPrice))
                          select place).ToList();
            }

            return result;
        }
        public IEnumerable<Place> GetByPriceAndQuery(int minPrice, int maxPrice, string query) {
            IEnumerable<Place> result = (from place in context.Places
                                         where ((minPrice <= place.MinPrice && place.MinPrice <= maxPrice) || (minPrice <= place.MaxPrice && place.MaxPrice <= maxPrice))
                                               && place.Name.Contains(query)
                                         select place).ToList();
            return result;
        }
    }
}