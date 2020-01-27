namespace CarDealer
{
    using AutoMapper;
    using Models;
    using Dtos.Import;
    using Dtos.Export;
    using System.Linq;

    public class CarDealerProfile : Profile
    {
        public CarDealerProfile()
        {
            // Import

            // 1
            this.CreateMap<ImportSupplierDto, Supplier>();

            // 2
            this.CreateMap<ImportPartDto, Part>();

            // 3
            this.CreateMap<ImportCarDto, Car>()
                .ForMember(x => x.TravelledDistance, y => y.MapFrom(s => s.TraveledDistance));

            // 4
            this.CreateMap<ImportCustomerDto, Customer>();

            // 5
            this.CreateMap<ImportSaleDto, Sale>();

            // Export

            // 16
            this.CreateMap<Supplier, LocalSupplierDto>()
                .ForMember(x => x.PartsCount, y => y.MapFrom(s => s.Parts.Count));

            // 17
            this.CreateMap<Car, CarWithListOfPartsDto>()
                .ForMember(x => x.Parts, y => y.MapFrom(s => s.PartCars.Select(pc => pc.Part).OrderByDescending(pc => pc.Price)));

            // 18
            this.CreateMap<Customer, CustomerTotalSalesDto>()
                .ForMember(x => x.FullName, y => y.MapFrom(s => s.Name))
                .ForMember(x => x.BoughtCars, y => y.MapFrom(s => s.Sales.Count))
                .ForMember(x => x.SpentMoney, y => y.MapFrom(s => s.Sales.Sum(ss => ss.Car.PartCars.Sum(pc => pc.Part.Price))));

            // 19
            this.CreateMap<Sale, SalesDiscountDto>()
                .ForMember(x => x.CustomerName, y => y.MapFrom(s => s.Customer.Name))
                .ForMember(x => x.Price, y => y.MapFrom(s => s.Car.PartCars.Sum(pc => pc.Part.Price)))
                .ForMember(x => x.PriceWithDiscount, y => y.MapFrom(s => (s.Car.PartCars.Sum(pc => pc.Part.Price)) - (s.Car.PartCars.Sum(pc => pc.Part.Price) * s.Discount / 100)));
        }
    }
}
