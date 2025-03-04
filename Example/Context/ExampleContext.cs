using Example.Entity;

using Microsoft.EntityFrameworkCore;

namespace Example.Context;

public class ExampleContext : DbContext
{
    public DbSet<Country> Countries { get; set; }
    public DbSet<City> Cities { get; set; }

    public ExampleContext(DbContextOptions<ExampleContext> options) : base(options)
    {
    }
}