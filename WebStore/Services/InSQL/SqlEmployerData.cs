using WebStore.DAL.Context;
using WebStore.Domain.Entities;
//using WebStore.Models;
using WebStore.Services.Interfaces;

namespace WebStore.Services.InSQL
{
    public class SqlEmployerData : IEmployerData
    {
        private readonly WebStoreDB _dataBase;
        private int _maxID;

        public SqlEmployerData(WebStoreDB dataBase)
        {
            _dataBase = dataBase;
            _maxID = _dataBase.Employers.DefaultIfEmpty().Max(employer => employer.ID) + 1;
        }

        public IEnumerable<Employer> GetAll() => _dataBase.Employers;

        public Employer? GetById(int id)
            => _dataBase.Employers.FirstOrDefault(employer => employer.ID == id);

        public int Add(Employer employer)
        {
            if (employer is null)
                throw new ArgumentNullException();
            if (_dataBase.Employers.Contains(employer))
                return employer.ID;
            employer.ID = _maxID++;
            _dataBase.Employers.Add(employer);
            return employer.ID;
        }

        public bool Edit(Employer employer)
        {
            if (employer is null)
                throw new ArgumentNullException();
            if (_dataBase.Employers.Contains(employer))
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
            _dataBase.Employers.Remove(employerFromDB);
            return true;
        }
    }
}
