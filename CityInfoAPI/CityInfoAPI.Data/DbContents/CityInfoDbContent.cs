using CityInfoAPI.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CityInfoAPI.Data.DbContents;

public class CityInfoDbContext : DbContext
{
    public CityInfoDbContext(DbContextOptions<CityInfoDbContext> options) : base(options)
    {
    }

    public DbSet<State> States { get; set; }

    public DbSet<City> Cities { get; set; }

    public DbSet<PointOfInterest> PointsOfInterest { get; set; }
}
