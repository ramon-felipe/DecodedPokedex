namespace Decoded.Poke.Domain.PokemonApi;

public sealed class PokemonApiSearchDto
{
    public bool HasNext => this.Next is not null;
    public string Next { get; set; } = string.Empty;

    public bool HasPrevious => this.Previous is not null;
    public string Previous { get; set; } = string.Empty;

    public int Count { get; set; }

    public IEnumerable<PokemonApiSearhResultDto> Results { get; set; } = [];
}
