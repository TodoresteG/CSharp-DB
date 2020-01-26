namespace FastFood.Web.Controllers
{
    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;
    using System;

    using Data;
    using ViewModels.Employees;
    using AutoMapper.QueryableExtensions;
    using System.Linq;
    using System.Collections.Generic;
    using FastFood.Models;

    public class EmployeesController : Controller
    {
        private readonly FastFoodContext context;
        private readonly IMapper mapper;

        public EmployeesController(FastFoodContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public IActionResult Register()
        {
            var positions = this.context
                .Positions
                .ProjectTo<RegisterEmployeeViewModel>(this.mapper.ConfigurationProvider)
                .ToList();

            return this.View(positions);
        }

        [HttpPost]
        public IActionResult Register(RegisterEmployeeInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return RedirectToAction("Error", "Home");
            }

            var employee = this.mapper.Map<Employee>(model);
            employee.Position = this.context.Positions
                        .FirstOrDefault(p => p.Name == model.PositionName);

            employee.PositionId = employee.Position.Id;

            this.context.Employees.Add(employee);

            this.context.SaveChanges();

            return this.RedirectToAction("All", "Employees");
        }

        public IActionResult All()
        {
            IList<EmployeesAllViewModel> employees = this.context
                .Employees
                .ProjectTo<EmployeesAllViewModel>(this.mapper.ConfigurationProvider)
                .ToList();

            return this.View(employees);
        }
    }
}
