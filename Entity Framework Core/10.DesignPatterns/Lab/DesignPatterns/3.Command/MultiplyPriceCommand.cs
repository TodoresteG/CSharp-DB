namespace _3.Command
{
    public class MultiplyPriceCommand : ICommand
    {
        private readonly Product product;
        private readonly int amount;

        public MultiplyPriceCommand(Product product, int amount)
        {
            this.product = product;
            this.amount = amount;
        }

        public string ExecuteAction()
        {
            return this.product.MultiplyPrice(amount);
        }
    }
}
