namespace Cinema.DataProcessor.ImportDtos
{
    using System.ComponentModel.DataAnnotations;

    using static ModelValidator.MovieValidator;

    public class ImportMovieDto
    {
        [Required]
        [StringLength(TitleMaxLength, MinimumLength = TitleMinLength)]
        public string Title { get; set; }

        [Required]
        public string Genre { get; set; }

        public string Duration { get; set; }

        [Range(RatingMinRange, RatingMaxRange)]
        public double Rating { get; set; }

        [Required]
        [StringLength(DirectorMaxLength, MinimumLength = DirectorMinLength)]
        public string Director { get; set; }
    }
}
