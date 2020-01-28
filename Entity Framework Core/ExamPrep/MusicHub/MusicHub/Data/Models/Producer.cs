namespace MusicHub.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static ModelValidator.ProducerValidator;

    public class Producer
    {
        public Producer()
        {
            this.Albums = new HashSet<Album>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string Name { get; set; }

        [RegularExpression(PseudonymPattern)]
        public string Pseudonym { get; set; }

        [RegularExpression(PhoneNumberPattern)]
        public string PhoneNumber { get; set; }

        public ICollection<Album> Albums { get; set; }
    }
}
