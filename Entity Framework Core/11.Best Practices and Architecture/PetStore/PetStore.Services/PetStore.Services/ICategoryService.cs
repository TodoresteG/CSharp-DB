namespace PetStore.Services
{
    using System.Collections.Generic;
    using Models.Category;

    public interface ICategoryService
    {
        int Create(string name, string description);

        IEnumerable<CategoryListingServiceModel> SearchByName(string name);

        bool Exists(int categoryId);

        IEnumerable<CategoryListingServiceModel> All();

        void Create(CategoryCreateServiceModel model);

        CategoryDetailsServiceModel Details(int id);

        void Edit(CategoryEditServiceModel model);
    }
}
