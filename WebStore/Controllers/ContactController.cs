using Microsoft.AspNetCore.Mvc;

namespace WebStore.Controllers;

public class ContactController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}