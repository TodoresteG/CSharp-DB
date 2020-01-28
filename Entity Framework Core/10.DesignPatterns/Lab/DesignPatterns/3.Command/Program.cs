namespace _3.Command
{
    using System;
    using System.Text;

    class Program
    {
        static void Main(string[] args)
        {
            var sb = new StringBuilder();

            var modifyPrice = new ModifyPrice();
            var product = new Product("Phone", 500);

            sb.AppendLine(Execute(product, modifyPrice, new IncreasePriceCommand(product, 100)));

            sb.AppendLine(Execute(product, modifyPrice, new IncreasePriceCommand(product, 50)));

            sb.AppendLine(Execute(product, modifyPrice, new DecreasePriceCommand(product, 25)));

            sb.AppendLine(Execute(product, modifyPrice, new MultiplyPriceCommand(product, 2)));

            sb.AppendLine(Execute(product, modifyPrice, new DevidePriceCommand(product, 4)));

            Console.WriteLine(sb.ToString().TrimEnd());
            Console.WriteLine(product);
        }

        private static string Execute(Product product, ModifyPrice modifyPrice, ICommand productCommand) 
        {
            modifyPrice.SetCommand(productCommand);
            return modifyPrice.Invoke();
        }
    }
}
