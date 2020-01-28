namespace MusicHub.DataProcessor
{
    using Data;
    using ExportDtos;

    using System;
    using System.Linq;
    using System.Globalization;
    using Newtonsoft.Json;
    using System.Xml.Serialization;
    using System.IO;

    public class Serializer
    {
        public static string ExportAlbumsInfo(MusicHubDbContext context, int producerId)
        {
            var albums = context
                .Albums
                .Where(a => a.ProducerId == producerId)
                .OrderByDescending(a => a.Songs.Sum(s => s.Price))
                .Select(a => new
                {
                    AlbumName = a.Name,
                    ReleaseDate = a.ReleaseDate.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture),
                    ProducerName = a.Producer.Name,
                    Songs = a.Songs.Select(s => new
                    {
                        SongName = s.Name,
                        Price = s.Price.ToString("f2"),
                        Writer = s.Writer.Name
                    })
                    .OrderByDescending(s => s.SongName)
                    .ThenBy(s => s.Writer)
                    .ToArray(),
                    AlbumPrice = a.Songs.Sum(s => s.Price).ToString("f2")
                })
                .ToArray();

            var albumsJson = JsonConvert.SerializeObject(albums, Formatting.Indented);

            return albumsJson;
        }

        public static string ExportSongsAboveDuration(MusicHubDbContext context, int duration)
        {
            var songs = context
                .Songs
                .Where(s => s.Duration.TotalSeconds > duration)
                .OrderBy(s => s.Name)
                .ThenBy(s => s.Writer.Name)
                .ThenBy(s => s.SongPerformers.Select(sp => $"{sp.Performer.FirstName} {sp.Performer.LastName}").ToString())
                .Select(s => new ExportSongAboveDurationDto 
                {
                    SongName = s.Name,
                    Writer = s.Writer.Name,
                    Performer = s.SongPerformers.Select(sp => $"{sp.Performer.FirstName} {sp.Performer.LastName}").FirstOrDefault(),
                    AlbumProducer = s.Album.Producer.Name,
                    Duration = s.Duration.ToString("c", CultureInfo.InvariantCulture)
                })
                .ToArray();

            var serializer = new XmlSerializer(typeof(ExportSongAboveDurationDto[]), new XmlRootAttribute("Songs"));

            var namespaces = new XmlSerializerNamespaces();
            namespaces.Add("", "");

            var options = new System.Xml.XmlWriterSettings
            {
                Indent = true
            };

            var stringWriter = new StringWriter();

            using (var writer = System.Xml.XmlWriter.Create(stringWriter, options))
            {
                serializer.Serialize(writer, songs, namespaces);
            }

            return stringWriter.ToString();
        }
    }
}