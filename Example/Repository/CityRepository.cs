using Example.Context;
using Example.Entity;

using Microsoft.EntityFrameworkCore;

namespace Example.Repository;

public class CityRepository : ICityRepository
{
    private readonly ExampleContext _dbContext;

    public CityRepository(ExampleContext dbContext) => _dbContext = dbContext;

    public async Task<int> Add(City city)
    {
        await _dbContext.Cities.AddAsync(city);
        await _dbContext.SaveChangesAsync();
        return city.Id;
    }

    public async Task<City?> Get(string name) => await _dbContext.Cities.FirstOrDefaultAsync(c => c.Name == name);
}