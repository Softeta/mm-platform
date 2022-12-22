using AdministrationSettings.API.Models.Configurations;
using API.Customization.Controllers;
using API.Customization.Controllers.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdministrationSettings.API.Controllers
{
    public class ConfigurationController : AuthorizedApiController
    {
        private readonly Configurations _configurationsResponse;

        public ConfigurationController(Configurations configurationsResponse)
        {
            _configurationsResponse = configurationsResponse;
        }

        [HttpGet("api/v1/configurations")]
        [Authorize]
        [ProducesResponseOk]
        public ActionResult<Configurations> GetConfigurations()
        {
            return Ok(_configurationsResponse);
        }
    }
}
