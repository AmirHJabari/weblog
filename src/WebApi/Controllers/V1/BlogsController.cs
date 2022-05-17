using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Weblog.WebApi.Controllers.V1;

[ApiVersion("1")]
public class BlogsController : ApiControllerBase<BlogsController>
{
    [HttpGet("[action]")]
    public ActionResult Get(string name = "Amir")
    {
        return Ok("Hello " + name);
    }
}