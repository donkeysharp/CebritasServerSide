using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cebritas.BusinessLogic.Entities;

namespace Cebritas.BusinessLogic.PrecioModule.Services {
    public interface IPrecioService {
        IEnumerable<Precio> GetPrecios();
        IEnumerable<Precio> GetByFirstCategoryId(string firstCategoryId);
        IEnumerable<Precio> GetByVenueId(string venueId);
        IEnumerable<Precio> GetMinPriceBetween(int low, int high);
        IEnumerable<Precio> GetMaxPriceBetween(int low, int high);
    }
}