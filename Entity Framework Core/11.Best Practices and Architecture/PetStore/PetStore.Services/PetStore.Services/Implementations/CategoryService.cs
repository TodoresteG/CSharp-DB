namespace PetStore.Services.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Data;
    using Data.Models;
    using PetStore.Services.Models.Category;

    public class CategoryService : ICategoryService
    {
        private readonly PetStoreDbContext data;

        public CategoryService(PetStoreDbContext data)
        {
            this.data = data;
        }

        public IEnumerable<CategoryListingServiceModel> All()
        {
            return this.data
                .Categories
                .Select(c => new CategoryListingServiceModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description
                })
                .ToList();
        }

        public int Create(string name, string description)
        {
            if (name == null)
            {
                throw new ArgumentException("Category name cannot be null");
            }

            if (name.Length > ModelValidator.NameMaxLength)
            {
                throw new InvalidOperationException($"Category name cannot be more than {ModelValidator.NameMaxLength} characters");
            }

            if (this.data.Categories.Any(c => c.Name == name))
            {
                throw new InvalidOperationException("Category name already exists");
            }

            var category = new Category() 
            {
                Name = name,
                Description = description
            };

            this.data.Categories.Add(category);
            this.data.SaveChanges();

            return category.Id;
        }

        public void Create(CategoryCreateServiceModel model)
        {
            var category = new Category 
            {
                Name = model.Name,
                Description = model.Description
            };

            this.data.Categories.Add(category);
            this.data.SaveChanges();
        }

        public CategoryDetailsServiceModel Details(int id)
        {
            return this.data
                .Categories
                .Where(c => c.Id == id)
                .Select(c => new CategoryDetailsServiceModel
                {
                    Id = id,
                    Name = c.Name,
                    Description = c.Description
                })
                .FirstOrDefault();
        }

        public void Edit(CategoryEditServiceModel model)
        {
            var category = this.data
                .Categories
                .FirstOrDefault(c => c.Id == model.Id);

            category.Name = model.Name;
            category.Description = model.Description;

            this.data.Categories.Update(category);
            this.data.SaveChanges();
        }

        public bool Exists(int categoryId)
        {
            return this.data.Categories.Any(c => c.Id == categoryId);
        }

        public IEnumerable<CategoryListingServiceModel> SearchByName(string name)
        {
            return this.data
                    .Categories
                    .Where(c => c.Name == name)
                    .Select(c => new CategoryListingServiceModel
                    {
                        Id = c.Id,
                        Name = c.Name,
                        Description = c.Description
                    })
                    .ToList();
        }
    }
}
