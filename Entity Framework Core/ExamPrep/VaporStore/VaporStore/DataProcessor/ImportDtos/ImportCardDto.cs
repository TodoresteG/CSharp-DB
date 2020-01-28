namespace VaporStore.DataProcessor.ImportDtos
{
    using System.ComponentModel.DataAnnotations;

    using static ModelValidator.CardValidator;

    public class ImportCardDto
    {
        [Required]
        [RegularExpression(NumberPattern)]
        public string Number { get; set; }

        [Required]
        [RegularExpression(CvcPattern)]
        public string CVC { get; set; }

        [Required]
        public string Type { get; set; }
    }
}
