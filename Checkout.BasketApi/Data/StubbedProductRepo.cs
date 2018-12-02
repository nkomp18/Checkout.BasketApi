using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Checkout.BasketApi.Core.Exceptions;
using Checkout.BasketApi.Models;

namespace Checkout.BasketApi.Data
{
    /// <summary>
    /// A stubbed product repository. Originally this was going to be used in the controller to provide Product information for the UI.
    /// However, as the project uses Swagger for UI, it didn't end up being used.
    /// </summary>
    public sealed class StubbedProductRepo : IProductRepo
    {
        private static readonly IEnumerable<Product> Products = new List<Product>()
            {
                new Product(1, "A", 10),
                new Product(2, "B", 15),
                new Product(3, "C", 20),
                new Product(4, "D", 25),
                new Product(5, "E", 30),
                new Product(6, "F", 35),
                new Product(7, "G", 40)
            };

        /// <inheritdoc/>
        public Task<Product> GetProductAsync(int id)
        {
            return Task.Run(() =>
            {
                var product = Products.SingleOrDefault(p => p.Id == id);

                if (product == default(Product))
                {
                    throw new ProductNotFoundException(id);
                }

                return product;
            });
        }

        /// <inheritdoc />
        public Task<bool> ProductExistsAsync(int id) => Task.Run(() => Products.Any(p => p.Id == id));
    }
}
