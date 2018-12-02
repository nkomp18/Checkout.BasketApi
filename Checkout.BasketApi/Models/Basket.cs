using System;
using System.Collections.Generic;
using System.Linq;

namespace Checkout.BasketApi.Models
{
    public sealed class Basket
    {
        public Guid UserId { get; }

        public List<Item> BasketItems { get; }

        public Basket(Guid userId)
        {
            this.UserId = userId;
            this.BasketItems = new List<Item>();    
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;

            var basket = (Basket) obj;
            return this.UserId == basket.UserId && BasketItems.SequenceEqual(basket.BasketItems);
        }
    }
}
