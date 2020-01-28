namespace VaporStore.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static ModelValidator.UserValidator;

    public class User
    {
        public User()
        {
            this.Cards = new HashSet<Card>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string Username { get; set; }

        [Required]
        [RegularExpression(FullNamePattern)]
        public string FullName { get; set; }

        [Required]
        public string Email { get; set; }

        [Range(AgeMinRange, AgeMaxRange)]
        public int Age { get; set; }

        public ICollection<Card> Cards { get; set; }
    }
}
