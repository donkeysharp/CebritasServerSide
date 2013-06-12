using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cebritas.Web.Areas.Api.Models {
    public class CategorySingleViewModel {
        public string Code { get; set; }
        public string Name { get; set; }
        public string SpanishName { get; set; }
        public string ParentCode { get; set; }
        public string Icon { get; set; }
    }
    public class CategoryViewModel : CategorySingleViewModel {
        public List<CategorySingleViewModel> SubCategories { get; set; }
    }
}