﻿namespace Template
{
    using System;

    public class Sourdough : Bread
    {
        public override void Bake()
        {
            Console.WriteLine("Baking the Sourdough Bread. (30 minutes)");
        }

        public override void MixIngredients()
        {
            Console.WriteLine("Gathering Ingredients for Sourdough Bread.");
        }
    }
}