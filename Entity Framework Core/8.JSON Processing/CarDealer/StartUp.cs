using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AutoMapper;
using CarDealer.Data;
using CarDealer.DTO;
using CarDealer.Models;
using Newtonsoft.Json;

namespace CarDealer
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            using (var context = new CarDealerContext())
            {
                //var json = File.ReadAllText("../../../Datasets/sales.json");

                Console.WriteLine(GetTotalSalesByCustomer(context));
            }
        }

        // 9
        public static string ImportSuppliers(CarDealerContext context, string inputJson)
        {
            var suppliers = JsonConvert.DeserializeObject<List<Supplier>>(inputJson);

            context.AddRange(suppliers);
            context.SaveChanges();

            return $"Successfully imported {suppliers.Count}.";
        }

        // 10
        public static string ImportParts(CarDealerContext context, string inputJson)
        {
            var parts = JsonConvert.DeserializeObject<List<Part>>(inputJson);

            var suppliers = context
                .Suppliers
                .Select(s => s.Id)
                .ToList();

            foreach (var part in parts)
            {
                if (suppliers.Contains(part.SupplierId))
                {
                    context.Add(part);
                }
            }

            var affectedRows = context.SaveChanges();

            return $"Successfully imported {affectedRows}.";
        }

        // 11
        public static string ImportCars(CarDealerContext context, string inputJson)
        {
            var carsDTO = JsonConvert.DeserializeObject<List<ImportCarDTO>>(inputJson);

            var cars = new List<Car>();
            var partCars = new List<PartCar>();

            foreach (var dto in carsDTO)
            {

                var car = new Car()
                {
                    Make = dto.Make,
                    Model = dto.Model,
                    TravelledDistance = dto.TravelledDistance
                };

                foreach (var partId in dto.PartsId.Distinct())
                {
                    var partCar = new PartCar()
                    {
                        PartId = partId,
                        Car = car
                    };

                    partCars.Add(partCar);
                }

                cars.Add(car);
            }

            context.Cars.AddRange(cars);
            context.PartCars.AddRange(partCars);

            context.SaveChanges();

            return $"Successfully imported {cars.Count}.";
        }

        // 12
        public static string ImportCustomers(CarDealerContext context, string inputJson)
        {
            var customers = JsonConvert.DeserializeObject<List<Customer>>(inputJson);

            context.AddRange(customers);
            context.SaveChanges();

            return $"Successfully imported {customers.Count}.";
        }

        // 13
        public static string ImportSales(CarDealerContext context, string inputJson)
        {
            var sales = JsonConvert.DeserializeObject<List<Sale>>(inputJson);

            context.AddRange(sales);
            context.SaveChanges();

            return $"Successfully imported {sales.Count}.";
        }

        // 14
        public static string GetOrderedCustomers(CarDealerContext context)
        {
            var customers = context
                .Customers
                .OrderBy(c => c.BirthDate)
                .ThenBy(c => c.IsYoungDriver)
                .Select(c => new
                {
                    c.Name,
                    BirthDate = c.BirthDate.ToString("dd/MM/yyyy"),
                    c.IsYoungDriver
                })
                .ToList();

            var customersJson = JsonConvert.SerializeObject(customers, Formatting.Indented);

            return customersJson;
        }

        // 15
        public static string GetCarsFromMakeToyota(CarDealerContext context)
        {
            var cars = context
                .Cars
                .Where(c => c.Make == "Toyota")
                .OrderBy(c => c.Model)
                .ThenByDescending(c => c.TravelledDistance)
                .Select(c => new
                {
                    c.Id,
                    c.Make,
                    c.Model,
                    c.TravelledDistance
                })
                .ToList();

            var carsJson = JsonConvert.SerializeObject(cars, Formatting.Indented);

            return carsJson;
        }

        // 16
        public static string GetLocalSuppliers(CarDealerContext context)
        {
            var suppliers = context
                .Suppliers
                .Where(s => s.IsImporter == false)
                .Select(s => new
                {
                    s.Id,
                    s.Name,
                    PartsCount = s.Parts.Count
                })
                .ToList();

            var suppliersJson = JsonConvert.SerializeObject(suppliers, Formatting.Indented);

            return suppliersJson;
        }

        // 17
        public static string GetCarsWithTheirListOfParts(CarDealerContext context)
        {
            var cars = context
                .Cars
                .Select(c => new
                {
                    car = new
                    {
                        c.Make,
                        c.Model,
                        c.TravelledDistance
                    },
                    parts = c.PartCars
                    .Where(pc => pc.CarId == c.Id)
                    .Select(pc => new
                    {
                        pc.Part.Name,
                        Price = pc.Part.Price.ToString("f2")
                    })
                    .ToList()
                })
                .ToList();

            var carsJson = JsonConvert.SerializeObject(cars, Formatting.Indented);

            return carsJson;
        }

        // 18
        public static string GetTotalSalesByCustomer(CarDealerContext context)
        {
            var customers = context
                .Customers
                .Where(c => c.Sales.Any())
                .Select(c => new
                {
                    fullName = c.Name,
                    boughtCars = c.Sales.Count,
                    spentMoney = c.Sales.Sum(s => s.Car.PartCars.Sum(pc => pc.Part.Price))
                })
                .ToList();

            var customersJson = JsonConvert.SerializeObject(customers, Formatting.Indented);

            return customersJson;
        }

        // 19
        public static string GetSalesWithAppliedDiscount(CarDealerContext context)
        {
            var sales = context
                .Sales
                .Select(s => new
                {
                    car = new
                    {
                        s.Car.Make,
                        s.Car.Model,
                        s.Car.TravelledDistance
                    },
                    customerName = s.Customer.Name,
                    Discount = s.Discount.ToString("f2"),
                    price = s.Car.PartCars.Sum(pc => pc.Part.Price).ToString("f2"),
                    priceWithDiscount = (s.Car.PartCars.Sum(pc => pc.Part.Price) - (s.Car.PartCars.Sum(pc => pc.Part.Price) * s.Discount / 100)).ToString("f2")
                })
                .Take(10)
                .ToList();

            var salesJson = JsonConvert.SerializeObject(sales, Formatting.Indented);

            return salesJson;
        }
    }
}