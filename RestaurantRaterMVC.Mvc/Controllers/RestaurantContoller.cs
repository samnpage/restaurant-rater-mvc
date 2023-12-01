using Microsoft.AspNetCore.Mvc;
using RestaurantRaterMVC.Models.Restaurant;
using RestaurantRaterMVC.Services.Restaurant;

namespace RestaurantRaterMVC.Mvc.Controllers;
public class RestaurantContoller : Controller
{   
    private IRestaurantService _service;
    public RestaurantContoller(IRestaurantService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        List<RestaurantListItem> restaurants = await _service.GetAllRestaurantsAsync();
        return View(restaurants);
    }
}