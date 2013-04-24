using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Cebritas.BusinessLogic.Entities {
    public class Place {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public double MinPrice { get; set; }
        public double MaxPrice { get; set; }
        public int Capacity { get; set; }
        public bool Parking { get; set; }
        public bool Holidays { get; set; }
        public bool SmokingArea { get; set; }
        public bool KidsArea { get; set; }
        public bool Delivery { get; set; }
        public double Rating { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public long CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }
    }
}