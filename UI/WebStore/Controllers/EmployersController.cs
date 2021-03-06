using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain.Entities;
using WebStore.Domain.Entities.Identity;
using WebStore.Domain.ViewModels;
using WebStore.Interfaces.Services;
//using WebStore.Models;

namespace WebStore.Controllers;
[Authorize]
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
    [Authorize(Roles = Role.Administrators)]
    public IActionResult Create() => View("Edit", new EmployerEditViewModel());

    public IActionResult Details(int id)
    {
        var employer = _employerData.GetById(id);
        if (employer is null)
            return NotFound();
        ViewBag.SelectedEmployer = employer;
        return View(employer);
    }
    [Authorize(Roles =Role.Administrators)]
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
    [Authorize(Roles = Role.Administrators)]
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
    [Authorize(Roles = Role.Administrators)]
    public IActionResult DeleteConfirmed(int id)
    {
        if (!_employerData.Delete(id))
        {
            _logger.LogWarning("Для операции удаления сотрудник с id: {0} не найден", id);
            return NotFound();
        }
        _logger.LogError("Удаление работника {0}", id);
        return RedirectToAction("Index");
    }
}