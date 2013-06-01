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
        public JsonResult GetProblems(string latitude, string longitude) {
            double platitude, plongitude;
            CultureInfo usCulture = new CultureInfo("en-US");
            ValidateGetProblems(latitude, longitude, out platitude, out plongitude);

            IProblemService problemService = ProblemService.CreateProblemService(new ProblemRepository());
            IEnumerable<Problem> problems = problemService.List(platitude, plongitude);

            List<ProblemViewModel> result = new List<ProblemViewModel>();
            ProblemViewModel item;
            ReportViewModel reportVm;
            foreach (Problem problem in problems) {
                item = new ProblemViewModel();
                item.Code = problem.Code;
                item.Latitude = problem.Latitude.ToString(usCulture);
                item.Longitude = problem.Longitude.ToString(usCulture);
                item.Verified = problem.Verified;
                if (problem.Reports != null) {
                    item.Importance = problem.Reports.ToList().Count;
                    item.FacebookCode = problem.Reports.ToList()[0].FacebookCode;
                    item.Type = problem.Reports.ToList()[0].Type;
                    item.Description = problem.Reports.ToList()[0].Description;
                } else {
                    item.Importance = 0;
                    item.FacebookCode = "asd";
                    item.Type = 1;
                    item.Description = "XD";
                }

                item.ReportedAt = 123123;
                item.Reporters = new List<ReportViewModel>();
                if (problem.Reports != null) {
                    foreach (Report report in problem.Reports) {
                        reportVm = new ReportViewModel();
                        reportVm.FacebookCode = report.FacebookCode;
                        reportVm.ReportedAt = 123123123;
                        reportVm.Type = report.Type;
                        reportVm.Description = report.Description;

                        ((List<ReportViewModel>)item.Reporters).Add(reportVm);
                    }
                }

                result.Add(item);
            }

            return SuccessResult(result, Messages.OK);
        }

        [HttpGet]
        public JsonResult GetProblemsReportedByFriends(string friends) {
            string[] friendsArray = friends.Split(new char[] {','});
            IProblemService problemService = ProblemService.CreateProblemService(new ProblemRepository());
            IEnumerable<Problem> problems = problemService.ListByFriends(friendsArray);

            return SuccessResult(problems, Messages.OK);
        }

        #region "Validation"
        private void ValidateGetProblems(string pLatitude, string pLongitude, out double latitude, out double longitude) {
            try {
                CultureInfo usCulture = new CultureInfo("en-US");
                latitude = double.Parse(pLatitude, usCulture);
                longitude = double.Parse(pLongitude, usCulture);
            } catch (Exception) {
                throw new CebraException(Messages.ALERTA_FORMATO_COORDENADAS_INCORRECTO);
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
                throw new CebraException(Constants.HTTP_BAD_REQUEST, Messages.ALERTA_FORMATO_COORDENADAS_INCORRECTO);
            }
        }
        #endregion "Utils"
    }
}