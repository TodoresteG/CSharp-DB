namespace _3.Command
{
    public class DevidePriceCommand : ICommand
    {
        private readonly Product product;
        private readonly int amount;

        public DevidePriceCommand(Product product, int amount)
        {
            this.product = product;
            this.amount = amount;
        }

        public string ExecuteAction()
        {
            return this.product.DevidePrice(this.amount);
        }
    }
}
