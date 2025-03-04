using System.Net.Http.Json;

using Example.Context;

using Microsoft.Extensions.DependencyInjection;

namespace ExampleTest;

[Collection("IntegrationTests")]
public class ExampleIntegrationTest : IClassFixture<ExampleApplicationFactory>
{
    private readonly ExampleApplicationFactory _app;
    private readonly HttpClient _client;

    public ExampleIntegrationTest(ExampleApplicationFactory app)
    {
        _app = app;
        _client = _app.CreateClient();
    }

    /*
        Conditions when this test passes:
        1. await is used in GeographyController before _geoService.Add(...)
        2. IGeographyService.Add(...) and GeographyService.Add(...) returns Task instead of void
        3. CountryRepository.Get() is implemented correctly: await _dbContext.Countries.FirstOrDefaultAsync(c => c.Name == name);

        Because of the await in the controller the method won't return a 200 status code
        prematurely and wait for the database inserts to go through, correctly inserting
        a total of two records.

        Also, this test runs correctly from VS Code (using the C# DevKit extension) and via dotnet test command-line command.
    */
    [Fact]
    public async Task ProperAwaitUsage_ContextNotDisposedPrematurely_TwoRowsInserted()
    {
        using var scope = _app.Services.CreateScope();
        using var context = scope.ServiceProvider.GetRequiredService<ExampleContext>();
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();

        Assert.Empty(context.Countries);
        Assert.Empty(context.Cities);

        var response = await _client.PostAsJsonAsync("/api/geography", new Dictionary<string, string>()
        {
            ["cityName"] = "Miskolc",
            ["countryName"] = "Hungary",
        }, TestContext.Current.CancellationToken);
        response.EnsureSuccessStatusCode();

        Assert.Equal(1, context.Countries.Count());
        Assert.Equal(1, context.Cities.Count());
    }
}