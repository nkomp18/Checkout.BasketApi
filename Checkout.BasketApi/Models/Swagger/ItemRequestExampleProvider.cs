using Checkout.BasketApi.Models.Requests;
using Swashbuckle.AspNetCore.Examples;

namespace Checkout.BasketApi.Models.Swagger
{
    public sealed class ItemRequestExampleProvider : IExamplesProvider
    {
        public object GetExamples()
        {
            return new ItemRequest()
            {
                Quantity = 5
            };
        }
    }
}
