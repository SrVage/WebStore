using System.Net.Http.Json;
using WebStore.Interfaces;
using WebStore.Interfaces.TestAPI;
using WebStore.WepAPI.Clients.Base;

namespace WebStore.WepAPI.Clients.Values;
public class ValuesClient : BaseClient, IValuesService
{
    private HttpClient _client;

    public ValuesClient(HttpClient client) : base(client, WebAPIAddresses.Values)
    {
    }

    public void Add(string value)
    {
        var response = Http.PostAsJsonAsync(Address, value).Result;
        response.EnsureSuccessStatusCode();

    }

    public int Count()
    {
        var response = Http.GetAsync($"{Address}/count").Result;
        if (response.IsSuccessStatusCode)
            return response.Content.ReadFromJsonAsync<int>().Result!;
        return -1;
    }

    public bool Delete(int ID)
    {
        var response = Http.DeleteAsync($"{Address}/{ID}").Result;
        return response.IsSuccessStatusCode;
    }

    public void Edit(int ID, string value)
    {
        var response = Http.PutAsJsonAsync($"{Address}/{ID}", value).Result;
        response.EnsureSuccessStatusCode();
    }

    public string? GetByID(int ID)
    {
        var response = Http.GetAsync($"{Address}/{ID}").Result;
        if (response.IsSuccessStatusCode)
            return response.Content.ReadFromJsonAsync<string>().Result!;
        return null;
    }

    public IEnumerable<string> GetValues()
    {
        var response = Http.GetAsync(Address).Result;
        if (response.IsSuccessStatusCode)
            return response.Content.ReadFromJsonAsync<IEnumerable<string>>().Result!;
        return Enumerable.Empty<string>();
    }
}
