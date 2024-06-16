using System.Text.RegularExpressions;

namespace Decoded.Poke.Domain.PokemonApi;

public sealed partial class PokemonApiSearhResultDto
{
    public string Name { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public int PokemonId => this.ExtractPokemonId();

    private int ExtractPokemonId()
    {
        var x = ExtractPokemonIdFromUrl().Match(this.Url);
        
        return Convert.ToInt32(x.Groups[1].Value);
    }

    [GeneratedRegex("(\\d+)/?$")]
    private static partial Regex ExtractPokemonIdFromUrl();
}