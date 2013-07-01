using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cebritas.BusinessModel;

namespace Cebritas.BusinessLogic.ProblemsModule {
    public interface IReportRepository : IRepository<Report> {
        IEnumerable<Report> GetReportsByFriends(string[] facebookFriends, TimeZoneInfo timeZone);
    }
}