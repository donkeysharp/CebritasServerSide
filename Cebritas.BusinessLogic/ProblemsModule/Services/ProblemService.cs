using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cebritas.BusinessModel;
using Cebritas.General.Geo;

namespace Cebritas.BusinessLogic.ProblemsModule.Services {
    public class ProblemService : IProblemService {
        private IProblemRepository db;

        private ProblemService(IProblemRepository db) {
            this.db = db;
        }

        public static ProblemService CreateProblemService(IProblemRepository db) {
            return new ProblemService(db);
        }

        /// <summary>
        /// Will list all problems created today
        /// in a 5000mts ratio(default), with importance greater than 0
        /// or are verified
        /// </summary>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        /// <returns></returns>
        public IEnumerable<Problem> List(double latitude, double longitude) {
            IEnumerable<Problem> problems;
            DateTime today = DateTime.Now.Date;
            problems = db.Filter(x => x.ReportedAt.Equals(today)
                                      && (x.Verified || x.Importance > 0)
                                     );
            List<Problem> result = new List<Problem>();
            foreach (Problem problem in problems) {
                double distance = GeoCodeCalc.CalcDistance(latitude, longitude, problem.Latitude, problem.Longitude, GeoCodeCalcMeasurement.Kilometers);
                // All problems in a 5000mts ratio or 5kms
                if (distance <= 5.0) {
                    result.Add(problem);
                }
            }
            return result;
        }
        /// <summary>
        /// Return true if the user has already reported a problem
        /// in 150mts ratio
        /// </summary>
        /// <param name="facebookCode"></param>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        /// <returns></returns>
        public bool IsReportedByUserNear(string facebookCode, double latitude, double longitude) {
            IEnumerable<Problem> problems;
            DateTime today = DateTime.Now.Date;
            problems = db.Filter(x => x.ReportedAt.Equals(today)
                                      && x.FacebookCode.Equals(facebookCode)
                                );

            foreach (Problem problem in problems) {
                double distance = GeoCodeCalc.CalcDistance(latitude, longitude, problem.Latitude, problem.Longitude, GeoCodeCalcMeasurement.Kilometers);
                distance *= 1000.0;
                if (distance <= 150) {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Get all today's problems reported by facebook friends
        /// </summary>
        /// <param name="facebookFriends"></param>
        /// <returns></returns>
        public IEnumerable<Problem> ListByFriends(string[] facebookFriends) {
            IEnumerable<Problem> problems;
            DateTime today = DateTime.Now.Date;
            problems = db.Filter(x => x.ReportedAt.Equals(today)
                                      && facebookFriends.Contains(x.FacebookCode)
                                );
            return problems;
        }

        public Problem Insert(Problem problem) {
            problem.ReportedAt = DateTime.Now;
            problem.Code = General.Cryptography.SecurityTokenGenerator.GenerateGuid();
            if (!IsReportedByUserNear(problem.FacebookCode, problem.Latitude, problem.Longitude)) {
                problem.Importance = 1;
            }

            return db.Insert(problem);
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