namespace PetStore.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static ModelValidator.UserValidator;

    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(NameMaxLength)]
        public string Name { get; set; }

        [Required]
        [StringLength(EmailMaxLength)]
        public string Email { get; set; }

        public ICollection<Order> Orders { get; set; } = new HashSet<Order>();

    }
}
