using System;
using System.Net;
using System.Threading.Tasks;
using Checkout.BasketApi.Core.Exceptions;
using Microsoft.AspNetCore.Http;

namespace Checkout.BasketApi.Middleware
{
    public sealed class ExceptionHandler
    {
        private readonly RequestDelegate next;

        public ExceptionHandler(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                context.Response.Clear();

                switch (ex)
                {
                    case ProductNotFoundException _:
                    case BasketNotFoundException _:
                        context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    case BadRequestException _:
                        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    case RepositoryException _:
                        context.Response.StatusCode = (int) HttpStatusCode.Conflict;
                        break;
                    default:
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                await context.Response.WriteAsync(ex.Message);
            }
        }
    }
}
