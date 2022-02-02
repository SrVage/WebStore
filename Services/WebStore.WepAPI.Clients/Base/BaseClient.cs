using System.Net.Http.Json;
namespace WebStore.WepAPI.Clients.Base
{
    public abstract class BaseClient:IDisposable
    {
        protected HttpClient Http { get; }
        protected string Address { get; }

        protected BaseClient(HttpClient client, string address)
        {
            Http = client;
            Address = address;
        }

        protected T? Get<T>(string URL) => GetAsync<T>(URL).Result;

        protected async Task<T?> GetAsync<T>(string URL, CancellationToken cancel = default)
        {
            var response = await Http.GetAsync(URL, cancel).ConfigureAwait(false);
            return await response.EnsureSuccessStatusCode().Content.ReadFromJsonAsync<T>().ConfigureAwait(false);
        }

        protected HttpResponseMessage Post<T>(string URL, T value) => PostAsync<T>(URL, value).Result;

        protected async Task<HttpResponseMessage> PostAsync<T>(string URL, T value, CancellationToken cancel = default)
        {
            var response = await Http.PostAsJsonAsync(URL,value, cancel).ConfigureAwait(false);
            return response.EnsureSuccessStatusCode();
        }

        protected HttpResponseMessage Put<T>(string URL, T value) => PutAsync<T>(URL, value).Result;

        protected async Task<HttpResponseMessage> PutAsync<T>(string URL, T value, CancellationToken cancel = default)
        {
            var response = await Http.PutAsJsonAsync(URL, value, cancel).ConfigureAwait(false);
            return response.EnsureSuccessStatusCode();
        }

        protected HttpResponseMessage Delete(string URL) => DeleteAsync(URL).Result;

        protected async Task<HttpResponseMessage> DeleteAsync(string URL, CancellationToken cancel = default)
        {
            var response = await Http.DeleteAsync(URL, cancel).ConfigureAwait(false);
            return response;
        }

        public void Dispose()
        {
            Dispose(true);
        }

        //~BaseClient() => Dispose(false);

        protected bool _disposed;
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;
            _disposed = true;
            if (disposing)
            {
                
            }

        }
    }
}
