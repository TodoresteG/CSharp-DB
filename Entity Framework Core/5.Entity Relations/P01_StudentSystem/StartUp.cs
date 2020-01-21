namespace P01_StudentSystem
{
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Data.Models;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            using (var context = new StudentSystemContext())
            {
                context.Database.Migrate();
            }
        }
    }
}
