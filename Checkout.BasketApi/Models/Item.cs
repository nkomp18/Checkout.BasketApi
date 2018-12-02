namespace Checkout.BasketApi.Models
{
    public sealed class Item
    {
        public int ProductId { get; }

        public int Quantity { get; set; }

        public Item(int productId, int quantity)
        {
            this.ProductId = productId;
            this.Quantity = quantity;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;

            var item = (Item)obj;
            return this.ProductId == item.ProductId && this.Quantity == item.Quantity;
        }
    }
}