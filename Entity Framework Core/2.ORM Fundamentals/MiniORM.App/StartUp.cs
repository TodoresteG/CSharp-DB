namespace MiniORM.App
{
    using MiniORM.App.Data;
    using MiniORM.App.Data.Entities;
    using System;
    using System.Linq;

    class StartUp
    {
        static void Main(string[] args)
        {
            //I really suck on this one and it wasted me a lot of time and gave me headaches... HOOOORAAAAAY

            string connectionString = @"Server=DESKTOP-NNVF465\SQLEXPRESS;Database=MiniORM;Integrated Security=True";

            var context = new SoftUniDbContext(connectionString);

            context.Employees.Add(new Employee
            {
                FirstName = "Gosho",
                LastName = "Inserted",
                DepartmentId = context.Departments.First().Id,
                IsEmployed = true
            });

            Employee employee = context.Employees.Last();
            employee.FirstName = "Modified";

            context.SaveChanges();
        }
    }
}
