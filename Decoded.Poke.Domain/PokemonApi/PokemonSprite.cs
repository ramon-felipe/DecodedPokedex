using System.Text.Json.Serialization;

namespace Decoded.Poke.Domain.PokemonApi;

public sealed class PokemonSprite
{
    [JsonPropertyName("front_default")]
    public string FrontDefault { get; set; } = string.Empty;
}