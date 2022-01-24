using System.Net.Http.Json;
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

        protected T Get<T>(string URL) => GetAsync<T>(URL).Result;

        protected async Task<T> GetAsync<T>(string URL)
        {
            var response = await Http.GetAsync(URL).ConfigureAwait(false);
            return await response.EnsureSuccessStatusCode().Content.ReadFromJsonAsync<T>().ConfigureAwait(false);
        }

        protected HttpResponseMessage Post<T>(string URL, T value) => PostAsync<T>(URL, value).Result;

        protected async Task<HttpResponseMessage> PostAsync<T>(string URL, T value)
        {
            var response = await Http.PostAsJsonAsync(URL,value).ConfigureAwait(false);
            return response.EnsureSuccessStatusCode();
        }

        protected HttpResponseMessage Put<T>(string URL, T value) => PutAsync<T>(URL, value).Result;

        protected async Task<HttpResponseMessage> PutAsync<T>(string URL, T value)
        {
            var response = await Http.PutAsJsonAsync(URL, value).ConfigureAwait(false);
            return response.EnsureSuccessStatusCode();
        }

        protected HttpResponseMessage Delete(string URL) => DeleteAsync(URL).Result;

        protected async Task<HttpResponseMessage> DeleteAsync(string URL)
        {
            var response = await Http.DeleteAsync(URL).ConfigureAwait(false);
            return response;
        }
    }
}
