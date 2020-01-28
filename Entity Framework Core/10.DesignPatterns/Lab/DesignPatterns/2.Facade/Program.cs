namespace _2.Facade
{
    using System;

    class Program
    {
        static void Main(string[] args)
        {
            var car = new CarBuilderFacade()
                .Info
                .WithType("BMW")
                .WithColor("Black")
                .WithNumberOfDoors(5)
                .Built
                .InCity("Sofia")
                .AtAddress("Tintyava")
                .Build();

            Console.WriteLine(car);
        }
    }
}
