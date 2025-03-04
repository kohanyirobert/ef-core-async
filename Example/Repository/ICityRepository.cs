using Example.Entity;

namespace Example.Repository;

public interface ICityRepository
{
    Task<City?> Get(string name);
    Task<int> Add(City city);
}