using System.Text.Json;

namespace Decoded.Poke.Infrastructure.HttpClients;

public sealed class JsonSerializerOptionsWrapper
{
    public JsonSerializerOptionsWrapper()
    {
        this.Options = new JsonSerializerOptions(JsonSerializerDefaults.Web);
    }

    public JsonSerializerOptions Options { get; }
}