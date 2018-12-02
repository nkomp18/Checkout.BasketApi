using System;
using System.Threading.Tasks;
using Checkout.BasketApi.Models;
using Checkout.BasketApi.Models.Requests;

namespace Checkout.BasketApi.HttpClient
{
    public sealed class BasketApiHttpClient : JsonHttpClient, IBasketApiHttpClient
    {
        public BasketApiHttpClient(Configuration configuration) : base(configuration) { }

        /// <summary>
        /// This has been added to allow the creation of a quick test server in the client tests. 
        /// In a real life scenario this wouldn't be required. 
        /// </summary>
        /// <param name="client"></param>
        public BasketApiHttpClient(System.Net.Http.HttpClient client) : base(client) { }

        /// <inheritdoc/>
        public async Task<Basket> GetBasketAsync(Guid userId)
        {
            return await GetAsync<Basket>($"basket/{userId}");
        }

        /// <inheritdoc/>
        public Basket GetBasket(Guid userId)
        {
            return GetBasketAsync(userId).Result;
        }

        /// <inheritdoc/>
        public async Task AddItemAsync(Guid userId, int productId, int quantity)
        {
            await PostAsync($"basket/{userId}/{productId}", new ItemRequest { Quantity = quantity });
        }

        /// <inheritdoc/>
        public void AddItem(Guid userId, int productId, int quantity)
        {
            AddItemAsync(userId, productId, quantity).Wait();
        }

        /// <inheritdoc/>
        public async Task UpdateQuantityAsync(Guid userId, int productId, int quantity)
        {
            await PutAsync($"basket/{userId}/{productId}", new ItemRequest { Quantity = quantity });
        }

        /// <inheritdoc/>
        public void UpdateQuantity(Guid userId, int productId, int quantity)
        {
            UpdateQuantityAsync(userId, productId, quantity).Wait();
        }

        /// <inheritdoc/>
        public async Task ClearBasketAsync(Guid userId)
        {
            await DeleteAsync($"basket/{userId}");
        }

        /// <inheritdoc/>
        public void ClearBasket(Guid userId)
        {
            ClearBasketAsync(userId).Wait();
        }
    }
}
