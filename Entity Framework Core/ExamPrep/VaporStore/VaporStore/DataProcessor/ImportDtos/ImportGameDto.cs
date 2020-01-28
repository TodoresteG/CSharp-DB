namespace VaporStore.DataProcessor.ImportDtos
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static ModelValidator.GameValidator;

    public class ImportGameDto
    {
        [Required]
        public string Name { get; set; }

        [Range(typeof(decimal), PriceMinRange, PriceMaxRange)]
        public decimal Price { get; set; }

        [Required]
        public string ReleaseDate { get; set; }

        [Required]
        public string Developer { get; set; }

        [Required]
        public string Genre { get; set; }

        public ICollection<string> Tags { get; set; }
    }
}
