namespace Template
{
    using System;

    public class WholeWheat : Bread
    {
        public override void Bake()
        {
            Console.WriteLine("Baking the WholeWheat Bread. (40 minutes)");
        }

        public override void MixIngredients()
        {
            Console.WriteLine("Gathering Ingredients for WholeWheat Bread.");
        }
    }
}
