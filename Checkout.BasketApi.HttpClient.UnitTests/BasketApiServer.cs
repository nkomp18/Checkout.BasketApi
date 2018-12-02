using System;
using System.IO;
using System.Net;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
namespace Checkout.BasketApi.HttpClient.IntegrationTests
{
    public sealed class BasketApiServer : IDisposable
    {
        public TestServer TestServer { get; }

        public const string BaseUri = "http://localhost:2000/";

        public BasketApiServer()
        {
            var baseAddress = new Uri(BaseUri);

            var builder = new WebHostBuilder()
                .UseKestrel(options =>
                {
                    var endpoint = new IPEndPoint(IPAddress.Any, 2000);
                    options.Listen(endpoint, listenOptions => { });
                })
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<Startup>();

            this.TestServer = new TestServer(builder) { BaseAddress = baseAddress };
        }

        public void Dispose()
        {
            this.TestServer?.Dispose();
        }
    }
}
