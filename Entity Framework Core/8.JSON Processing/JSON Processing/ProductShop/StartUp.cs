namespace ProductShop
{
    using Data;
    using Models;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using ProductShop.DTO.Products;
    using ProductShop.DTO.Users;
    using ProductShop.DTO.Categories;

    public class StartUp
    {
        public static void Main()
        {
            //string json = File.ReadAllText("../../../Datasets/categories-products.json");

            Mapper.Initialize(cfg => cfg.AddProfile<ProductShopProfile>());

            using (var context = new ProductShopContext())
            {
                Console.WriteLine(GetUsersWithProducts(context));
            }

        }

        // 1
        public static string ImportUsers(ProductShopContext context, string inputJson)
        {
            var users = JsonConvert.DeserializeObject<List<User>>(inputJson);

            context.AddRange(users);
            context.SaveChanges();

            return $"Successfully imported {users.Count}";
        }

        // 2
        public static string ImportProducts(ProductShopContext context, string inputJson)
        {
            var products = JsonConvert.DeserializeObject<List<Product>>(inputJson);

            context.AddRange(products);
            context.SaveChanges();

            return $"Successfully imported {products.Count}";
        }

        // 3
        public static string ImportCategories(ProductShopContext context, string inputJson)
        {
            var categories = JsonConvert.DeserializeObject<List<Category>>(inputJson);

            var validCategories = categories.Where(c => c.Name != null && c.Name.Length >= 3).ToList();

            context.AddRange(validCategories);
            context.SaveChanges();

            return $"Successfully imported {validCategories.Count}";
        }

        // 4
        public static string ImportCategoryProducts(ProductShopContext context, string inputJson)
        {
            var categoryProducts = JsonConvert.DeserializeObject<List<CategoryProduct>>(inputJson);

            context.AddRange(categoryProducts);
            context.SaveChanges();

            return $"Successfully imported {categoryProducts.Count}";
        }

        // 5
        public static string GetProductsInRange(ProductShopContext context)
        {
            var products = context.Products
                .Where(p => p.Price >= 500 && p.Price <= 1000)
                .OrderBy(p => p.Price)
                .ProjectTo<ProductInRangeDTO>()
                .ToList();

            var productsJson = JsonConvert.SerializeObject(products, Formatting.Indented);

            return productsJson;
        }

        // 6
        public static string GetSoldProducts(ProductShopContext context)
        {
            var soldProducts = context
                .Users
                .Where(u => u.ProductsSold.Any(u => u.Buyer != null))
                .OrderBy(u => u.LastName)
                .ThenBy(u => u.FirstName)
                .ProjectTo<UserSoldProductsDTO>()
                .ToList();

            var soldProductsJson = JsonConvert.SerializeObject(soldProducts, Formatting.Indented);

            return soldProductsJson;
        }

        // 7
        public static string GetCategoriesByProductsCount(ProductShopContext context)
        {
            var categories = context
                .Categories
                .OrderByDescending(c => c.CategoryProducts.Count)
                .ProjectTo<CategoriesByCountDTO>()
                .ToList();

            var categoriesJson = JsonConvert.SerializeObject(categories, Formatting.Indented);

            return categoriesJson;
        }

        // 8
        public static string GetUsersWithProducts(ProductShopContext context)
        {
            // TODO When UsersWithSoldItemCount is collection it works. When it is an object it doesnt ?????
            var users = context
                .Users
                .Where(u => u.ProductsSold.Any(u => u.Buyer != null))
                .Select(u => new UsersWithSoldItemInfoDTO()
                {
                    FisrtName = u.FirstName,
                    LastName = u.LastName,
                    Age = u.Age,
                    SoldProducts = new SoldProductsWithCountDTO()
                    {
                        Count = u.ProductsSold.Count,
                        Products = u.ProductsSold
                        .Where(ps => ps.Buyer != null)
                        .Select(ps => new SimpleProductDTO()
                        {
                            Name = ps.Name,
                            Price = ps.Price
                        })
                        .ToList()
                    }
                })
                .OrderByDescending(u => u.SoldProducts.Count)
                .ToList();

            var userDto = new AllUsersWithSoldItemCountDTO()
            {
                UsersCount = users.Count,
                Users = users
            };

            var usersJson = JsonConvert.SerializeObject(userDto, new JsonSerializerSettings 
            {
                NullValueHandling = NullValueHandling.Ignore,
                Formatting = Formatting.Indented
            });


            return usersJson;
        }
    }
}
