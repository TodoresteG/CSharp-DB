namespace PetStore.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static ModelValidator;

    public class Brand
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(NameMaxLength)]
        public string Name { get; set; }

        public ICollection<Toy> Toys { get; set; } = new HashSet<Toy>();

        public ICollection<Food> Foods { get; set; } = new HashSet<Food>();
    }
}
