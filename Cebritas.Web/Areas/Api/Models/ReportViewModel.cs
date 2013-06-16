using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cebritas.Web.Areas.Api.Models {
    public class ReportViewModel {
        public string FacebookCode { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int Type { get; set; }
        public string Description { get; set; }
        public long ReportedAt { get; set; }
    }
}