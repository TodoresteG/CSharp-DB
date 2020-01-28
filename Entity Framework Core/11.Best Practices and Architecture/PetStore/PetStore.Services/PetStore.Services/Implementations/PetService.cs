namespace PetStore.Services.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Data;
    using Data.Models;
    using PetStore.Services.Models.Pet;

    public class PetService : IPetService
    {
        private const int PetsPageSize = 25;

        private readonly PetStoreDbContext data;
        private readonly IBreedService breedService;
        private readonly ICategoryService categoryService;
        private readonly IUserService userService;

        public PetService(PetStoreDbContext data, IBreedService breedService, ICategoryService categoryService, IUserService userService)
        {
            this.data = data;
            this.breedService = breedService;
            this.categoryService = categoryService;
            this.userService = userService;
        }

        public IEnumerable<PetListingServiceModel> All(int page = 1)
        {
            return this.data
                .Pets
                .Skip((page - 1) * PetsPageSize)
                .Take(PetsPageSize)
                .Select(p => new PetListingServiceModel
                {
                    Id = p.Id,
                    Description = p.Description,
                    Breed = p.Breed.Name,
                    Category = p.Category.Name,
                    Price = p.Price
                })
                .ToList();
        }

        public void BuyPet(Gender gender, DateTime dateOfBirth, decimal price, string description, int breedId, int categoryId)
        {
            if (price < 0)
            {
                throw new ArgumentException("Price of the pet cannot be less than zero");
            }

            if (!this.breedService.Exists(breedId))
            {
                throw new ArgumentException("Given breed id does not exists in the database");
            }

            if (!this.categoryService.Exists(categoryId))
            {
                throw new ArgumentException("Given category id does not exists in the database");
            }

            var pet = new Pet() 
            {
                Gender = gender,
                DateOfBirth = dateOfBirth,
                Price = price,
                Description = description,
                BreedId = breedId,
                CategoryId = categoryId
            };

            this.data.Pets.Add(pet);
            this.data.SaveChanges();
        }

        public bool Delete(int id)
        {
            var pet = this.data
                .Pets
                .Find(id);

            if (pet == null)
            {
                return false;
            }

            this.data.Pets.Remove(pet);
            this.data.SaveChanges();

            return true;
        }

        public PetDetailsServiceModel Details(int id)
        {
            return this.data
                .Pets
                .Where(p => p.Id == id)
                .Select(p => new PetDetailsServiceModel
                {
                    Id = p.Id,
                    Gender = p.Gender,
                    DateOfBirth = p.DateOfBirth,
                    Description = p.Description,
                    Price = p.Price,
                    Category = p.Category.Name,
                    Breed = p.Breed.Name
                })
                .FirstOrDefault();
        }

        public bool Exists(int petId)
        {
            return this.data.Pets.Any(p => p.Id == petId);
        }

        public void SellPet(int petId, int userId)
        {
            if (!this.Exists(petId))
            {
                throw new ArgumentException("Given pet does not exists in the database");
            }

            if (!this.userService.Exists(userId))
            {
                throw new ArgumentException("Given user does not exists int the database");
            }

            var pet = this.data.Pets.Find(petId); 

            var order = new Order() 
            {
                PurchaseDate = DateTime.UtcNow,
                Status = OrderStatus.Done,
                UserId = userId
            };

            this.data.Orders.Add(order);
            pet.Order = order;

            this.data.SaveChanges();
        }

        public int Total() => this.data.Pets.Count();
    }
}
