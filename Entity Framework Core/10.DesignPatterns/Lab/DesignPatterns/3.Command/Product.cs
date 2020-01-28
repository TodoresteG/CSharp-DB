namespace _3.Command
{
    using System;

    public class Product
    {
        public Product(string name, int price)
        {
            this.Name = name;
            this.Price = price;
        }

        public string Name { get; set; }

        public int Price { get; set; }

        public string IncreasePrice(int amount)
        {
            this.Price += amount;
            return $"The price for the {this.Name} has been increased by {amount}$.";
        }

        public string DecreasePrice(int amount)
        {
            if (amount > this.Price)
            {
                return "Cannot decrease with bigger number than the price";
            }

            this.Price -= amount;
            return $"The price for the {this.Name} has been decreased by {amount}$.";
        }

        public string MultiplyPrice(int amount)
        {
            this.Price *= amount;
            return $"The price for the {this.Name} has been multiplied by {amount}.";
        }

        public string DevidePrice(int amount) 
        {
            if (amount > this.Price)
            {
                return "Cannot divide with bigger number than the price";
            }

            this.Price /= amount;
            return $"The price for the {this.Name} has been divided by {amount}.";
        }

        public override string ToString()
        {
            return $"Current price for the {this.Name} product is {this.Price}$.";
        }
    }
}
