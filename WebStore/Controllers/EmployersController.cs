using Microsoft.AspNetCore.Mvc;
using WebStore.Data;
using WebStore.Models;
using WebStore.Services.Interfaces;
using WebStore.ViewModels;

namespace WebStore.Controllers;

public class EmployersController : Controller
{
    private readonly IEmployerData _employerData;

    public EmployersController(IEmployerData employerData)
    {
        _employerData = employerData;
    }

    // GET
    public IActionResult Index()
    {
        return View(_employerData.GetAll());
    }
    
    public IActionResult Details(int id)
    {
        var employer = _employerData.GetById(id);
        if (employer is null)
            return NotFound();
        ViewBag.SelectedEmployer = employer;
        return View(employer);
    }

    public IActionResult Edit(int id)
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
    public IActionResult Edit(EmployerEditViewModel model)
    {
       var employer = new Employer(model.ID,model.LastName,model.FirstName, model.MiddleName, model.Age, model.TelephoneNumber, model.City);
       if (!_employerData.Edit(employer))
           return NotFound();
       return RedirectToAction("Index");
    }

    public IActionResult Delete(int id)
    {
        return View();
    }
}