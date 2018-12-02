using System;
using System.Threading.Tasks;
using Checkout.BasketApi.Models;

namespace Checkout.BasketApi.Core
{
    public interface IBasketService
    {
        /// <summary>
        /// Returns the basket of a user.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>Basket</returns>
        Task<Basket> GetAsync(Guid userId);

        /// <summary>
        /// Adds an item in the basket of a user.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="productId"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        Task AddItemAsync(Guid userId, int productId, int quantity);

        /// <summary>
        /// Updates the quantity of an item in the basket of a user.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="productId"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        Task UpdateItemAsync(Guid userId, int productId, int quantity);

        /// <summary>
        /// Clears the contents of a user's basket.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task ClearAsync(Guid userId);
    }
}
