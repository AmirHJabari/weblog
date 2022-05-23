using  Weblog.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Weblog.Application.Common.Models;
using Weblog.Application.Commands.CreateTag;
using Weblog.Application.Queries.GetTagsWithPagination;

namespace Weblog.WebApi.Controllers.V1;

[ApiVersion("1")]
public class TagsController : ApiControllerBase<TagsController>
{
    [HttpGet]
    public async Task<ActionResult<PaginatedList<TagDto>>> GetPaginatedList([FromQuery] GetTagsWithPaginationQuery request,
        CancellationToken cancellationToken)
    {
        return await Mediator.Send(request, cancellationToken);
    }

    [HttpPost]
    public async Task<ActionResult<int>> Create(CreateTagCommand request,
        CancellationToken cancellationToken)
    {
        return await Mediator.Send(request, cancellationToken);
    }
}