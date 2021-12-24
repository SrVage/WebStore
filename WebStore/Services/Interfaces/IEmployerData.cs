using WebStore.Models;

namespace WebStore.Services.Interfaces;

public interface IEmployerData
{
    IEnumerable<Employer> GetAll();
    Employer? GetById(int id);
    int Add(Employer employer);
    bool Edit(Employer employer);
    bool Delete(int id);
}