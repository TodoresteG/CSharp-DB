namespace ProductShop
{
    using AutoMapper;
    using Models;
    using System.Linq;
    using ProductShop.DTO.Products;
    using ProductShop.DTO.Users;
    using ProductShop.DTO.Categories;
    using System.Collections.Generic;

    public class ProductShopProfile : Profile
    {
        public ProductShopProfile()
        {
            // Products
            CreateMap<Product, ProductInRangeDTO>()
                .ForMember(x => x.Seller, y => y.MapFrom(s => $"{s.Seller.FirstName} {s.Seller.LastName}"));

            CreateMap<Product, SimpleProductDTO>()
                .ForMember(x => x.Name, y => y.MapFrom(s => s.Name))
                .ForMember(x => x.Price, y => y.MapFrom(s => s.Price))
                .ReverseMap();

            CreateMap<ICollection<Product>, ICollection<SimpleProductDTO>>()
                .ReverseMap();

            // Users
            CreateMap<User, SoldProductDTO>()
                .ForMember(x => x.BuyerFirstName, y => y.MapFrom(s => s.FirstName))
                .ForMember(x => x.BuyerLastName, y => y.MapFrom(s => s.LastName));

            CreateMap<User, UserSoldProductsDTO>()
                .ForMember(x => x.SoldProducts, y => y.MapFrom(s => s.ProductsSold));

            CreateMap<User, UsersWithSoldItemInfoDTO>()
                .ForMember(x => x.SoldProducts, y => y.MapFrom(s => s.ProductsSold));

            CreateMap<UsersWithSoldItemInfoDTO, User>()
                .ForMember(x => x.ProductsSold, y => y.MapFrom(s => s.SoldProducts.Products));

            // Categories
            CreateMap<Category, CategoriesByCountDTO>()
                .ForMember(x => x.Category, y => y.MapFrom(s => s.Name))
                .ForMember(x => x.ProductsCount, y => y.MapFrom(s => s.CategoryProducts.Count))
                .ForMember(x => x.AveragePrice, y => y.MapFrom(s => s.CategoryProducts.Average(p => p.Product.Price).ToString("f2")))
                .ForMember(x => x.TotalRevenue, y => y.MapFrom(s => s.CategoryProducts.Sum(p => p.Product.Price).ToString("f2")));
        }
    }
}
