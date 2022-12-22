using API.Customization.Controllers;
using API.Customization.Controllers.Attributes;
using FrontOffice.Bff.API.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace FrontOffice.Bff.API.Areas.Settings.Controllers
{
    public class SettingsController : AuthorizedApiController
    {
        private readonly CountrySettings _countrySettings;

        public SettingsController(IOptions<CountrySettings> countrySettings)
        {
            _countrySettings = countrySettings.Value;
        }

        [HttpGet("api/v1/country-settings")]
        [ProducesResponseOk]
        public ActionResult<CountrySettings> GetCountrySettings()
        {
            return Ok(_countrySettings);
        }
    }
}
