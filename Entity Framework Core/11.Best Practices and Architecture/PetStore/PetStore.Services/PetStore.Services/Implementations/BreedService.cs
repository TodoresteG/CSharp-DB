namespace PetStore.Services.Implementations
{
    using Data;
    using Data.Models;

    using System;
    using System.Linq;

    public class BreedService : IBreedService
    {
        private readonly PetStoreDbContext data;

        public BreedService(PetStoreDbContext data)
        {
            this.data = data;
        }

        public int Add(string name)
        {
            if (String.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Breed name cannot be null or whitespace");
            }

            if (name.Length > ModelValidator.NameMaxLength)
            {
                throw new ArgumentException($"Breed name cannot be more than {ModelValidator.NameMaxLength} characters");
            }

            var breed = new Breed() 
            {
                Name = name
            };

            this.data.Breeds.Add(breed);
            this.data.SaveChanges();

            return breed.Id;
        }

        public bool Exists(int breedId)
        {
            return this.data.Breeds.Any(b => b.Id == breedId);
        }
    }
}
