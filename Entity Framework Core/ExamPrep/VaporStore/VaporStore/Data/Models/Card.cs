namespace VaporStore.Data.Models
{
    using Enums;

    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static ModelValidator.CardValidator;

    public class Card
    {
        public Card()
        {
            this.Purchases = new HashSet<Purchase>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [RegularExpression(NumberPattern)]
        public string Number { get; set; }

        [Required]
        [RegularExpression(CvcPattern)]
        public string Cvc { get; set; }

        [Required]
        public CardType Type { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public ICollection<Purchase> Purchases { get; set; }
    }
}
