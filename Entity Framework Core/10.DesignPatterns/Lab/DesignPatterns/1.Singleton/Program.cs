using System;

namespace _1.Singleton
{
    class Program
    {
        static void Main(string[] args)
        {
            var db = SingletonDataContainer.Instance;
            Console.WriteLine(db.GetPopulation("Washington, D.C"));

            var db1 = SingletonDataContainer.Instance;
            Console.WriteLine(db.GetPopulation("London"));
        }
    }
}
