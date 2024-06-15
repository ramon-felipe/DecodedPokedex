using CSharpFunctionalExtensions;
using Decoded.Poke.Domain;
using Decoded.Poke.Domain.PokemonApi;

namespace Decoded.Poke.Infrastructure.HttpClients;

public interface IPokeApiClient
{
    Task<Result<IEnumerable<Pokemon>>> GetAllPokemons();
    Task<Result<Maybe<PokemonFromApiDto>>> GetPokemon(int id);
}
