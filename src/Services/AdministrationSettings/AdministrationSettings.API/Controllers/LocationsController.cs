using AdministrationSettings.API.Models.Locations;
using API.Customization.Controllers;
using API.Customization.Controllers.Attributes;
using API.WebClients.Clients.HereSearch;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdministrationSettings.API.Controllers
{
    public class LocationsController : AuthorizedApiController
    {
        private readonly ILocationProvider _locationProvider;

        public LocationsController(ILocationProvider locationProvider)
        {
            _locationProvider = locationProvider;
        }

        [HttpGet("api/v1/locations")]
        [Authorize]
        [ProducesResponseOk]
        public async Task<ActionResult<LocationResponse>> GetLocation([FromQuery] LocationRequest request)
        {
            var addressDetails = await _locationProvider.GetAddressDetailsAsync(request.Address);

            return Ok(new LocationResponse
            {
                Latitude = addressDetails.Latitude,
                Longitude = addressDetails.Longitude,
                AddressLine = addressDetails.AddressLine,
                Country = addressDetails.Country,
                City = addressDetails.City,
                PostalCode = addressDetails.PostalCode
            });
        }
    }
}
