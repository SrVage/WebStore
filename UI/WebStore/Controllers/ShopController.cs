using Microsoft.AspNetCore.Mvc;

namespace WebStore.Controllers;

public class ShopController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult ProductDetails()
    {
        return View();
    }

    public IActionResult Checkout()
    {
        return View();
    }

    public IActionResult Cart()
    {
        return View();
    }

    public IActionResult Login()
    {
        return View();
    }
}