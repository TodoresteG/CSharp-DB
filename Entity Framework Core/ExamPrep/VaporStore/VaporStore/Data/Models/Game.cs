namespace VaporStore.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static ModelValidator.GameValidator;

    public class Game
    {
        public Game()
        {
            this.Purchases = new HashSet<Purchase>();
            this.GameTags = new HashSet<GameTag>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Range(typeof(decimal), PriceMinRange, PriceMaxRange)]
        public decimal Price { get; set; }

        public DateTime ReleaseDate { get; set; }

        public int DeveloperId { get; set; }

        public Developer Developer { get; set; }

        public int GenreId { get; set; }

        public Genre Genre { get; set; }

        public ICollection<Purchase> Purchases { get; set; }

        public ICollection<GameTag> GameTags { get; set; }
    }
}
