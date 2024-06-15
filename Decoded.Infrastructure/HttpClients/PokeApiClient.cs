using CSharpFunctionalExtensions;
using Decoded.Poke.Domain;
using Decoded.Poke.Domain.PokemonApi;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;

namespace Decoded.Poke.Infrastructure.HttpClients;

public sealed class PokeApiClient : IPokeApiClient
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptionsWrapper _jsonSerializerOptionsWrapper;

    public PokeApiClient(HttpClient httpClient, IOptions<PokeApiSettings> settings, JsonSerializerOptionsWrapper jsonSerializerOptionsWrapper)
    {
        this._httpClient = httpClient;
        this._httpClient.Timeout = new TimeSpan(0, 0, 30);
        this._httpClient.BaseAddress = new Uri(settings.Value.Url);
        this._jsonSerializerOptionsWrapper = jsonSerializerOptionsWrapper;
    }

    public Task<Result<IEnumerable<Pokemon>>> GetAllPokemons()
    {
        return Result
            .Success()
            .Map(() => this._httpClient.GetAsync("pokemon"))
            .Ensure(result => result.IsSuccessStatusCode, "test")
            .Map(result => result.Content.ReadFromJsonAsync<IEnumerable<Pokemon>>(this._jsonSerializerOptionsWrapper.Options))
            .Map(pokemonList => pokemonList ?? []);
    }

    public Task<Result<Maybe<PokemonFromApiDto>>> GetPokemon(int id)
    {
        return Result
            .Success()
            .Map(() => this._httpClient.GetAsync($"pokemon/{id}"))
            .Ensure(result => result.IsSuccessStatusCode, "test")
            .Map(result => result.Content.ReadFromJsonAsync<PokemonFromApiDto>(this._jsonSerializerOptionsWrapper.Options).AsMaybe());
    }
}
