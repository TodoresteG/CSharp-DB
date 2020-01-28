namespace PetStore.Services.Implementations
{
    using System.Collections.Generic;
    using Data;

    using Models.Brand;
    using Models.Toy;
    using Data.Models;
    using System;
    using System.Linq;

    public class BrandService : IBrandService
    {
        private readonly PetStoreDbContext data;

        public BrandService(PetStoreDbContext data)
        {
            this.data = data;
        }

        public int Create(string name)
        {
            if (name == null)
            {
                throw new InvalidOperationException($"Brand name cannot be null");
            }

            if (name.Length > ModelValidator.NameMaxLength)
            {
                throw new InvalidOperationException($"Brand name cannot be more than {ModelValidator.NameMaxLength} characters");
            }

            if (this.data.Brands.Any(b => b.Name == name))
            {
                throw new InvalidOperationException("Brand name already exists");
            }

            var brand = new Brand() 
            {
                Name = name
            };

            this.data.Brands.Add(brand);
            this.data.SaveChanges();

            return brand.Id;
        }

        public BrandWithToysServiceModel FindByIdWithToys(int id)
        {
            return this.data
                      .Brands
                      .Where(b => b.Id == id)
                      .Select(b => new BrandWithToysServiceModel
                      {
                          Name = b.Name,
                          Toys = b.Toys.Select(t => new ToyListingServiceModel
                          {
                              Id = t.Id,
                              Name = t.Name,
                              Price = t.Price,
                              TotalOrders = t.Orders.Count
                          })
                      })
                      .FirstOrDefault();
        }

        public IEnumerable<BrandListingServiceModel> SearchByName(string name)
        {
            return this.data
                    .Brands
                    .Where(b => b.Name.ToLower().Contains(name.ToLower()))
                    .Select(b => new BrandListingServiceModel
                    {
                        Id = b.Id,
                        Name = b.Name
                    })
                    .ToList();
        }
    }
}
