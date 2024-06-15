using Microsoft.AspNetCore.Mvc;

namespace Decoded.Poke.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class PokeApiController : ControllerBase
{
    private readonly ILogger<PokeApiController> _logger;

    public PokeApiController(ILogger<PokeApiController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IEnumerable<string> Search()
    {
        return Enumerable
            .Range(1, 5)
            .Select(index => index.ToString());
    }
}
