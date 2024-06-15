using Microsoft.Extensions.DependencyInjection;

namespace Decoded.Poke.Application.ServicesCollection;

public static class ApplicationServicesCollection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        => services.AddScoped<IDecodePokeService, DecodePokeService>();
}
