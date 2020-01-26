namespace FastFood.Web.MappingConfiguration
{
    using AutoMapper;
    using Models;

    using ViewModels.Positions;
    using ViewModels.Employees;
    using FastFood.Web.ViewModels.Categories;
    using FastFood.Web.ViewModels.Items;
    using FastFood.Web.ViewModels.Orders;
    using System.Linq;

    public class FastFoodProfile : Profile
    {
        public FastFoodProfile()
        {
            //Positions
            this.CreateMap<CreatePositionInputModel, Position>()
                .ForMember(x => x.Name, y => y.MapFrom(s => s.PositionName));

            this.CreateMap<Position, PositionsAllViewModel>()
                .ForMember(x => x.Name, y => y.MapFrom(s => s.Name));

            this.CreateMap<Position, RegisterEmployeeViewModel>()
                .ForMember(x => x.PositionName, y => y.MapFrom(s => s.Name));

            //Employees
            this.CreateMap<RegisterEmployeeInputModel, Employee>();

            this.CreateMap<Employee, RegisterEmployeeInputModel>()
                .ForMember(x => x.PositionName, y => y.MapFrom(s => s.Position.Name));

            this.CreateMap<Employee, EmployeesAllViewModel>()
                .ForMember(x => x.Position, y => y.MapFrom(s => s.Position.Name));

            //Categories
            this.CreateMap<CreateCategoryInputModel, Category>()
                .ForMember(x => x.Name, y => y.MapFrom(s => s.CategoryName))
                .ReverseMap();

            this.CreateMap<CategoryAllViewModel, Category>();

            this.CreateMap<CreateItemViewModel, Category>()
                .ForMember(x => x.Name, y => y.MapFrom(s => s.CategoryName))
                .ReverseMap();

            //Items
            this.CreateMap<CreateItemInputModel, Item>();

            this.CreateMap<Item, ItemsAllViewModels>()
                .ForMember(x => x.Category, y => y.MapFrom(s => s.Category.Name));

            //Orders
            this.CreateMap<Order, CreateOrderInputModel>()
                .ForMember(x => x.EmployeeName, y => y.MapFrom(s => s.Employee.Name))
                .ForMember(x => x.ItemName, y => y.MapFrom(s => s.OrderItems.Select(x => x.Item.Name)))
                .ReverseMap();

            this.CreateMap<Order, OrderAllViewModel>()
                .ForMember(x => x.OrderId, y => y.MapFrom(s => s.Id))
                .ForMember(x => x.Employee, y => y.MapFrom(s => s.Employee.Name))
                .ForMember(x => x.DateTime, y => y.MapFrom(s => s.DateTime.ToString("g")))
                .ReverseMap();
        }
    }
}
