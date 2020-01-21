namespace BillsPaymentSystem.App
{
    using BillsPaymentSystem.Data;
    using Microsoft.EntityFrameworkCore;

    class StartUp
    {
        static void Main(string[] args)
        {
            using (var context = new BillsPaymentSystemContext())
            {
                context.Database.Migrate();
            }
        }
    }
}
