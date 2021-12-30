using WebStore.Data;
using WebStore.Models;
using WebStore.Services.Interfaces;

namespace WebStore.Services.InMemory;

public class EmployerDataMemoryService : IEmployerData
{
    private readonly ICollection<Employer> _employers;
    private int _maxID;

    public EmployerDataMemoryService()
    {
        _employers = TestData.Employers;
        _maxID = _employers.DefaultIfEmpty().Max(employer => employer?.ID ?? 0) + 1;
    }

    public IEnumerable<Employer> GetAll() => _employers;

    public Employer? GetById(int id)
        => _employers.FirstOrDefault(employer => employer.ID == id);

    public int Add(Employer employer)
    {
        if (employer is null)
            throw new ArgumentNullException();
        if (_employers.Contains(employer))
            return employer.ID;
        employer.ID = _maxID++;
        _employers.Add(employer);
        return employer.ID;
    }

    public bool Edit(Employer employer)
    {
        if (employer is null)
            throw new ArgumentNullException();
        if (_employers.Contains(employer))
            return true;
        var employerFromDB = GetById(employer.ID);
        if (employerFromDB is null)
            return false;
        employerFromDB.FirstName = employer.FirstName;
        employerFromDB.LastName = employer.LastName;
        employerFromDB.MiddleName = employer.MiddleName;
        employerFromDB.Age = employer.Age;
        employerFromDB.City = employer.City;
        employerFromDB.TelephoneNumber = employer.TelephoneNumber;
        return true;
    }

    public bool Delete(int id)
    {
        var employerFromDB = GetById(id);
        if (employerFromDB is null)
            return false;
        _employers.Remove(employerFromDB);
        return true;
    }
}