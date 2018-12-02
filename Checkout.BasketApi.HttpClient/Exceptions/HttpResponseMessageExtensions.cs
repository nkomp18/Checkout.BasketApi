using System.Net.Http;
using System.Threading.Tasks;

namespace Checkout.BasketApi.HttpClient.Exceptions
{
    public static class HttpResponseMessageExtensions
    {
        public static async Task ThrowIfErrorAsync(this HttpResponseMessage response)
        {
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                var reasonPhrase = response.ReasonPhrase;
               
                response.Content?.Dispose();
                var message = $"Status code {response.StatusCode} due to error {reasonPhrase}. Exception {content}";
                throw new ApiException(message);
            }
        }
    }
}
