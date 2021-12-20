using Microsoft.AspNetCore.Mvc;
using WebStore.Models;

namespace WebStore.Controllers;

public class EmployersController : Controller
{
    private static readonly List<Employer> _employers = new List<Employer>
    {
        new Employer(1, "Петров", "Иван", "Федорович", 36, 5453421, "Москва"),
        new Employer(2, "Сидоров", "Кирилл", "Андреевич", 32, 5423447, "Королев"),
        new Employer(3, "Васин", "Александр", "Алексеевич", 41, 2543724, "Владимир"),
        new Employer(4, "Вениминов", "Илья", "Альбертович", 26, 3373227, "Краснодар"),
    };
    // GET
    public IActionResult Index()
    {
        return View(_employers);
    }
    
    public IActionResult EmployerID(int id)
    {
        var employer = _employers.FirstOrDefault(e => e.ID == id);
        if (employer is null)
            return NotFound();
        ViewBag.SelectedEmployer = employer;
        return View(employer);
    }
}