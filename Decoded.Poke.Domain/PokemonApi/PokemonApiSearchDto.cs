namespace Decoded.Poke.Domain.PokemonApi;

public sealed class PokemonApiSearchDto
{
    public string Next { get; set; } = string.Empty;
    public string Previous { get; set; } = string.Empty;
    public IEnumerable<PokemonApiSearhResultDto> Results { get; set; } = [];
}
