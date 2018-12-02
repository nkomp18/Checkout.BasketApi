using System;
using Checkout.BasketApi.Models;
using NUnit.Framework;

namespace Checkout.BasketApi.HttpClient.IntegrationTests
{
    public sealed class BaskApiClientTests
    {
        private BasketApiServer _basketApiServer;
        private BasketApiHttpClient _client;

        private readonly Guid userId = Guid.NewGuid();

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            this._basketApiServer = new BasketApiServer();
            this._client = new BasketApiHttpClient(this._basketApiServer.TestServer.CreateClient());
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            this._basketApiServer.Dispose();
        }

        [SetUp]
        public void SetUp()
        {
            _client.ClearBasket(userId);
        }

        [Test]
        public void GetBasket_NoBasket_EmptyBasket()
        {
            var expectedBasket = new Basket(userId);
            var basket = _client.GetBasket(userId);

            Assert.AreEqual(expectedBasket, basket);
        }

        [Test]
        public void GetBasket_WithAddedItems_ReturnsCorrectBasket()
        {
            _client.AddItem(userId, 1, 15);
            _client.AddItem(userId, 2, 25);
            _client.AddItem(userId, 1, 5);

            var expectedBasket = new Basket(userId)
            {
                BasketItems =
                {
                    new Item(1, 20),
                    new Item(2, 25)
                }
            };
            var basket = _client.GetBasket(userId);

            Assert.AreEqual(expectedBasket, basket);
        }

        [Test]
        public void GetBasket_WithUpdatedItems_ReturnsCorrectBasket()
        {
            _client.AddItem(userId, 1, 15);
            _client.AddItem(userId, 2, 25);
            _client.AddItem(userId, 1, 5);

            _client.UpdateQuantity(userId, 2, 30);
            _client.UpdateQuantity(userId, 3, 1);

            var expectedBasket = new Basket(userId)
            {
                BasketItems =
                {
                    new Item(1, 20),
                    new Item(2, 30),
                    new Item(3, 1)
                }
            };
            var basket = _client.GetBasket(userId);

            Assert.AreEqual(expectedBasket, basket);
        }

        [Test]
        public void GetBasket_AfterClear_ReturnsEmpty()
        {
            _client.AddItem(userId, 1, 15);
            _client.AddItem(userId, 2, 25);
            _client.AddItem(userId, 3, 5);

            _client.ClearBasket(userId);

            var expectedBasket = new Basket(userId);
            
            var basket = _client.GetBasket(userId);

            Assert.AreEqual(expectedBasket, basket);
        }
    }
}
