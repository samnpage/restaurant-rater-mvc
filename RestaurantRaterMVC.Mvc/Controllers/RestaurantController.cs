using Microsoft.AspNetCore.Mvc;
using RestaurantRaterMVC.Models.Restaurant;
using RestaurantRaterMVC.Services.Restaurant;

namespace RestaurantRaterMVC.Controllers;

public class RestaurantController : Controller
{
    private IRestaurantService _service;
    public RestaurantController(IRestaurantService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        List<RestaurantListItem> restaurants = (List<RestaurantListItem>)await _service.GetAllRestaurantsAsync();
        return View(restaurants);
    }

    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        RestaurantDetail? model = await _service.GetRestaurantAsync(id);

        if (model is null)
            return NotFound();
        
        return View(model);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(RestaurantCreate model)
    {
        if (!ModelState.IsValid)
            return View(model);

        await _service.CreateRestaurantAsync(model);

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        RestaurantDetail? restaurant = await _service.GetRestaurantAsync(id);
        if (restaurant is null)
            return NotFound();

        RestaurantEdit model = new()
        {
            Id = restaurant.Id,
            Name = restaurant.Name ?? "",
            Location = restaurant.Location ?? ""
        };

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, RestaurantEdit model)
    {
        if (!ModelState.IsValid)
            return View(model);

        if (await _service.UpdateRestaurantAsync(model))
            return RedirectToAction(nameof(Details), new { id = id });

        ModelState.AddModelError("Saver Error", "Could not update the Restaurant. Please try again.");
        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        RestaurantDetail? restaurant = await _service.GetRestaurantAsync(id);
        if (restaurant is null)
            return RedirectToAction(nameof(Index));

        return View(restaurant);
    }

    [HttpPost]
    [ActionName(nameof(Delete))]
    public async Task<IActionResult> ConfirmDelete(int id)
    {
        await _service.DeleteRestaurantAsync(id);
        return RedirectToAction(nameof(Index));
    }
}