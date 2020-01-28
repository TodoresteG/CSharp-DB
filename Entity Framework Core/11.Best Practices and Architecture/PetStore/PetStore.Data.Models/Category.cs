namespace PetStore.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static ModelValidator;

    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(NameMaxLength)]
        public string Name { get; set; }

        [StringLength(DescriptionMaxLength)]
        public string Description { get; set; }

        public ICollection<Pet> Pets { get; set; } = new HashSet<Pet>();

        public ICollection<Food> Foods { get; set; } = new HashSet<Food>();

        public ICollection<Toy> Toys { get; set; } = new HashSet<Toy>();
    }
}
