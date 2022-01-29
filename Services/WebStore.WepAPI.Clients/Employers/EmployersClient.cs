using WebStore.Domain.Entities;
using WebStore.Interfaces.Services;
using WebStore.WepAPI.Clients.Base;
using System.Net.Http.Json;

namespace WebStore.WepAPI.Clients.Employers
{
    public class EmployersClient : BaseClient, IEmployerData
    {
        public EmployersClient(HttpClient client, string address) : base(client, "api/employers")
        {
        }

        public int Add(Employer employer)
        {
            var response = Post(Address, employer);
            var addedEmployer = response.Content.ReadFromJsonAsync<Employer>().Result;
            if (addedEmployer is null)
                return -1;
            var id = addedEmployer.ID;
            employer.ID = id;
            return id;
        }

        public bool Delete(int id)
        {
            var response = Delete($"{Address}/{id}");
            return response.IsSuccessStatusCode;
        }

        public bool Edit(Employer employer)
        {
            var response = Put(Address, employer);
            return response.EnsureSuccessStatusCode().Content.ReadFromJsonAsync<bool>().Result;
        }

        public IEnumerable<Employer> GetAll() 
            => Get<IEnumerable<Employer>>(Address);

        public Employer? GetById(int id) 
            => Get<Employer>($"{Address}/{id}");
        
    }
}
