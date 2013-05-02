using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cebritas.Web.Models.PlacesModule {
    public class PlaceModel {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string WebSite { get; set; }
        public string MinPrice { get; set; }
        public string MaxPrice { get; set; }
        public bool Parking { get; set; }
        public bool Holidays { get; set; }
        public bool SmokingArea { get; set; }
        public bool KidsArea { get; set; }
        public bool Delivery { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public long CategoryId { get; set; }
    }
}