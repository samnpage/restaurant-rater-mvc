using RestaurantRaterMVC.Models.Rating;

namespace RestaurantRaterMVC.Services.Ratings;
public interface IRatingService
{
    // Read Methods
    Task<List<RatingListItem>> GetRatingsAsync();
    Task<List<RatingListItem>> GetRestaurantRatingsAsync(int restaurantId);
}