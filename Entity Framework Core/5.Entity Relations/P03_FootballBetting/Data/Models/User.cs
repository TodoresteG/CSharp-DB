namespace P03_FootballBetting.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static ModelValidator.UserValidator;

    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        [MaxLength(UsernameMaxLength)]
        public string Username { get; set; }

        [Required]
        [MaxLength(PasswordMaxLength)]
        public string Password { get; set; }

        [MaxLength(EmailMaxLength)]
        public string Email { get; set; }

        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        public decimal Balance { get; set; }

        public ICollection<Bet> Bets { get; set; } = new HashSet<Bet>();
    }
}
