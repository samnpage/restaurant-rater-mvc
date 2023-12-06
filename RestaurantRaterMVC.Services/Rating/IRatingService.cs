using RestaurantRaterMVC.Models.Rating;

namespace RestaurantRaterMVC.Services.Rating;
public interface IRatingService
{
    // Create
    Task<bool> CreateRatingAsync(RatingCreate model);
    
    // Read
    Task<List<RatingListItem>> GetRatingsAsync();
    Task<List<RatingListItem>> GetRestaurantRatingsAsync(int restaurantId);
    Task<RatingDetail?> GetRatingByIdAsync(int id);

    // Delete
    Task<bool> DeleteRatingAsync(int id);
}