using API.Customization.HttpContexts;
using Domain.Seedwork.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Customization.Controllers;

[ApiController]
[Route("")]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
public abstract class AuthorizedApiController : BaseApiController
{
    protected Guid? CompanyId => HttpContext.User.CompanyId();

    protected Guid? ContactId => HttpContext.User.ContactId();

    protected Guid UserId => HttpContext.User.UserId();

    protected string? Email => HttpContext.User.Email();

    protected string Scope => HttpContext.User.Scope();

    protected bool IsAdmin => HttpContext.User.IsAdmin();

    protected void ValidateEmail()
    {
        if (string.IsNullOrWhiteSpace(Email))
        {
            throw new BadRequestException($"No Email address found in Claims",
                ErrorCodes.BadRequest.NoEmailInClaims);
        }
    }
}