using CSharpFunctionalExtensions;
using Decoded.Poke.Application.RequestModels;
using Decoded.Poke.Infrastructure.Repo;
using Decoded.Poke.Domain;
using Decoded.Poke.Infrastructure.HttpClients;

namespace Decoded.Poke.Application;

public class DecodePokeService : IDecodePokeService
{
    private readonly IRepository<Pokemon> _pokemonRepository;
    private readonly IPokeApiClient pokeApiClient;

    public DecodePokeService(IRepository<Pokemon> pokemonRepository, IPokeApiClient pokeApiClient)
    {
        this._pokemonRepository = pokemonRepository;
        this.pokeApiClient = pokeApiClient;
    }

    public IMaybe<Pokemon> Get(int id)
    {
        return this._pokemonRepository.Get(id);
    }

    public Task<Result<Pokemon>> Add(AddPokemonRequest request)
    {
      return Result.Success()
            .Ensure(() => this.EnsurePokemonIsNotAdded(request.Id), "Pokemon already added!")
            .Bind(() =>  
                this.pokeApiClient
                .GetPokemon(request.Id)
                .Ensure(pokemonOrNothing => pokemonOrNothing.HasValue, _ => "Pokemon not found!")
                .Map(pokemonOrNothing => (Pokemon)pokemonOrNothing.Value)
                .Check(this._pokemonRepository.Add)
                .Check(_ => this._pokemonRepository.Save()));
    }

    private bool EnsurePokemonIsNotAdded(int pokemonId) => this._pokemonRepository.Get(pokemonId).HasNoValue;
}
