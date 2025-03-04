using Example.Context;
using Example.Entity;

using Microsoft.EntityFrameworkCore;

namespace Example.Repository;

public class CountryRepository : ICountryRepository
{
    private readonly ExampleContext _dbContext;

    public CountryRepository(ExampleContext dbContext) => _dbContext = dbContext;

    public async Task<Country> Add(Country country)
    {
        await _dbContext.Countries.AddAsync(country);
        await _dbContext.SaveChangesAsync();
        return country;
    }

    public async Task<Country?> Get(string name) => await _dbContext.Countries.FirstOrDefaultAsync(c => c.Name == name);
}