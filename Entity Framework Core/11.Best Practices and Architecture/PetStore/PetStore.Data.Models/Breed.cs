namespace PetStore.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static ModelValidator;

    public class Breed
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(NameMaxLength)]
        public string Name { get; set; }

        public ICollection<Pet> Pets { get; set; } = new HashSet<Pet>();
    }
}
