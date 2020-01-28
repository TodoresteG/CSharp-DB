namespace VaporStore.Data.Models
{
    using Enums;

    using System;
    using System.ComponentModel.DataAnnotations;

    using static ModelValidator.PurchaseValidator;

    public class Purchase
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public PurchaseType Type { get; set; }

        [Required]
        [RegularExpression(ProductKeyPattern)]
        public string ProductKey { get; set; }

        public DateTime Date { get; set; }

        public int CardId { get; set; }

        public Card Card { get; set; }

        public int GameId { get; set; }

        public Game Game { get; set; }
    }
}
