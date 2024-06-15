using CSharpFunctionalExtensions;
using Decoded.Poke.Application.RequestModels;
using Decoded.Poke.Domain;

namespace Decoded.Poke.Application;

public interface IDecodePokeService
{
    IMaybe<Pokemon> Get(int id);
    Task<Result<Pokemon>> Add(AddPokemonRequest request);
}
