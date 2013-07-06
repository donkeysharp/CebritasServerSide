using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cebritas.BusinessModel;
using Cebritas.General;
using Cebritas.General.Geo;

namespace Cebritas.BusinessLogic.ProblemsModule.Services {
    public class ProblemService : IProblemService {
        private IProblemRepository db;
        private IReportRepository reportDb;

        private ProblemService(IProblemRepository db) {
            this.db = db;
        }

        private ProblemService(IProblemRepository db, IReportRepository reportDb) {
            this.db = db;
            this.reportDb = reportDb;
        }

        public static ProblemService CreateProblemService(IProblemRepository db) {
            return new ProblemService(db);
        }

        public static ProblemService CreateProblemService(IProblemRepository db, IReportRepository reportDb) {
            return new ProblemService(db, reportDb);
        }

        /// <summary>
        /// Get all problems reported today around the world
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Problem> GetAll(TimeZoneInfo timeZone) {
            IEnumerable<Problem> problems;
            DateTime today = TimeUtil.UtcToTimeZone(DateTime.UtcNow, timeZone, false);
            problems = db.Filter(x => x.ReportedDate.Equals(today));

            return problems;
        }

        /// <summary>
        /// Will list all problems created today
        /// in a 5000mts ratio(default), with importance greater than 0
        /// or are verified
        /// </summary>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        /// <returns></returns>
        public IEnumerable<Problem> List(double latitude, double longitude, TimeZoneInfo timeZone) {
            IEnumerable<Problem> problems;
            DateTime today = TimeUtil.UtcToTimeZone(DateTime.UtcNow, timeZone, false);
            problems = db.Filter(x => x.ReportedDate.Equals(today));
            List<Problem> result = new List<Problem>();
            foreach (Problem problem in problems) {
                double distance = GeoCodeCalc.CalcDistance(latitude, longitude, problem.Latitude, problem.Longitude, GeoCodeCalcMeasurement.Kilometers);
                // All problems in a 15000mts ratio or 5kms
                if (distance <= Constants.PROBLEM_MAX_NEAR_RADIUS_KM) {
                    result.Add(problem);
                }
            }
            return result;
        }

        /// <summary>
        /// Get the problem in a radio of 150mts from latitude and longitude
        /// parameters.
        /// </summary>
        /// <param name="latitude">Current latitude</param>
        /// <param name="longitude">Current longitude</param>
        /// <returns></returns>
        public Problem ReportedToday(double latitude, double longitude, TimeZoneInfo timeZone) {
            IEnumerable<Problem> problems;
            DateTime today = TimeUtil.UtcToTimeZone(DateTime.UtcNow, timeZone, false);
            problems = db.Filter(x => x.ReportedDate.Equals(today));

            foreach(Problem problem in problems) {
                double distance = GeoCodeCalc.CalcDistance(latitude, longitude, problem.Latitude, problem.Longitude, GeoCodeCalcMeasurement.Kilometers);
                distance *= 1000.0;
                if (distance <= Constants.REPORTED_PROBLEM_RADIUS) {
                    return problem;
                }
            }
            return null;
        }

        /// <summary>
        /// Get all today's problems reported by facebook friends
        /// </summary>
        /// <param name="facebookFriends"></param>
        /// <returns></returns>
        public IEnumerable<Report> ListByFriends(string[] facebookFriends, TimeZoneInfo timeZone) {
            IEnumerable<Report> reports = reportDb.GetReportsByFriends(facebookFriends, timeZone);
            return reports;
        }
        /// <summary>
        /// Report a new problem. If there is a problem already reported
        /// it will add a new reporter to that problem
        /// </summary>
        /// <param name="problem"></param>
        /// <param name="facebookCode"></param>
        /// <param name="description"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public Problem Insert(Problem problem, string facebookCode, string description, int type, TimeZoneInfo timeZone) {
            // Get today's report with central point latitude and longitude
            // in a 150mts radio
            Problem todayNearProblem = ReportedToday(problem.Latitude, problem.Longitude, timeZone);

            if (todayNearProblem == null) {
                problem.Code = General.Cryptography.SecurityTokenGenerator.GenerateGuid();
                problem.ReportedAt = TimeUtil.UtcToTimeZone(DateTime.UtcNow, timeZone);
                problem.ReportedDate = TimeUtil.UtcToTimeZone(DateTime.UtcNow, timeZone, false); ;

                todayNearProblem = db.Insert(problem);
            }
            // Validates whether or not a user has already
            // reported near a problem
            if (todayNearProblem.Reports != null && todayNearProblem.Reports.Count > 0) {
                Report todayUserReport = (from rep in todayNearProblem.Reports
                                          where rep.FacebookCode.Equals(facebookCode)
                                          select rep).FirstOrDefault();
                if (todayUserReport != null) {
                    throw new CebraException(Messages.USER_HAS_ALREADY_REPORTED_HERE);
                }
            }
            // Insert new reporter for today problem
            Report report = new Report();
            report.FacebookCode = facebookCode;
            report.Description = description;
            report.Latitude = problem.Latitude;
            report.Longitude = problem.Longitude;
            report.Type = type;
            report.ReportedAt = TimeUtil.UtcToTimeZone(DateTime.UtcNow, timeZone);
            report.ReportedDate = TimeUtil.UtcToTimeZone(DateTime.UtcNow, timeZone, false);
            report.ProblemId = todayNearProblem.Id;

            reportDb.Insert(report);

            return todayNearProblem;
        }

        public int Update(Problem problem) {
            return db.Update(problem);
        }

        public int Delete(long id) {
            Problem problem = db.Get(id);
            if (problem != null) {
                return db.Delete(problem);
            }
            return 0;
        }
    }
}