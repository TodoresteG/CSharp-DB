namespace PetStore.Services.Implementations
{
    using System;
    using System.Linq;

    using Data;
    using Data.Models;
    using Services.Models.Food;
    using Services.Models.Category;

    public class FoodService : IFoodService
    {
        private readonly PetStoreDbContext data;
        private readonly IUserService userService;

        public FoodService(PetStoreDbContext data, IUserService userService)
        {
            this.data = data;
            this.userService = userService;
        }

        public int BuyFromDistributor(string name, double weigth, decimal price, DateTime expirationDate, int brandId, int categoryId)
        {
            ValidateFood(name, brandId, categoryId, weigth, price);

            var food = new Food() 
            {
                Name = name,
                Weight = weigth,
                Price = price,
                ExpirationDate = expirationDate,
                BrandId = brandId,
                CategoryId = categoryId
            };

            this.data.Food.Add(food);
            this.data.SaveChanges();

            return food.Id;
        }

        public int BuyFromDistributor(AddingFoodServiceModel model)
        {
            ValidateFood(model.Name, model.BrandId, model.CategoryId, model.Weight, model.Price);

            var food = new Food() 
            {
                Name = model.Name,
                Weight = model.Weight,
                Price = model.Price,
                ExpirationDate = model.ExpirationDate,
                BrandId = model.BrandId,
                CategoryId = model.CategoryId
            };

            this.data.Food.Add(food);
            this.data.SaveChanges();

            return food.Id;
        }

        public bool Exists(int foodId)
        {
            return this.data.Food.Any(f => f.Id == foodId);
        }

        public FoodListingService SearchFoodByNameWithCategory(string name)
        {
            return this.data
                    .Food
                    .Select(f => new FoodListingService
                    {
                        Name = f.Name,
                        Price = f.Price,
                        ExpirationDate = f.ExpirationDate,
                        Category = new CategoryListingServiceModel
                        {
                            Id = f.CategoryId,
                            Name = f.Category.Name,
                            Description = f.Category.Description
                        }
                    })
                    .FirstOrDefault(f => f.Name == name);
        }

        public void SellFoodToUser(int foodId, int userId)
        {
            if (!this.Exists(foodId) || !userService.Exists(userId))
            {
                throw new InvalidOperationException("Given food or user do not exist in the database");
            }

            var order = new Order()
            {
                PurchaseDate = DateTime.UtcNow,
                Status = OrderStatus.Done,
                UserId = userId
            };

            var foodOrder = new FoodOrder() 
            {
                FoodId = foodId,
                Order = order
            };

            this.data.Orders.Add(order);
            this.data.FoodOrders.Add(foodOrder);

            this.data.SaveChanges();
        }

        private void ValidateFood(string name, int brandId, int categoryId, double weigth, decimal price) 
        {
            if (name.Length > ModelValidator.NameMaxLength)
            {
                throw new ArgumentException($"Food name cannot bew more than {ModelValidator.NameMaxLength} characters");
            }

            if (String.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Food name have to be filled");
            }

            if (!this.data.Brands.Any(b => b.Id == brandId) || !this.data.Categories.Any(c => c.Id == categoryId))
            {
                throw new InvalidOperationException("Brand id and Category id have to be filled");
            }

            if (weigth < 0 || price < 0)
            {
                throw new ArgumentException("Weight and price have to be positive numbers");
            }

            if (this.data.Food.Any(f => f.Name == name))
            {
                throw new InvalidOperationException("Food with given name already exists");
            }
        }
    }
}
