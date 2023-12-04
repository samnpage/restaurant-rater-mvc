using RestaurantRaterMVC.Models.Restaurant;

namespace RestaurantRaterMVC.Services.Restaurant;
public interface IRestaurantService
{
    // Task<List<RestaurantListItem>> GetAllRestaurantsAsync();
    Task<bool> CreateRestaurantAsync(RestaurantCreate model);
    Task<IEnumerable<RestaurantListItem>> GetAllRestaurantsAsync();
    Task<RestaurantDetail?> GetRestaurantAsync(int id);
}