namespace MusicHub.DataProcessor.ImportDtos
{
    using System.ComponentModel.DataAnnotations;
    using System.Xml.Serialization;

    using static ModelValidator.SongValidator;

    [XmlType("Song")]
    public class ImportSongDto
    {
        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string Name { get; set; }

        [Required]
        public string Duration { get; set; }

        [Required]
        public string CreatedOn { get; set; }

        [Required]
        public string Genre { get; set; }

        [Range(typeof(decimal), PriceMinRange, PriceMaxRange)]
        public decimal Price { get; set; }

        public int? AlbumId { get; set; }

        public int WriterId { get; set; }
    }
}
