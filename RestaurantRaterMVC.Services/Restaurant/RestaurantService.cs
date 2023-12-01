using Microsoft.EntityFrameworkCore;
using RestaurantRaterMVC.Data;
using RestaurantRaterMVC.Models.Restaurant;

namespace RestaurantRaterMVC.Services.Restaurant;
public class RestaurantService : IRestaurantService
{
    private readonly RestaurantDbContext _context;
    public RestaurantService(RestaurantDbContext context)
    {
        _context = context;
    }

    // Get All Method
    public async Task<List<RestaurantListItem>> GetAllRestaurantsAsync()
    {
        List<RestaurantListItem> restaurants = await _context.Restaurants
            .Include(r => r.Ratings)
            .Select(r => new RestaurantListItem()
            {
                Id = r.Id,
                Name = r.Name,
                Score = r.AverageRating
            })
            .ToListAsync();
        
        return restaurants;
    }
}