using API.Customization.Controllers;
using API.Customization.Pagination;
using BackOffice.Bff.API.Models.Candidate.Response;
using Microsoft.AspNetCore.Mvc;

namespace BackOffice.Bff.API.Controllers.BaseControllers
{
    public abstract class CandidatesBaseController : AuthorizedApiController
    {
        protected PagedResponse<GetCandidateResponse> PrepareEmptyResponse(PagedFilter filterParams, string methodName)
        {
            return new PagedResponse<GetCandidateResponse>(
                0,
                new List<GetCandidateResponse>(),
                filterParams.PageNumber,
                filterParams.PageSize,
                Url.RouteUrl(methodName)!,
                Request.QueryString.ToString());
        }
    }
}
