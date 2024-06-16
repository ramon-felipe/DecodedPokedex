using CSharpFunctionalExtensions;
using Decoded.Poke.Domain;
using Decoded.Poke.Domain.PokemonApi;
using Microsoft.Extensions.Options;
using System.Net;
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
            .Ensure(result => result.IsSuccessStatusCode, "error retrieving pokemons")
            .Map(result => result.Content.ReadFromJsonAsync<IEnumerable<Pokemon>>(this._jsonSerializerOptionsWrapper.Options))
            .Map(pokemonList => pokemonList ?? []);
    }

    public Task<Result<PokemonApiSearchDto>> List(int limit = 20, int offset = 0)
    {
        return Result
            .Success()
            .Map(() => this._httpClient.GetAsync($"pokemon?limit={limit}&offset={offset}"))
            .Ensure(result => result.IsSuccessStatusCode, "error retrieving pokemons")
            .Map(result => result.Content.ReadFromJsonAsync<PokemonApiSearchDto>(this._jsonSerializerOptionsWrapper.Options))
            .Map(result => result ?? new());
    }

    public Task<Result<Maybe<PokemonFromApiDto>>> GetPokemonById(int id)
        => this.GetPokemon(this._httpClient.GetAsync, $"pokemon/{id}");

    public Task<Result<Maybe<PokemonFromApiDto>>> GetPokemonByName(string name) 
        => this.GetPokemon(this._httpClient.GetAsync, $"pokemon/{name}");
    

    private Task<Result<Maybe<PokemonFromApiDto>>> GetPokemon(Func<string?, Task<HttpResponseMessage>> func, string url)
    {
        return Result
            .Success()
            .Map(() => func(url))
            .Ensure(result => result.IsSuccessStatusCode, _ => _.StatusCode == HttpStatusCode.NotFound ? "Pokemon not found!" : $"error retrieving pokemon {url}: {_.StatusCode}")
            .Map(result => result.Content.ReadFromJsonAsync<PokemonFromApiDto>(this._jsonSerializerOptionsWrapper.Options).AsMaybe());
    }
}
