using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Cebritas.BusinessModel {
    public class Report {
        public long Id { get; set; }
        public string FacebookCode { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int Type { get; set; }

        public string Description { get; set; }
        public DateTime ReportedAt { get; set; }
        public DateTime ReportedDate { get; set; }

        public long ProblemId { get; set; }
        [ForeignKey("ProblemId")]
        public virtual Problem Problem { get; set; }
    }
}