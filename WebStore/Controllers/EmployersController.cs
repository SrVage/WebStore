using Microsoft.AspNetCore.Mvc;
using WebStore.Data;
using WebStore.Domain.Entities;
//using WebStore.Models;
using WebStore.Services.Interfaces;
using WebStore.ViewModels;

namespace WebStore.Controllers;

public class EmployersController : Controller
{
    private readonly IEmployerData _employerData;
    private readonly ILogger<EmployersController> _logger;

    public EmployersController(IEmployerData employerData, ILogger<EmployersController> logger)
    {
        _employerData = employerData;
        _logger = logger;
    }

    // GET
    public IActionResult Index()
    {
        return View(_employerData.GetAll());
    }

    public IActionResult Create() => View("Edit", new EmployerEditViewModel());

    public IActionResult Details(int id)
    {
        var employer = _employerData.GetById(id);
        if (employer is null)
            return NotFound();
        ViewBag.SelectedEmployer = employer;
        return View(employer);
    }

    public IActionResult Edit(int? id)
    {
        if (id is null)
        {
            _logger.LogWarning("Создание нового работника");
            return View(new EmployerEditViewModel());
        }
        var employer = _employerData.GetById((int)id);
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

    [HttpPost]
    public IActionResult Edit(EmployerEditViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);
        var employer = new Employer {ID = model.ID, LastName = model.LastName, FirstName = model.FirstName, MiddleName = model.MiddleName, Age = model.Age, TelephoneNumber = model.TelephoneNumber, City = model.City };
        if (model.ID==0) _employerData.Add(employer);
        else if (!_employerData.Edit(employer))
            return NotFound();
        _logger.LogWarning("Редактирование работника: {0}", employer.FirstName);
        return RedirectToAction("Index");
    }

    public IActionResult Delete(int id)
    {
        var employer = _employerData.GetById(id);
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

    [HttpPost]
    public IActionResult DeleteConfirmed(int id)
    {
        if (!_employerData.Delete(id))
            return NotFound();
        _logger.LogError("Удаление работника {0}", id);
        return RedirectToAction("Index");
    }
}