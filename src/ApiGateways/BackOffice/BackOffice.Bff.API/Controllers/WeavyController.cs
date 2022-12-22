using API.Customization.Controllers;
using API.Customization.Controllers.Attributes;
using BackOffice.Bff.API.Infrastructure;
using BackOffice.Bff.API.Models.Weavy;
using JWT.Algorithms;
using JWT.Builder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace BackOffice.Bff.API.Controllers
{
    public class WeavyController : AuthorizedApiController
    {
        private readonly WeavyJwtSettings _weavyJwt;

        public WeavyController(IOptions<WeavyJwtSettings> weavyJwt)
        {
            _weavyJwt = weavyJwt.Value;
        }

        [HttpPost("api/v1/weavy-jwt")]
        [Authorize]
        [ProducesResponseOk]
        public ActionResult<WeavyJwtResponse> GetJwt()
        { 
            var token = new JwtBuilder()
                .WithAlgorithm(new HMACSHA256Algorithm())
                .AddClaim("exp", DateTimeOffset.UtcNow.AddMinutes(_weavyJwt.ExpiresInMinutes).ToUnixTimeSeconds())
                .AddClaim("iss", _weavyJwt.Client)
                .AddClaim("sub", User.Identity?.Name)
                .AddClaim("email", User.Identity?.Name)
                .WithSecret(_weavyJwt.Key)
                .Encode();

            return Ok(new WeavyJwtResponse { AccessToken = token } );
        }
    }
}
