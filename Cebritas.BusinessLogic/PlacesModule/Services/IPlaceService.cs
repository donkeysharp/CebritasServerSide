using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cebritas.BusinessLogic.Entities;

namespace Cebritas.BusinessLogic.PlacesModule.Services {
    public interface IPlaceService {
        Place Get(long id);
        Place Get(string code);
        IEnumerable<Place> Filter(Expression<Func<Place, bool>> filter = null,
            Func<IQueryable<Place>, IOrderedQueryable<Place>> orderBy = null);
        Place Insert(Place place);
        int Update(Place place);
        int Delete(long id);
    }
}