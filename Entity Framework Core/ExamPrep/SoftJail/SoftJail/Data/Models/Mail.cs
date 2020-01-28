namespace SoftJail.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using static ModelValidator.MailValidator;

    public class Mail
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Sender { get; set; }

        [Required]
        [RegularExpression(AddressPattern)]
        public string Address { get; set; }

        public int PrisonerId { get; set; }

        public Prisoner Prisoner { get; set; }
    }
}
