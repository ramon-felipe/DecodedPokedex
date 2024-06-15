namespace Decoded.Poke.Infrastructure.HttpClients;

public sealed class PokeApiSettings
{
    public const string Section = "PokeApiSettings";

    public string Url { get; set; } = string.Empty;
}