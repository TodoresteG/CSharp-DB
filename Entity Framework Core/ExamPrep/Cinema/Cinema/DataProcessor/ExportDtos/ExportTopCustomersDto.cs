namespace Cinema.DataProcessor.ExportDtos
{
    using System.Xml.Serialization;

    [XmlType("Customer")]
    public class ExportTopCustomersDto
    {
        [XmlAttribute("FirstName")]
        public string FirstName { get; set; }

        [XmlAttribute("LastName")]
        public string LastName { get; set; }

        public string SpentMoney { get; set; }

        public string SpentTime { get; set; }
    }
}
