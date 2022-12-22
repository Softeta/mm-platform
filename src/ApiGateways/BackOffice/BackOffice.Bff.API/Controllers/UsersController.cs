using API.Customization.Controllers;
using API.Customization.Controllers.Attributes;
using BackOffice.Bff.API.Models.Users;
using BackOffice.Shared.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackOffice.Bff.API.Controllers
{
    public class UsersController : AuthorizedApiController
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("api/v1/users")]
        [Authorize]
        [ProducesResponseOk]
        public async Task<ActionResult<GetBackOfficeUsersResponse>> GetUsers()
        {
            var cachedUsers = await _mediator.Send(new GetCachedBackOfficeUsersQuery());
            var users = cachedUsers.Select(BackOfficeUser.FromTableEntity);
            
            return Ok(new GetBackOfficeUsersResponse(users));
        }
    }
}
