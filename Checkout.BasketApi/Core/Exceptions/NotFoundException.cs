using System;

namespace Checkout.BasketApi.Core.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message)  
        {
            
        }
    }
}
