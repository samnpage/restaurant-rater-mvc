using Microsoft.EntityFrameworkCore;
using RestaurantRaterMVC.Data;
using RestaurantRaterMVC.Entities.Data;
using RestaurantRaterMVC.Models.Restaurant;

namespace RestaurantRaterMVC.Services.Restaurant;
public class RestaurantService : IRestaurantService
{
    private readonly RestaurantDbContext _context;
    public RestaurantService(RestaurantDbContext context)
    {
        _context = context;
    }

    // Create Method
    public async Task<bool> CreateRestaurantAsync(RestaurantCreate model)
    {
        RestaurantEntity entity = new()
        {
            Name = model.Name,
            Location = model.Location
        };

        _context.Restaurants.Add(entity);
        return await _context.SaveChangesAsync() == 1;
    }

    // Get All Method
    public async Task<IEnumerable<RestaurantListItem>> GetAllRestaurantsAsync()
        {
            List<RestaurantListItem> restaurants = await _context.Restaurants
                .Include(r => r.Ratings)
                .Select(r => new RestaurantListItem()
                {
                    Id = r.Id,
                    Name = r.Name,
                    Score = (double)r.AverageRating
                })
                .ToListAsync();
            return restaurants;
        }

    // Get by Id Method
    public async Task<RestaurantDetail?> GetRestaurantAsync(int id)
    {
        RestaurantEntity? restaurant = await _context.Restaurants
            .Include(r => r.Ratings)
            .FirstOrDefaultAsync(r => r.Id == id);

            return restaurant is null ? null : new()
            {
                Id = restaurant.Id,
                Name = restaurant.Name,
                Location = restaurant.Location,
                Score = restaurant.AverageRating
            };
    }

    // Update Method
    public async Task<bool> UpdateRestaurantAsync(RestaurantEdit model)
    {
        RestaurantEntity? entity = await _context.Restaurants.FindAsync(model.Id);

        if (entity is null)
            return false;

        entity.Name = model.Name;
        entity.Location = model.Location;
        return await _context.SaveChangesAsync() == 1;
    }

    // Delete Method
    public async Task<bool> DeleteRestaurantAsync(int id)
    {
        RestaurantEntity? entity = await _context.Restaurants.FindAsync(id);
        if (entity is null)
            return false;

        var ratings = await _context.Ratings
            .Where(r => r.RestaurantId == entity.Id)
            .ToListAsync();
        _context.Ratings.RemoveRange(ratings);
        await _context.SaveChangesAsync();

        _context.Restaurants.Remove(entity);
        return await _context.SaveChangesAsync() == 1;
    }
}