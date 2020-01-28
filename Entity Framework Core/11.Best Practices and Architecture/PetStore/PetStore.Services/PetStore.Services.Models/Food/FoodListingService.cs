namespace PetStore.Services.Models.Food
{
    using System;
    using Category;

    public class FoodListingService
    {
        public string Name { get; set; }

        public decimal Price { get; set; }

        public DateTime ExpirationDate { get; set; }

        public CategoryListingServiceModel Category { get; set; }
    }
}
