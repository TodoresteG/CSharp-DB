namespace PetStore.Services
{
    using System;
    using Models.Food;
    using Models.Category;

    public interface IFoodService
    {
        int BuyFromDistributor(string name, double weigth, decimal price, DateTime expirationDate, int brandId, int categoryId);

        int BuyFromDistributor(AddingFoodServiceModel model);

        FoodListingService SearchFoodByNameWithCategory(string name);

        void SellFoodToUser(int foodId, int userId);

        bool Exists(int foodId);
    }
}
