using RestaurantRaterMVC.Models.Restaurant;

namespace RestaurantRaterMVC.Services.Restaurant;
public interface IRestaurantService
{
    Task<List<RestaurantListItem>> GetAllRestaurantsAsync();
}