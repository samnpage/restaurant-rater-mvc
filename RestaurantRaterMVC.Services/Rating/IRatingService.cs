using RestaurantRaterMVC.Models.Rating;

namespace RestaurantRaterMVC.Services.Rating;
public interface IRatingService
{
    // Create Method
    Task<bool> CreateRatingAsync(RatingCreate model);
    
    // Read Methods
    Task<List<RatingListItem>> GetRatingsAsync();
    Task<List<RatingListItem>> GetRestaurantRatingsAsync(int restaurantId);
    Task<RatingDetail?> GetRatingAsync(int id);

    // Delete Methods
    Task<bool> DeleteRestaurantAsync(int id);
}