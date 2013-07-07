using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cebritas.BusinessLogic.Entities;
using Cebritas.BusinessLogic.ProblemsModule.Services;
using Cebritas.BusinessModel;
using Cebritas.DataAccess.Repositories;
using Cebritas.General;
using Cebritas.Web.Authentication.Security;
using Cebritas.Web.Models.Problems;

namespace Cebritas.Web.Controllers {
    [Authorize(Roles="media")]
    public class UserReportController : CebraControllerBase {
        [HttpGet]
        public ActionResult Index() {
            ViewBag.PageName = "UserReport";
            return View();
        }
        [HttpPost]
        public ActionResult ReportProblem(ProblemViewModel problemViewModel) {
            IProblemService problemService = ProblemService.CreateProblemService(new ProblemRepository(), new ReportRepository());
            Problem problem = new Problem();
            Usuario user = SessionManager.GetAuthenticatedUser();
            TimeZoneInfo timeZone = TimeUtil.GetTimeZone(user.TimeZone);

            if (ViewModelToEntity(problemViewModel, problem)) {
                problem.Verified = true;
                problem.TimeZone = user.TimeZone;
                problemService.Insert(problem, user.AuthenticationCode, problemViewModel.Description, problemViewModel.Type, timeZone);

                return RedirectToAction("Index", "Home");
            }
            // TODO: on fail send an error message
            return RedirectToAction("Index", "Home");
        }

        private bool ViewModelToEntity(ProblemViewModel problemViewModel, Problem problem) {
            bool result = true;
            try {
                problemViewModel.Latitude = problemViewModel.Latitude.Replace(",", ".");
                problemViewModel.Longitude = problemViewModel.Longitude.Replace(",", ".");

                CultureInfo usCulture = new CultureInfo("en-US");
                problem.Latitude = double.Parse(problemViewModel.Latitude, usCulture);
                problem.Longitude = double.Parse(problemViewModel.Longitude, usCulture);
            } catch (Exception) {
                result = false;
            }
            return result;
        }
    }
}