using Example.Entity;

namespace Example.Repository;

public interface ICountryRepository
{
    Task<Country?> Get(string name);
    Task<Country> Add(Country country);
}