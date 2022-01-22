using WebStore.Interfaces.TestAPI;
namespace WebStore.WepAPI.Clients.Values;
internal class ValuesClient : IValuesService
{
    private HttpClient _client;


    public void Add(string value)
    {
        throw new NotImplementedException();
    }

    public int Count()
    {
        throw new NotImplementedException();
    }

    public bool Delete(int ID)
    {
        throw new NotImplementedException();
    }

    public void Edit(int ID, string value)
    {
        throw new NotImplementedException();
    }

    public string GetByID(int ID)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<string> GetValues()
    {
        throw new NotImplementedException();
    }
}
