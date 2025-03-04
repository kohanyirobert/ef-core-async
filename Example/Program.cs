using Example.Context;
using Example.Repository;
using Example.Service;

using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ExampleContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));
builder.Services.AddScoped<ICityRepository, CityRepository>();
builder.Services.AddScoped<ICountryRepository, CountryRepository>();
builder.Services.AddScoped<IGeographyService, GeographyService>();
builder.Services.AddControllers();


var app = builder.Build();

using var scope = app.Services.CreateScope();
using var context = scope.ServiceProvider.GetRequiredService<ExampleContext>();
context.Database.EnsureCreated();

app.MapControllers();
app.Run();


public partial class Program { }