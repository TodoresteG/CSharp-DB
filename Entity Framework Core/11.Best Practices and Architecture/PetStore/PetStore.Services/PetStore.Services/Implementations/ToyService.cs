namespace PetStore.Services.Implementations
{
    using System;
    using System.Linq;

    using Data.Models;
    using Data;
    using PetStore.Services.Models.Toy;

    public class ToyService : IToyService
    {
        private readonly PetStoreDbContext data;
        private readonly IUserService userService;

        public ToyService(PetStoreDbContext data, IUserService userService)
        {
            this.data = data;
            this.userService = userService;
        }

        public int BuyFromDistributor(string name, string description, decimal price, int brandId, int categoryId)
        {
            ValidateToy(name, brandId, categoryId, price);

            var toy = new Toy() 
            {
                Name = name,
                Description = description,
                Price = price,
                BrandId = brandId,
                CategoryId = categoryId
            };

            this.data.Toys.Add(toy);
            this.data.SaveChanges();

            return toy.Id;
        }

        public int BuyFromDistributor(AddingToyServiceModel model)
        {
            ValidateToy(model.Name, model.BrandId, model.CategoryId, model.Price);

            var toy = new Toy() 
            {
                Name = model.Name,
                Description = model.Description,
                Price = model.Price,
                BrandId = model.BrandId,
                CategoryId = model.CategoryId
            };

            this.data.Toys.Add(toy);
            this.data.SaveChanges();

            return toy.Id;
        }

        public bool Exists(int toyId)
        {
            return this.data.Toys.Any(t => t.Id == toyId);
        }

        public void SellToyToUser(int toyId, int userId)
        {
            if (!this.Exists(toyId) || !userService.Exists(userId))
            {
                throw new InvalidOperationException("Given toy or user dont exist in the database");
            }

            var order = new Order() 
            {
                PurchaseDate = DateTime.UtcNow,
                Status = OrderStatus.Done,
                UserId = userId
            };

            var toyOrder = new ToyOrder() 
            {
                ToyId = toyId,
                Order = order
            };

            this.data.Orders.Add(order);
            this.data.ToyOrders.Add(toyOrder);

            this.data.SaveChanges();
        }

        private void ValidateToy(string name, int brandId, int categoryId, decimal price)
        {
            if (String.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Name cannot be null or whitespace");
            }

            if (!this.data.Brands.Any(b => b.Id == brandId) || !this.data.Categories.Any(c => c.Id == categoryId))
            {
                throw new ArgumentException("Received brand or category doens't exists");
            }

            if (name.Length > ModelValidator.NameMaxLength)
            {
                throw new ArgumentException($"Toy name cannot be more than {ModelValidator.NameMaxLength} characters");
            }

            if (price < 0)
            {
                throw new ArgumentException("Price have to be a positive number");
            }
        }
    }
}
