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
        [HttpPost]
        public JsonResult Report(ProblemViewModel problemViewModel) {
            IProblemService problemService = ProblemService.CreateProblemService(new ProblemRepository(), new ReportRepository());
            Problem problem = new Problem();
            ViewModelToEntity(problemViewModel, problem);

            problemService.Insert(problem, problemViewModel.FacebookCode, problemViewModel.Description, problemViewModel.Type);

            return SuccessResult(null, Messages.OK);
        }
        [HttpGet]
        public JsonResult GetAllProblems() {
            IProblemService problemService = ProblemService.CreateProblemService(new ProblemRepository());
            IEnumerable<Problem> problems = problemService.GetAll();

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
        public JsonResult GetProblems(string latitude, string longitude) {
            double platitude, plongitude;
            ValidateGetProblems(latitude, longitude, out platitude, out plongitude);

            IProblemService problemService = ProblemService.CreateProblemService(new ProblemRepository());
            IEnumerable<Problem> problems = problemService.List(platitude, plongitude);

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
        public JsonResult GetProblemsReportedByFriends(string friends) {
            string[] friendsArray = friends.Split(new char[] {','});
            List<ReportViewModel> result = new List<ReportViewModel>();
            ReportViewModel item;
            IProblemService problemService = ProblemService.CreateProblemService(new ProblemRepository(), new ReportRepository());
            IEnumerable<Report> reports = problemService.ListByFriends(friendsArray);

            foreach (Report report in reports) {
                item = new ReportViewModel();
                EntityToViewModel(report, item);

                result.Add(item);
            }

            return SuccessResult(result, Messages.OK);
        }

        #region "Validation"
        private void ValidateGetProblems(string pLatitude, string pLongitude, out double latitude, out double longitude) {
            try {
                CultureInfo usCulture = new CultureInfo("en-US");
                latitude = double.Parse(pLatitude, usCulture);
                longitude = double.Parse(pLongitude, usCulture);
            } catch (Exception) {
                throw new CebraException(Messages.FORMATO_COORDENADAS_INCORRECTO);
            }
        }

        #endregion "Validation"

        #region "Utils"

        private void ViewModelToEntity(ProblemViewModel problemViewModel, Problem problem) {
            try {
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