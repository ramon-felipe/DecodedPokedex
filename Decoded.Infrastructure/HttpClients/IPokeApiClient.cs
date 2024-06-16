using CSharpFunctionalExtensions;
using Decoded.Poke.Domain;
using Decoded.Poke.Domain.PokemonApi;

namespace Decoded.Poke.Infrastructure.HttpClients;

public interface IPokeApiClient
{
    Task<Result<IEnumerable<Pokemon>>> GetAllPokemons();
    Task<Result<Maybe<PokemonFromApiDto>>> GetPokemonById(int id);
    Task<Result<Maybe<PokemonFromApiDto>>> GetPokemonByName(string name);
    Task<Result<PokemonApiSearchDto>> List(int limit = 20, int offset = 0);
}
