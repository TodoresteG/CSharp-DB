namespace Prototype
{
    using System;

    public class Sandwich : SandwichPrototype
    {
        private string meat;
        private string cheese;
        private string bread;
        private string veggies;

        public Sandwich(string meat, string cheese, string bread, string veggies)
        {
            this.meat = meat;
            this.cheese = cheese;
            this.bread = bread;
            this.veggies = veggies;
        }

        public override SandwichPrototype Clone()
        {
            string ingredientList = this.GetIngredientList();
            Console.WriteLine("Cloning sandwich with ingredients: {0}", ingredientList);

            return MemberwiseClone() as SandwichPrototype;
        }

        private string GetIngredientList() 
        {
            return $"{this.bread}, {this.meat}, {this.cheese}, {this.veggies}";
        }
    }
}
