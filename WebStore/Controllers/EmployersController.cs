using Microsoft.AspNetCore.Mvc;
using WebStore.Data;
using WebStore.Models;
using WebStore.ViewModels;

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
    
    public IActionResult Details(int id)
    {
        var employer = _employers.FirstOrDefault(e => e.ID == id);
        if (employer is null)
            return NotFound();
        ViewBag.SelectedEmployer = employer;
        return View(employer);
    }

    public IActionResult Edit(int id)
    {
        var employer = _employers.FirstOrDefault(e => e.ID == id);
        if (employer is null)
            return NotFound();
        var model = new EmployerEditViewModel()
        {
            ID = employer.ID,
            Age = employer.Age,
            FirstName = employer.FirstName,
            LastName = employer.LastName,
            MiddleName = employer.MiddleName,
        };
        return View(model);
    }

    public IActionResult Edit(EmployerEditViewModel model)
    {
        return RedirectToAction("Index");
    }

    public IActionResult Delete(int id)
    {
        return View();
    }
}