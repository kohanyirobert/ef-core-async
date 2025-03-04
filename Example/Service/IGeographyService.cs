namespace Example.Service;

public interface IGeographyService
{
    Task Add(string cityName, string countryName);
}