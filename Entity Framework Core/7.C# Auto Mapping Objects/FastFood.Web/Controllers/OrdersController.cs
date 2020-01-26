namespace FastFood.Web.Controllers
{
    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Linq;

    using Data;
    using ViewModels.Orders;
    using FastFood.Models;
    using AutoMapper.QueryableExtensions;
    using FastFood.Models.Enums;
    using System.Collections.Generic;

    public class OrdersController : Controller
    {
        private readonly FastFoodContext context;
        private readonly IMapper mapper;

        public OrdersController(FastFoodContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public IActionResult Create()
        {
            var viewOrder = new CreateOrderViewModel
            {
                Items = this.context.Items.Select(x => x.Name).ToList(),
                Employees = this.context.Employees.Select(x => x.Name).ToList(),
            };

            return this.View(viewOrder);
        }

        [HttpPost]
        public IActionResult Create(CreateOrderInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return RedirectToAction("Error", "Home");
            }

            var order = this.mapper.Map<Order>(model);
            order.DateTime = DateTime.UtcNow;

            var employee = this.context
                .Employees
                .FirstOrDefault(e => e.Name == model.EmployeeName);

            order.Employee = employee;
            order.EmployeeId = employee.Id;

            var item = this.context
                .Items
                .FirstOrDefault(i => i.Name == model.ItemName);

            var orderItems = new List<OrderItem>()
            {
                new OrderItem()
                {
                    Item = item,
                    Order = order,
                    Quantity = model.Quantity
                }
            };

            order.OrderItems = orderItems;

            order.Type = Enum.Parse<OrderType>(model.Type);

            this.context.Orders.Add(order);
            this.context.SaveChanges();

            //Damn.. This shit looks ugly

            return this.RedirectToAction("All", "Orders");
        }

        public IActionResult All()
        {
            var orders = this.context
                .Orders
                .ProjectTo<OrderAllViewModel>(this.mapper.ConfigurationProvider)
                .ToList();

            return this.View(orders);
        }
    }
}
