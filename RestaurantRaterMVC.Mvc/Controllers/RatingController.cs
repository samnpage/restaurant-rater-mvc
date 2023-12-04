using Microsoft.AspNetCore.Mvc;
using RestaurantRaterMVC.Models.Rating;
using RestaurantRaterMVC.Services.Rating;

namespace RestaurantRaterMVC.MVC.Controllers;
public class RatingController
{
    private readonly IRatingService _service;
    public RatingController(IRatingService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        List<RatingListItem> model = await _service.GetRatingsAsync();
        return View(model);
    }
}