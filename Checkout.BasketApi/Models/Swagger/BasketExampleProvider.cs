using System;
using Swashbuckle.AspNetCore.Examples;

namespace Checkout.BasketApi.Models.Swagger
{
    public sealed class BasketExampleProvider : IExamplesProvider
    {
        public object GetExamples()
        {
            return new Basket(Guid.NewGuid())
            {
                BasketItems =
                {
                    new Item(1, 10),
                    new Item(2, 20),
                    new Item(3, 15)
                }
            };
        }
    }
}
