namespace Composite
{
    using System;
    using System.Collections.Generic;

    public class CompositeGift : GiftBase, IGiftOperation
    {
        private List<GiftBase> gifts;

        public CompositeGift(string name, int price) 
            : base(name, price)
        {
            this.gifts = new List<GiftBase>();
        }

        public void Add(GiftBase gift)
        {
            this.gifts.Add(gift);
        }

        public void Remove(GiftBase gift)
        {
            this.gifts.Remove(gift);
        }

        public override int CalculateTotalPrice()
        {
            var total = 0;

            Console.WriteLine($"{this.name} contains the following products with prices:");

            foreach (var gift in this.gifts)
            {
                total += gift.CalculateTotalPrice();
            }

            return total;
        }
    }
}
