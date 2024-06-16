using CSharpFunctionalExtensions;
using Decoded.Poke.Application;
using Decoded.Poke.Application.RequestModels;
using Decoded.Poke.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Decoded.Poke.Api.Controllers;

/// <summary>
/// This controller represents our collection of pokemon
/// </summary>
[ApiController]
[Route("[controller]")]
public class PokemonCollectionController : ControllerBase
{
    private readonly ILogger<PokemonCollectionController> _logger;
    private readonly IDecodePokeService _decodePokeService;

    public PokemonCollectionController(ILogger<PokemonCollectionController> logger, IDecodePokeService decodePokeService)
    {
        _logger = logger;
        this._decodePokeService = decodePokeService;
    }

    [HttpGet]
    public ActionResult<Pokemon> GetAll()
    {
        var pokemonCollection = this._decodePokeService.GetAll();

        return pokemonCollection.Any() ? this.Ok(pokemonCollection) : this.NotFound();
    }

    [HttpGet("{id:int}")]
    public ActionResult<Pokemon> Get(int id)
    {
        var pokemonOrNothing = this._decodePokeService.Get(id);

        return pokemonOrNothing.HasValue ?
            this.Ok(pokemonOrNothing.Value) :
            this.NotFound();
    }

    [HttpPost]
    public async Task<ActionResult<Pokemon>> Post(AddPokemonRequest request)
    {
        return await this._decodePokeService
            .Add(request)
            .Finally(result => result.IsSuccess ? this.Ok(result.Value) : this.Problem(result.Error));
    }

    [HttpDelete]
    public IActionResult Delete(RemovePokemonRequest request)
    {
        var result = this._decodePokeService.Remove(request.Id);

        if (result.IsSuccess)
            this.NoContent();
        
        return this.Problem(result.Error);
    }
}
