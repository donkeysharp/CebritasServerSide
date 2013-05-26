using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cebritas.Web.Areas.Api.Models {
    public class ProblemViewModel {
        public string Code { get; set; }
        public string FacebookCode { get; set; }
        public int Importance { get; set; }
        public bool Verified { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public int Type { get; set; }
        public string Description { get; set; }
        public long ReportedAt { get; set; }
    }
}