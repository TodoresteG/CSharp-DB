namespace _3.Command
{
    public class DecreasePriceCommand : ICommand
    {
        private readonly Product product;
        private readonly int amount;

        public DecreasePriceCommand(Product product, int amount)
        {
            this.product = product;
            this.amount = amount;
        }

        public string ExecuteAction()
        {
            return this.product.DecreasePrice(this.amount);
        }
    }
}
