﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cebritas.BusinessModel;
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
        /// Will list all problems created today
        /// in a 5000mts ratio(default), with importance greater than 0
        /// or are verified
        /// </summary>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        /// <returns></returns>
        public IEnumerable<Problem> List(double latitude, double longitude) {
            IEnumerable<Problem> problems;
            DateTime today = DateTime.UtcNow.Date;
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

        public Problem ReportedToday(double latitude, double longitude) {
            IEnumerable<Problem> problems;
            DateTime today = DateTime.UtcNow.Date;
            problems = db.Filter(x => x.ReportedAt.Equals(today));

            foreach (Problem problem in problems) {
                double distance = GeoCodeCalc.CalcDistance(latitude, longitude, problem.Latitude, problem.Longitude, GeoCodeCalcMeasurement.Kilometers);
                distance *= 1000.0;
                if (distance <= 150) {
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
        public IEnumerable<Problem> ListByFriends(string[] facebookFriends) {
            IEnumerable<Problem> problems;
            DateTime today = DateTime.UtcNow.Date;
            problems = db.Filter(x => x.ReportedAt.Equals(today)
                                      && facebookFriends.Contains(x.FacebookCode)
                                );
            return problems;
        }

        public Problem Insert(Problem problem) {
            problem.ReportedAt = DateTime.UtcNow;

            Problem todayNearProblem = ReportedToday(problem.Latitude, problem.Longitude);

            if (todayNearProblem == null) {
                problem.Code = General.Cryptography.SecurityTokenGenerator.GenerateGuid();
                problem.Importance = 1;
                todayNearProblem = db.Insert(problem);
            }

            // Insert new reporter for today problem
            Report report = new Report();
            report.ReportedAt = problem.ReportedAt;
            report.Type = problem.Type;
            report.Description = problem.Description;
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

        public void NewReport(Report report) {
            // TODO: Assign report to this shit problem
        }
    }
}