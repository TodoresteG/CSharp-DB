namespace Cinema.DataProcessor
{
    using Data;
    using Data.Models;
    using ImportDtos;
    using Data.Models.Enums;

    using System;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;
    using System.IO;
    using System.Xml.Serialization;
    using System.Linq;
    using System.Globalization;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";
        private const string SuccessfulImportMovie 
            = "Successfully imported {0} with genre {1} and rating {2}!";
        private const string SuccessfulImportHallSeat 
            = "Successfully imported {0}({1}) with {2} seats!";
        private const string SuccessfulImportProjection 
            = "Successfully imported projection {0} on {1}!";
        private const string SuccessfulImportCustomerTicket 
            = "Successfully imported customer {0} {1} with bought tickets: {2}!";

        public static string ImportMovies(CinemaContext context, string jsonString)
        {
            var sb = new StringBuilder();

            var movieDtos = JsonConvert.DeserializeObject<ImportMovieDto[]>(jsonString);

            var movies = new List<Movie>();

            foreach (var dto in movieDtos)
            {
                if (!IsValid(dto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var movie = new Movie 
                {
                    Title = dto.Title,
                    Genre = Enum.Parse<Genre>(dto.Genre),
                    Duration = TimeSpan.Parse(dto.Duration, CultureInfo.InvariantCulture),
                    Rating = dto.Rating,
                    Director = dto.Director
                };

                movies.Add(movie);
                sb.AppendLine(string.Format(SuccessfulImportMovie, movie.Title, movie.Genre, movie.Rating.ToString("f2")));
            }

            context.Movies.AddRange(movies);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static string ImportHallSeats(CinemaContext context, string jsonString)
        {
            var sb = new StringBuilder();

            var hallDtos = JsonConvert.DeserializeObject<ImportHallDto[]>(jsonString);

            var halls = new List<Hall>();
            var seats = new List<Seat>();

            foreach (var dto in hallDtos)
            {
                if (!IsValid(dto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }
               
                var hall = new Hall 
                {
                    Name = dto.Name,
                    Is4Dx = dto.Is4Dx,
                    Is3D = dto.Is3D
                };

                for (int i = 0; i < dto.Seats; i++)
                {
                    var seat = new Seat
                    {
                        Hall = hall
                    };

                    seats.Add(seat);
                }

                halls.Add(hall);

                var projectionType = string.Empty;

                if (hall.Is4Dx && hall.Is3D)
                {
                    projectionType = "4Dx/3D";
                }
                else if (!hall.Is4Dx && !hall.Is3D)
                {
                    projectionType = "Normal";
                }
                else
                {
                    projectionType = hall.Is4Dx ? "4Dx" : "3D";
                }

                sb.AppendLine(string.Format(SuccessfulImportHallSeat, hall.Name, projectionType, dto.Seats));
            }

            context.Halls.AddRange(halls);
            context.Seats.AddRange(seats);

            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static string ImportProjections(CinemaContext context, string xmlString)
        {
            var sb = new StringBuilder();

            var root = new XmlRootAttribute("Projections");
            var serializer = new XmlSerializer(typeof(ImportProjectionDto[]), root);

            ImportProjectionDto[] projectionDtos;

            using (var reader = new StringReader(xmlString))
            {
                projectionDtos = (ImportProjectionDto[])serializer.Deserialize(reader);
            }

            var projections = new List<Projection>();

            foreach (var dto in projectionDtos)
            {
                var movie = context.Movies.Find(dto.MovieId);
                var hall = context.Halls.Find(dto.HallId);

                if (!IsValid(dto) || movie == null || hall == null)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var projection = new Projection
                {
                    MovieId = dto.MovieId,
                    HallId = dto.HallId,
                    DateTime = DateTime.ParseExact(dto.DateTime, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)
                };

                projections.Add(projection);

                var movieTitle = context
                    .Movies
                    .Where(m => m.Id == dto.MovieId)
                    .Select(m => m.Title)
                    .FirstOrDefault();

                sb.AppendLine(string.Format(SuccessfulImportProjection, movieTitle, projection.DateTime.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)));
            }

            context.Projections.AddRange(projections);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static string ImportCustomerTickets(CinemaContext context, string xmlString)
        {
            var sb = new StringBuilder();

            var root = new XmlRootAttribute("Customers");
            var serializer = new XmlSerializer(typeof(ImportCustomerDto[]), root);

            ImportCustomerDto[] customerDtos;

            using (var reader = new StringReader(xmlString))
            {
                customerDtos = (ImportCustomerDto[])serializer.Deserialize(reader);
            }

            var customers = new List<Customer>();
            var tickets = new List<Ticket>();

            var projectionCount = context.Projections.Count();

            foreach (var dto in customerDtos)
            {
                if (!IsValid(dto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var customer = new Customer 
                {
                    FirstName = dto.FirstName,
                    LastName = dto.LastName,
                    Age = dto.Age,
                    Balance = dto.Balance
                };

                foreach (var ticketDto in dto.Tickets)
                {
                    if (ticketDto.ProjectionId > projectionCount)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    var ticket = new Ticket 
                    {
                        Customer = customer,
                        Price = ticketDto.Price,
                        ProjectionId = ticketDto.ProjectionId
                    };

                    tickets.Add(ticket);
                }

                customers.Add(customer);

                sb.AppendLine(string.Format(SuccessfulImportCustomerTicket, customer.FirstName, customer.LastName, dto.Tickets.Count()));
            }

            context.Customers.AddRange(customers);
            context.Tickets.AddRange(tickets);

            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        private static bool IsValid(object entity) 
        {
            var validationContext = new ValidationContext(entity);
            var validationResults = new List<ValidationResult>();

            return Validator.TryValidateObject(entity, validationContext, validationResults, true);
        }
    }
}