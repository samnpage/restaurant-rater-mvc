using Microsoft.EntityFrameworkCore;
using RestaurantRaterMVC.Data;
using RestaurantRaterMVC.Entities.Data;
using RestaurantRaterMVC.Models.Rating;

namespace RestaurantRaterMVC.Services.Rating;
public class RatingService : IRatingService
{
    private readonly RestaurantDbContext _context;
    public RatingService(RestaurantDbContext context)
    {
        _context = context;
    }

    // Create Method
    public async Task<bool> CreateRatingAsync(RatingCreate model)
    {
        RatingEntity entity = new()
        {
            RestaurantId = model.RestaurantId,
            Score = model.Score
        };

        _context.Ratings.Add(entity);
        return await _context.SaveChangesAsync() == 1;
    }

    // Read Method
    public async Task<List<RatingListItem>> GetRatingsAsync()
    {
        var ratings = await _context.Ratings
            .Include(r => r.Restaurant)
            .Select(r => new RatingListItem
            {
                RestaurantName = r.Restaurant.Name,
                Score = r.Score
            })
            .ToListAsync();

        return ratings;            
    }

    // Read by Id Method
    public async Task<List<RatingListItem>> GetRestaurantRatingsAsync(int restaurantId)
    {
        var ratings = await _context.Ratings
            .Include(r => r.Restaurant)
            .Where(r => r.RestaurantId == restaurantId)
            .Select(r => new RatingListItem
            {
                RestaurantName = r.Restaurant.Name,
                Score = r.Score
            })
            .ToListAsync();

        return ratings;
    }

    public async Task<RatingDetail?> GetRatingAsync(int id)
    {
        RatingEntity? rating = await _context.Ratings
            .FindAsync(id);
        
        RatingDetail detail = new()
        {
            rating.Restaurant = detail.
        }
    }

    // Delete Method
    public async Task<bool> DeleteRestaurantAsync(int id)
    {
        RatingEntity? entity = await _context.Ratings.FindAsync(id);
        if (entity is null)
            return false;

        var ratings = await _context.Ratings
            .Where(r => r.RestaurantId == entity.Id)
            .ToListAsync();
        _context.Ratings.RemoveRange(ratings);
        await _context.SaveChangesAsync();

        _context.Ratings.Remove(entity);
        return await _context.SaveChangesAsync() == 1;
    }

}