using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Checkout.BasketApi.Core.Exceptions;
using Checkout.BasketApi.Models;

namespace Checkout.BasketApi.Data
{
    /// <summary>
    /// An In-Memory basket repository
    /// </summary>
    public sealed class InMemoryBasketRepo : IBasketRepo
    {
        /// <summary>
        /// Could also used ConcurrentDictionary if our environment was multi-threaded, but a simple Dictionary is a lot more
        /// efficient in a single threaded environment.
        /// </summary>
        private static readonly Dictionary<Guid, Basket> _basketStore = new Dictionary<Guid, Basket>();

        /// <inheritdoc/>
        public Task CreateAsync(Guid userId)
        {
            return Task.Run(() =>
            {
                CheckBasketIsNull(userId);

                _basketStore[userId] = new Basket(userId);
            });
        }

        /// <inheritdoc/>
        public Task<Basket> GetBasketAsync(Guid userId)
        {
            return Task.Run(() =>
            {
                CheckBasketIsNotNull(userId);

                return _basketStore[userId];
            });
        }

        /// <inheritdoc/>
        public Task AddItemAsync(Guid userId, Item item)
        {
            return Task.Run(() =>
            {
                CheckBasketIsNotNull(userId);
                CheckValidQuantity(item.Quantity);

                // If product already present in the item list, update quantity
                if (_basketStore[userId].BasketItems.Any(x => x.ProductId == item.ProductId))
                {
                    var updatedItems = _basketStore[userId].BasketItems.Select(basketItem =>
                    {
                        if (basketItem.ProductId == item.ProductId)
                        {
                            basketItem.Quantity = basketItem.Quantity + item.Quantity;
                        }

                        return basketItem;
                    }).ToList();
                    var updatedBasket = new Basket(userId);
                    updatedBasket.BasketItems.AddRange(updatedItems);
                    _basketStore[userId] = updatedBasket;
                }

                // If product not present in item list, add it
                else
                {
                    _basketStore[userId].BasketItems.Add(item);
                }
            });
        }

        /// <inheritdoc/>
        public Task UpdateItemAsync(Guid userId, int productId, int quantity)
        {
            return Task.Run(() =>
            {
                CheckBasketIsNotNull(userId);
                CheckValidQuantity(quantity);

                // If updating a product that does not exist in the basket, add it
                if (_basketStore[userId].BasketItems.All(x => x.ProductId != productId))
                {
                    _basketStore[userId].BasketItems.Add(new Item(productId, quantity));
                    return;
                }

                // Otherwise update existing quantity   
                var updatedItems = _basketStore[userId].BasketItems.Select(item =>
                {
                    if (item.ProductId == productId)
                    {
                        item.Quantity = quantity;
                    }
                    return item;
                }).ToList();
                var updatedBasket = new Basket(userId);
                updatedBasket.BasketItems.AddRange(updatedItems);
                _basketStore[userId] = updatedBasket;
            });
        }

        /// <inheritdoc/>
        public Task ClearBasketAsync(Guid userId)
        {
            return Task.Run(() =>
            {
                CheckBasketIsNotNull(userId);

                _basketStore[userId] = new Basket(userId);
            });
        }

        /// <inheritdoc/>
        public Task<bool> BasketExistsAsync(Guid userId) => Task.Run(() => _basketStore.ContainsKey(userId));


        #region Validation functions
        private void CheckBasketIsNull(Guid userId)
        {
            if (_basketStore.ContainsKey(userId))
            {
                throw new RepositoryException($"Basket must not exist for user {userId}");
            }
        }

        private void CheckBasketIsNotNull(Guid userId)
        {
            if (!_basketStore.ContainsKey(userId))
            {
                throw new RepositoryException($"Basket must exist for user {userId}");
            }
        }

        private void CheckValidQuantity(int quantity)
        {
            if (quantity <= 0)
            {
                throw new RepositoryException($"Invalid quantity {quantity}");
            }
        }
        #endregion
    }
}
