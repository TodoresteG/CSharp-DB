namespace Cinema.DataProcessor.ImportDtos
{
    using System.ComponentModel.DataAnnotations;
    using System.Xml.Serialization;

    using static ModelValidator.TicketValidator;

    [XmlType("Ticket")]
    public class ImportTicketDto
    {
        public int ProjectionId { get; set; }

        [Range(PriceMinRange, PriceMaxRange)]
        public decimal Price { get; set; }
    }
}
