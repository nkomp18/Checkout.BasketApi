using Checkout.BasketApi.Core;
using Checkout.BasketApi.Data;
using Checkout.BasketApi.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace Checkout.BasketApi
{
    public sealed class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public const string ApplicationName = "Checkout.BasketApi";

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddSingleton<IBasketService, BasketService>();
            services.AddSingleton<IBasketRepo, InMemoryBasketRepo>();
            services.AddSingleton<IProductRepo, StubbedProductRepo>();

            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new Info { Title = ApplicationName, Version = "v1" });
                s.EnableAnnotations();
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseMiddleware<ExceptionHandler>();
            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(s =>
                s.SwaggerEndpoint("/swagger/v1/swagger.json", ApplicationName)
            );
        }
    }
}