namespace CarDealer.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static ModelValidator.SupplierValidator;

    public class Supplier
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        public bool IsImported { get; set; }

        public ICollection<Part> Parts { get; set; }
    }
}
