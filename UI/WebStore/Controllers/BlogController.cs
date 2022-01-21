using Microsoft.AspNetCore.Mvc;

namespace WebStore.Controllers;

public class BlogController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult SingleBlog()
    {
        return View();
    }
}