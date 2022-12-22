using AdministrationSettings.API.Models.Classificators;
using API.Customization.Controllers;
using API.Customization.Controllers.Attributes;
using Domain.Seedwork.Enums;
using Microsoft.AspNetCore.Mvc;

namespace AdministrationSettings.API.Controllers
{
    public class ClassificatorController : BaseApiController
    {
        private static Classificators? classificators = null;
        private readonly ILogger<ClassificatorController> _logger;

        public ClassificatorController(ILogger<ClassificatorController> logger)
        {
            _logger = logger;
        }

        [HttpGet("api/v1/log-critical")]
        [ProducesResponseOk]
        public ActionResult<Classificators> LogCritical()
        {
            _logger.LogCritical(new Exception("Critical exception"), "Testing alerting");
            return Ok("done");
        }

        [HttpGet("api/v1/classificators")]
        [ProducesResponseOk]
        public ActionResult<Classificators> GetClassificators()
        {
            if (classificators is null)
            {
                classificators = new Classificators
                {
                    WorkTypes = Enum.GetValues(typeof(WorkType)).Cast<WorkType>(),
                    FormatTypes = Enum.GetValues(typeof(FormatType)).Cast<FormatType>(),
                    SeniorityLevels = Enum.GetValues(typeof(SeniorityLevel)).Cast<SeniorityLevel>(),
                    WorkingHourTypes = Enum.GetValues(typeof(WorkingHoursType)).Cast<WorkingHoursType>(),
                    JobStages = Enum.GetValues(typeof(JobStage)).Cast<JobStage>(),
                    SelectedCandidateStages = Enum.GetValues(typeof(SelectedCandidateStage)).Cast<SelectedCandidateStage>(),
                    ArchivedCandidateStages = Enum.GetValues(typeof(ArchivedCandidateStage)).Cast<ArchivedCandidateStage>(),
                    ActivityStatuses = Enum.GetValues(typeof(ActivityStatus)).Cast<ActivityStatus>(),
                    SystemLanguages = Enum.GetValues(typeof(SystemLanguage)).Cast<SystemLanguage>(),
                    CompanyWorkingHourTypes = Enum.GetValues(typeof(CompanyWorkingHoursType)).Cast<CompanyWorkingHoursType>(),
                };
            }

            return Ok(classificators);
        }
    }
}
