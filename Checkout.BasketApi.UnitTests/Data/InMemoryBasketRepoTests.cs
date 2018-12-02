using System;
using System.Linq;
using System.Threading.Tasks;
using Checkout.BasketApi.Core.Exceptions;
using Checkout.BasketApi.Data;
using Checkout.BasketApi.Models;
using NUnit.Framework;

namespace Checkout.BasketApi.UnitTests.Data
{
    public sealed class InMemoryBasketRepoTests
    {
        //System under test
        private InMemoryBasketRepo _sut;
        private Guid userId;

        private Product product1;
        private Product product2;

        [SetUp]
        public void SetUp()
        {
            _sut = new InMemoryBasketRepo();
            userId = Guid.NewGuid();

            product1 = new Product(1, "A", 10);
            product2 = new Product(2, "B", 15);
        }

        [Test]
        public async Task CreateBasket_DoesNotExist_Success()
        {
            await _sut.CreateAsync(userId);

            var basket = await _sut.GetBasketAsync(userId);
            Assert.NotNull(basket);
            Assert.AreEqual(userId, basket.UserId);
        }

        [Test]
        public async Task CreateBasket_Exists_ThrowsRepositoryException()
        {
            await _sut.CreateAsync(userId);

            Assert.ThrowsAsync<RepositoryException>(async () => await _sut.CreateAsync(userId));
        }

        [Test]
        public void GetBasket_DoesNotExist_ThrowsRepositoryException()
        {
            Assert.ThrowsAsync<RepositoryException>(async () => await _sut.GetBasketAsync(userId));
        }

        [Test]
        public void AddItem_DoesNotExist_ThrowsRepositoryException()
        {
            Assert.ThrowsAsync<RepositoryException>(async () => await _sut.AddItemAsync(userId, new Item(1, 10)));
        }

        [TestCase(0)]
        [TestCase(-100)]
        public async Task AddItem_Exists_InvalidQuantity_ThrowsRepositoryException(int quantity)
        {
            await _sut.CreateAsync(userId);

            Assert.ThrowsAsync<RepositoryException>(async () => await _sut.AddItemAsync(userId, new Item(product1.Id, quantity)));
        }

        [TestCase(10)]
        [TestCase(100)]
        public async Task AddItem_Exist_Success(int quantity)
        {
            await _sut.CreateAsync(userId);

            await _sut.AddItemAsync(userId, new Item(product1.Id, quantity));

            var basket = await _sut.GetBasketAsync(userId);
            Assert.NotNull(basket);
            Assert.AreEqual(userId, basket.UserId);
            Assert.AreEqual(1, basket.BasketItems.Count);

            var basketItem = basket.BasketItems.Single();

            Assert.AreEqual(product1.Id, basketItem.ProductId);
            Assert.AreEqual(quantity, basketItem.Quantity);
        }

        [TestCase(10)]
        [TestCase(100)]
        public async Task AddItem_DuplicateProduct_ShouldAggregateQuantity(int quantity)
        {
            await _sut.CreateAsync(userId);

            await _sut.AddItemAsync(userId, new Item(product1.Id, quantity));
            await _sut.AddItemAsync(userId, new Item(product1.Id, quantity));

            var basket = await _sut.GetBasketAsync(userId);
            Assert.NotNull(basket);
            Assert.AreEqual(userId, basket.UserId);
            Assert.AreEqual(1, basket.BasketItems.Count);

            var basketItem = basket.BasketItems.Single();

            Assert.AreEqual(product1.Id, basketItem.ProductId);
            Assert.AreEqual(quantity * 2, basketItem.Quantity);
        }

        [Test]
        public void UpdateItem_DoesNotExist_ThrowsRepositoryException()
        {
            Assert.ThrowsAsync<RepositoryException>(async () => await _sut.UpdateItemAsync(userId, product1.Id, 1));
        }

        [TestCase(2)]
        [TestCase(15)]
        public async Task UpdateItem_EmptyBasket_AddItem(int quantity)
        {
            await _sut.CreateAsync(userId);

            await _sut.UpdateItemAsync(userId, product1.Id, quantity);

            var basket = await _sut.GetBasketAsync(userId);
            Assert.NotNull(basket);
            Assert.AreEqual(userId, basket.UserId);
            Assert.AreEqual(1, basket.BasketItems.Count);

            var basketItem = basket.BasketItems.Single();

            Assert.AreEqual(product1.Id, basketItem.ProductId);
            Assert.AreEqual(quantity, basketItem.Quantity);
        }

        [Test]
        public async Task UpdateItem_InvalidQuantity_ThrowsRepositoryException()
        {
            await _sut.CreateAsync(userId);

            await _sut.AddItemAsync(userId, new Item(product1.Id, 10));

            Assert.ThrowsAsync<RepositoryException>(async () => await _sut.UpdateItemAsync(userId, product1.Id, 0));
        }

        [Test]
        public async Task UpdateItem_ShouldUpdateSpecificProductOnly()
        {
            await _sut.CreateAsync(userId);

            await _sut.AddItemAsync(userId, new Item(product1.Id, 1));
            await _sut.AddItemAsync(userId, new Item(product2.Id, 1));

            await _sut.UpdateItemAsync(userId, product1.Id, 145);
            await _sut.UpdateItemAsync(userId, product2.Id, 321);

            var basket = await _sut.GetBasketAsync(userId);
            Assert.NotNull(basket);
            Assert.AreEqual(userId, basket.UserId);
            Assert.AreEqual(2, basket.BasketItems.Count);

            var basketItem = basket.BasketItems.Single(x => x.ProductId == product2.Id);
        
            Assert.AreEqual(321, basketItem.Quantity);
        }

        [Test]
        public void ClearBasket_DoesNotExist_ThrowsRepositoryException()
        {
            Assert.ThrowsAsync<RepositoryException>(async () => await _sut.ClearBasketAsync(userId));
        }

        [Test]
        public async Task ClearBasket_ShouldEmptyTheBasket()
        {
            await _sut.CreateAsync(userId);

            await _sut.AddItemAsync(userId, new Item(product1.Id, 1));
            await _sut.AddItemAsync(userId, new Item(product2.Id, 1));

            await _sut.ClearBasketAsync(userId);
            var basket = await _sut.GetBasketAsync(userId);
            Assert.NotNull(basket);
            Assert.AreEqual(userId, basket.UserId);
            Assert.AreEqual(0, basket.BasketItems.Count);
        }
    }
}
