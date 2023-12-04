using RestaurantRaterMVC.Models.Restaurant;

namespace RestaurantRaterMVC.Services.Restaurant;
public interface IRestaurantService
{
    // Create
    Task<bool> CreateRestaurantAsync(RestaurantCreate model);

    // Read All
    Task<IEnumerable<RestaurantListItem>> GetAllRestaurantsAsync();

    // Read by Id
    Task<RestaurantDetail?> GetRestaurantAsync(int id);

    // Update
    Task<bool> UpdateRestaurantAsync(RestaurantEdit model);

    // Delete
    Task<bool> DeleteRestaurantAsync(int id);
}