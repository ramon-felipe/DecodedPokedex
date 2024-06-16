using Decoded.Poke.Application;
using Decoded.Poke.Application.RequestModels;
using Decoded.Poke.Domain;
using Decoded.Poke.Infrastructure.Repo;
using FluentValidation;
using Microsoft.Extensions.Options;

namespace Decoded.Poke.Api.RequestValidators;

public sealed class RemovePokemonRequestValidator : AbstractValidator<RemovePokemonRequest>
{
    public RemovePokemonRequestValidator()
    {
        this.RuleFor(_ => _.Id)
            .GreaterThan(0);
    }
}

public sealed class AddPokemonRequestValidator : AbstractValidator<AddPokemonRequest>
{
    public AddPokemonRequestValidator(IRepository<Pokemon> pokemonRepository, IOptions<PokeCollectionOptions> options)
    {
        var dbQty = pokemonRepository.Count();

        this.RuleFor(_ => _.Id)
            .GreaterThan(0);

        this.RuleFor(_ => _.Id)
            .Must(pokemonId => pokemonRepository.Get(pokemonId).HasNoValue)
            .WithMessage(_ => $"Pokemon already present in the collection!");

        this.RuleFor(_ => _)
            .Must(_ => dbQty < options.Value.AllowedQuantity)
            .WithMessage($"Number of {options.Value.AllowedQuantity} pokemons in collection already reached!");
    }
}