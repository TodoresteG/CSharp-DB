namespace CarDealer
{
    using AutoMapper;
    using DTO;
    using Models;
    using System.Linq;

    public class CarDealerProfile : Profile
    {
        public CarDealerProfile()
        {
            CreateMap<Customer, ExperiencedDriverDTO>()
                .ForMember(x => x.BirthDate, y => y.MapFrom(s => s.BirthDate.ToString("dd/MM/yyyy")));

            CreateMap<Customer, CustomerWithCarDTO>()
                .ForMember(x => x.FullName, y => y.MapFrom(s => s.Name))
                .ForMember(x => x.BoughtCars, y => y.MapFrom(s => s.Sales.Count));
        }
    }
}
