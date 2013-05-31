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
            IProblemService problemService = ProblemService.CreateProblemService(new ProblemRepository());
            Problem problem = new Problem();
            ViewModelToEntity(problemViewModel, problem);

            return SuccessResult(null, Messages.OK);
        }

        [HttpGet]
        public JsonResult GetProblems(string latitude, string longitude) {
            double platitude, plongitude;
            ValidateGetProblems(latitude, longitude, out platitude, out plongitude);

            IProblemService problemService = ProblemService.CreateProblemService(new ProblemRepository());
            IEnumerable<Problem> problems = problemService.List(platitude, plongitude);

            return SuccessResult(problems, Messages.OK);
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
                problem.FacebookCode = problemViewModel.FacebookCode;
                problem.Type = problemViewModel.Type;
                problem.Description = problemViewModel.Description;
            } catch (Exception) {
                throw new CebraException(Constants.HTTP_BAD_REQUEST, Messages.ALERTA_FORMATO_COORDENADAS_INCORRECTO);
            }
        }
        #endregion "Utils"
    }
}