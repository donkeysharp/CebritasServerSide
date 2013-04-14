using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cebritas.BusinessLogic.Entities {
    public class Precio {
        public long Id { get; set; }
        public string FourSquareFirstCategoryId { get; set; }
        public string FourSquareVenueId { get; set; }
        public int MinPrice { get; set; }
        public int MaxPrice { get; set; }
        public int Capacity { get; set; }
        public bool Parking { get; set; }
        public bool Holidays { get; set; }
        public bool SmokingArea { get; set; }
        public bool KidsArea { get; set; }
        public bool Delivery { get; set; }
    }
}