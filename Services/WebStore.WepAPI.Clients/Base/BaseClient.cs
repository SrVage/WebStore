namespace WebStore.WepAPI.Clients.Base
{
    public abstract class BaseClient
    {
        protected HttpClient Http { get; set; }
        protected string Address { get; set; }

        protected BaseClient(HttpClient client, string address)
        {
            Http = client;
            Address = address;
        }
    }
}
