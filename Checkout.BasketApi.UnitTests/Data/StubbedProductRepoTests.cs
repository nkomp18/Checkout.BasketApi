using System.Threading.Tasks;
using Checkout.BasketApi.Core.Exceptions;
using Checkout.BasketApi.Data;
using Checkout.BasketApi.Models;
using NUnit.Framework;

namespace Checkout.BasketApi.UnitTests.Data
{
    public sealed class StubbedProductRepoTests
    {
        //System under test
        private StubbedProductRepo _sut;
        private Product product1;

        [SetUp]
        public void SetUp()
        {
            _sut = new StubbedProductRepo();
            product1 = new Product(1, "A", 10);
        }

        [Test]
        public async Task ProductExistsAsync_ReturnsFalse()
        {
            Assert.IsFalse(await _sut.ProductExistsAsync(100));
        }

        [Test]
        public async Task ProductExistsAsync_ReturnsTrue()
        {
            Assert.IsTrue(await _sut.ProductExistsAsync(product1.Id));
        }

        [Test]
        public async Task GetProductAsync_ValidId_ReturnsProduct()
        {
            var product = await _sut.GetProductAsync(product1.Id);

            Assert.AreEqual(product1.Id, product.Id);
            Assert.AreEqual(product1.Name, product.Name);
            Assert.AreEqual(product1.Price, product.Price);
        }

        [Test]
        public void GetProductAsync_UnknownId_ProductNotFoundException()
        {
            Assert.ThrowsAsync<ProductNotFoundException>(async () => await _sut.GetProductAsync(100));
        }
    }
}
