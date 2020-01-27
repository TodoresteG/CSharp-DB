namespace ProductShop
{
    using Data;
    using Models;
    using Dtos.Import;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Xml.Linq;
    using System.Xml.Serialization;
    using System.Linq;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Dtos.Export;
    using System.Xml;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            Mapper.Initialize(cfg => cfg.AddProfile<ProductShopProfile>());

            using (var context = new ProductShopContext())
            {
                //var xml = File.ReadAllText("../../../Datasets/categories-products.xml");

                Console.WriteLine(GetCategoriesByProductsCount(context));
            }
        }

        // 1
        public static string ImportUsers(ProductShopContext context, string inputXml)
        {
            var root = new XmlRootAttribute("Users");

            var serializer = new XmlSerializer(typeof(ImportUserDto[]), root);

            ImportUserDto[] usersDtos;

            using (var reader = new StringReader(inputXml))
            {
                usersDtos = (ImportUserDto[])serializer.Deserialize(new StringReader(inputXml));
            }


            var users = Mapper.Map<User[]>(usersDtos);

            context.Users.AddRange(users);
            context.SaveChanges();

            return $"Successfully imported {users.Length}";
        }

        // 2
        public static string ImportProducts(ProductShopContext context, string inputXml)
        {
            var root = new XmlRootAttribute("Products");

            var serializer = new XmlSerializer(typeof(ImportProductDto[]), root);

            ImportProductDto[] productsDtos;

            using (var reader = new StringReader(inputXml))
            {
                productsDtos = (ImportProductDto[])serializer.Deserialize(reader);
            }


            var products = Mapper.Map<Product[]>(productsDtos);

            context.Products.AddRange(products);
            context.SaveChanges();

            return $"Successfully imported {products.Length}";
        }

        // 3
        public static string ImportCategories(ProductShopContext context, string inputXml)
        {
            var root = new XmlRootAttribute("Categories");
            var serializer = new XmlSerializer(typeof(ImportCategoryDto[]), root);

            ImportCategoryDto[] categoryDtos;

            using (var reader = new StringReader(inputXml))
            {
                categoryDtos = (ImportCategoryDto[])serializer.Deserialize(reader);
            }

            var categories = Mapper.Map<Category[]>(categoryDtos);
            var validCategories = categories.Where(c => c.Name != null);

            context.Categories.AddRange(validCategories);
            context.SaveChanges();

            return $"Successfully imported {categories.Length}";
        }

        // 4
        public static string ImportCategoryProducts(ProductShopContext context, string inputXml)
        {
            var categoryIds = context.Categories.Select(c => c.Id).ToArray();
            var productIds = context.Products.Select(c => c.Id).ToArray();

            var root = new XmlRootAttribute("CategoryProducts");
            var serializer = new XmlSerializer(typeof(ImportCategoryProductDto[]), root);

            ImportCategoryProductDto[] categoryProductDtos;

            using (var reader = new StringReader(inputXml))
            {
                categoryProductDtos = (ImportCategoryProductDto[])serializer.Deserialize(reader);
            }

            var categoryProducts = Mapper.Map<CategoryProduct[]>(categoryProductDtos);
            var validCategoryProducts = categoryProducts.Where(cp => categoryIds.Contains(cp.CategoryId) &&
                                                                        productIds.Contains(cp.ProductId)).ToArray();

            context.CategoryProducts.AddRange(validCategoryProducts);
            context.SaveChanges();

            return $"Successfully imported {validCategoryProducts.Length}";
        }

        // 5
        public static string GetProductsInRange(ProductShopContext context)
        {
            var products = context
                .Products
                .Where(p => p.Price >= 500 && p.Price <= 1000)
                .OrderBy(p => p.Price)
                .Take(10)
                .ProjectTo<ProductInRangeDto>()
                .ToArray();

            var root = new XmlRootAttribute("Products");
            var serializer = new XmlSerializer(typeof(ProductInRangeDto[]), root);

            var stringWriter = new StringWriter();
            var settings = new XmlWriterSettings()
            {
                Indent = true
            };

            var namespaces = new XmlSerializerNamespaces();
            namespaces.Add("","");

            using (var writer = XmlWriter.Create(stringWriter, settings))
            {
                serializer.Serialize(writer, products, namespaces);
            }

            return stringWriter.ToString();
        }

        // 6
        public static string GetSoldProducts(ProductShopContext context) 
        {
            var users = context
                .Users
                .Where(u => u.ProductsSold.Any())
                .OrderBy(u => u.LastName)
                .ThenBy(u => u.FirstName)
                .Take(5)
                .ProjectTo<UserSoldProductDto>()
                .ToArray();

            var root = new XmlRootAttribute("Users");
            var serializer = new XmlSerializer(typeof(UserSoldProductDto[]), root);

            var stringWriter = new StringWriter();
            var settings = new XmlWriterSettings()
            {
                Indent = true
            };

            var namespaces = new XmlSerializerNamespaces();
            namespaces.Add("", "");

            using (var writer = XmlWriter.Create(stringWriter, settings))
            {
                serializer.Serialize(writer, users, namespaces);
            }

            return stringWriter.ToString();
        }

        // 7
        public static string GetCategoriesByProductsCount(ProductShopContext context) 
        {
            var categories = context
                .Categories
                .ProjectTo<CategoriesByProductDto>()
                .OrderByDescending(c => c.Count)
                .ThenByDescending(c => c.TotalRevenue)
                .ToArray();

            var root = new XmlRootAttribute("Categories");
            var serializer = new XmlSerializer(typeof(CategoriesByProductDto[]), root);

            var settings = new XmlWriterSettings()
            {
                Indent = true
            };

            var namespaces = new XmlSerializerNamespaces();
            namespaces.Add("", "");

            var stringWriter = new StringWriter();

            using (var writer = XmlWriter.Create(stringWriter, settings))
            {
                serializer.Serialize(writer, categories, namespaces);
            }

            return stringWriter.ToString();
        }

        // 8
        public static string GetUsersWithProducts(ProductShopContext context) 
        {
            var usersDtos = context
                .Users
                .Where(u => u.ProductsSold.Any())
                .OrderByDescending(u => u.ProductsSold.Count)
                .ProjectTo<UserWithSoldProductDto>()
                .ToArray();

            var users = new UsersAndProductsDto
            {
                Count = usersDtos.Length,
                UserWithSoldProducts = usersDtos
            };

            var root = new XmlRootAttribute("Users");
            var serializer = new XmlSerializer(typeof(UsersAndProductsDto), root);

            var settings = new XmlWriterSettings()
            {
                Indent = true
            };

            var namespaces = new XmlSerializerNamespaces();
            namespaces.Add("", "");

            var stringWriter = new StringWriter();

            using (var writer = XmlWriter.Create(stringWriter, settings))
            {
                serializer.Serialize(writer, users, namespaces);
            }

            return stringWriter.ToString();
        }
    }
}