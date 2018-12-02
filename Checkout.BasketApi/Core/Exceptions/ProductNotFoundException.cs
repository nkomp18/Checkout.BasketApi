namespace Checkout.BasketApi.Core.Exceptions
{
    public sealed class ProductNotFoundException : NotFoundException
    {
        public ProductNotFoundException(int id) : base($"Product with Id {id} not found.") { }

    }
}
