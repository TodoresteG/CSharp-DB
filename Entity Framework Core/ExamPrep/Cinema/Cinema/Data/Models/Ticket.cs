namespace Cinema.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using static ModelValidator.TicketValidator;

    public class Ticket
    {
        [Key]
        public int Id { get; set; }

        [Range(PriceMinRange, PriceMaxRange)]
        public decimal Price { get; set; }

        public int CustomerId { get; set; }

        public Customer Customer { get; set; }

        public int ProjectionId { get; set; }

        public Projection Projection { get; set; }
    }
}
