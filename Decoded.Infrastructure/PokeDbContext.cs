using Decoded.Poke.Domain;
using Decoded.Poke.Domain.PokemonApi;
using Microsoft.EntityFrameworkCore;

namespace Decoded.Poke.Infrastructure;

public sealed class PokeDbContext : DbContext
{
    public PokeDbContext(DbContextOptions<PokeDbContext> options): base(options) {}

    public DbSet<Pokemon> Pokemons { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<Pokemon>()
            .ToTable("Pokemons")
            .HasKey(_ => _.Id);

        modelBuilder.Ignore<PokemonSprite>();

        base.OnModelCreating(modelBuilder);
    }
}