using Microsoft.EntityFrameworkCore;
using RestaurantRaterMVC.Entities.Data;

namespace RestaurantRaterMVC.Data;
public class RestaurantDbContext : DbContext
{
    public RestaurantDbContext(DbContextOptions<RestaurantDbContext> options)
        : base(options) {}

    public DbSet<RestaurantEntity> Restaurants { get; set; }
    public DbSet<RatingEntity> Ratings { get; set; } = null!;
}