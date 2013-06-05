using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cebritas.BusinessLogic.ProblemsModule;
using Cebritas.BusinessModel;

namespace Cebritas.DataAccess.Repositories {
    public class ReportRepository : GenericRepository<Report>, IReportRepository {
        public IEnumerable<Report> GetReportsByFriends(string[] facebookFriends) {
            DateTime today = DateTime.UtcNow.Date;
            var result = from report in context.Reports
                         where report.ReportedDate.Equals(today)
                               && facebookFriends.Contains(report.FacebookCode)
                         select report;

            return result;
        }
    }
}