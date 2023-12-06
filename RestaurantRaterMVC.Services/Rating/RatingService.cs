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

    // Create
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

    // Read
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

    // Read by Restaurant Id
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

    // Read by Rating Id
    public async Task<RatingDetail?> GetRatingByIdAsync(int id)
    {
        RatingEntity? rating = await _context.Ratings
            .FirstOrDefaultAsync(r => r.Id == id);

        return rating is null ? null : new()
        {
            Id = rating.Id,
            Score = rating.Score
        };
    }

    // Delete
    public async Task<bool> DeleteRatingAsync(int id)
    {
        RatingEntity? entity = await _context.Ratings.FindAsync(id);
        if (entity is null)
            return false;

        // var ratings = await _context.Ratings.Where(r => r.Id == entity.Id).ToListAsync();
        // _context.Ratings.RemoveRange(ratings);
        // await _context.SaveChangesAsync();

        _context.Ratings.Remove(entity);
        return await _context.SaveChangesAsync() == 1;
    }

}