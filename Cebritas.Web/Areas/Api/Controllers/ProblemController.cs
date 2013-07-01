using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cebritas.BusinessLogic.ProblemsModule.Services;
using Cebritas.BusinessModel;
using Cebritas.DataAccess.Repositories;
using Cebritas.General;
using Cebritas.Web.Areas.Api.Models;

namespace Cebritas.Web.Areas.Api.Controllers {
    public class ProblemController : RestControllerBase {
        [HttpGet]
        public JsonResult GetTimeZones() {
            List<object> result = new List<object>();
            foreach(var zone in TimeUtil.timeZones) {
                result.Add(new { TimeZone = zone.Key, Name = zone.Value.DisplayName });
            }
            return SuccessResult(result, Messages.OK);
        }

        [HttpPost]
        public JsonResult Report(ProblemViewModel problemViewModel, int timeZone) {
            IProblemService problemService = ProblemService.CreateProblemService(new ProblemRepository(), new ReportRepository());
            TimeZoneInfo timeZoneInfo = TimeUtil.GetTimeZone(timeZone);
            Problem problem = new Problem();
            ViewModelToEntity(problemViewModel, problem);

            problemService.Insert(problem, problemViewModel.FacebookCode, problemViewModel.Description, problemViewModel.Type, timeZoneInfo);

            return SuccessResult(null, Messages.OK);
        }
        [HttpGet]
        public JsonResult GetAllProblems(int? timeZone) {
            if (!timeZone.HasValue) {
                throw new CebraException(Constants.HTTP_BAD_REQUEST, string.Format(Messages.ERROR_PARAM_REQUIRED, "timezone"));
            }
            IProblemService problemService = ProblemService.CreateProblemService(new ProblemRepository());
            TimeZoneInfo timeZoneInfo = TimeUtil.GetTimeZone(timeZone.Value);
            IEnumerable<Problem> problems = problemService.GetAll(timeZoneInfo);

            List<ProblemViewModel> result = new List<ProblemViewModel>();
            ProblemViewModel item;
            foreach (Problem problem in problems) {
                item = new ProblemViewModel();
                EntityToViewModel(problem, item);

                result.Add(item);
            }

            return SuccessResult(result, Messages.OK);
        }

        [HttpGet]
        public JsonResult GetProblems(string latitude, string longitude, int? timeZone) {
            double platitude, plongitude;
            ValidateGetProblems(latitude, longitude, timeZone, out platitude, out plongitude);
            TimeZoneInfo timeZoneInfo = TimeUtil.GetTimeZone(timeZone.Value);
            IProblemService problemService = ProblemService.CreateProblemService(new ProblemRepository());
            IEnumerable<Problem> problems = problemService.List(platitude, plongitude, timeZoneInfo);

            List<ProblemViewModel> result = new List<ProblemViewModel>();
            ProblemViewModel item;
            foreach (Problem problem in problems) {
                item = new ProblemViewModel();
                EntityToViewModel(problem, item);

                result.Add(item);
            }

            return SuccessResult(result, Messages.OK);
        }

        [HttpGet]
        public JsonResult GetProblemsReportedByFriends(string friends, int timeZone) {
            string[] friendsArray = friends.Split(new char[] {','});
            TimeZoneInfo timeZoneInfo = TimeUtil.GetTimeZone(timeZone);
            List<ReportViewModel> result = new List<ReportViewModel>();
            ReportViewModel item;
            IProblemService problemService = ProblemService.CreateProblemService(new ProblemRepository(), new ReportRepository());
            IEnumerable<Report> reports = problemService.ListByFriends(friendsArray, timeZoneInfo);

            foreach (Report report in reports) {
                item = new ReportViewModel();
                EntityToViewModel(report, item);

                result.Add(item);
            }

            return SuccessResult(result, Messages.OK);
        }

        #region "Validation"
        private void ValidateGetProblems(string pLatitude, string pLongitude, int? timeZone, out double latitude, out double longitude) {
            try {
                CultureInfo usCulture = new CultureInfo("en-US");
                latitude = double.Parse(pLatitude, usCulture);
                longitude = double.Parse(pLongitude, usCulture);
            } catch (Exception) {
                throw new CebraException(Messages.FORMATO_COORDENADAS_INCORRECTO);
            }
            if (!timeZone.HasValue) {
                throw new CebraException(Constants.HTTP_BAD_REQUEST, string.Format(Messages.ERROR_PARAM_REQUIRED, "timezone"));
            }
        }

        #endregion "Validation"

        #region "Utils"
        private void ViewModelToEntity(ProblemViewModel problemViewModel, Problem problem) {
            try {
                problemViewModel.Latitude = problemViewModel.Latitude.Replace(",", ".");
                CultureInfo usCulture = new CultureInfo("en-US");
                problem.Latitude = double.Parse(problemViewModel.Latitude, usCulture);
                problem.Longitude = double.Parse(problemViewModel.Longitude, usCulture);
            } catch (Exception) {
                throw new CebraException(Constants.HTTP_BAD_REQUEST, Messages.FORMATO_COORDENADAS_INCORRECTO);
            }
        }

        private void EntityToViewModel(Report report, ReportViewModel reportViewModel) {
            reportViewModel.FacebookCode = report.FacebookCode;
            reportViewModel.Type = report.Type;
            reportViewModel.Latitude = report.Latitude;
            reportViewModel.Longitude = report.Longitude;
            reportViewModel.Description = report.Description;
            reportViewModel.ReportedAt = TimeUtil.DateTimeToUnixTime(report.ReportedAt, UnixTimeType.Seconds);
        }

        private void EntityToViewModel(Problem problem, ProblemViewModel viewModel) {
            CultureInfo usCulture = new CultureInfo("en-US");
            ReportViewModel reportVm;

            viewModel.Code = problem.Code;
            viewModel.Latitude = problem.Latitude.ToString(usCulture);
            viewModel.Longitude = problem.Longitude.ToString(usCulture);
            viewModel.Verified = problem.Verified;
            if (problem.Reports != null) {
                viewModel.Importance = problem.Reports.ToList().Count;
                viewModel.FacebookCode = problem.Reports.ToList()[0].FacebookCode;
                viewModel.Type = problem.Reports.ToList()[0].Type;
                viewModel.Description = problem.Reports.ToList()[0].Description;
            } else {
                viewModel.Importance = 0;
                viewModel.FacebookCode = "";
                viewModel.Type = 1;
                viewModel.Description = "";
            }

            viewModel.ReportedAt = TimeUtil.DateTimeToUnixTime(problem.ReportedAt, UnixTimeType.Seconds);
            viewModel.Reporters = new List<ReportViewModel>();
            if (problem.Reports != null) {
                foreach (Report report in problem.Reports) {
                    reportVm = new ReportViewModel();
                    EntityToViewModel(report, reportVm);

                    ((List<ReportViewModel>)viewModel.Reporters).Add(reportVm);
                }
            }
        }

        #endregion "Utils"
    }
}