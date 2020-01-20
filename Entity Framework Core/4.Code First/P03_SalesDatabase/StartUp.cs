namespace P03_SalesDatabase
{
    using Data;
    using Microsoft.EntityFrameworkCore;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            using (var context = new SalesContext())
            {
                context.Database.Migrate();
            }
        }
    }
}
