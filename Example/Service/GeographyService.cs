using Example.Entity;
using Example.Repository;

namespace Example.Service;

public class GeographyService : IGeographyService
{
    private readonly ILogger<GeographyService> _logger;
    private readonly ICityRepository _cityRepo;
    private readonly ICountryRepository _countryRepo;

    public GeographyService(ILogger<GeographyService> logger, ICityRepository cityRepo, ICountryRepository contryRepo)
    {
        _logger = logger;
        _cityRepo = cityRepo;
        _countryRepo = contryRepo;
    }

    public async void Add(string cityName, string countryName)
    {
        try
        {
            Country country = await _countryRepo.Get(countryName) ?? await _countryRepo.Add(new() { Name = countryName });
            City city = new() { Name = cityName, Country = country };
            await _cityRepo.Add(city);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed add");
            throw;
        }
    }
}