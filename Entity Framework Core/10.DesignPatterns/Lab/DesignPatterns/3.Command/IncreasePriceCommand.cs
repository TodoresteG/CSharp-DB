namespace _3.Command
{
    public class IncreasePriceCommand : ICommand
    {
        private readonly Product product;
        private readonly int amount;

        public IncreasePriceCommand(Product product, int amount)
        {
            this.product = product;
            this.amount = amount;
        }

        public string ExecuteAction()
        {
            return this.product.IncreasePrice(this.amount);
        }
    }
}
