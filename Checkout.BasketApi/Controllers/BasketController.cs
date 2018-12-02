using System;
using System.Threading.Tasks;
using Checkout.BasketApi.Core;
using Checkout.BasketApi.Models;
using Checkout.BasketApi.Models.Requests;
using Checkout.BasketApi.Models.Swagger;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Examples;

namespace Checkout.BasketApi.Controllers
{
    [Route("basket")]
    public sealed class BasketController : Controller
    {
        private readonly IBasketService _basketService;

        public BasketController(IBasketService basketService)
        {
            this._basketService = basketService;
        }

        #region SwaggerHeaders
        [SwaggerOperation(Summary = "Get a user's basket")]
        [SwaggerResponse(200, "Basket", typeof(Basket))]
        [SwaggerResponseExample(200, typeof(BasketExampleProvider))]
        #endregion
        [HttpGet("{userId}")]
        public async Task<ActionResult> GetAsync(Guid userId)
        {
            return new OkObjectResult(await _basketService.GetAsync(userId));
        }

        #region SwaggerHeaders
        [SwaggerOperation(Summary = "Add item to the basket", Description = "The quantity aggregates if the product is already present in the basket")]
        [SwaggerResponse(200)]
        [SwaggerResponse(400, "Bad request")]
        #endregion
        [HttpPost("{userId}/{productId}")]
        public async Task<ActionResult> AddItem(Guid userId, int productId, [FromBody] ItemRequest basketItemRequest)
        {
            await _basketService.AddItemAsync(userId, productId, basketItemRequest.Quantity);

            return new OkResult();
        }

        #region SwaggerHeaders
        [SwaggerOperation(Summary = "Update the quantity of an item in the basket", Description = "The item gets added in the basket if it is not already present")]
        [SwaggerRequestExample(typeof(ItemRequest), typeof(ItemRequestExampleProvider))]
        [SwaggerResponse(200)]
        [SwaggerResponse(400, "Bad request")]
        #endregion
        [HttpPut("{userId}/{productId}")]
        public async Task<ActionResult> SetItemQuantity(Guid userId, int productId, [FromBody] ItemRequest basketItemRequest)
        {
            await _basketService.UpdateItemAsync(userId, productId, basketItemRequest.Quantity);

            return new OkResult();
        }

        #region SwaggerHeaders
        [SwaggerOperation(Summary = "Clear basket")]
        [SwaggerResponse(200)]
        [SwaggerResponse(404, "Basket not found")]
        #endregion
        [HttpDelete("{userId}")]
        public async Task<ActionResult> ClearBasketAsync(Guid userId)
        {
            await _basketService.ClearAsync(userId);

            return new OkResult();
        }
    }
}
