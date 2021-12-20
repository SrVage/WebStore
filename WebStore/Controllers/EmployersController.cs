using Microsoft.AspNetCore.Mvc;
using WebStore.Data;
using WebStore.Models;

namespace WebStore.Controllers;

public class EmployersController : Controller
{
    private readonly ICollection<Employer> _employers;

    public EmployersController()
    {
        _employers = TestData.Employers;
    }

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