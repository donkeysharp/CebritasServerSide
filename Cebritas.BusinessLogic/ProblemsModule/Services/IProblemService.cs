﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cebritas.BusinessModel;

namespace Cebritas.BusinessLogic.ProblemsModule.Services {
    public interface IProblemService {
        IEnumerable<Problem> List(double latitude, double longitude);
        Problem ReportedToday(double latitude, double longitude);
        IEnumerable<Report> ListByFriends(string[] facebookFriends);
        Problem Insert(Problem problem, string facebookCode, string description, int type);
        int Update(Problem problem);
        int Delete(long id);
    }
}