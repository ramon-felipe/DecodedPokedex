using Decoded.Poke.Domain;
using Decoded.Poke.Infrastructure.HttpClients;
using Decoded.Poke.Infrastructure.Repo;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Decoded.Poke.Infrastructure.ServicesCollection;

public static class ServicesCollection
{
    public static IServiceCollection AddRepositoryServices(this IServiceCollection services, IConfiguration config)
    {
        var connString = config.GetSection("ConnectionStrings:SqlLite").Value;

        return services
        .AddDbContext<PokeDbContext>(options =>
        {
                options.UseSqlite(connString, b => b.MigrationsAssembly("Decoded.Poke.Api"));
            })
            .AddScoped(typeof(IRepository<>), typeof(GenericRepository<>))
            .AddSingleton<JsonSerializerOptionsWrapper>()
            .AddHttpClients()
        ;
    }

    private static IServiceCollection AddHttpClients(this IServiceCollection services)
    {
        services.AddHttpClient<IPokeApiClient, PokeApiClient>();

        return services;
    }
}