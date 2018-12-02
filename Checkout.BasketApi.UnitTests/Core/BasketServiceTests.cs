using System;
using System.Threading.Tasks;
using Checkout.BasketApi.Core;
using Checkout.BasketApi.Data;
using Checkout.BasketApi.Models;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace Checkout.BasketApi.UnitTests.Core
{
    public sealed class BasketServiceTests
    {
        //System under test
        private BasketService _sut;

        private Mock<ILogger<BasketService>> mockLogger;
        private Mock<IBasketRepo> mockBasketRepository;

        private Guid userId;

        [SetUp]
        public void SetUp()
        {
            mockLogger = new Mock<ILogger<BasketService>>();
            mockBasketRepository = new Mock<IBasketRepo>();

            _sut = new BasketService(mockBasketRepository.Object, mockLogger.Object);
            userId = Guid.NewGuid();
        }

        [Test]
        public async Task GetBasket_WhenBasketIsNull_Initialize()
        {
            // Arrange
            mockBasketRepository.Setup(m => m.BasketExistsAsync(userId)).Returns(Task.FromResult(false));
            mockBasketRepository.Setup(m => m.GetBasketAsync(userId)).Returns(Task.FromResult(new Basket(userId)));

            // Act
            var basket = await _sut.GetAsync(userId);

            // Assert
            mockBasketRepository.Verify(b => b.CreateAsync(userId), Times.Once);
            Assert.AreEqual(userId, basket.UserId);
            Assert.AreEqual(0, basket.BasketItems.Count);
        }

        [Test]
        public async Task GetBasket_WhenBasketNotNull_SkipInitialization()
        {
            // Arrange
            mockBasketRepository.Setup(m => m.BasketExistsAsync(userId)).Returns(Task.FromResult(true));
            mockBasketRepository.Setup(m => m.GetBasketAsync(userId)).Returns(Task.FromResult(new Basket(userId)));

            // Act
            var basket = await _sut.GetAsync(userId);

            // Assert
            mockBasketRepository.Verify(b => b.CreateAsync(userId), Times.Never);
            Assert.AreEqual(userId, basket.UserId);
            Assert.AreEqual(0, basket.BasketItems.Count);
        }

        [Test]
        public async Task AddItem_WhenBasketIsNull_Initialize()
        {
            // Arrange
            mockBasketRepository.Setup(m => m.BasketExistsAsync(userId)).Returns(Task.FromResult(false));

            // Act
            await _sut.AddItemAsync(userId, 1, 1);

            // Assert
            mockBasketRepository.Verify(b => b.CreateAsync(userId), Times.Once);
        }

        [Test]
        public async Task AddItem_WhenBasketNotNull_SkipInitialization()
        {
            // Arrange
            mockBasketRepository.Setup(m => m.BasketExistsAsync(userId)).Returns(Task.FromResult(true));

            // Act
            await _sut.AddItemAsync(userId, 1, 1);

            // Assert
            mockBasketRepository.Verify(b => b.CreateAsync(userId), Times.Never);
        }

        [Test]
        public async Task UpdateItem_WhenBasketIsNull_Initialize()
        {
            // Arrange
            mockBasketRepository.Setup(m => m.BasketExistsAsync(userId)).Returns(Task.FromResult(false));

            // Act
            await _sut.UpdateItemAsync(userId, 1, 1);

            // Assert
            mockBasketRepository.Verify(b => b.CreateAsync(userId), Times.Once);
        }

        [Test]
        public async Task UpdateItem_WhenBasketNotNull_SkipInitialization()
        {
            // Arrange
            mockBasketRepository.Setup(m => m.BasketExistsAsync(userId)).Returns(Task.FromResult(true));

            // Act
            await _sut.UpdateItemAsync(userId, 1, 1);

            // Assert
            mockBasketRepository.Verify(b => b.CreateAsync(userId), Times.Never);
        }

        [Test]
        public async Task ClearBasket_WhenBasketIsNull_Initialize()
        {
            // Arrange
            mockBasketRepository.Setup(m => m.BasketExistsAsync(userId)).Returns(Task.FromResult(false));

            // Act
            await _sut.ClearAsync(userId);

            // Assert
            mockBasketRepository.Verify(b => b.CreateAsync(userId), Times.Once);
        }

        [Test]
        public async Task ClearBasket_WhenBasketNotNull_SkipInitialization()
        {
            // Arrange
            mockBasketRepository.Setup(m => m.BasketExistsAsync(userId)).Returns(Task.FromResult(true));

            // Act
            await _sut.ClearAsync(userId);

            // Assert
            mockBasketRepository.Verify(b => b.CreateAsync(userId), Times.Never);
        }
    }
}
