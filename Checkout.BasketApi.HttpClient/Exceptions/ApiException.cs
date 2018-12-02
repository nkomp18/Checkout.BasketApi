using System;

namespace Checkout.BasketApi.HttpClient.Exceptions
{
    public sealed class ApiException : Exception
    {
        public ApiException(string message) : base (message) { }
    }
}
