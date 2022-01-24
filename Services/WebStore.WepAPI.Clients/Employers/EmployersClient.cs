using WebStore.WepAPI.Clients.Base;

namespace WebStore.WepAPI.Clients.Employers
{
    public class EmployersClient : BaseClient
    {
        public EmployersClient(HttpClient client, string address) : base(client, "api/employers")
        {
        }
    }
}
