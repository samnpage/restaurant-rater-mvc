using RestaurantRaterMVC.Services.Ratings;

namespace RestaurantRaterMVC.MVC.Controllers;
public class RatingController
{
    private readonly IRatingService _service;
    public RatingController(IRatingService service)
    {
        _service = service;
    }
}