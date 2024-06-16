using Decoded.Poke.Api.RequestValidators;
using Decoded.Poke.Application;
using Decoded.Poke.Application.ServicesCollection;
using Decoded.Poke.Infrastructure.HttpClients;
using Decoded.Poke.Infrastructure.ServicesCollection;
using FluentValidation;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen()
    .AddRepositoryServices(builder.Configuration)
    .AddApplicationServices()
    ;

builder.Services
    .AddValidatorsFromAssemblyContaining<RemovePokemonRequestValidator>()
    .AddFluentValidationAutoValidation()
    .AddFluentValidationClientsideAdapters();

// builder.Services.Configure<PokeApiSettings>(builder.Configuration.GetRequiredSection(nameof(PokeApiSettings)));

builder.Services
    .AddOptions<PokeApiSettings>()
    .Bind(builder.Configuration.GetSection(PokeApiSettings.Section));

builder.Services
    .AddOptions<PokeCollectionOptions>()
    .Bind(builder.Configuration.GetSection(PokeCollectionOptions.Section));

builder.Services.AddHttpClient();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
