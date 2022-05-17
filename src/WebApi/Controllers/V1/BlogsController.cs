using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Weblog.WebApi.Controllers.V1;

[ApiVersion("1")]
public class BlogsController : ApiControllerBase
{
    [HttpGet("[action]")]
    public ActionResult Get(string name = "Amir")
    {
        return Ok("Hello " + name);
    }
}