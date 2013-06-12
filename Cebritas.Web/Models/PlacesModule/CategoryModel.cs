using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cebritas.BusinessLogic.Entities;

namespace Cebritas.Web.Models.PlacesModule {
    public class CategoryModel {
        public long Id { get; set; }
        public string Name { get; set; }
        public string SpanishName { get; set; }
        public string Icon { get; set; }
        public long? ParentId { get; set; }

        public CategoryModel(Category category) {
            this.Id = category.Id;
            this.Name = category.Name;
            this.SpanishName = category.SpanishName;
            this.ParentId = category.ParentId;
        }

        public CategoryModel() {
        }
    }
}