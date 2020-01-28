namespace VaporStore.DataProcessor
{
    using Data;
    using ExportDtos;
    using Data.Models.Enums;

    using System;
    using System.Linq;
    using Newtonsoft.Json;
    using System.Globalization;
    using System.Xml.Serialization;
    using System.IO;

    public static class Serializer
	{
		public static string ExportGamesByGenres(VaporStoreDbContext context, string[] genreNames)
		{
            var genres = context
                .Genres
                .Where(g => genreNames.Contains(g.Name))
                .OrderByDescending(g => g.Games.Sum(gs => gs.Purchases.Count))
                .ThenBy(g => g.Id)
                .Select(g => new
                {
                    Id = g.Id,
                    Genre = g.Name,
                    Games = g.Games.Where(gs => gs.Purchases.Any()).Select(gs => new
                    {
                        Id = gs.Id,
                        Title = gs.Name,
                        Developer = gs.Developer.Name,
                        Tags = string.Join(", ", gs.GameTags.Select(gt => gt.Tag.Name)),
                        Players = gs.Purchases.Count
                    })
                    .OrderByDescending(gs => gs.Players)
                    .ThenBy(gs => gs.Id)
                    .ToArray(),
                    TotalPlayers = g.Games.Sum(gs => gs.Purchases.Count)
                })
                .ToArray();

            var genresJson = JsonConvert.SerializeObject(genres, Formatting.Indented);

            return genresJson;
		}

		public static string ExportUserPurchasesByType(VaporStoreDbContext context, string storeType)
		{
            var purchaseType = Enum.Parse<PurchaseType>(storeType);

            var users = context
                .Users
                .OrderByDescending(u => u.Cards.Sum(c => c.Purchases.Where(p => p.Type == purchaseType).Sum(p => p.Game.Price)))
                .ThenBy(u => u.Username)
                .Select(u => new ExportUserDto
                {
                    Username = u.Username,
                    Purchases = u.Cards.SelectMany(c => c.Purchases)
                    .Where(p => p.Type == purchaseType).OrderBy(p => p.Date).Select(p => new ExportPurchaseDto
                    {
                        Card = p.Card.Number,
                        Cvc = p.Card.Cvc,
                        Date = p.Date.ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture),
                        Game = new ExportGameDto
                        {
                            Title = p.Game.Name,
                            Genre = p.Game.Genre.Name,
                            Price = p.Game.Price
                        }
                    })
                    .ToArray(),
                    TotalSpent = u.Cards.Sum(c => c.Purchases.Where(p => p.Type == purchaseType).Sum(p => p.Game.Price))
                })
                .Where(u => u.Purchases.Count() > 0)
                .ToArray();

            var serializer = new XmlSerializer(typeof(ExportUserDto[]), new XmlRootAttribute("Users"));

            var settings = new System.Xml.XmlWriterSettings
            {
                Indent = true
            };

            var namespaces = new XmlSerializerNamespaces();
            namespaces.Add("", "");

            var stringWriter = new StringWriter();

            using (var writer = System.Xml.XmlWriter.Create(stringWriter, settings))
            {
                serializer.Serialize(stringWriter, users, namespaces);
            }

            return stringWriter.ToString();
		}
	}
}