namespace Cinema.DataProcessor.ImportDtos
{
    using System.Xml.Serialization;

    [XmlType("Projection")]
    public class ImportProjectionDto
    {
        public int MovieId { get; set; }

        public int HallId { get; set; }

        public string DateTime { get; set; }
    }
}
