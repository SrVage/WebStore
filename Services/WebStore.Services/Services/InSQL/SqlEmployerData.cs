using WebStore.DAL.Context;
using WebStore.Domain.Entities;
using WebStore.Interfaces.Services;
//using WebStore.Models;

namespace WebStore.Services.Services.InSQL
{
    public class SqlEmployerData : IEmployerData
    {
        private readonly WebStoreDB _dataBase;

        public SqlEmployerData(WebStoreDB dataBase)
        {
            _dataBase = dataBase;
        }

        public IEnumerable<Employer> GetAll() => _dataBase.Employers.AsEnumerable();

        public Employer? GetById(int id)
            => _dataBase.Employers.Find(id);

        public int Add(Employer employer)
        {
            _dataBase.Entry(employer).State = Microsoft.EntityFrameworkCore.EntityState.Added;
            _dataBase.SaveChanges();
            return employer.ID;
        }

        public bool Edit(Employer employer)
        {
            _dataBase.Employers.Update(employer);
            return _dataBase.SaveChanges() != 0;
        }

        public bool Delete(int id)
        {
            var employer = _dataBase.Employers
                .Select(e => new Employer { ID = e.ID })
                .FirstOrDefault(e => e.ID == id);
            if (employer is null)
                return false;
            _dataBase.Employers.Remove(employer);
            _dataBase.SaveChanges();
            return true;
        }
    }
}
