using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Weblog.Application.Queries.GetCategoriesWithPagination;
using Weblog.Application.Commands.CreateCategory;
using Weblog.Application.Common.Models;

namespace Weblog.WebApi.Controllers.V1;

[ApiVersion("1")]
public class CategoriesController : ApiControllerBase<CategoriesController>
{
    [HttpGet]
    public async Task<ActionResult<PaginatedList<CategoryDto>>> GetPaginatedList([FromQuery] GetCategoriesWithPaginationQuery request,
        CancellationToken cancellationToken)
    {
        return await Mediator.Send(request, cancellationToken);
    }

    [HttpPost]
    public async Task<ActionResult<int>> Creat(CreateCategoryCommand request,
        CancellationToken cancellationToken)
    {
        return await Mediator.Send(request, cancellationToken);
    }
}