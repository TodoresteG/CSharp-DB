namespace Cinema.DataProcessor
{
    using Data;
    using ExportDtos;

    using Newtonsoft.Json;
    using System;
    using System.IO;
    using System.Linq;
    using System.Globalization;
    using System.Xml.Serialization;

    public class Serializer
    {
        public static string ExportTopMovies(CinemaContext context, int rating)
        {
            var movies = context
                .Movies
                .Where(m => m.Rating >= rating && m.Projections.Any(p => p.Tickets.Count > 0))
                .OrderByDescending(m => m.Rating)
                .ThenByDescending(m => m.Projections.Sum(p => p.Tickets.Sum(t => t.Price)))
                .Select(m => new
                {
                    MovieName = m.Title,
                    Rating = m.Rating.ToString("f2"),
                    TotalIncomes = m.Projections.Sum(p => p.Tickets.Sum(t => t.Price)).ToString("f2"),
                    Customers = m.Projections.SelectMany(p => p.Tickets).Select(t => new
                    {
                        FirstName = t.Customer.FirstName,
                        LastName = t.Customer.LastName,
                        Balance = t.Customer.Balance.ToString("f2")
                    })
                    .OrderByDescending(c => c.Balance)
                    .ThenBy(c => c.FirstName)
                    .ThenBy(c => c.LastName)
                    .ToArray()
                })
                .Take(10)
                .ToArray();

            var moviesJson = JsonConvert.SerializeObject(movies, Formatting.Indented);

            return moviesJson;
        }

        public static string ExportTopCustomers(CinemaContext context, int age)
        {
            var customers = context
                .Customers
                .Where(c => c.Age >= age)
                .OrderByDescending(c => c.Tickets.Sum(t => t.Price))
                .Take(10)
                .Select(c => new ExportTopCustomersDto
                {
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    SpentMoney = c.Tickets.Sum(t => t.Price).ToString("f2"),
                    SpentTime = TimeSpan.FromSeconds(c.Tickets.Sum(t => t.Projection.Movie.Duration.TotalSeconds)).ToString(@"hh\:mm\:ss")
                })
                .ToArray();

            var root = new XmlRootAttribute("Customers");
            var serializer = new XmlSerializer(typeof(ExportTopCustomersDto[]), root);

            var namespaces = new XmlSerializerNamespaces();
            namespaces.Add("", "");

            var options = new System.Xml.XmlWriterSettings()
            {
                Indent = true
            };

            var stringWriter = new StringWriter();

            using (var writer = System.Xml.XmlWriter.Create(stringWriter, options))
            {
                serializer.Serialize(stringWriter, customers, namespaces);
            }

            return stringWriter.ToString();
        }
    }
}