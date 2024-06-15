using Decoded.Poke.Infrastructure.ServicesCollection;
using Decoded.Poke.Application.ServicesCollection;
using Decoded.Poke.Infrastructure.HttpClients;

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

// builder.Services.Configure<PokeApiSettings>(builder.Configuration.GetRequiredSection(nameof(PokeApiSettings)));

builder.Services
    .AddOptions<PokeApiSettings>()
    .Bind(builder.Configuration.GetSection(PokeApiSettings.Section));

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
