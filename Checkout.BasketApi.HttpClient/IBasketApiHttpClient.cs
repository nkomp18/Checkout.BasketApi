using System;
using System.Threading.Tasks;
using Checkout.BasketApi.Models;

namespace Checkout.BasketApi.HttpClient
{
    public interface IBasketApiHttpClient
    {
        /// <summary>
        /// Get the basket of a user asynchronously.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>Basket</returns>
        Task<Basket> GetBasketAsync(Guid userId);

        /// <summary>
        /// Get the basket of a user.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>Basket</returns>
        Basket GetBasket(Guid userId);

        /// <summary>
        /// Adds an item into the basket of a user asynchronously.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="productId"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        Task AddItemAsync(Guid userId, int productId, int quantity);

        /// <summary>
        /// Adds an item into the basket of a user.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="productId"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        void AddItem(Guid userId, int productId, int quantity);

        /// <summary>
        /// Updates the quantity of an item in the basket of a user asynchronously.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="productId"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        Task UpdateQuantityAsync(Guid userId, int productId, int quantity);

        /// <summary>
        /// Updates the quantity of an item in the basket of a user.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="productId"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        void UpdateQuantity(Guid userId, int productId, int quantity);

        /// <summary>
        /// Clears the basket of a user asynchronously.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task ClearBasketAsync(Guid userId);


        /// <summary>
        /// Clears the basket of a user.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        void ClearBasket(Guid userId);
    }
}