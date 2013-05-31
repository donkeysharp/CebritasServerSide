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
        /// User will be authenticated via facebook
        /// </summary>
        public string FacebookCode { get; set; }
        /// <summary>
        /// Problem latitude
        /// </summary>
        public double Latitude { get; set; }
        /// <summary>
        /// Problem longitude
        /// </summary>
        public double Longitude { get; set; }
        /// <summary>
        /// When there are more than one problems reported
        /// over an area of 150mts, this cout will increase for
        /// the first report
        /// </summary>
        public int Importance { get; set; }
        /// <summary>
        /// As the application can be controlled by police
        /// or media this indicates wheter or not
        /// a report is 100% true
        /// </summary>
        public bool Verified { get; set; }
        /// <summary>
        /// Type, it will be the type for the problem. The values
        /// can be:
        ///    + 1 - trafico
        ///    + 2 - manifestacion
        ///    + 3 - desfile
        ///    + 4 - bloqueo
        ///    + 5 - accidente
        ///    + 6 - otro
        /// </summary>
        public int Type { get; set; }
        /// <summary>
        /// Description for the problem
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Date of problem, will only contain day, month and year
        /// </summary>
        public DateTime ReportedAt { get; set; }
        /// <summary>
        /// Date of problem in unix stamp
        /// </summary>
        public long ReportedAtStamp { get; set; }

        public virtual ICollection<Report> Reports { get; set; }
    }
}