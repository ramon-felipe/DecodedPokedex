using System.Text.RegularExpressions;

namespace Decoded.Poke.Domain.PokemonApi;

public sealed partial class PokemonApiSearhResultDto
{
    public string Name { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    // public int PokemonId => Convert.ToInt32(ExtractPokemonIdFromUrl().Match(this.Url).Value);

    public string MyProperty => this.Test();

    private string Test()
    {
        var x = ExtractPokemonIdFromUrl().Match(this.Url);
        var y = x.Value;

        return y;
    }

    [GeneratedRegex("(\\d+)/?$")]
    private static partial Regex ExtractPokemonIdFromUrl();
}