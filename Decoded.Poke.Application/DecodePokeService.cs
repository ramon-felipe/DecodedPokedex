using CSharpFunctionalExtensions;
using Decoded.Poke.Application.RequestModels;
using Decoded.Poke.Domain;
using Decoded.Poke.Domain.PokemonApi;
using Decoded.Poke.Infrastructure.HttpClients;
using Decoded.Poke.Infrastructure.Repo;

namespace Decoded.Poke.Application;

public class DecodePokeService : IDecodePokeService
{
    private readonly IRepository<Pokemon> _pokemonRepository;
    private readonly IPokeApiClient _pokeApiClient;

    public DecodePokeService(IRepository<Pokemon> pokemonRepository, IPokeApiClient pokeApiClient)
    {
        this._pokemonRepository = pokemonRepository;
        this._pokeApiClient = pokeApiClient;
    }

    public IEnumerable<Pokemon> GetAll()
        => [.. this._pokemonRepository.GetAll()];

    public IMaybe<Pokemon> Get(int id)
        => this._pokemonRepository.Get(id);

    public Result Remove(int pokemonId)
    {
        this._pokemonRepository.Delete(pokemonId);

        return this._pokemonRepository.Save();
    }

    public Task<Result<Pokemon>> Add(AddPokemonRequest request)
    {
      return this._pokeApiClient
            .GetPokemonById(request.Id)
            .Ensure(pokemonOrNothing => pokemonOrNothing.HasValue, _ => "Pokemon not found!")
            .Map(pokemonOrNothing => (Pokemon)pokemonOrNothing.Value)
            .Check(this._pokemonRepository.Add)
            .Check(_ => this._pokemonRepository.Save());
    }

    public Task<Result<PokemonApiSearchDto>> List(int limit = 20, int offset = 0)
        => this._pokeApiClient.List(limit, offset);

    public Task<Result<Pokemon>> Search(string name)
        => this._pokeApiClient
            .GetPokemonByName(name)
            .Ensure(pokemonOrNothing => pokemonOrNothing.HasValue, _ => "Pokemon not found!")
            .Map(pokemonOrNothing => (Pokemon)pokemonOrNothing.Value);
}
