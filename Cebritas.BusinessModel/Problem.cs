using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cebritas.BusinessModel {
    public class Problem {
        public long Id { get; set; }
        /// <summary>
        /// It will be unique code, generated for API
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// Problem latitude
        /// </summary>
        public double Latitude { get; set; }
        /// <summary>
        /// Problem longitude
        /// </summary>
        public double Longitude { get; set; }
        /// <summary>
        /// As the application can be controlled by police
        /// or media this indicates wheter or not
        /// a report is 100% true
        /// </summary>
        public bool Verified { get; set; }
        /// <summary>
        /// Date and time when the problem has been reported
        /// </summary>
        public DateTime ReportedAt { get; set; }
        /// <summary>
        /// Only date when problem has bee reported
        /// </summary>
        public DateTime ReportedDate { get; set; }

        public virtual ICollection<Report> Reports { get; set; }
    }
}