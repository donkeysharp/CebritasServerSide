using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cebritas.Web.Areas.Api.Models {
    public class PrecioViewModel {
        public string FourSquareFirstCategoryId { get; set; }
        public string FourSquareVenueId { get; set; }
        public int MinPrice { get; set; }
        public int MaxPrice { get; set; }
        public int Capacity { get; set; }
        public string Parking { get; set; }
        public string Holidays { get; set; }
        public string SmokingArea { get; set; }
        public string KidsArea { get; set; }
        public string Delivery { get; set; }
    }
}