using Microsoft.AspNetCore.Mvc;

namespace Weblog.WebUI.Controllers;

[ApiExplorerSettings(IgnoreApi = true)]
public class OidcConfigurationController : Controller
{
    private readonly ILogger<OidcConfigurationController> logger;

    public OidcConfigurationController(ILogger<OidcConfigurationController> _logger)
    {
        logger = _logger;
    }

    [HttpGet("_configuration/{clientId}")]
    public IActionResult GetClientRequestParameters([FromRoute] string clientId)
    {
        //var parameters = ClientRequestParametersProvider.GetClientParameters(HttpContext, clientId);
        //return Ok(parameters);
        return NoContent();
    }
}
