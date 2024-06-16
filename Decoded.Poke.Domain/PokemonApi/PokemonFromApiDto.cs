namespace Decoded.Poke.Domain.PokemonApi;

public sealed class PokemonFromApiDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public PokemonSprite Sprites { get; set; } = new ();

    public static explicit operator Pokemon(PokemonFromApiDto dto)
    {
        ArgumentNullException.ThrowIfNull(dto);

        return new Pokemon
        {
            Id = dto.Id,
            Name = dto.Name,
            Image = dto.Sprites.FrontDefault ?? string.Empty
        };
    }
}
