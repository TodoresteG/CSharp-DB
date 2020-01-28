namespace PetStore
{
    using Data;
    using Data.Models;
    using Services.Implementations;
    using System;
    using System.Linq;

    class Program
    {
        static void Main(string[] args)
        {
            using var context = new PetStoreDbContext();

            //var brandService = new BrandService(context);
            //var categoryService = new CategoryService(context);
            //var breedService = new BreedService(context);
            //var userService = new UserService(context);
            //var toyService = new ToyService(context, userService);
            //var foodService = new FoodService(context, userService);
            //var petService = new PetService(context, breedService, categoryService, userService);

            //petService.SellPet(1, 1);

            for (int i = 0; i < 10; i++)
            {
                var breed = new Breed
                {
                    Name = "Random Breed" + i
                };

                context.Breeds.Add(breed);
            }

            context.SaveChanges();

            for (int i = 0; i < 20; i++)
            {
                var category = new Category
                {
                    Name = "Random Category" + i,
                    Description = "Random Description" + i
                };

                context.Categories.Add(category);
            }

            context.SaveChanges();

            for (int i = 0; i < 100; i++)
            {
                var categoryId = context
                    .Categories
                    .OrderBy(c => Guid.NewGuid())
                    .Select(c => c.Id)
                    .FirstOrDefault();

                var breedId = context
                    .Breeds
                    .OrderBy(b => Guid.NewGuid())
                    .Select(b => b.Id)
                    .FirstOrDefault();

                var pet = new Pet
                {
                    DateOfBirth = DateTime.UtcNow.AddDays(-80 + i),
                    Price = 50 + i * 2,
                    Gender = (Gender)(i % 2),
                    Description = "Some random description" + i,
                    CategoryId = categoryId,
                    BreedId = breedId
                };

                context.Pets.Add(pet);
            }

            context.SaveChanges();
        }
    }
}
