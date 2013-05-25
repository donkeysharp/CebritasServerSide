using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cebritas.BusinessModel;

namespace Cebritas.BusinessLogic.ProblemsModule.Services {
    public interface IProblemService {
        IEnumerable<Problem> List(double latitude, double longitude);
        bool IsReportedByUserNear(string facebookCode, double latitude, double longitude);
        IEnumerable<Problem> ListByFriends(string[] facebookFriends);
        Problem Insert(Problem problem);
        int Update(Problem problem);
        int Delete(long id);
    }
}