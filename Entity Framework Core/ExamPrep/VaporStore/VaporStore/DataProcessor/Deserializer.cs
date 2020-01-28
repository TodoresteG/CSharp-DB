namespace VaporStore.DataProcessor
{
	using Data;
    using ImportDtos;
    using Data.Models;
    using Data.Models.Enums;

    using System;
    using System.Text;
    using System.Linq;
    using Newtonsoft.Json;
    using System.Globalization;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Xml.Serialization;
    using System.IO;

    public static class Deserializer
	{
		public static string ImportGames(VaporStoreDbContext context, string jsonString)
		{
            var sb = new StringBuilder();

            var gameDtos = JsonConvert.DeserializeObject<ImportGameDto[]>(jsonString);

            var games = new List<Game>();

            foreach (var dto in gameDtos)
            {
                if (!IsValid(dto) || dto.Tags.Count == 0)
                {
                    sb.AppendLine("Invalid Data");
                    continue;
                }

                var developer = GetDeveloper(context, dto.Developer);
                var genre = GetGenre(context, dto.Genre);

                var game = new Game
                {
                    Name = dto.Name,
                    Price = dto.Price,
                    ReleaseDate = DateTime.ParseExact(dto.ReleaseDate, "yyyy-MM-dd", CultureInfo.InvariantCulture),
                    Developer = developer,
                    Genre = genre
                };

                foreach (var tagName in dto.Tags)
                {
                    var tag = GetTag(context, tagName);

                    var gameTag = new GameTag
                    {
                        Game = game,
                        Tag = tag
                    };

                    game.GameTags.Add(gameTag);
                }

                games.Add(game);
                sb.AppendLine($"Added {game.Name} ({dto.Genre}) with {dto.Tags.Count} tags");
            }

            context.Games.AddRange(games);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
		}

        public static string ImportUsers(VaporStoreDbContext context, string jsonString)
		{
            var sb = new StringBuilder();

            var userDto = JsonConvert.DeserializeObject<ImportUserDto[]>(jsonString);

            var users = new List<User>();

            foreach (var dto in userDto)
            {
                if (!IsValid(dto) || !dto.Cards.All(IsValid))
                {
                    sb.AppendLine("Invalid Data");
                    continue;
                }

                var user = new User 
                {
                    FullName = dto.FullName,
                    Username = dto.Username,
                    Email = dto.Email,
                    Age = dto.Age
                };

                foreach (var cardDto in dto.Cards)
                {
                    var card = new Card
                    {
                        Number = cardDto.Number,
                        Cvc = cardDto.CVC,
                        Type = Enum.Parse<CardType>(cardDto.Type)
                    };

                    user.Cards.Add(card);
                }

                users.Add(user);
                sb.AppendLine($"Imported {user.Username} with {user.Cards.Count} cards");
            }

            context.Users.AddRange(users);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
		}

		public static string ImportPurchases(VaporStoreDbContext context, string xmlString)
		{
            var sb = new StringBuilder();

            var serializer = new XmlSerializer(typeof(ImportPurchaseDto[]), new XmlRootAttribute("Purchases"));

            ImportPurchaseDto[] purchaseDtos;

            using (var reader = new StringReader(xmlString))
            {
                purchaseDtos = (ImportPurchaseDto[])serializer.Deserialize(reader);
            }

            var purchases = new List<Purchase>();

            foreach (var dto in purchaseDtos)
            {
                var card = context.Cards.FirstOrDefault(c => c.Number == dto.Card);
                var game = context.Games.FirstOrDefault(g => g.Name == dto.Title);

                if (!IsValid(dto) || card == null || game == null)
                {
                    sb.AppendLine("Invalid Data");
                    continue;
                }

                var purchase = new Purchase
                {
                    ProductKey = dto.Key,
                    Type = Enum.Parse<PurchaseType>(dto.Type),
                    Date = DateTime.ParseExact(dto.Date, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture),
                    Card = card,
                    Game = game
                };

                var username = context.Cards.Where(c => c.Number == purchase.Card.Number).Select(c => c.User.Username).FirstOrDefault();

                purchases.Add(purchase);
                sb.AppendLine($"Imported {purchase.Game.Name} for {username}");
            }

            context.Purchases.AddRange(purchases);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
		}

        private static Tag GetTag(VaporStoreDbContext context, string tagName)
        {
            var tag = context.Tags.FirstOrDefault(t => t.Name == tagName);

            if (tag == null)
            {
                tag = new Tag 
                {
                    Name = tagName
                };

                context.Tags.Add(tag);
                context.SaveChanges();
            }

            return tag;
        }

        private static Genre GetGenre(VaporStoreDbContext context, string genreName)
        {
            var genre = context.Genres.FirstOrDefault(g => g.Name == genreName);

            if (genre == null)
            {
                genre = new Genre
                {
                    Name = genreName
                };

                context.Genres.Add(genre);
                context.SaveChanges();
            }

            return genre;
        }

        private static Developer GetDeveloper(VaporStoreDbContext context, string developerName)
        {
            var dev = context.Developers.FirstOrDefault(d => d.Name == developerName);

            if (dev == null)
            {
                dev = new Developer 
                {
                    Name = developerName
                };

                context.Developers.Add(dev);
                context.SaveChanges();
            }

            return dev;
        }

        private static bool IsValid(object entity) 
        {
            var context = new ValidationContext(entity);
            var results = new List<ValidationResult>();

            return Validator.TryValidateObject(entity, context, results, true);
        }
	}
}