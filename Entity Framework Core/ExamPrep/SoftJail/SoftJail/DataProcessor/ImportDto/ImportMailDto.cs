namespace SoftJail.DataProcessor.ImportDto
{
    using System.ComponentModel.DataAnnotations;

    using static ModelValidator.MailValidator;

    public class ImportMailDto
    {
        [Required]
        public string Description { get; set; }

        [Required]
        public string Sender { get; set; }

        [Required]
        [RegularExpression(AddressPattern)]
        public string Address { get; set; }
    }
}
