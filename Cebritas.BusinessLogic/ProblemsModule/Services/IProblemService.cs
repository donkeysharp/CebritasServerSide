using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cebritas.BusinessModel;

namespace Cebritas.BusinessLogic.ProblemsModule.Services {
    public interface IProblemService {
        IEnumerable<Problem> List(double latitude, double longitude, TimeZoneInfo timeZone);
        Problem ReportedToday(double latitude, double longitude, TimeZoneInfo timeZone);
        IEnumerable<Report> ListByFriends(string[] facebookFriends, TimeZoneInfo timeZone);
        Problem Insert(Problem problem, string facebookCode, string description, int type, TimeZoneInfo timeZone);
        int Update(Problem problem);
        int Delete(long id);

        IEnumerable<Problem> GetAll(TimeZoneInfo timeZoneInfo);
    }
}