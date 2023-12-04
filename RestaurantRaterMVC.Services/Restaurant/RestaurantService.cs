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

    // public async Task<List<RestaurantListItem>> GetAllRestaurantsAsync()
    // {
    //     List<RestaurantListItem> restaurants = await _context.Restaurants
    //         .Include(r => r.Ratings)
    //         .Select(r => new RestaurantListItem()
    //         {
    //             Id = r.Id,
    //             Name = r.Name,
    //             Score = r.AverageRating
    //         })
    //         .ToListAsync();
        
    //     return restaurants;
    // }

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
}