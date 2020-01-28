namespace Template
{
    using System;

    class Program
    {
        static void Main(string[] args)
        {
            var wholeWheat = new WholeWheat();
            wholeWheat.Make();

            var sourdough = new Sourdough();
            sourdough.Make();

            var twelveGrain = new TwelveGrain();
            twelveGrain.Make();
        }
    }
}
