using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Cebritas.BusinessLogic.Entities {
    public class Place {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public double MinPrice { get; set; }
        public double MaxPrice { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public long CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }
    }
}