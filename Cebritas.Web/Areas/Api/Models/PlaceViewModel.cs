using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cebritas.Web.Areas.Api.Models {
    public class PlaceViewModel {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string WebSite { get; set; }
        public double MinPrice { get; set; }
        public double MaxPrice { get; set; }
        public bool Parking { get; set; }
        public bool Holidays { get; set; }
        public bool SmokingArea { get; set; }
        public bool KidsArea { get; set; }
        public bool Delivery { get; set; }
        public double Rating { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string CategoryCode { get; set; }
    }
}