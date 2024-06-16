using CSharpFunctionalExtensions;
using Decoded.Poke.Application.RequestModels;
using Decoded.Poke.Domain;
using Decoded.Poke.Domain.PokemonApi;

namespace Decoded.Poke.Application;

public interface IDecodePokeService
{
    IEnumerable<Pokemon> GetAll();
    IMaybe<Pokemon> Get(int id);
    Task<Result<Pokemon>> Add(AddPokemonRequest request);
    Result Remove(int pokemonId);

    // PokeApi methods
    Task<Result<PokemonApiSearchDto>> List(int limit = 20, int offset = 0);
    Task<Result<Pokemon>> Search(string name);
}
