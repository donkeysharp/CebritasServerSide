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
        IEnumerable<Place> List();
        IEnumerable<Place> GetByCategoryId(long categoryId);
        IEnumerable<Place> GetByCategoryCode(string categoryCode);
        IEnumerable<Place> GetByCategoryIdNear(long categoryId, double latitude, double longitude);
        IEnumerable<Place> GetByCategoryCodeNear(string categoryCode, double latitud, double longitude);
        Place Insert(Place place);
        int Update(Place place);
        int Delete(long id);
    }
}