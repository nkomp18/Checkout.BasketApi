namespace Checkout.BasketApi.Models
{
    /// <summary>
    /// Didn't end up being used
    /// </summary>
    public sealed class Product
    {
        public int Id { get; }

        public string Name { get; }

        public decimal Price { get; }

        public Product(int id, string name, decimal price)
        {
            this.Id = id;
            this.Name = name;
            this.Price = price;
        }
    }
}
