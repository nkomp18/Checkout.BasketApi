using System;

namespace Checkout.BasketApi.HttpClient
{
    public sealed class Configuration
    {
        public Configuration(string baseUrl, double timeoutMs)
        {
            this.BaseUrl = baseUrl;
            this.TimeoutMs = timeoutMs;
        }

        /// <summary>
        /// The base address url.
        /// </summary>
        public string BaseUrl { get; set; }

        /// <summary>
        /// The timeout period of the client in milliseconds.
        /// </summary>
        public double TimeoutMs { get; set; }
    }
}