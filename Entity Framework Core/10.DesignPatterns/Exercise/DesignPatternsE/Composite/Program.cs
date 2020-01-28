namespace Composite
{
    using System;

    public class Program
    {
        public static void Main(string[] args)
        {
            var phone = new SingleGift("Phone", 500);
            phone.CalculateTotalPrice();
            Console.WriteLine();

            var rootBox = new CompositeGift("RootBox", 0);
            var truckToy = new SingleGift("Truck toy", 200);
            var plainToy = new SingleGift("Plain toy", 300);

            rootBox.Add(truckToy);
            rootBox.Add(plainToy);

            var childBox = new CompositeGift("Child Box", 0);
            var soldierToy = new SingleGift("Soldier toy", 400);

            childBox.Add(soldierToy);
            rootBox.Add(childBox);

            Console.WriteLine($"Total price from Composite present is: {rootBox.CalculateTotalPrice()}");
        }
    }
}
