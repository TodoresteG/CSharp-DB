namespace SoftJail.DataProcessor
{

    using Data;
    using ExportDto;

    using System;
    using System.Linq;
    using Newtonsoft.Json;
    using System.Globalization;
    using System.Xml.Serialization;
    using System.IO;

    public class Serializer
    {
        public static string ExportPrisonersByCells(SoftJailDbContext context, int[] ids)
        {
            var prisoners = context
                .Prisoners
                .Where(p => ids.Contains(p.Id))
                .OrderBy(p => p.FullName)
                .ThenBy(p => p.Id)
                .Select(p => new
                {
                    Id = p.Id,
                    Name = p.FullName,
                    CellNumber = p.Cell.CellNumber,
                    Officers = p.PrisonerOfficers.OrderBy(o => o.Officer.FullName).ThenBy(o => o.Officer.Id).Select(po => new
                    {
                        OfficerName = po.Officer.FullName,
                        Department = po.Officer.Department.Name
                    }).ToArray(),
                    TotalOfficerSalary = p.PrisonerOfficers.Sum(po => po.Officer.Salary)
                })
                .ToArray();

            var prisonersJson = JsonConvert.SerializeObject(prisoners, Formatting.Indented);

            return prisonersJson;
        }

        public static string ExportPrisonersInbox(SoftJailDbContext context, string prisonersNames)
        {
            var splittedPrisonerNames = prisonersNames.Split(",", StringSplitOptions.RemoveEmptyEntries);

            var prisoners = context
                .Prisoners
                .Where(p => splittedPrisonerNames.Contains(p.FullName))
                .OrderBy(p => p.FullName)
                .ThenBy(p => p.Id)
                .Select(p => new ExportPrisonerDto 
                {
                    Id = p.Id,
                    Name = p.FullName,
                    IncarcerationDate = p.IncarcerationDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
                    EncryptedMessages = p.Mails.Select(m => new ExportEncryptedMessageDto 
                    {
                        Description = Reverse(m.Description)
                    })
                    .ToArray()
                })
                .ToArray();

            var serializer = new XmlSerializer(typeof(ExportPrisonerDto[]), new XmlRootAttribute("Prisoners"));

            var settings = new System.Xml.XmlWriterSettings 
            {
                Indent = true
            };

            var namespaces = new XmlSerializerNamespaces();
            namespaces.Add("", "");

            var stringWriter = new StringWriter();

            using (var writer = System.Xml.XmlWriter.Create(stringWriter, settings))
            {
                serializer.Serialize(stringWriter, prisoners, namespaces);
            }

            return stringWriter.ToString();
        }

        private static string Reverse(string text) 
        {
            char[] chars = text.ToCharArray();
            Array.Reverse(chars);
            return new String(chars);
        }
    }
}