namespace Cinema.Data.Models
{
    using Enums;

    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static ModelValidator.MovieValidator;

    public class Movie
    {
        public Movie()
        {
            this.Projections = new HashSet<Projection>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(TitleMaxLength, MinimumLength = TitleMinLength)]
        public string Title { get; set; }

        [Required]
        public Genre Genre { get; set; }

        public TimeSpan Duration { get; set; }

        [Range(RatingMinRange, RatingMaxRange)]
        public double Rating { get; set; }

        [Required]
        [StringLength(DirectorMaxLength, MinimumLength = DirectorMinLength)]
        public string Director { get; set; }

        public ICollection<Projection> Projections { get; set; }
    }
}
