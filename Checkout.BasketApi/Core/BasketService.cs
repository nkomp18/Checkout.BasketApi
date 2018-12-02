using System;
using System.Threading.Tasks;
using Checkout.BasketApi.Data;
using Checkout.BasketApi.Models;
using Microsoft.Extensions.Logging;

namespace Checkout.BasketApi.Core
{
    public sealed class BasketService : IBasketService
    {
        private readonly IBasketRepo _basketRepository;
        private readonly ILogger<BasketService> _logger;

        public BasketService(
            IBasketRepo basketRepository, 
            ILogger<BasketService> logger)
        {
            this._basketRepository = basketRepository;
            this._logger = logger;
        }

        /// <inheritdoc/>
        public async Task<Basket> GetAsync(Guid userId)
        {
            await InitializeBasketAsync(userId);

            return await _basketRepository.GetBasketAsync(userId);
        }

        /// <inheritdoc/>
        public async Task AddItemAsync(Guid userId, int productId, int quantity)
        {
            await InitializeBasketAsync(userId);

            this._logger.LogInformation($"Adding {quantity} items of product {productId} in basket for user {userId}");

            await _basketRepository.AddItemAsync(userId, new Item(productId, quantity));
        }

        /// <inheritdoc/>
        public async Task UpdateItemAsync(Guid userId, int productId, int quantity)
        {
            await InitializeBasketAsync(userId);

            this._logger.LogInformation($"Updating quantity {quantity} of product {productId} in basket for user {userId}");

            await _basketRepository.UpdateItemAsync(userId, productId, quantity);
        }

        /// <inheritdoc/>
        public async Task ClearAsync(Guid userId)
        {
            await InitializeBasketAsync(userId);

            this._logger.LogInformation($"Clearing basket for user {userId}");

            await _basketRepository.ClearBasketAsync(userId);
        }

        private async Task InitializeBasketAsync(Guid userId)
        {
            if (!(await _basketRepository.BasketExistsAsync(userId)))
            {
                this._logger.LogInformation($"Initialized basket for UserId {userId}");

                await _basketRepository.CreateAsync(userId);
            }
        }
    }
}
