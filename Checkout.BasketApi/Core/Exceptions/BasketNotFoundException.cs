using System;

namespace Checkout.BasketApi.Core.Exceptions
{
    public sealed class BasketNotFoundException : NotFoundException
    {
        public BasketNotFoundException(Guid id) : base($"Basket of user {id} not found.") { }
    }
}
