namespace P01_HospitalDatabase
{
    using Data;
    using Microsoft.EntityFrameworkCore;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            using (var db = new HospitalContext())
            {
                db.Database.Migrate();
            }
        }
    }
}
