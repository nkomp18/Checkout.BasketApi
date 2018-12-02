using System;
using System.Threading.Tasks;
using Checkout.BasketApi.Models;

namespace Checkout.BasketApi.Data
{
    public interface IBasketRepo
    {
        /// <summary>
        /// Creates a new basket for a user.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task CreateAsync(Guid userId);

        /// <summary>
        /// Gets the basket of a user.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>Basket</returns>
        Task<Basket> GetBasketAsync(Guid userId);

        /// <summary>
        /// Adds an item into the basket for a user.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        Task AddItemAsync(Guid userId, Item item);

        /// <summary>
        /// Updates the properties of an item for a user.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="productId"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        Task UpdateItemAsync(Guid userId, int productId, int quantity);

        /// <summary>
        /// Empties the basket of a user.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task ClearBasketAsync(Guid userId);

        /// <summary>
        /// Checks whether a basket exists for a specific user.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>bool</returns>
        Task<bool> BasketExistsAsync(Guid userId);
    }
}
