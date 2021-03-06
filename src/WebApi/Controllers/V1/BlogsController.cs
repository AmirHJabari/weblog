using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Weblog.Application.Queries.GetBlogsWithPagination;
using Weblog.Application.Commands.CreateBlog;
using Weblog.Application.Common.Models;

namespace Weblog.WebApi.Controllers.V1;

[ApiVersion("1")]
public class BlogsController : ApiControllerBase<BlogsController>
{
    [HttpGet]
    public async Task<ActionResult<PaginatedList<BlogBriefDto>>> GetPaginatedList([FromQuery] GetBlogsWithPaginationQuery request,
        CancellationToken cancellationToken)
    {
        return await Mediator.Send(request, cancellationToken);
    }

    [HttpPost]
    public async Task<ActionResult<int>> Create(CreateBlogCommand request,
        CancellationToken cancellationToken)
    {
        return await Mediator.Send(request, cancellationToken);
    }
}