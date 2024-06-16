using CSharpFunctionalExtensions;
using Decoded.Poke.Application;
using Decoded.Poke.Domain.PokemonApi;
using Microsoft.AspNetCore.Mvc;

namespace Decoded.Poke.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class PokeApiController : ControllerBase
{
    private readonly ILogger<PokeApiController> _logger;
    private readonly IDecodePokeService _decodeService;

    public PokeApiController(ILogger<PokeApiController> logger, IDecodePokeService decodeService)
    {
        _logger = logger;
        this._decodeService = decodeService;
    }

    [HttpGet]
    public async Task<ActionResult<PokemonApiSearchDto>> List([FromQuery] int limit, [FromQuery] int offset)
    {
        return await this._decodeService
            .List(limit, offset)
            .Finally(result => result.IsSuccess ? this.Ok(result.Value) : this.Problem(result.Error));
    }

    [HttpGet("{name:alpha}")]
    public async Task<ActionResult<PokemonApiSearchDto>> Search(string name)
    {
        return await this._decodeService
            .Search(name)
            .Finally(result => result.IsSuccess ? this.Ok(result.Value) : this.Problem(result.Error));
    }
}
