using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using Checkout.BasketApi.HttpClient.Exceptions;
using Newtonsoft.Json;

namespace Checkout.BasketApi.HttpClient
{
    public class JsonHttpClient
    {
        private readonly System.Net.Http.HttpClient _httpClient;

        public JsonHttpClient(Configuration configuration)
        {
            this._httpClient = new System.Net.Http.HttpClient()
            {
                BaseAddress = new Uri(configuration.BaseUrl),
                Timeout = TimeSpan.FromMilliseconds(configuration.TimeoutMs)
            };
        }

        public JsonHttpClient(System.Net.Http.HttpClient client)
        {
            this._httpClient = client;
        }

        public async Task<TResponse> GetAsync<TResponse>(string relativeUri, IReadOnlyDictionary<string, string> headers = null)
        {
            return await this.RunWithTimeout(async () =>
            {
                var request = this.BuildRequest(HttpMethod.Get, relativeUri, null, headers);
                using (var response = await this._httpClient.SendAsync(request))
                {
                    await response.ThrowIfErrorAsync();
                    var responseContent = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<TResponse>(responseContent);
                }
            });
        }

        public async Task PostAsync(string relativeUri, object body, IReadOnlyDictionary<string, string> headers = null)
        {
            await this.RunWithTimeout(async () =>
            {
                var request = this.BuildRequest(HttpMethod.Post, relativeUri, body, headers);
                using (var response = await this._httpClient.SendAsync(request))
                {
                    await response.ThrowIfErrorAsync();
                }
            });
        }

        public async Task PutAsync(string relativeUri, object body, IReadOnlyDictionary<string, string> headers = null)
        {
            await this.RunWithTimeout(async () =>
            {
                var request = this.BuildRequest(HttpMethod.Put, relativeUri, body, headers);
                using (var response = await this._httpClient.SendAsync(request))
                {
                    await response.ThrowIfErrorAsync();
                }
            });
        }

        public async Task DeleteAsync(string relativeUri, IReadOnlyDictionary<string, string> headers = null)
        {
            await this.RunWithTimeout(async () =>
            {
                var request = this.BuildRequest(HttpMethod.Delete, relativeUri, null, headers);
                using (var response = await this._httpClient.SendAsync(request))
                {
                    await response.ThrowIfErrorAsync();
                }
            });
        }

        private async Task<T> RunWithTimeout<T>(Func<Task<T>> func)
        {
            try
            {
                return await func.Invoke();
            }
            catch (TaskCanceledException ex)
            {
                if (!ex.CancellationToken.IsCancellationRequested)
                {
                    throw new TimeoutException(
                        $"Timed out connecting to {this._httpClient.BaseAddress} after {this._httpClient.Timeout.TotalMilliseconds} milliseconds");
                }

                throw;
            }
        }

        private async Task RunWithTimeout(Func<Task> func)
        {
            try
            {
                await func.Invoke();
            }
            catch (TaskCanceledException ex)
            {
                if (!ex.CancellationToken.IsCancellationRequested)
                {
                    throw new TimeoutException(
                        $"Timed out connecting to {this._httpClient.BaseAddress} after {this._httpClient.Timeout.TotalMilliseconds} milliseconds");
                }

                throw;
            }
        }

        private HttpRequestMessage BuildRequest(HttpMethod method, string relativeUri, object body, IReadOnlyDictionary<string, string> headers)
        {
            var request = new HttpRequestMessage(method, relativeUri);

            if (body != null)
            {
                request.Content = new ObjectContent(body.GetType(), body, new JsonMediaTypeFormatter());
            }

            if (!request.Headers.Contains("Accept"))
            {
                request.Headers.TryAddWithoutValidation("Accept", "application/json");
            }

            return request;
        }
    }
}
