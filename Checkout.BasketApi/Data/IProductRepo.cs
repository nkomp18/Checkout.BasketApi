using System.Threading.Tasks;
using Checkout.BasketApi.Models;

namespace Checkout.BasketApi.Data
{
    /// <summary>
    /// Didn't end up being used in Client
    /// </summary>
    public interface IProductRepo
    {
        /// <summary>
        /// Gets Product by Product id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Product</returns>
        Task<Product> GetProductAsync(int id);

        /// <summary>
        /// Checks whether product exists.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>bool</returns>
        Task<bool> ProductExistsAsync(int id);
    }
}
