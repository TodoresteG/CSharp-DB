namespace CarDealer
{
    using AutoMapper;
    using Dtos.Import;
    using Data;
    using System;
    using System.IO;
    using System.Xml.Serialization;
    using Models;
    using System.Linq;
    using System.Collections.Generic;
    using AutoMapper.QueryableExtensions;
    using Dtos.Export;
    using System.Xml;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            Mapper.Initialize(cfg => cfg.AddProfile<CarDealerProfile>());

            using (var context = new CarDealerContext())
            {
                //var xml = File.ReadAllText("../../../Datasets/sales.xml");

                Console.WriteLine(GetCarsWithTheirListOfParts(context));
            }
        }

        // 9
        public static string ImportSuppliers(CarDealerContext context, string inputXml)
        {
            var root = new XmlRootAttribute("Suppliers");
            var serializer = new XmlSerializer(typeof(ImportSupplierDto[]), root);

            ImportSupplierDto[] suppliersDtos;

            using (var reader = new StringReader(inputXml))
            {
                suppliersDtos = (ImportSupplierDto[])serializer.Deserialize(reader);
            }

            var suppliers = Mapper.Map<Supplier[]>(suppliersDtos).ToArray();

            context.Suppliers.AddRange(suppliers);
            context.SaveChanges();

            return $"Successfully imported {suppliers.Length}";
        }

        // 10
        public static string ImportParts(CarDealerContext context, string inputXml) 
        {
            var supplierIds = context.Suppliers.Select(s => s.Id).ToList();

            var root = new XmlRootAttribute("Parts");
            var serializer = new XmlSerializer(typeof(ImportPartDto[]), root);

            ImportPartDto[] partsDtos;

            using (var reader = new StringReader(inputXml))
            {
                partsDtos = (ImportPartDto[])serializer.Deserialize(reader);
            }

            var validParts = partsDtos
                .Where(p => supplierIds.Contains(p.SupplierId))
                .ToArray();

            var parts = Mapper.Map<Part[]>(validParts);

            context.Parts.AddRange(parts);
            context.SaveChanges();

            return $"Successfully imported {parts.Length}";
        }

        // 11
        public static string ImportCars(CarDealerContext context, string inputXml) 
        {
            var partIds = context.Parts.Select(p => p.Id).ToArray();

            var root = new XmlRootAttribute("Cars");
            var serializer = new XmlSerializer(typeof(ImportCarDto[]), root);

            ImportCarDto[] carDtos;

            using (var reader = new StringReader(inputXml))
            {
                carDtos = (ImportCarDto[])serializer.Deserialize(reader);
            }

            var cars = new List<Car>();
            var partCars = new List<PartCar>();

            foreach (var dto in carDtos)
            {
                var car = new Car() 
                {
                    Make = dto.Make,
                    Model = dto.Model,
                    TravelledDistance = dto.TraveledDistance
                };

                var parts = dto
                    .Parts
                    .Where(pdto => context.Parts.Any(p => p.Id == pdto.PartId))
                    .Select(p => p.PartId)
                    .Distinct();

                foreach (var partId in parts)
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

            return $"Successfully imported {cars.Count}";
        }

        // 12
        public static string ImportCustomers(CarDealerContext context, string inputXml) 
        {
            var root = new XmlRootAttribute("Customers");
            var serializer = new XmlSerializer(typeof(ImportCustomerDto[]), root);

            ImportCustomerDto[] customerDtos;

            using (var reader = new StringReader(inputXml))
            {
                customerDtos = (ImportCustomerDto[])serializer.Deserialize(reader);
            }

            var customers = Mapper.Map<Customer[]>(customerDtos);

            context.Customers.AddRange(customers);
            context.SaveChanges();

            return $"Successfully imported {customers.Length}";
        }

        // 13
        public static string ImportSales(CarDealerContext context, string inputXml) 
        {
            var root = new XmlRootAttribute("Sales");
            var serializer = new XmlSerializer(typeof(ImportSaleDto[]), root);

            ImportSaleDto[] saleDtos;

            using (var reader = new StringReader(inputXml))
            {
                saleDtos = ((ImportSaleDto[])serializer.Deserialize(reader))
                            .Where(s => context.Cars.Any(c => c.Id == s.CarId))
                            .ToArray();
            }

            var sales = Mapper.Map<Sale[]>(saleDtos);

            context.Sales.AddRange(sales);
            context.SaveChanges();

            return $"Successfully imported {sales.Length}";
        }

        // 14
        public static string GetCarsWithDistance(CarDealerContext context) 
        {
            var cars = context
                .Cars
                .Where(c => c.TravelledDistance > 2000000)
                .OrderBy(c => c.Make)
                .ThenBy(c => c.Model)
                .Take(10)
                .ProjectTo<CarWithDistanceDto>()
                .ToArray();

            var root = new XmlRootAttribute("cars");
            var serializer = new XmlSerializer(typeof(CarWithDistanceDto[]), root);

            var settings = new XmlWriterSettings() 
            {
                Indent = true
            };

            var namespaces = new XmlSerializerNamespaces();
            namespaces.Add("", "");

            var stringWriter = new StringWriter();

            using (var writer = XmlWriter.Create(stringWriter, settings))
            {
                serializer.Serialize(writer, cars, namespaces);
            }


            return stringWriter.ToString();
        }

        // 15
        public static string GetCarsFromMakeBmw(CarDealerContext context) 
        {
            var cars = context
                .Cars
                .Where(c => c.Make == "BMW")
                .OrderBy(c => c.Model)
                .ThenByDescending(c => c.TravelledDistance)
                .ProjectTo<CarFromMakeDto>()
                .ToArray();

            var root = new XmlRootAttribute("cars");
            var serializer = new XmlSerializer(typeof(CarFromMakeDto[]), root);

            var settings = new XmlWriterSettings() 
            {
                Indent = true
            };

            var namespaces = new XmlSerializerNamespaces();
            namespaces.Add("", "");

            var stringWriter = new StringWriter();

            using (var writer = XmlWriter.Create(stringWriter, settings))
            {
                serializer.Serialize(writer, cars, namespaces);
            }

            return stringWriter.ToString();
        }

        // 16
        public static string GetLocalSuppliers(CarDealerContext context) 
        {
            var suppliers = context
                .Suppliers
                .Where(s => s.IsImporter == false)
                .ProjectTo<LocalSupplierDto>()
                .ToArray();

            var root = new XmlRootAttribute("suppliers");
            var serializer = new XmlSerializer(typeof(LocalSupplierDto[]), root);

            var settings = new XmlWriterSettings() 
            {
                Indent = true
            };

            var namespaces = new XmlSerializerNamespaces();
            namespaces.Add("", "");

            var stringWriter = new StringWriter();

            using (var writer = XmlWriter.Create(stringWriter, settings))
            {
                serializer.Serialize(writer, suppliers, namespaces);
            }

            return stringWriter.ToString();
        }

        // 17
        public static string GetCarsWithTheirListOfParts(CarDealerContext context) 
        {
            var cars = context
                .Cars
                .OrderByDescending(c => c.TravelledDistance)
                .ThenBy(c => c.Model)
                .Take(5)
                .ProjectTo<CarWithListOfPartsDto>()
                .ToArray();

            var root = new XmlRootAttribute("cars");
            var serializer = new XmlSerializer(typeof(CarWithListOfPartsDto[]), root);

            var settings = new XmlWriterSettings() 
            {
                Indent = true
            };

            var namespaces = new XmlSerializerNamespaces();
            namespaces.Add("", "");

            var stringWriter = new StringWriter();

            using (var writer = XmlWriter.Create(stringWriter, settings))
            {
                serializer.Serialize(writer, cars, namespaces);
            }

            return stringWriter.ToString();
        }

        // 18
        public static string GetTotalSalesByCustomer(CarDealerContext context) 
        {
            var customers = context
                .Customers
                .Where(c => c.Sales.Any())
                .ProjectTo<CustomerTotalSalesDto>()
                .OrderByDescending(c => c.SpentMoney)
                .ToArray();

            var root = new XmlRootAttribute("customers");
            var serializer = new XmlSerializer(typeof(CustomerTotalSalesDto[]), root);

            var settings = new XmlWriterSettings() 
            {
                Indent = true
            };

            var namespaces = new XmlSerializerNamespaces();
            namespaces.Add("", "");

            var stringWriter = new StringWriter();

            using (var writer = XmlWriter.Create(stringWriter, settings))
            {
                serializer.Serialize(writer, customers, namespaces);
            }

            return stringWriter.ToString();
        }

        // 19
        public static string GetSalesWithAppliedDiscount(CarDealerContext context) 
        {
            var sales = context
                .Sales
                .ProjectTo<SalesDiscountDto>()
                .ToArray();

            var root = new XmlRootAttribute("sales");
            var serializer = new XmlSerializer(typeof(SalesDiscountDto[]), root);

            var settings = new XmlWriterSettings() 
            {
                Indent = true
            };

            var namespaces = new XmlSerializerNamespaces();
            namespaces.Add("", "");

            var stringWriter = new StringWriter();

            using (var writter = XmlWriter.Create(stringWriter, settings))
            {
                serializer.Serialize(writter, sales, namespaces);
            }

            return stringWriter.ToString();
        }
    }
}