namespace ProductShop
{
    using AutoMapper;
    using Models;
    using Dtos.Export;
    using Dtos.Import;
    using System.Linq;

    public class ProductShopProfile : Profile
    {
        public ProductShopProfile()
        {
            // Import Dtos

            // 1
            this.CreateMap<ImportUserDto, User>();

            // 2
            this.CreateMap<ImportProductDto, Product>();

            // 3
            this.CreateMap<ImportCategoryDto, Category>();

            // 4
            this.CreateMap<ImportCategoryProductDto, CategoryProduct>();

            // Export Dtos

            // 5
            this.CreateMap<Product, ProductInRangeDto>()
                .ForMember(x => x.Buyer, y => y.MapFrom(s => $"{s.Buyer.FirstName} {s.Buyer.LastName}"));

            // 6
            this.CreateMap<Product, SoldProductDto>();

            this.CreateMap<User, UserSoldProductDto>()
                .ForMember(x => x.SoldProducts, y => y.MapFrom(s => s.ProductsSold));

            // 7
            this.CreateMap<Category, CategoriesByProductDto>()
                .ForMember(x => x.Count, y => y.MapFrom(s => s.CategoryProducts.Count))
                .ForMember(x => x.AveragePrice, y => y.MapFrom(s => s.CategoryProducts.Average(cp => cp.Product.Price)))
                .ForMember(x => x.TotalRevenue, y => y.MapFrom(s => s.CategoryProducts.Sum(cp => cp.Product.Price)));

            // 8
            this.CreateMap<User, UserWithSoldProductDto>()
                .ForMember(x => x.SoldProducts, y => y.MapFrom(s => s));

            this.CreateMap<User, SoldProductsWithCountDto>()
                .ForMember(x => x.Count, y => y.MapFrom(s => s.ProductsSold.Count))
                .ForMember(x => x.Products, y => y.MapFrom(s => s.ProductsSold.OrderByDescending(ps => ps.Price)));
        }
    }
}
